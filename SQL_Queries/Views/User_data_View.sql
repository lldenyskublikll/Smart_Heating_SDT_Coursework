Create view User_data_View as
Select Us.UserID as 'ID Користувача', Us.UserLogin as 'Логін', Pd.Surname as 'Прізвище', Pd.PrsnName as 'Ім`я', 
       ISNULL(Pd.SecondName, 'Відсутнє') as 'По-Батькові',
	   Pd.Gender as 'Стать', Pd.BirthDate as 'Дата народження', Pd.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Pd.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
	   St.StreetName as 'Назва вулиці', Ds.DistrictName as 'Район', Adr.House as '№ Будинку',
       ISNULL(Adr.Flat, '-----') as '№ Квартири',
       ISNULL(Adr.Office, '-----') as '№ Офісу',
       ISNULL(Adr.EstablishmentName, 'Не вказано') as 'Назва закладу'
From Users Us
Join PersonData Pd on Pd.PersonID = Us.PersonID
Join Addresses Adr on Adr.AddressID = Pd.AddressInfo
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
Where Pd.AddressInfo IS NOT NULL
