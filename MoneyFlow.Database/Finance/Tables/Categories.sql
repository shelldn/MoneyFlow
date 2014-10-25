CREATE TABLE Finance.Categories (
	Id				INT				NOT NULL	IDENTITY(1, 1)	PRIMARY KEY,
	AccountId		INT				NOT NULL	FOREIGN KEY REFERENCES [Identity].Accounts (Id),
	Words			NVARCHAR(MAX)	NOT NULL
);