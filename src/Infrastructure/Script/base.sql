CREATE TABLE Product (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Details NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
	Active BIT NOT NULL DEFAULT 1,                 
	DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
	DateEdit DATETIME 
);

CREATE TABLE Cost (
    Id INT PRIMARY KEY IDENTITY,
    Quantity NVARCHAR(255),
    Name NVARCHAR(255) NOT NULL,
    DateCost DATETIME NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
	DateEdit DATETIME 
);

CREATE TABLE Client (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
	Phone VARCHAR(15) NULL,
	Location NVARCHAR(255) NULL,
    Active BIT NOT NULL DEFAULT 1,                 
    DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
	DateEdit DATETIME 
);

CREATE TABLE Sale (
    Id INT PRIMARY KEY IDENTITY,
	IdProduct INT,
    IdClient INT,
    DateSale DATETIME NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Details NVARCHAR(MAX),
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
	Pay BIT,
	DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
	DateEdit DATETIME ,
    CONSTRAINT FK_Sale_Client FOREIGN KEY (IdClient) REFERENCES Client(Id),
    CONSTRAINT FK_Sale_Product FOREIGN KEY (IdProduct) REFERENCES Product(Id)
);

CREATE TABLE AccessLevel(
    Id INT PRIMARY KEY,
	Name  NVARCHAR(25) NOT NULL
);

CREATE TABLE UserCredentials (
    Id INT PRIMARY KEY IDENTITY, 
    Username NVARCHAR(255) NOT NULL UNIQUE, 
	Name VARCHAR(50) NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE, 
    PasswordHash NVARCHAR(255) NOT NULL,           
    PasswordSalt NVARCHAR(255) NOT NULL,           
    Token NVARCHAR(255) NULL,
    TokenExpiration DATETIME NULL,
	LastLogin DATETIME NULL,
	Active BIT NOT NULL DEFAULT 1,                 
    DateCreate DATETIME NOT NULL DEFAULT GETDATE(), 
    DateEdit DATETIME NULL,
);
CREATE TABLE UserProfile (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
	AccessLevelId int NULL,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NULL,
    Phone NVARCHAR(15) NULL,
    Image NVARCHAR(MAX) NULL,
    ZipCode NVARCHAR(10) NULL,
    Address NVARCHAR(255) NULL,
    Number NVARCHAR(10) NULL,
    Neighborhood NVARCHAR (255) NULL,
    City NVARCHAR(255) NULL,
    State NVARCHAR(255) NULL,
    DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
    DateEdit DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES UserCredentials(Id),
	FOREIGN KEY (AccessLevelId) REFERENCES AccessLevel(Id)
);

CREATE TABLE Employee (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(255) NOT NULL,
	LastName NVARCHAR(255) NOT NULL,
	Email NVARCHAR(255) NULL UNIQUE,
	JobTitle NVARCHAR(255) NOT NULL,
	BirthDate DATETIME NULL,
    Phone VARCHAR(15) NULL,
    Address NVARCHAR(255) NULL,
    City NVARCHAR(255) NULL,
    State NVARCHAR(255) NULL,
    ZipCode NVARCHAR(10) NULL,
	Salary DECIMAL(18,2)  NULL,
	Active BIT NOT NULL DEFAULT 1,
    DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
    DateEdit DATETIME NULL
);

CREATE TABLE ProductCost (
    Id INT PRIMARY KEY IDENTITY,
	IdProductTotalCost INT,
    IdCost INT NULL,
	TotalProductPrice DECIMAL(18,2) NOT NULL,
    TotalQuantity INT NOT NULL,
    QuantityRequired INT NOT NULL,
    IngredientCost DECIMAL(18,2) NOT NULL,
	DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
	DateEdit DATETIME NULL,
	CONSTRAINT FK_ProductCost_ProductTotalCost FOREIGN KEY (IdProductTotalCost) REFERENCES ProductTotalCost(Id),
    CONSTRAINT FK_ProductCost_Cost FOREIGN KEY (IdCost) REFERENCES Cost(Id)
);

CREATE TABLE  ProductTotalCost(
    Id INT PRIMARY KEY IDENTITY,
	IdProduct INT NULL,
	TotalProductCost DECIMAL(18,2),
    Active BIT NOT NULL DEFAULT 1,
	DateCreate DATETIME NOT NULL DEFAULT GETDATE(),
	DateEdit DATETIME NULL,
	CONSTRAINT FK_ProductCost_Product FOREIGN KEY (IdProduct) REFERENCES Product(Id)
)