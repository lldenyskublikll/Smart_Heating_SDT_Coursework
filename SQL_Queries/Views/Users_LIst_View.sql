Create view Users_List_View as
Select Us.UserID as 'ID Користувача', Us.UserLogin as 'Логін', Usr.RoleName as 'Роль у системі',
       Us.Surname as 'Прізвище', Us.PrsnName as 'Ім`я', 
	   ISNULL(Us.SecondName, '-----') as 'По-Батькові',
	   Us.Gender as 'Стать', Us.BirthDate as 'Дата народження', Us.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Us.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Convert(varchar(10), Adr.AddressID)
	   End as 'ID адреси',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Convert(varchar(10), St.StreetID)
	   End as 'ID вулиці',
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
		   Else ISNULL(Convert(varchar(10), Adr.Flat) , '-----')
	   End as '№ Квартири',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else ISNULL(Convert(varchar(10), Adr.Office), '-----')
	   End as '№ Офісу',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Convert(varchar(10), Ds.DistrictID)
	   End as 'ID району',
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