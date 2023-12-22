Create view Sensor_List_View as
Select Sn.SensorID as 'ID датчика', Snt.SensorTypeName as 'Тип датчика', 
       Adr.AddressID as 'ID адреси',
	   St.StreetID as 'ID вулиці', St.StreetName as 'Назва вулиці', 
       Ds.DistrictID as 'ID району' , Ds.DistrictName as 'Район', 
	   Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'
From Sensors Sn
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District