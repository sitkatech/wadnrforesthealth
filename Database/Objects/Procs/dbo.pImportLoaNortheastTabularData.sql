IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.pImportLoaNortheastTabularData'))
    DROP PROCEDURE dbo.pImportLoaNortheastTabularData
GO

CREATE PROCEDURE dbo.pImportLoaNortheastTabularData
AS
begin
              if object_id('tempdb.dbo.#projectGrantAllocation') is not null drop table #projectGrantAllocation
              select x.ProjectID
              , x.GrantAllocationID
              , x.Match
              , x.Pay
              , x.LetterDate
              , x.ProjectExpirationDate
              , x.Status
              into #projectGrantAllocation
              from dbo.vLoaNortheastProjectGrantAllocation x

              if object_id('tempdb.dbo.#projectGrantAllocationRequestPart') is not null drop table #projectGrantAllocationRequestPart
              select x.ProjectID,
                     x.GrantAllocationID
                     , x.Match + x.Pay as TotalAmount
                     , x.Match as MatchAmount
                     , x.Pay as PayAmount
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





              insert into dbo.ProjectGrantAllocationRequest(ProjectID, GrantAllocationID, TotalAmount, MatchAmount, PayAmount)

              select x.ProjectID,
                     x.GrantAllocationID
                     , x.TotalAmount
                     , x.MatchAmount
                     , x.PayAmount
                      from #projectGrantAllocationRequest x 


                update dbo.Project 
                set EstimatedTotalCost = y.EstimatedTotalCost
                ,   PlannedDate = y.Letterdate
                ,   ExpirationDate = y.ExpirationDate
                from dbo.Project p
                join (select x.ProjectID
                , sum(x.Match) + sum(x.Pay) as EstimatedTotalCost
                , min(x.LetterDate) as Letterdate
                , max(x.ProjectExpirationDate) as ExpirationDate
                  from  #projectGrantAllocation x group by x.ProjectID) y on y.ProjectID = p.ProjectID

end

/*

exec dbo.pImportLoaNortheastTabularData

*/