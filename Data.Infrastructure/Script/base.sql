use DB_Sales

CREATE TABLE Sale (
    Id INT PRIMARY KEY IDENTITY,
	IdProduto INT,
    DateSale DATETIME NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Details NVARCHAR(MAX),
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
	DateCreate DATETIME NOT NULL,
	PAY BIT,
	DateEdit DATETIME 
);

CREATE TABLE Product (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(255) NOT NULL,
    Details NVARCHAR(MAX),
	Active BIT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
	DateCreate DATETIME NOT NULL,
	DateEdit DATETIME 
);

CREATE TABLE Costs (
    Id INT PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Value DECIMAL(18,2) NOT NULL,
    Date DATE NOT NULL,
    Category NVARCHAR(100),
    Notes NVARCHAR(MAX)
);
