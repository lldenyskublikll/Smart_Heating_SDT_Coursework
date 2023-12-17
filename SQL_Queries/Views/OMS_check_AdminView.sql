Create view OMS_check_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as 'Логін', Oms.OMSUSerID as 'ÎÌÑ User ID', 
       Pd.Surname as 'Ïð³çâèùå', Pd.PrsnName as '²ì`ÿ', 
       ISNULL(Pd.SecondName, 'Â³äñóòíº') as 'Ïî-Áàòüêîâ³',
	   Pd.Gender as 'Ñòàòü', Pd.BirthDate as 'Äàòà íàðîäæåííÿ', Pd.PhoneNumber as 'Í-ð òåëåôîíó',	   
	   ISNULL(Pd.RsrvPhoneNumber, 'Íå âêàçàíî') as 'Ðåçåðâíèé í-ð òåëåôîíó',
	   St.StreetName as 'Íàçâà âóëèö³', Ds.DistrictName as 'Ðàéîí', Adr.House as '¹ Áóäèíêó'
From OMS_Participants Oms
Join PersonData Pd on Pd.PersonID = Oms.PersonID
Join Users Us on Us.PersonID = Pd.PersonID
Join Addresses Adr on Adr.AddressID = Pd.AddressInfo
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
