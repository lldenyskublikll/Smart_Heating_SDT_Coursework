Create view Maintenance_List_StandartView as
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
	   End as '���� ��������� ����',
	   Adr.AddressID as 'ID ������',
       St.StreetID as 'ID ������', St.StreetName as '����� ������', 
	   Ds.DistrictID as 'ID ������', Ds.DistrictName as '�����', 
	   Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'	
From Maintenance Mnt
Join Users Us on Us.UserID = Mnt.StaffUserID
Join Addresses Adr on Adr.AddressID = Mnt.MaintAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District