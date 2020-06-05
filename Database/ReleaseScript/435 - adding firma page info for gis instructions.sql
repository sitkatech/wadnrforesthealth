insert into dbo.FirmaPageType(FirmaPageTypeID, FirmaPageTypeName, FirmaPageTypeDisplayName, FirmaPageRenderTypeID)
values
(63, 'GisUploadAttemptInstructions', 'GIS Upload Attempt Instructions', 1)


insert into dbo.FirmaPage (FirmaPageTypeID, FirmaPageContent)
values
(63, '<p>This GIS Import wizard is used to import spatial data to create Treatments and Projects.</p>  <p>The various stages of a GIS Import that are available can be seen on the left. Several of these sections are optional, but others are required:</p>  <ul>  <li><strong>Upload Gis File:</strong>&nbsp;<em>You must select a file for upload</em></li>  <li><strong>Validate Features:</strong>&nbsp;<em>You must review and validate the features</em></li> </ul>  <p>In order to import GIS data, all required stages must be completed. You will see some icons next to some pages &ndash; here is what they mean:</p>  <ul style="list-style-type: none; padding: 0">     <li class="iconRow">         <div class="iconDefinition"><span title="Required information has not been completely provided (not ready to submit)" class="glyphicon glyphicon-exclamation-sign"></span></div> Required information has not been completely provided (you can''t submit this update)     </li>     <li class="iconRow">         <div class="iconDefinition"><span title="Required information has been provided (ready to submit)" class="glyphicon glyphicon-ok"></span></div> Required information has been provided and the stage is complete     </li> </ul>  <p>Approving this upload will create/update Projects and Treatments</p>  <p>Click the&nbsp;&nbsp;icon to view field-specific help.</p>  <p>If you have questions or run into problems, please&nbsp;<a href="https://foresthealthtracker.dnr.wa.gov/Help/Support" onclick="return modalDialogLink(this, null, null);">Request Support</a>.</p> ')


