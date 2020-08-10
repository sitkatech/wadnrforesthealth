
alter table dbo.Treatment drop column [TreatmentChippingAcres]

alter table dbo.Treatment drop column [TreatmentPruningAcres]


alter table dbo.Treatment drop column [TreatmentThinningAcres]

alter table dbo.Treatment drop column [TreatmentMasticationAcres]

alter table dbo.Treatment drop column [TreatmentGrazingAcres]

alter table dbo.Treatment drop column [TreatmentLopAndScatterAcres]

alter table dbo.Treatment drop column [TreatmentBiomassRemovalAcres]

alter table dbo.Treatment drop column [TreatmentHandPileAcres]

alter table dbo.Treatment drop column [TreatmentBroadcastBurnAcres]

alter table dbo.Treatment drop column [TreatmentHandPileBurnAcres]

alter table dbo.Treatment drop column [TreatmentMachinePileBurnAcres]

alter table dbo.Treatment drop column [TreatmentOtherTreatmentAcres]

alter table dbo.Treatment drop column [TreatmentSlashAcres]

alter table dbo.Treatment add TreatmentTreatedAcres decimal(38,10)

