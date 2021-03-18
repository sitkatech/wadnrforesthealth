IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pImportLoaTabularData'))
    DROP PROCEDURE dbo.pImportLoaTabularData
GO

CREATE PROCEDURE dbo.pImportLoaTabularData
AS
begin
              if object_id('tempdb.dbo.#projectGrantAllocation') is not null drop table #projectGrantAllocation
              select x.ProjectID
              , x.GrantAllocationID
              , x.MatchAmount
              , x.PayAmount
              , x.LetterDate
              , x.ProjectExpirationDate
              , x.ProjectStatus
              into #projectGrantAllocation
              from dbo.vLoaStageProjectGrantAllocation x


              if object_id('tempdb.dbo.#projectFocusArea') is not null drop table #projectFocusArea
              select distinct p.ProjectID, fa.FocusAreaID
              into #projectFocusArea
              from dbo.LoaStage x
              join dbo.FocusArea fa on (fa.FocusAreaName like '%'+ x.FocusAreaName + '%') or 
                                       ((fa.FocusAreaName like '%'+(case when LEN(x.FocusAreaName) > 5 then LEFT(x.FocusAreaName, LEN(x.FocusAreaName)-5) else x.FocusAreaName end) + '%') and x.IsSoutheast = 1)
              join dbo.Project p on p.ProjectGisIdentifier = x.ProjectIdentifier
              where x.FocusAreaName is not null 

              if object_id('tempdb.dbo.#projectFocusAreaForUpdate') is not null drop table #projectFocusAreaForUpdate
              select distinct x.ProjectID, min(x.FocusAreaID) as FocusAreaID
              into #projectFocusAreaForUpdate
              from #projectFocusArea x
              group by x.ProjectID having count(*) =1


              update dbo.Project
              set FocusAreaID = pfa.FocusAreaID
              from dbo.Project p
              join #projectFocusAreaForUpdate pfa on pfa.ProjectID = p.ProjectID


              if object_id('tempdb.dbo.#projectGrantAllocationRequestPart') is not null drop table #projectGrantAllocationRequestPart
              select x.ProjectID,
                     x.GrantAllocationID
                     , x.MatchAmount + x.PayAmount as TotalAmount
                     , x.MatchAmount as MatchAmount
                     , x.PayAmount as PayAmount
                     into #projectGrantAllocationRequestPart
                      from #projectGrantAllocation x where x.GrantAllocationID is not null


            if object_id('tempdb.dbo.#projectGrantAllocationRequest') is not null drop table #projectGrantAllocationRequest
              select x.ProjectID,
                     x.GrantAllocationID
                     , sum(x.TotalAmount) as TotalAmount
                     , sum(x.MatchAmount) as MatchAmount
                     , sum(x.PayAmount) as PayAmount
                     into #projectGrantAllocationRequest
                      from #projectGrantAllocationRequestPart x group by x.ProjectID, x.GrantAllocationID




              delete from dbo.ProjectGrantAllocationRequest where ProjectGrantAllocationRequestID in (select ProjectGrantAllocationRequestID 
                                                                                                      from 
                                                                                                      dbo.ProjectGrantAllocationRequest pgar
                                                                                                      join dbo.Project p on pgar.ProjectID = p.ProjectID
                                                                                                      where p.ProgramID = 3 -- LoaProgram)
                                                                                                      )

              insert into dbo.ProjectGrantAllocationRequest(ProjectID, GrantAllocationID, TotalAmount, MatchAmount, PayAmount, CreateDate, ImportedFromTabularData)

              select x.ProjectID,
                     x.GrantAllocationID
                     , x.TotalAmount
                     , x.MatchAmount
                     , x.PayAmount
                     , getdate()
                     , 1
                      from #projectGrantAllocationRequest x 


                update dbo.Project 
                set EstimatedTotalCost = y.EstimatedTotalCost
                ,   PlannedDate = y.Letterdate
                ,   ExpirationDate = y.ExpirationDate
                from dbo.Project p
                join (select x.ProjectID
                , sum(x.MatchAmount) + sum(x.PayAmount) as EstimatedTotalCost
                , min(x.LetterDate) as Letterdate
                , max(x.ProjectExpirationDate) as ExpirationDate
                  from  #projectGrantAllocation x group by x.ProjectID) y on y.ProjectID = p.ProjectID

end

/*

exec dbo.pImportLoaTabularData

*/