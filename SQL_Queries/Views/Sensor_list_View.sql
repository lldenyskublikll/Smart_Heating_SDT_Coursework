Create view Sensor_list_View as
Select Sn.SensorID as 'ID', Snt.SensorTypeName as '��� �������', St.StreetName as '����� ������', 
       Ds.DistrictName as '�����', Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'
From Sensors Sn
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
