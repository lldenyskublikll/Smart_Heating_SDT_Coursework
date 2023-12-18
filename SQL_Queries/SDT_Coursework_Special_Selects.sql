USE SMART_HEATING

-- Special select for Addresses
Select Adr.AddressID as 'ID Вулиці', St.StreetName as 'Назва вулиці', Ds.DistrictName as 'Район',
       Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу', 
	   Bldt.BuildingTypeName as 'Тип будівлі',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'
From Addresses Adr
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
Join Building_types Bldt on Bldt.BuildingTypeID = Adr.BuildingType

-- Special select for Sensors
Select Sn.SensorID as 'ID датчика', Snt.SensorTypeName as 'Тип датчика', St.StreetName as 'Назва вулиці', 
       Ds.DistrictName as 'Район', Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'
From Sensors Sn
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District

-- Special select for Indicators
Select Ind.IndDate as 'Дата та час', Sn.SensorID as 'ID сенсору', Snt.SensorTypeName as 'Тип датчика',
       Case
	       When (Sn.SensorType = 1 or Sn.SensorType = 3) Then Concat(Convert(varchar(30), Ind.Indicator), ' °C') 
		   Else Concat(Convert(varchar(30), Ind.Indicator), ' Бар') 
       End as 'Показник',
       St.StreetName as 'Назва вулиці', Ds.DistrictName as 'Район', Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'	   
From Indicators Ind
Join Sensors Sn on Sn.SensorID = Ind.Sensor
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District

-- Special select for all users
Select Us.UserID as 'ID Користувача', Us.UserLogin as 'Логін', Usr.RoleName as 'Роль у системі',
       Us.Surname as 'Прізвище', Us.PrsnName as 'Ім`я', 
	   ISNULL(Us.SecondName, '-----') as 'По-Батькові',
	   Us.Gender as 'Стать', Us.BirthDate as 'Дата народження', Us.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Us.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else St.StreetName
	   End as 'Назва вулиці',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
	       Else Adr.House
	   End as '№ Будинку',
	   Case
	       When Us.AddressInfo IS NULL Then '-----'
		   Else ISNULL(Convert(varchar(10),Adr.Flat) , '-----')
	   End as '№ Квартири',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else ISNULL(Convert(varchar(10), Adr.Office), '-----')
	   End as '№ Офісу',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Ds.DistrictName
	   End as 'Район',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Bdt.BuildingTypeName
	   End as 'Тип будинку',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
	       Else ISNULL(Adr.EstablishmentName, 'Не вказано')
       End as 'Назва закладу'
From Users Us
Left Join UserRoles Usr on Usr.RoleID = Us.UserRole
Left Join Addresses Adr on Adr.AddressID = Us.AddressInfo
Left Join Building_types Bdt on Bdt.BuildingTypeID = Adr.BuildingType
Left Join Streets St on St.StreetID = Adr.Street
Left Join Districts Ds on Ds.DistrictID = St.District
 
-- Special select for Maintenance if standart view
Select Mnt.MaintID as 'ID Тех. роботи', Us.Surname as 'Прізвище працівника', 
       Us.PrsnName as 'Ім`я працівника', 
       ISNULL(Us.SecondName, '------') as 'По-Батькові працівника', 
	   Us.BirthDate as 'Дата народження', Us.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Us.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
       Mnt.MaintType as 'Тип роботи', Mnt.MaintStatus as 'Статус', 
	   Case 
	       When Mnt.MaintStartDate is null Then 'Не надано'
		   Else Convert(varchar(50), Mnt.MaintStartDate)
	   End as 'Дата початку робіт',
	   Case 
	       When Mnt.MaintEndDate is null Then 'Не надано'
		   Else Convert(varchar(50), Mnt.MaintEndDate)
	   End as 'Дата закінчення робіт'
From Maintenance Mnt
Join Users Us on Us.UserID = Mnt.StaffUserID

-- Special select for Maintenance if admin view
Select Mnt.MaintID as 'ID Тех. роботи', Us.UserID as 'ID Працівника', 
       Us.UserLogin as 'Логін', Us.Surname as 'Прізвище працівника', 
       Us.PrsnName as 'Ім`я працівника', ISNULL(Us.SecondName, '-----') as 'По-Батькові працівника',
	   Us.BirthDate as 'Дата народження', Us.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Us.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
       Mnt.MaintType as 'Тип роботи', Mnt.MaintStatus as 'Статус', 
	   Case 
	       When Mnt.MaintStartDate is null Then 'Не надано'
		   Else Convert(varchar(50), Mnt.MaintStartDate)
	   End as 'Дата початку робіт',
	   Case 
	       When Mnt.MaintEndDate is null Then 'Не надано'
		   Else Convert(varchar(50), Mnt.MaintEndDate)
	   End as 'Дата закінчення робіт'
From Maintenance Mnt
Join Users Us on Us.UserID = Mnt.StaffUserID
