




delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 20069 and PersonID != 6080

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 20118 and PersonID != 9907

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 20562 and PersonID != 6493

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 21022 and PersonID != 9907

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 18643 and PersonID != 9907

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 18148 and PersonID != 9907

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 21183 and PersonID != 10055

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 21854 and PersonID != 6857

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 21870 and PersonID != 9908

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 19531 and PersonID != 6081

delete from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1
and ProjectID = 22323 and PersonID != 6907



CREATE UNIQUE INDEX [UNQ_ProjectPerson_ProjectPersonRelationshipTypeID]
  ON dbo.ProjectPerson(ProjectID)
  WHERE   (ProjectPersonRelationshipTypeID = 1);--Primary Contact





/*

select ProjectID, count(*)
from dbo.ProjectPerson
where ProjectPersonRelationshipTypeID = 1 --Primary Contact
group by ProjectID
having count(*) > 1

*/