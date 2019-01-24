if not exists(select 1 from dbo.FirmaPage fp where fp.FirmaPageTypeID = 60)
begin
insert into dbo.FirmaPage (FirmaPageTypeID) values(60)
end