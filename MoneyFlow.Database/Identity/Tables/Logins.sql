CREATE TABLE [Identity].Logins (

	LoginProvider			NVARCHAR(128)				NOT NULL,
	ProviderKey				NVARCHAR(128)				NOT NULL,
	AccountId				INT							NOT NULL	FOREIGN KEY REFERENCES [Identity].Accounts (Id) ON DELETE CASCADE

	CONSTRAINT [PK_Identity.Logins] PRIMARY KEY CLUSTERED (LoginProvider, ProviderKey)
);