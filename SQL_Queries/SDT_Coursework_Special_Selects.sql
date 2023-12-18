USE SMART_HEATING

-- Special select for Addresses
Select Adr.AddressID as 'ID ������', St.StreetName as '����� ������', Ds.DistrictName as '�����',
       Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����', 
	   Bldt.BuildingTypeName as '��� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'
From Addresses Adr
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
Join Building_types Bldt on Bldt.BuildingTypeID = Adr.BuildingType

-- Special select for Sensors
Select Sn.SensorID as 'ID �������', Snt.SensorTypeName as '��� �������', St.StreetName as '����� ������', 
       Ds.DistrictName as '�����', Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'
From Sensors Sn
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District

-- Special select for Indicators
Select Ind.IndDate as '���� �� ���', Sn.SensorID as 'ID �������', Snt.SensorTypeName as '��� �������',
       Case
	       When (Sn.SensorType = 1 or Sn.SensorType = 3) Then Concat(Convert(varchar(30), Ind.Indicator), ' �C') 
		   Else Concat(Convert(varchar(30), Ind.Indicator), ' ���') 
       End as '��������',
       St.StreetName as '����� ������', Ds.DistrictName as '�����', Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'	   
From Indicators Ind
Join Sensors Sn on Sn.SensorID = Ind.Sensor
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District

-- Special select for all users
Select Us.UserID as 'ID �����������', Us.UserLogin as '����', Usr.RoleName as '���� � ������',
       Us.Surname as '�������', Us.PrsnName as '��`�', 
	   ISNULL(Us.SecondName, '-----') as '��-�������',
	   Us.Gender as '�����', Us.BirthDate as '���� ����������', Us.PhoneNumber as '�-� ��������',	   
	   ISNULL(Us.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
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
		   Else ISNULL(Convert(varchar(10),Adr.Flat) , '-----')
	   End as '� ��������',
	   Case 
	       When Us.AddressInfo IS NULL Then '-----'
		   Else ISNULL(Convert(varchar(10), Adr.Office), '-----')
	   End as '� �����',
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
 
-- Special select for Maintenance if standart view
Select Mnt.MaintID as 'ID ���. ������', Us.Surname as '������� ����������', 
       Us.PrsnName as '��`� ����������', 
       ISNULL(Us.SecondName, '------') as '��-������� ����������', 
	   Us.BirthDate as '���� ����������', Us.PhoneNumber as '�-� ��������',	   
	   ISNULL(Us.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
       Mnt.MaintType as '��� ������', Mnt.MaintStatus as '������', 
	   Case 
	       When Mnt.MaintStartDate is null Then '�� ������'
		   Else Convert(varchar(50), Mnt.MaintStartDate)
	   End as '���� ������� ����',
	   Case 
	       When Mnt.MaintEndDate is null Then '�� ������'
		   Else Convert(varchar(50), Mnt.MaintEndDate)
	   End as '���� ��������� ����'
From Maintenance Mnt
Join Users Us on Us.UserID = Mnt.StaffUserID

-- Special select for Maintenance if admin view
Select Mnt.MaintID as 'ID ���. ������', Us.UserID as 'ID ����������', 
       Us.UserLogin as '����', Us.Surname as '������� ����������', 
       Us.PrsnName as '��`� ����������', ISNULL(Us.SecondName, '-----') as '��-������� ����������',
	   Us.BirthDate as '���� ����������', Us.PhoneNumber as '�-� ��������',	   
	   ISNULL(Us.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
       Mnt.MaintType as '��� ������', Mnt.MaintStatus as '������', 
	   Case 
	       When Mnt.MaintStartDate is null Then '�� ������'
		   Else Convert(varchar(50), Mnt.MaintStartDate)
	   End as '���� ������� ����',
	   Case 
	       When Mnt.MaintEndDate is null Then '�� ������'
		   Else Convert(varchar(50), Mnt.MaintEndDate)
	   End as '���� ��������� ����'
From Maintenance Mnt
Join Users Us on Us.UserID = Mnt.StaffUserID
