Create view Maintenance_List_AdminView as
Select Mnt.MaintID as 'ID Тех. роботи', Us.UserID as 'ID Працівника', 
       Us.UserLogin as 'Логін',  Us.Surname as 'Прізвище працівника', 
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