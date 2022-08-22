
-- Delete treatments with mismatched ProjectID from ProjectLocation
	delete dbo.Treatment
	from dbo.Treatment t
		join dbo.ProjectLocation pl on pl.ProjectLocationID = t.ProjectLocationID
	where pl.ProjectID != t.ProjectID

-- Update existing temporary cache ID to null
	update dbo.ProjectLocation
	set TemporaryTreatmentCacheID = null
