
-- Same as it is in Gemini
CREATE TABLE [dbo].[Country]
(
    [CountryID] [int] NOT NULL,
    [CountryName] [varchar](255) NOT NULL,
    [CountryAbbrev] [varchar](2) NOT NULL,
 CONSTRAINT [PK_Country_CountryID] PRIMARY KEY CLUSTERED 
(
    [CountryID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

insert into dbo.Country(CountryID, CountryName, CountryAbbrev)
values
(1,'United States of America','US'),
(2, 'Canada', 'CA'),
(3, 'Mexico', 'MX')
GO

ALTER TABLE [dbo].StateProvince  WITH CHECK 
ADD  CONSTRAINT [FK_StateProvince_Country_CountryID] FOREIGN KEY(CountryID)
REFERENCES [dbo].Country (CountryID)
GO



