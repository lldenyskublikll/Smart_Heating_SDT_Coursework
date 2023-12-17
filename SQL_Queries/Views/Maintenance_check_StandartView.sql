Create view Maintenance_check_StandartView as
Select Mnt.MaintID as 'ID Тех. роботи', Pd.Surname as 'Прізвище працівника', Pd.PrsnName as 'Ім`я працівника', 
       ISNULL(Pd.SecondName, 'Відсутнє') as 'По-Батькові працівника', 
	   Pd.BirthDate as 'Дата народження', Pd.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Pd.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону',
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
Join Staff Sf on Mnt.Staff = Sf.StaffID
Join PersonData Pd on Pd.PersonID = Sf.PersonID
