Create view Indicators_List_View as
Select Ind.IndDate as 'Дата та час', Sn.SensorID as 'ID сенсору', Snt.SensorTypeName as 'Тип датчика',
       Case
	       When (Sn.SensorType = 1 or Sn.SensorType = 3) Then Concat(Convert(varchar(30), Ind.Indicator), ' °C') 
		   Else Concat(Convert(varchar(30), Ind.Indicator), ' Бар') 
       End as 'Показник',
	   Adr.AddressID as 'ID адреси',
       St.StreetID as 'ID вулиці', St.StreetName as 'Назва вулиці', 
	   Ds.DistrictID as 'ID району', Ds.DistrictName as 'Район', 
	   Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'	   
From Indicators Ind
Join Sensors Sn on Sn.SensorID = Ind.Sensor
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District