# Exemplo API com Dapper

Instalar essas duas dependencias 

->Dapper
->System.Data.SqlClient


CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](200) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[SchoolClass] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL
) ON [PRIMARY]
GO

SELECT [Id]
	,[Fullname]      
	,[BirthDate]      
	,[SchoolClass]    
	,[IsActive]  
FROM [Students]
