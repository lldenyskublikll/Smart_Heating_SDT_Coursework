Create view Address_list_View as
Select Adr.AddressID as 'ID', St.StreetName as '����� ������', Ds.DistrictName as '�����',
       Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����', 
	   Bldt.BuildingTypeName as '��� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'
From Addresses Adr
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District
Join Building_types Bldt on Bldt.BuildingTypeID = Adr.BuildingType

