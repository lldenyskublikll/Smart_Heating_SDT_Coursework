Create view Maintenance_check_StandartView as
Select Mnt.MaintID as 'ID ���. ������', Pd.Surname as '������� ����������', Pd.PrsnName as '��`� ����������', 
       ISNULL(Pd.SecondName, '³�����') as '��-������� ����������', 
	   Pd.BirthDate as '���� ����������', Pd.PhoneNumber as '�-� ��������',	   
	   ISNULL(Pd.RsrvPhoneNumber, '�� �������') as '��������� �-� ��������',
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
Join Staff Sf on Mnt.Staff = Sf.StaffID
Join PersonData Pd on Pd.PersonID = Sf.PersonID
