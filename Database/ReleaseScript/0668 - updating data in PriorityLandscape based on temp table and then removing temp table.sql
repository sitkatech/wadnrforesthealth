

CREATE SEQUENCE priorityLandscapeSequence  
    START WITH 7587  
    INCREMENT BY 1;

--first insert new priority landscapes
INSERT INTO dbo.PriorityLandscape
(
	PriorityLandscapeID,
	PriorityLandscapeName,
	PriorityLandscapeLocation,
	PlanYear,
	PriorityLandscapeAboveMapText,
	PriorityLandscapeCategoryID
)
SELECT 
	NEXT VALUE FOR priorityLandscapeSequence,
	PriorityLandscapeName,
	PriorityLandscapeLocation,
	PlanYear,
	PriorityLandscapeAboveMapText,
	PriorityLandscapeCategoryID
FROM dbo.tempPriorityLandscape where PriorityLandscapeID is null


DROP SEQUENCE priorityLandscapeSequence;


--now update existing landscapes
update dbo.PriorityLandscape set
    PriorityLandscapeLocation = temppl.PriorityLandscapeLocation,
	PlanYear = temppl.PlanYear
FROM
	dbo.PriorityLandscape as pl 
	join
    dbo.tempPriorityLandscape temppl
ON 
    pl.PriorityLandscapeID = temppl.PriorityLandscapeID




	drop table dbo.tempPriorityLandscape