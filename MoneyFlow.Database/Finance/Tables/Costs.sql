﻿CREATE TABLE Finance.Costs (
	Id				INT				NOT NULL	IDENTITY(1, 1)	PRIMARY KEY,
	CategoryId		INT				NULL		FOREIGN KEY REFERENCES Finance.Categories (Id) ON DELETE SET NULL,
	AccountId		INT				NOT NULL	FOREIGN KEY REFERENCES [Identity].Accounts (Id),
	[Date]			DATETIME2		NOT NULL,
	Amount			DECIMAL			NOT NULL
);