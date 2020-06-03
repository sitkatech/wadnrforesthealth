/*
select * from FirmaPage where FirmaPageContent like '%organization%'

select * from ProjectImageUpdate
*/

update dbo.FirmaPage 
set FirmaPageContent = '<p>DNR LOA Focus Areas are geographic project containers primarily distinguished by funding source.</p>  <p>Click on a DNR LOA Focus Area name to view details such as grant allocation awards, projects, and closeout summary report.</p> '
where FirmaPageID = 495

go

update dbo.FirmaPage
set FirmaPageContent = '<p>The project type assigns a project to a specific category, and only one type can be selected.&nbsp; Additional project attributes can be added to each project to further describe project details.&nbsp; The project type allows us to aggregate and view specific actions being planned and implemented across the state over a period of time.&nbsp; For example, it allows us to pull from all the data in this system to answer <em>&quot;how much prescribed fire was planned or conducted in eastern Washington since the 20-Year Forest Health Strategic Plan for eastern Washington was adopted?&quot;</em>.&nbsp; Additionally, the project type allows us to view and differentiate between an integrated forest health project plan for a particular piece of ground that will be implemented over multiple years and each individual treatment that is completed within that plan (i.e. a commercial thin then followed by a surface fuels treatment to pile fuels followed by a prescribed burn).&nbsp;</p>  <p>Each landowner, land and resource agency, tribe, and contributing organization may have their own terms used to describe and track a project and forest health treatments.&nbsp; We have created this simplified set of of project types to allow us a way to categorize across all land ownerships.</p>  <p>Currently the project types are mainly focused on vegetation management, but as Forest Health Tracker evolves we intend to add a greater diversity of forest health project types.</p> '
where FirmaPageID = 428

go

update dbo.FirmaPage
set FirmaPageContent = '<p>The following contributing organizations have partcipiated in forest health,&nbsp;fuels reduction, or related&nbsp;projects. Click a contributing organization to see details, including a map of&nbsp;projects associated with each contributing organization.</p> '
where FirmaPageID = 432

go

update dbo.FirmaPage
set FirmaPageContent = '<p>This list shows agreements related to forest health and fuels reduction work.&nbsp;Use the grid header to search for an agreement.</p>  <p>Click an agreement title to see details, including&nbsp;projects and contacts&nbsp;associated with each agreement. Click on the contributing organization name to view the contributing organization details.</p> '
where FirmaPageID = 496

go

update dbo.ClassificationSystem
set ClassificationSystemDefinition = 'Projects by Theme'
where ClassificationSystemID = 11

go

