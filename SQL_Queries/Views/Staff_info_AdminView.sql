Create view Staff_info_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as '����', Sf.StaffID as 'ID ����������', 
       Sf.Post as '������ ����������', Pd.Surname as '�������', Pd.PrsnName as '��`�', 
       ISNULL(Pd.SecondName, '³�����') as '��-�������',
	   Pd.Gender as '�����', Pd.BirthDate as '���� ����������', Pd.PhoneNumber as '�-� ��������',	   
	   ISNULL(Pd.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������'
From Staff Sf
Join PersonData Pd on Pd.PersonID = Sf.PersonID
Join Users Us on Us.PersonID = Pd.PersonID