--��������� �������
USE SMART_HEAT

--������� "������ ����"
--  * ID ������
--  * ����� ������
CREATE TABLE Districts
(
	DistrictID int Identity Primary key NOT NULL, 
	DistrictName varchar(100) NOT NULL
)

--������� "������ ����"
--  * ID ������
--  * ����� ������
--  * ID ������, � ����� ����������� ������
CREATE TABLE Streets
(
	StreetID int Identity Primary key NOT NULL,
	StreetName varchar(100) NOT NULL,
	District int Foreign key references Districts(DistrictID) NOT NULL
)

--������� "���� �������"
--  * ID ���� �����
--  * ��� ����� (�����, ������, ��������, ����)
CREATE TABLE Building_types
(
	BuildingTypeID int Identity Primary key NOT NULL,
	BuildingTypeName varchar(100) NOT NULL
)

--������� "������"
--  * ID ������
--  * ID ������, �� ��� ����������� �������
--  * ����� �������
--  * ����� ��������
--  * ����� �����
--  * ID ���� �����
--  * ����� �������
CREATE TABLE Addresses
(
	AddressID int Identity Primary key NOT NULL,
	Street int Foreign key references Streets(StreetID) NOT NULL,
	House varchar(10) NOT NULL,
	Flat int,
	Office int,
	BuildingType int Foreign key references Building_types(BuildingTypeID) NOT NULL,
	EstablishmentName varchar(300) NOT NULL
)

--������� "���� �������"
--  * ID ���� �������
--  * ����� ���� �������
CREATE TABLE SensorTypes
(
	SensorTypeID int Identity Primary key NOT NULL,
	SensorTypeName varchar(100) NOT NULL
)

--������� "�������"
--  * ID �������
--  * ID ���� �������
--  * ID ������, �� ���� ����������� ������
CREATE TABLE Sensors
(
	SensorID int Identity Primary key NOT NULL,
	SensorType int Foreign key references SensorTypes(SensorTypeID) NOT NULL,
	SensorAddress int Foreign key references Addresses(AddressID) NOT NULL
)

--������� "���������"
--  * ID ������ ������������� ��������� � �������
--  * ID �������
--  * ���� �� ���, � ���� ���� �������� ����� �����
--  * �������� ����� ���������
CREATE TABLE Indicators
(
	IndicatorID int Identity Primary key NOT NULL,
	Sensor int Foreign key references Sensors(SensorID) NOT NULL,
	IndDate datetime NOT NULL,
	Indicator decimal NOT NULL
)

--������� "��� ������������"
--  * ID ������
--  * �������
--  * ��'�
--  * ��-�������
--  * �����
--  * ���� ����������
--  * ����� ��������
--  * ��������� ����� ��������
--  * ID ������, �� ���� ������� ������
CREATE TABLE PersonData
(
	PersonID int Identity Primary key NOT NULL,
	Surname varchar(100) NOT NULL,
	PrsnName varchar(100) NOT NULL,
	SecondName varchar(100),
	Gender varchar(10),
	BirthDate date NOT NULL,
	PhoneNumber varchar(15) NOT NULL,
	RsrvPhoneNumber varchar(15),
	AddressInfo int Foreign key references Addresses(AddressID)
)

--������� "�����������"
--  * ID �����������
--  * ID ������
--  * ���� �����������
--  * ������ �����������
CREATE TABLE Users
(
	UserID int Identity Primary key NOT NULL,
	PersonID int Foreign key references PersonData(PersonID) NOT NULL,
	UserLogin varchar(20) NOT NULL,
	UserPassword varchar(20) Not NULL
)

--������� "������ �������� ��������������"
--  * ID �������� ���
--  * ID ������, ��� � ��������� ���
CREATE TABLE OMS_Participants
(
	OMSUSerID int Identity Primary key NOT NULL,
	PersonID int Foreign key references PersonData(PersonID) NOT NULL
)

--������� "������ ��������� ��������������"
--  * ID �������� ���
--  * ID ������, ��� � ��������� ���
CREATE TABLE ORS_Participants
(
	ORSUSerID int Identity Primary key NOT NULL,
	PersonID int Foreign key references PersonData(PersonID) NOT NULL
)

--������� "��'������� ����������� �������� ��������������"
--  * ID �������� ����
--  * ID ������, ��� � ��������� ����
CREATE TABLE OSBB_Participants
(
	OSBBUSerID int Identity Primary key NOT NULL,
	PersonID int Foreign key references PersonData(PersonID) NOT NULL
)

--������� "������������"
--  * ID ������������
--  * ID ������, ��� � �������������
CREATE TABLE Administrators
(
	AdminID int Identity Primary key NOT NULL,
	PersonID int Foreign key references PersonData(PersonID) NOT NULL
)

--������� "������������� ��������"
--  * ID ��������
--  * ID ������
--  * ����, ���� ���� ������ �����
CREATE TABLE Staff
(
	StaffID int Identity Primary key NOT NULL,
	PersonID int Foreign key references PersonData(PersonID) NOT NULL,
	Post varchar(50) NOT NULL
)

-- ������� "������ ������"
--  * ID ������� ������
--  * ³����������� �� ��������� ������
--  * ������ �� ���� ����������� ������
--  * ��� ���������� ������
--  * ������ ��������� ������
--  * ���� ������� ��������� ������
--  * ���� ��������� ��������� ������
CREATE TABLE Maintenance
(
	MaintID int Identity Primary key NOT NULL,
	Staff int Foreign key references Staff(StaffID) NOT NULL,
	MaintAddress int Foreign key references Addresses(AddressID) NOT NULL,
	MaintType varchar(500),
	MaintStatus varchar(500),
	MaintStartDate datetime,
	MaintEndDate datetime 
)
