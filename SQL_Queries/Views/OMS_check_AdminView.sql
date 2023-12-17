Create view OMS_check_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as 'Логін', Oms.OMSUSerID as 'ОМС User ID', 
       Pd.Surname as 'Прізвище', Pd.PrsnName as 'Ім`я', 
       ISNULL(Pd.SecondName, 'Відсутнє') as 'По-Батькові',
	   Pd.Gender as 'Стать', Pd.BirthDate as 'Дата народження', Pd.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Pd.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
	   St.StreetName as 'Назва вулиці', Ds.DistrictName as 'Район', Adr.House as '№ Будинку'
From OMS_Participants Oms
Join PersonData Pd on Pd.PersonID = Oms.PersonID
Join Users Us on Us.PersonID = Pd.PersonID
Join Addresses Adr on Adr.AddressID = Pd.AddressInfo
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
