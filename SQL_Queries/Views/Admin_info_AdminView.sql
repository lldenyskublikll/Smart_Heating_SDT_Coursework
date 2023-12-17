Create view Admin_info_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as '����', Ad.AdminID as 'Admin ID', Pd.Surname as '�������', Pd.PrsnName as '��`�', 
       ISNULL(Pd.SecondName, '³�����') as '��-�������',
	   Pd.Gender as '�����', Pd.BirthDate as '���� ����������', Pd.PhoneNumber as '�-� ��������',	   
	   ISNULL(Pd.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������'
From Administrators Ad
Join PersonData Pd on Pd.PersonID = Ad.PersonID
Join Users Us on Us.PersonID = Pd.PersonID