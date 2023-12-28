--Створення таблиць
USE SMART_HEATING

--Таблиця "Райони міста"
--  * ID району
--  * Назва району
CREATE TABLE Districts
(
	DistrictID int Identity Primary key NOT NULL, 
	DistrictName varchar(100) Unique NOT NULL
)

--Таблиця "Вулиці міста"
--  * ID вулиці
--  * Назва вулиці
--  * ID району, у якому знаходиться вулиця
CREATE TABLE Streets
(
	StreetID int Identity Primary key NOT NULL,
	StreetName varchar(100) Unique NOT NULL,
	District int Foreign key references Districts(DistrictID) NOT NULL
)

--Таблиця "Види будівель"
--  * ID виду будівлі
--  * Вид будівлі (школа, лікарня, дитсадок, тощо)
CREATE TABLE Building_types
(
	BuildingTypeID int Identity Primary key NOT NULL,
	BuildingTypeName varchar(100) Unique NOT NULL
)

--Таблиця "Адреси"
--  * ID адреси
--  * ID вулиці, на якій знаходиться будинок
--  * Номер будинку
--  * Номер квартири
--  * Номер офісу
--  * ID виду будівлі
--  * Назва закладу
CREATE TABLE Addresses
(
	AddressID int Identity Primary key NOT NULL,
	Street int Foreign key references Streets(StreetID) NOT NULL,
	House varchar(10) NOT NULL,
	Flat varchar(10),
	Office varchar(10),
	BuildingType int Foreign key references Building_types(BuildingTypeID) NOT NULL,
	EstablishmentName varchar(300)
)

--Таблиця "Типи датчиків"
--  * ID типу датчика
--  * Назва типу датчика
CREATE TABLE SensorTypes
(
	SensorTypeID int Identity Primary key NOT NULL,
	SensorTypeName varchar(100) Unique NOT NULL
)

--Таблиця "Датчики"
--  * ID датчика
--  * ID типу датчика
--  * ID адреси, за якою встановлено датчик
CREATE TABLE Sensors
(
	SensorID int Identity Primary key NOT NULL,
	SensorType int Foreign key references SensorTypes(SensorTypeID) NOT NULL,
	SensorAddress int Foreign key references Addresses(AddressID) NOT NULL
)

--Таблиця "Показники"
--  * ID запису зафіксованого показника з датчика
--  * ID датчика
--  * Дата та час, у який було зроблено даний запис
--  * Значення цього показника
CREATE TABLE Indicators
(
	IndicatorID int Identity Primary key NOT NULL,
	Sensor int Foreign key references Sensors(SensorID) NOT NULL,
	IndDate datetime NOT NULL,
	Indicator float NOT NULL
)

--Таблиця "Ролі користувачів"
--  * ID Ролі у системі
--  * Назва ролі у системі
CREATE TABLE UserRoles
(
	RoleID int Identity Primary Key NOT NULL,
	RoleName varchar(100) Unique NOT NULL
)

--Таблиця "Користувачі"
--  * ID Користувача
--  * Логін користувача
--  * Пароль користувача
--  * Роль користувача
--  * Прізвище
--  * Ім'я
--  * По-батькові
--  * Стать
--  * Дата народження
--  * Номер телефону
--  * Резервний номер телефону
--  * ID адреси, за якою проживає людина
CREATE TABLE Users
(
	UserID int Identity Primary key NOT NULL,
	UserLogin varchar(20) Unique NOT NULL,
	UserPassword varchar(20) Unique NOT NULL,
	UserRole int Foreign key references UserRoles(RoleID) NOT NULL,
	Surname varchar(100) NOT NULL,
	PrsnName varchar(100) NOT NULL,
	SecondName varchar(100),
	Gender varchar(10),
	BirthDate date NOT NULL,
	PhoneNumber varchar(13) NOT NULL,
	RsrvPhoneNumber varchar(13),
	AddressInfo int Foreign key references Addresses(AddressID)
)

-- Таблиця "Технічні роботи"
--  * ID технічної роботи
--  * Відповідальний за виконання роботи
--  * Адреса за якою проводиться робота
--  * Тип виконуваної роботи
--  * Статус виконання роботи
--  * Дата початку виконання роботи
--  * Дата закінчення виконання роботи
CREATE TABLE Maintenance
(
	MaintID int Identity Primary key NOT NULL,
	StaffUserID int Foreign key references Users(UserID) NOT NULL,
	MaintAddress int Foreign key references Addresses(AddressID) NOT NULL,
	MaintType varchar(500),
	MaintStatus varchar(500),
	MaintStartDate date,
	MaintEndDate date 
)
