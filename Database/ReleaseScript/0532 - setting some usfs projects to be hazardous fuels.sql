 update dbo.Project 
 set ProgramID = (select ProgramID from dbo.Program p where p.ProgramName = 'USFS Hazardous Fuels')
 where ProjectID in (12736
,12734
,12733
,12728
,12727
,12726
,12725
,12723
,12717
,12710
,12709
,12708
,12703
,12700
,12699
,12694
,12692)