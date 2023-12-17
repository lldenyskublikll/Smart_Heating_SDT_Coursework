Create view Staff_info_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as 'Логін', Sf.StaffID as 'ID Працівника', 
       Sf.Post as 'Посада працівника', Pd.Surname as 'Прізвище', Pd.PrsnName as 'Ім`я', 
       ISNULL(Pd.SecondName, 'Відсутнє') as 'По-Батькові',
	   Pd.Gender as 'Стать', Pd.BirthDate as 'Дата народження', Pd.PhoneNumber as 'Н-р телефону',	   
	   ISNULL(Pd.RsrvPhoneNumber, 'Не вказано') as 'Резервний н-р телефону'
From Staff Sf
Join PersonData Pd on Pd.PersonID = Sf.PersonID
Join Users Us on Us.PersonID = Pd.PersonID
