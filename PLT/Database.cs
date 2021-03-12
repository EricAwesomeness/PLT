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

        private const string DatabaseFile = @"C:\...\PLT.db";
        
        private readonly SqliteConnection _sqlConnection;
        
        /// <summary>
        /// opening sql connection 
        /// </summary>
        private Database()
        {
            _sqlConnection = new SqliteConnection(@$"Data Source={DatabaseFile};Mode=ReadWriteCreate");
            _sqlConnection.Open();
            var sqlCommand = new SqliteCommand("SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1", _sqlConnection);
            var reader = sqlCommand.ExecuteReader();

            if (reader.HasRows) return;

            sqlCommand = new SqliteCommand(GetResource("PLT.sql"), _sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }


        public async Task AddLocationAsync(string location)
        {
            var sqlCommand = new SqliteCommand("insert into Locations (LocationName) values (@locationName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@locationName", location);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public void AddLocation(string location)
        {
            var sqlCommand = new SqliteCommand("insert into Locations (LocationName) values (@locationName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@locationName", location);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task AddDepartmentAsync(string department)
        {
            var sqlCommand = new SqliteCommand("insert into Departments (DepartmentName) values (@departmentName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@departmentName", department);
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public void AddDepartment(string department)
        {
            var sqlCommand = new SqliteCommand("insert into Departments (DepartmentName) values (@departmentName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@departmentName", department);
            sqlCommand.ExecuteNonQuery();
        }

        public async Task AddPrinterAsync(string priWarrantyCode, string priModel, string priIp, string priTicketHistory, string priDepartmentName, string priLocationName)
        {

            var sqlCommand = new SqliteCommand("insert into Printers (WarrantyCode, Model, IP, TicketHistory, Department, Location) values (@PriWarrantyCode, @PriModel, @PriIP, @PriTicketHistory, @PriDepartmentName, @PriLocationName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PriWarrantyCode", priWarrantyCode);
            sqlCommand.Parameters.AddWithValue("@PriModel", priModel);
            sqlCommand.Parameters.AddWithValue("@PriIP", priIp);
            sqlCommand.Parameters.AddWithValue("@PriTicketHistory", priTicketHistory);
            sqlCommand.Parameters.AddWithValue("@PriDepartmentName", priDepartmentName);
            sqlCommand.Parameters.AddWithValue("@PriLocationName", priLocationName);
            
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public void AddPrinter(string priWarrantyCode, string priModel, string priIp, string priTicketHistory, string priDepartmentName, string priLocationName)
        {
            var sqlCommand = new SqliteCommand("insert into Printers (WarrantyCode, Model, IP, TicketHistory, Department, Location) values (@PriWarrantyCode, @PriModel, @PriIP, @PriTicketHistory, @PriDepartmentName, @PriLocationName)", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@PriWarrantyCode", priWarrantyCode);
            sqlCommand.Parameters.AddWithValue("@PriModel", priModel);
            sqlCommand.Parameters.AddWithValue("@PriIP", priIp);
            sqlCommand.Parameters.AddWithValue("@PriTicketHistory", priTicketHistory);
            sqlCommand.Parameters.AddWithValue("@PriDepartmentName", priDepartmentName);
            sqlCommand.Parameters.AddWithValue("@PriLocationName", priLocationName);
            sqlCommand.ExecuteNonQuery();
        }

        
        public List<string> LoadLocations() 
        {
            List<string> result = new();

            var sqlCommand = new SqliteCommand("select LocationName from Locations", _sqlConnection);
            SqliteDataReader x = sqlCommand.ExecuteReader();

            while (x.Read())
            {
                string _locName = x.GetString(0);
                result.Add(_locName);
            }
            return result;
        }

        public List<string> GetDepartmentsAtLocation(string Locname)
        {
            List<string> result = new();

            var sqlCommand = new SqliteCommand("select DepartmentName from Departments where DepartmentLocation = @LocName", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@LocName", Locname);

            SqliteDataReader y = sqlCommand.ExecuteReader();

            while (y.Read())
            {
                string _depName = y.GetString(0);
                result.Add(_depName);
            }

            return result;
        }
        public List<Array> GetPrintersAtDepartment(string Depname) 
        {
            List<Array> result = new();
         
            var sqlCommand = new SqliteCommand("select WarrantyCode, Model, IP, TicketHistory from Printers where PrinterDepartment = @DepName", _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@DepName", Depname);
            SqliteDataReader z = sqlCommand.ExecuteReader();

            while (z.Read())
            {

                string _warrentyCode = z[0].ToString();
                string _model = z[1].ToString();
                string _ip= z[2].ToString();
                string _ticketHistory = z[3].ToString();

                string[] pters = { _warrentyCode, _model, _ip, _ticketHistory };

                result.Add(pters);
            }



            return result;
        }





    }
}
