create view Streets_List_View as
Select St.StreetID as 'ID вулиці',
       St.StreetName as 'Вулиця',
	   Dst.DistrictID as 'ID району', 
	   Dst.DistrictName as 'Район'
From Streets St
join Districts Dst on Dst.DistrictID = St.District