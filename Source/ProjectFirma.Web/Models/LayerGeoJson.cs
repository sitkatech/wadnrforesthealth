/*-----------------------------------------------------------------------
<copyright file="LayerGeoJson.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System.Drawing;
using System.Linq;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectFirma.Web.Views.Shared.FileResourceControls;

namespace ProjectFirma.Web.Models
{
    /// <summary>
    /// example: Jurisdiction layer or Road layer
    /// </summary>
    public class LayerGeoJson
    {
        public readonly string LayerName;
        public readonly FeatureCollection GeoJsonFeatureCollection;
        public readonly string MapServerUrl;
        public readonly string MapServerLayerName;
        public readonly string LayerColor;
        public readonly decimal LayerOpacity;
        public readonly LayerInitialVisibility LayerInitialVisibility;
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly LayerGeoJsonType LayerType;
        public readonly bool HasCustomPopups;
        public string LayerIconImageLocation;
        public bool HasCqlFilter;
        public string CqlFilter;
        public int ContextObjectId;

        /// <summary>
        /// Constructor for LayerGeoJson with Vector Type
        /// </summary>
        public LayerGeoJson(string layerName, FeatureCollection geoJsonFeatureCollection, string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            LayerName = layerName;
            GeoJsonFeatureCollection = geoJsonFeatureCollection;
            LayerColor = layerColor.StartsWith("#") ? layerColor : GetColorString(layerColor);
            LayerOpacity = layerOpacity;
            LayerInitialVisibility = layerInitialVisibility;
            LayerType = LayerGeoJsonType.Vector;
            HasCustomPopups = geoJsonFeatureCollection?.Features.Any(x => x.Properties.ContainsKey("PopupUrl")) ?? false;
        }
        /// <summary>
        /// Constructor for LayerGeoJson with WMS Type
        /// </summary>
        public LayerGeoJson(string layerName, string mapServerUrl, string mapServerLayerName, string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility)
        {
            LayerName = layerName;
            MapServerUrl = mapServerUrl;
            MapServerLayerName = mapServerLayerName;
            LayerColor = layerColor;
            LayerOpacity = layerOpacity;
            LayerInitialVisibility = layerInitialVisibility;
            LayerType = LayerGeoJsonType.Wms;
        }

        /// <summary>
        /// Constructor for LayerGeoJson with WMS Type
        /// </summary>
        public LayerGeoJson(string layerName, string mapServerUrl, string mapServerLayerName, string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, string mapLayerIconImageLocation)
        {
            LayerName = layerName;
            MapServerUrl = mapServerUrl;
            MapServerLayerName = mapServerLayerName;
            LayerColor = layerColor;
            LayerOpacity = layerOpacity;
            LayerInitialVisibility = layerInitialVisibility;
            LayerType = LayerGeoJsonType.Wms;
            LayerIconImageLocation = mapLayerIconImageLocation;
        }


        /// <summary>
        /// Constructor for LayerGeoJson with WMS Type
        /// </summary>
        public LayerGeoJson(string layerName, string mapServerUrl, string mapServerLayerName, string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, string cqlFilter, bool hasCqlFilter) : this(layerName, mapServerUrl, mapServerLayerName, layerColor, layerOpacity, layerInitialVisibility, cqlFilter, hasCqlFilter, string.Empty)
        {
            
        }

        /// <summary>
        /// Constructor for LayerGeoJson with WMS Type
        /// </summary>
        public LayerGeoJson(string layerName, string mapServerUrl, string mapServerLayerName, string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, string cqlFilter, bool hasCqlFilter, int contextObjectId) : this(layerName, mapServerUrl, mapServerLayerName, layerColor, layerOpacity, layerInitialVisibility, cqlFilter, hasCqlFilter, string.Empty)
        {
            ContextObjectId = contextObjectId;
        }


        /// <summary>
        /// Constructor for LayerGeoJson with WMS Type
        /// </summary>
        public LayerGeoJson(string layerName, string mapServerUrl, string mapServerLayerName, string layerColor, decimal layerOpacity, LayerInitialVisibility layerInitialVisibility, string cqlFilter, bool hasCqlFilter, string mapLayerIconImageLocation) : this(layerName, mapServerUrl, mapServerLayerName, layerColor, layerOpacity, layerInitialVisibility, mapLayerIconImageLocation)
        {
            if (!string.IsNullOrEmpty(cqlFilter) && hasCqlFilter)
            {
                HasCqlFilter = hasCqlFilter;
                CqlFilter = cqlFilter;
            }
        }




        public string GetGeoJsonFeatureCollectionAsJsonString()
        {
            return JsonTools.SerializeObject(GeoJsonFeatureCollection);
        }

        private static string GetColorString(string colorName)
        {
            var color = Color.FromName(colorName);
            return $"#{color.R:x2}{color.G:x2}{color.B:x2}";
        }
    }
}
