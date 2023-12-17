Create view Address_list_View as
Select Adr.AddressID as 'ID', St.StreetName as 'Назва вулиці', Ds.DistrictName as 'Район',
       Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу', 
	   Bldt.BuildingTypeName as 'Тип будівлі',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'
From Addresses Adr
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
Join Building_types Bldt on Bldt.BuildingTypeID = Adr.BuildingType

