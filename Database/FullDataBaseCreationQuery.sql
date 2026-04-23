-- Database creation to save tables and relations

Create Database DVLDDBTest

use DVLDDBTest

--1 Person and related Tables
create table Persons
(
 PersonID int IDENTITY(1,1) NOT NULL,
 NationalNumber nvarchar(20) NOT NULL,
 FirstName nvarchar(30) NOT NULL,
 FatherName nvarchar(30) NOT NULL,
 GrandFatherName nvarchar(30) NOT NULL,
 FamilyName nvarchar(30) NOT NULL,
 DateOfBirth Date NOT NULL,
 Constraint PK_Person Primary Key(PersonID),
 Constraint UQ_NationalNumber Unique (NationalNumber)
);

create Table Images
(
ImageID int IDENTITY(1,1) NOT NULL,
PersonImage Image NOT NULL,
PersonID int NOT NULL,
CONSTRAINT PK_Image Primary Key (ImageID),
CONSTRAINT FK_PersonImage Foreign Key (PersonID)
References Persons(PersonID)
);
create Table PhoneNumbers
(
PhoneNumberID int IDENTITY(1,1) NOT NULL,
PhoneNumber nvarchar(20) NOT NULL,
PersonID int NOT NULL,
CONSTRAINT PK_PhoneNumber Primary Key (PhoneNumberID),
Constraint FK_PersonPhoneNumber Foreign Key (PersonID)
References Persons(PersonID)
);

create Table Emails
(
EmailID int IDENTITY(1,1) NOT NULL,
Email nvarchar(50) NOT NULL,
PersonID int Not NULL,
CONSTRAINT PK_Email Primary Key (EmailID),
Constraint FK_PersonEmail Foreign Key (PersonID)
References Persons(PersonID)
);

create Table Nationalities 
(
NationalityID INT IDENTITY(1,1) NOT NULL,
Nationality NVarChar(30) Not NULL,
CONSTRAINT PK_Nationality Primary Key (NationalityID)
);

create Table PersonNationalities
(
PersonNationalityID INT IDENTITY(1,1) NOT NULL,
PersonID Int NOT NULL,
NationalityID int NOT NULL,
CONSTRAINT PK_PersonNationality Primary Key (PersonNationalityID),
Constraint FK_PersonNationality Foreign Key (PersonID)
References Persons(PersonID),
Constraint FK_NationalityOfPerson Foreign Key (NationalityID)
References Nationalities(NationalityID)
);


-- 2 User Table and related Tables to it 

Create Table UserStatuses
(
UserStatusID INT IDENTITY(1,1) NOT NULL,
UserStatus NVarChar(30) NOT NULL,
CONSTRAINT PK_UserStatus Primary Key (UserStatusID)
);

Create Table Users
(
UserID INT IDENTITY(1,1) NOT NULL,
UserName NVarChar(30) NOT NULL,
UserPassword NvarChar(30) Not NULL,
StatusID int NOT NULL,
PersonID int NOT NULL,
CONSTRAINT PK_User Primary Key (UserID),
Constraint FK_PersonUser Foreign Key (PersonID)
References Persons(PersonID),
Constraint FK_StatusUser Foreign Key (StatusID)
References UserStatuses(UserStatusID)
);


Create Table Authorities
(
AuthorityID INT IDENTITY(1,1) NOT NULL,
Authority NVarChar(30) NOT NULL,
CONSTRAINT PK_Authority Primary Key (AuthorityID)
);

Create Table UserAuthorities
(
 UserAuthorityID INT IDENTITY(1,1) NOT NULL,
 UserID INT NOT NULL,
 AuthorityID INT NOT NULL,
 Constraint PK_UserAuthority Primary Key(UserAuthorityID),
 Constraint Fk_UserWhoHasAuthority Foreign Key (UserID)
 References Users(UserID),
 Constraint FK_AuthorityTOUser Foreign Key (AuthorityID)
 References Authorities(AuthorityID)
);

-- 3 Driver Table 

Create Table Drivers
(
DriverID INT IDENTITY(1,1) NOT NULL,
DriverSince Date Not NULL,
UserID INT NOT NULL,
Constraint PK_Driver Primary Key(DriverID),
Constraint FK_DriverUser Foreign Key (UserID)
References Users(UserID)
);

-- 4 license Classes and related Tables

Create Table LicenseClasses
(
LicenseClassID INT IDENTITY(1,1) NOT NULL,
ClassDescripition NvarChar(200) NOT NULl, 
PreRequirements NVarChar(200) ,
Constraint PK_LicenseClass Primary Key (LicenseClassID)
);

Create Table Vehicles
(
VehicleID INT IDENTITY(1,1) NOT NULL,
VehicleName NVarChar(40) Not NULL,
LicenseClassID Int Not NUll,
Constraint PK_Vehicle Primary Key (VehicleID),
Constraint FK_VehicleLicenseClass Foreign Key (LicenseClassID)
References LicenseClasses(LicenseClassID)
);


Create Table LicenseClassHistories
(
LicenseClassHistoryID INT IDENTITY(1,1) NOT NULL,
ClassFee Decimal Not NULL,
MinimumAllowedAge Int NOT NUll,
ValidityLength Int NOT Null,
LastChangeDate DateTime2 Default GETDATE(),
LicenseClassID Int Not NULL,
Constraint PK_LicenseClassHistory Primary Key(LicenseClassHistoryID),
Constraint FK_LicenseClassInHistory Foreign Key (LicenseClassID)
References LicenseClasses(LicenseClassID)
);

-- 5 Test Tables

Create Table TestClasses
(
TestClassID INT IDENTITY(1,1) NOT NULL,
TestClassFees Decimal Not NULL,
TestClassMinimumDegree Int NOT NULL,
PreRequirements nVarChar(100) ,
AdditionalFee Decimal ,
LicenseClassID Int Not NULL,
Constraint PK_TestClass Primary Key (TestClassID),
Constraint FK_LicenseClassForTestClass Foreign Key (LicenseClassID)
References LicenseClasses(LicenseClassID)
);

Create Table TestStatuses
(
TestStatusID INT IDENTITY(1,1) NOT NULL,
TestStatus NvarChar(30)
Constraint PK_TestStatus Primary Key (TestStatusID)
);

Create Table Tests
(
TestID INT IDENTITY(1,1) NOT NULL,
TestDate DateTime2 Not NULL,
TestStatusID int NOT NULL,
Degree Int,
TestClassID INT NOT NULL,
UserID INt Not NULL,
Constraint PK_Test Primary Key (TestID),
Constraint FK_TestClassForTest Foreign Key (TestClassID)
References TestClasses(TestClassID),
Constraint FK_UserWhoTakeTest Foreign Key (UserID)
References Users(UserID),
Constraint FK_TestStatus Foreign  Key(TestStatusID)
References TestStatuses(TestStatusID)
);

-- 6 Driver License And Related Table 

Create Table LicenseStatuses
(
LicenseStatusID INT IDENTITY(1,1) NOT NULL,
LicenseStatus NVarChar(30) NOT NULL,
Constraint PK_LicenseStatus Primary Key(LicenseStatusID)
);

Create Table DriverLicenses
(
DriverLicenseID INT IDENTITY(1,1) NOT NULL,
LicenseReleaseDate Date Default GETDATE(),
LicenseStatusID Int NOT NULL,
DriverID Int NOT NULL,
LicenseClassHistoryID Int NOT NULL,
Constraint PK_DriverLicense Primary Key (DriverLicenseID),
Constraint FK_DriverLicenseStatus Foreign Key (LicenseStatusID)
References LicenseStatuses(LicenseStatusID),
Constraint FK_TheDriveOfLicense Foreign Key (DriverID)
References Drivers(DriverID),
Constraint FK_DriverLicenseClassHistory Foreign Key ( LicenseClassHistoryID)
References LicenseClassHistories(LicenseClassHistoryID)
);

Alter Table DriverLicenses 
Add ExpirationDate as LicenseReleaseDate + LicenseClassHistories(ValidityLength);

Create Table LicenseFees
(
LicenseFeeID INT IDENTITY(1,1) NOT NULL,
FeeDescripition NVarChar(100) NOT NULL,
FeeAmount Decimal NOT NULL,
FeeReleaseDate Date Default GETDATE(),
PaidDate Date,
IsPaid Bit default 0,
DriverLicenseID INT not NULL,
Constraint PK_LicenseFee Primary Key(LicenseFeeID),
Constraint FK_LicenseThatHasFee Foreign Key(DriverLicenseID)
References DriverLicenses(DriverLicenseID)
);
