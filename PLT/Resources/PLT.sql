BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Locations" (
	"LocationID"	INTEGER,
	"LocationName"	STRING,
	PRIMARY KEY("LocationID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Printers" (
	"PrinterID"	INTEGER,
	"WarrantyCode"	TEXT,
	"Model"	TEXT,
	"IP"	NUMERIC,
	"TicketHistory"	TEXT,
	"Department"	TEXT,
	"Location"	TEXT,
	PRIMARY KEY("PrinterID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Departments" (
	"DepartmentID"	INTEGER,
	"DepartmentName"	STRING,
	PRIMARY KEY("DepartmentID" AUTOINCREMENT)
);
COMMIT;
