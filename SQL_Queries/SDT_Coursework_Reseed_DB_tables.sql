USE SMART_HEATING

-- Reset identity seed for each table
DBCC CHECKIDENT ('Districts', RESEED, 0)
DBCC CHECKIDENT ('Streets', RESEED, 0)
DBCC CHECKIDENT ('Building_types', RESEED, 0)
DBCC CHECKIDENT ('Addresses', RESEED, 0)
DBCC CHECKIDENT ('SensorTypes', RESEED, 0)
DBCC CHECKIDENT ('Sensors', RESEED, 0)
DBCC CHECKIDENT ('Indicators', RESEED, 0)
DBCC CHECKIDENT ('UserRoles', RESEED, 0)
DBCC CHECKIDENT ('Users', RESEED, 0)
DBCC CHECKIDENT ('Maintenance', RESEED, 0)