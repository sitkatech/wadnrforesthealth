 update dbo.Project 
 set ProgramID = (select ProgramID from dbo.Program p where p.ProgramName = 'USFS Timber Harvest')
 where ProjectID in (12735
,12732
,12731
,12730
,12729
,12724
,12722
,12721
,12720
,12719
,12718
,12716
,12715
,12714
,12713
,12712
,12711
,12707
,12706
,12705
,12704
,12702
,12701
,12698
,12697
,12696
,12695
,12693
,12691
,12690
,12689
,12688
,12687)