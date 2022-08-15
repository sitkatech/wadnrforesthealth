-- New table for External Map Layers
create table dbo.ExternalMapLayer(
	ExternalMapLayerID int not null identity(1,1) constraint PK_ExternalMapLayer_ExternalMapLayerID primary key,
	DisplayName varchar(75) not null,
	LayerUrl varchar(500) not null,
	LayerDescription varchar(200) null,
	FeatureNameField varchar(100) null,
	DisplayOnPriorityLandscape bit not null,
	DisplayOnProjectMap bit not null,
	DisplayOnAllOthers bit not null,
	IsActive bit not null,
	IsTiledMapService bit not null
);
go

alter table dbo.ExternalMapLayer
add constraint AK_ExternalMapLayer_DisplayName unique(DisplayName)

alter table dbo.ExternalMapLayer
add constraint AK_ExternalMapLayer_LayerUrl unique(LayerUrl)

-- Add Firma Page content to External map layers page
insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(69, 'ExternalMapLayers', 'External Map Layers', 1)

insert into dbo.FirmaPage(FirmaPageTypeID, FirmaPageContent)
values
(69, '<p>Use this page to add a connection between Forest Health Tracker and an external map web service hosted by ArcGIS Online or ArcServer. The system supports integration with both feature and tiled layers. After an external map layer connection is added, then the layer will appear on various maps throughout the system with the symbology provided by the service.</p>')

select * from FirmaPage order by FirmaPageID desc



