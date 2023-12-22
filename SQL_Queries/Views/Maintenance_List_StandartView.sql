Create view Maintenance_List_StandartView as
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
	   End as 'Дата закінчення робіт',
	   Adr.AddressID as 'ID адреси',
       St.StreetID as 'ID вулиці', St.StreetName as 'Назва вулиці', 
	   Ds.DistrictID as 'ID району', Ds.DistrictName as 'Район', 
	   Adr.House as '№ Будинку', 
	   ISNULL(Adr.Flat, '-----') as '№ Квартири', 
	   ISNULL(Adr.Office, '-----') as '№ Офісу',
	   ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'	
From Maintenance Mnt
Join Users Us on Us.UserID = Mnt.StaffUserID
Join Addresses Adr on Adr.AddressID = Mnt.MaintAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District