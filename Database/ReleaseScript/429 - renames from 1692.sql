/*
select * from FirmaPage

select * from ProjectImageUpdate
*/

update dbo.FirmaPage 
set FirmaPageContent = '<p>DNR LOA Focus Areas are geographic project containers primarily distinguished by funding source.</p>  <p>Click on a DNR LOA Focus Area name to view details such as grant allocation awards, projects, and closeout summary report.</p> '
where FirmaPageID = 495

go

update dbo.ClassificationSystem
set ClassificationSystemDefinition = 'Projects by Theme'
where ClassificationSystemID = 11

go