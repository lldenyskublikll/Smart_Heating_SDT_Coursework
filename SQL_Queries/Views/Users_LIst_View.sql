Create view Users_List_View as
Select Us.UserID as 'ID �����������', Us.UserLogin as '����', Usr.RoleName as '���� � ������',
       Us.Surname as '�������', Us.PrsnName as '��`�', 
	   ISNULL(Us.SecondName, '-----') as '��-�������',
	   Us.Gender as '�����', Us.BirthDate as '���� ����������', Us.PhoneNumber as '�-� ��������',	   
	   ISNULL(Us.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Convert(varchar(10), Adr.AddressID)
	   End as 'ID ������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Convert(varchar(10), St.StreetID)
	   End as 'ID ������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else St.StreetName
	   End as '����� ������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
	       Else Adr.House
	   End as '� �������',
	   Case
	       When Us.AddressInfo IS NULL Then '-----'
		   Else ISNULL(Convert(varchar(10), Adr.Flat) , '-----')
	   End as '� ��������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else ISNULL(Convert(varchar(10), Adr.Office), '-----')
	   End as '� �����',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Convert(varchar(10), Ds.DistrictID)
	   End as 'ID ������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Ds.DistrictName
	   End as '�����',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else Bdt.BuildingTypeName
	   End as '��� �������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
	       Else ISNULL(Adr.EstablishmentName, '�� �������')
       End as '����� �������'
From Users Us
Left Join UserRoles Usr on Usr.RoleID = Us.UserRole
Left Join Addresses Adr on Adr.AddressID = Us.AddressInfo
Left Join Building_types Bdt on Bdt.BuildingTypeID = Adr.BuildingType
Left Join Streets St on St.StreetID = Adr.Street
Left Join Districts Ds on Ds.DistrictID = St.District