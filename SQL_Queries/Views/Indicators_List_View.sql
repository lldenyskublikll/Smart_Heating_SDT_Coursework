Create view Indicators_List_View as
Select Ind.IndDate as '���� �� ���', Sn.SensorID as 'ID �������', Snt.SensorTypeName as '��� �������',
       Case
	       When (Sn.SensorType = 1 or Sn.SensorType = 3) Then Concat(Convert(varchar(30), Ind.Indicator), ' �C') 
		   Else Concat(Convert(varchar(30), Ind.Indicator), ' ���') 
       End as '��������',
	   Adr.AddressID as 'ID ������',
       St.StreetID as 'ID ������', St.StreetName as '����� ������', 
	   Ds.DistrictID as 'ID ������', Ds.DistrictName as '�����', 
	   Adr.House as '� �������', 
	   ISNULL(Adr.Flat, '-----') as '� ��������', 
	   ISNULL(Adr.Office, '-----') as '� �����',
	   ISNULL(Adr.EstablishmentName, '�� �������') as '����� �������'	   
From Indicators Ind
Join Sensors Sn on Sn.SensorID = Ind.Sensor
Join SensorTypes Snt on Snt.SensorTypeID = Sn.SensorType
Join Addresses Adr on Adr.AddressID = Sn.SensorAddress
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District