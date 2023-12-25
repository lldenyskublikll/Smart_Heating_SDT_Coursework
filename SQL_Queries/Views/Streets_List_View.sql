create view Streets_List_View as
Select St.StreetID as 'ID ������',
       St.StreetName as '������',
	   Dst.DistrictID as 'ID ������', 
	   Dst.DistrictName as '�����'
From Streets St
join Districts Dst on Dst.DistrictID = St.District