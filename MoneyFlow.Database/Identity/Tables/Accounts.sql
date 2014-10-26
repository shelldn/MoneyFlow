CREATE TABLE [Identity].Accounts (
	Id				INT				NOT NULL	IDENTITY(1, 1)	PRIMARY KEY,
	UserName		VARCHAR(128)	NOT NULL,
	EmailConfirmed	BIT				NOT NULL,
	PasswordHash	NVARCHAR(MAX)	NULL,
	SecurityStamp	NVARCHAR(MAX)	NULL
);