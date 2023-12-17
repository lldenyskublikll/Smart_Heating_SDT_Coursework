Create view OSBB_check_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as '����', Osbb.OSBBUSerID as '���� User ID', 
       Pd.Surname as '�������', Pd.PrsnName as '��`�', 
       ISNULL(Pd.SecondName, '³�����') as '��-�������',
	   Pd.Gender as '�����', Pd.BirthDate as '���� ����������', Pd.PhoneNumber as '�-� ��������',	   
	   ISNULL(Pd.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
	   St.StreetName as '����� ������', Ds.DistrictName as '�����', Adr.House as '� �������',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'	 
From OSBB_Participants Osbb
Join PersonData Pd on Pd.PersonID = Osbb.PersonID
Join Users Us on Us.PersonID = Pd.PersonID
Join Addresses Adr on Adr.AddressID = Pd.AddressInfo
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District