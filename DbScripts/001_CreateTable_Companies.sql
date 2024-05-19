


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Companies](
	[Id] [uniqueidentifier] NOT NULL,
	[CompanyName] [nvarchar](1000) NOT NULL,
	[CompanyNameID] [nvarchar](250) NOT NULL,
	[IsMasterCompany] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CreatedUserID] [uniqueidentifier] NOT NULL,
	[DeletionDate] [datetime] NULL,
	[DeleteUserID] [nvarchar](450) NULL,
	[ImageExtn] [varchar](8) NULL,
 CONSTRAINT [PK_CompanyMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Companies] UNIQUE NONCLUSTERED 
(
	[CompanyNameID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Companies] ADD  CONSTRAINT [DF_Companies_IsMasterCompany]  DEFAULT ((0)) FOR [IsMasterCompany]
GO

ALTER TABLE [Companies] ADD  CONSTRAINT [DF_Companies_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

INSERT INTO [Companies]([Id],[CompanyName],[CompanyNameID],[IsMasterCompany],[IsDeleted],[CreationDate],[CreatedUserID])
VALUES('18D57488-FE3B-4096-8B73-0903D18A0E39','Master Company','MasterCompany',1,0,GETDATE(),'18D57488-FE3B-4096-8B73-0903D18A0E39')


