Create view OMS_check_AdminView as
Select Us.UserID as 'User ID', Us.UserLogin as '����', Oms.OMSUSerID as '��� User ID', 
       Pd.Surname as '�������', Pd.PrsnName as '��`�', 
       ISNULL(Pd.SecondName, '³�����') as '��-�������',
	   Pd.Gender as '�����', Pd.BirthDate as '���� ����������', Pd.PhoneNumber as '�-� ��������',	   
	   ISNULL(Pd.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
	   St.StreetName as '����� ������', Ds.DistrictName as '�����', Adr.House as '� �������'
From OMS_Participants Oms
Join PersonData Pd on Pd.PersonID = Oms.PersonID
Join Users Us on Us.PersonID = Pd.PersonID
Join Addresses Adr on Adr.AddressID = Pd.AddressInfo
Join Streets St on St.StreetID = Adr.Street
Join Districts Ds on Ds.DistrictID = St.District