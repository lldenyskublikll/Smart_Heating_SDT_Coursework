Create view User_data_View as
Select Us.UserID as 'ID �����������', Us.UserLogin as '����', Pd.Surname as '�������', Pd.PrsnName as '��`�', 
       ISNULL(Pd.SecondName, '³�����') as '��-�������',
	   Pd.Gender as '�����', Pd.BirthDate as '���� ����������', Pd.PhoneNumber as '�-� ��������',	   
	   ISNULL(Pd.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
	   St.StreetName as '����� ������', Ds.DistrictName as '�����', Adr.House as '� �������',
       ISNULL(Adr.Flat, '-----') as '� ��������',
       ISNULL(Adr.Office, '-----') as '� �����',
       ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'
From Users Us
Join PersonData Pd on Pd.PersonID = Us.PersonID
Join Addresses Adr on Adr.AddressID = Pd.AddressInfo
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
Where Pd.AddressInfo IS NOT NULL