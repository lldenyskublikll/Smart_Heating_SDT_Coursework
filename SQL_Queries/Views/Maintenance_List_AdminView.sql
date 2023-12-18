Create view Maintenance_List_AdminView as
Select Mnt.MaintID as 'ID ���. ������', Us.UserID as 'ID ����������', 
       Us.UserLogin as '����',  Us.Surname as '������� ����������', 
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