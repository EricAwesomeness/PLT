using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using PLT.Pages;

namespace PLT
{
    public class Database
    {
        private static Database _instance;
        public static Database Instance => _instance ??= new Database();

        public static string GetResource(String name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"PLT.Resources.{name}";

            //does and dispose (release memory after done)
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream!);

            return reader.ReadToEnd();
        }

        private const string DatabaseFile = @"PLT.db";

        private readonly SqliteConnection _sqlConnection;

        /// <summary>
        /// opening sql connection 
        /// </summary>
        private Database()
        {
            _sqlConnection = new SqliteConnection(@$"Data Source={DatabaseFile};Mode=ReadWriteCreate;foreign keys=true;");
            _sqlConnection.Open();
            var sqlCommand = new SqliteCommand("SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1", _sqlConnection);
            var reader = sqlCommand.ExecuteReader();

            if (reader.HasRows) return;

            sqlCommand = new SqliteCommand(GetResource("PLT.sql"), _sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public void AddLocation(string location)
        {
            var sqlCommand = new SqliteCommand("insert into Locations (name) values (@locationName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@locationName", location);
            sqlCommand.ExecuteNonQuery();
        }


        public void AddDepartment(string department)
        {
            var sqlCommand = new SqliteCommand("insert into Departments (name) values (@departmentName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@departmentName", department);
            sqlCommand.ExecuteNonQuery();
        }

        public void AddPrinter(Printer printer)
        {
            var sqlCommand = new SqliteCommand("insert into Printers (warranty_code, model, ip, ticket_history, department_id, location_id) values (@PriWarrantyCode, @PriModel, @PriIP, @PriTicketHistory, @PriDepId, @PriLocId)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PriWarrantyCode", printer.WarrantyCode);
            sqlCommand.Parameters.AddWithValue("@PriModel", printer.Model);
            sqlCommand.Parameters.AddWithValue("@PriIP", printer.Ip);
            sqlCommand.Parameters.AddWithValue("@PriTicketHistory", printer.TicketHistory);
            sqlCommand.Parameters.AddWithValue("@PriDepId", printer.Department.Id);
            sqlCommand.Parameters.AddWithValue("@PriLocId", printer.Department.Location.Id);
            sqlCommand.ExecuteNonQuery();
        }

        public List<Location> LoadLocations()
        {
            var sqlCommand = new SqliteCommand(@"
                SELECT L.id, L.name, D.id, D.name
                FROM Locations L
                INNER JOIN DepartmentLocations DL on L.id = DL.location_id
                INNER JOIN Departments D on D.id = DL.department_id", _sqlConnection);
            SqliteDataReader x = sqlCommand.ExecuteReader();

            Dictionary<int, Location> locations = new Dictionary<int, Location>();

            while (x.Read())
            {
                int locationId = x.GetInt32(0);
                string locationName = x.GetString(1);

                int departmentId = x.GetInt32(2);
                string departmentName = x.GetString(3);

                if (!locations.ContainsKey(locationId))
                {
                    var location = new Location(locationId, locationName);
                    location.Departments.Add(new Department(departmentId, departmentName, location));
                    locations.Add(locationId, location);
                }
                else
                {
                    locations[locationId].Departments.Add(new Department(departmentId, departmentName, locations[locationId]));
                }
            }
            
            return locations.Select(z => z.Value).ToList();
        }

        // public List<string> GetDepartmentsAtLocation(string Locname)
        // {
        //     List<string> result = new();
        //
        //     var sqlCommand = new SqliteCommand("select DepartmentName from Departments where DepartmentLocation = @LocName", _sqlConnection);
        //     sqlCommand.Parameters.AddWithValue("@LocName", Locname);
        //
        //     SqliteDataReader y = sqlCommand.ExecuteReader();
        //
        //     while (y.Read())
        //     {
        //         string _depName = y.GetString(0);
        //         result.Add(_depName);
        //     }
        //
        //     return result;
        // }

        public List<Printer> GetPrintersAtDepartment(Department department)
        {
            List<Printer> result = new();

            var sqlCommand = new SqliteCommand("select warranty_code, model, ip, ticket_history from Printers where department_id = @depId", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@depId", department.Id);
            SqliteDataReader z = sqlCommand.ExecuteReader();
            while (z.Read())
            {
                var warrantyCode = z[0].ToString();
                var model = z[1].ToString();
                var ip = z[2].ToString();
                var ticketHistory = z[3].ToString();

                var printer = new Printer(warrantyCode, model, ip, ticketHistory, department);
                result.Add(printer);
            }
            return result;
        }
    }
}