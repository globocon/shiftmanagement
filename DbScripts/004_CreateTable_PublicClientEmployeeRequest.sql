
CREATE TABLE [PublicClientEmployeeRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[RequestType] [nvarchar](1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Gender] [nvarchar](1) NULL,
	[EmployementTypeId] [int] NULL,
	[DOB] [date] NULL,
	[DOJ] [date] NULL,	
	[ImageExtn] [nvarchar](8) NULL,
	[Email] [nvarchar](250) NULL,
	[Address] [nvarchar](max) NULL,
	[RequestDateTime] [datetime] NULL,
	[CompanyId] [uniqueidentifier] NULL,
	[RequestStatus] [nvarchar](10) NOT NULL default 'PENDING',
	[New_Emp_Client_Id] [int] null,
 CONSTRAINT [PK_PublicClientEmployeeRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



