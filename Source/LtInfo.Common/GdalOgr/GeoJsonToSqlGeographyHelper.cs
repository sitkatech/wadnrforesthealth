﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoJSON.Net;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;
using GeoJSON.Net.Feature;

namespace LtInfo.Common.GeoJson
{
	/*
        MsSqlSpatialConvert
        Partial class. Only methods from GeoJSON to Sql Server geometry are here
        For Sql spatial types to GeoJSON, see MsSqlSpatialConvertToGeoJson.cs file
        For GeoJSON to Sql Server geometry, see MsSqlSpatialConvertToSqlGeography.cs file
        For GeoJSON to Sql Server geography, see MsSqlSpatialConvertToSqlGeography.cs file
    */
	/// <summary>
	/// GeoJSON.Net / MS Sql Server Spatial data types converter.
	/// All methods here are static and extensions to GeoJSON.Net types and Sql Server types.
	/// </summary>
	public static partial class GeoJsonToSqlGeographyHelper
	{

		#region GeoJSON to SqlGeography

		/// <summary>
		/// Converts a GeoJSON Point to an SqlGeography
		/// </summary>
		/// <param name="point"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this Point point, int srid = 4326)
		{
			SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);

			Internal_FillGeographyBuilder(gb, point);

			return gb.ConstructedGeography;
		}
		private static void Internal_FillGeographyBuilder(SqlGeographyBuilder gb, Point point)
		{
			gb.BeginGeography(OpenGisGeographyType.Point);
			Position pos = point.Coordinates as Position;
			gb.BeginFigure(pos.Latitude, pos.Longitude);
			gb.EndFigure();
			gb.EndGeography();
		}

		/// <summary>
		/// Converts a GeoJSON MultiPoint to an SqlGeography
		/// </summary>
		/// <param name="multiPoint"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this MultiPoint multiPoint, int srid = 4326)
		{
            SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);
			Internal_FillGeographyBuilder(gb, multiPoint);
			return gb.ConstructedGeography;
		}
		private static void Internal_FillGeographyBuilder(SqlGeographyBuilder gb, MultiPoint multiPoint)
		{
			gb.BeginGeography(OpenGisGeographyType.MultiPoint);
			List<Point> coords = multiPoint.Coordinates.ToList();
			foreach (var coord in coords)
			{
				Position pos = coord.Coordinates as Position;
				gb.BeginGeography(OpenGisGeographyType.Point);
				gb.BeginFigure(pos.Latitude, pos.Longitude);
				gb.EndFigure();
				gb.EndGeography();
			}
			gb.EndGeography();
		}

		/// <summary>
		/// Converts a GeoJSON LineString to an SqlGeography
		/// </summary>
		/// <param name="lineString"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this LineString lineString, int srid = 4326)
		{
			SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);
			Internal_FillGeographyBuilder(gb, lineString);
			return gb.ConstructedGeography;
		}
		private static void Internal_FillGeographyBuilder(SqlGeographyBuilder gb, LineString lineString)
		{
			gb.BeginGeography(OpenGisGeographyType.LineString);
			bool beginFigureCalled = false;
			foreach (var ipos in lineString.Coordinates)
			{
				Position pos = ipos as Position;
				if (!beginFigureCalled)
				{
					gb.BeginFigure(pos.Latitude, pos.Longitude);
					beginFigureCalled = true;

				}
				else
				{
					gb.AddLine(pos.Latitude, pos.Longitude);
				}

			}
			gb.EndFigure();
			gb.EndGeography();
		}

		/// <summary>
		/// Converts a GeoJSON MultiLineString to an SqlGeography
		/// </summary>
		/// <param name="multiLineString"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this MultiLineString multiLineString, int srid = 4326)
		{
			SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);
			Internal_FillGeographyBuilder(gb, multiLineString);
			return gb.ConstructedGeography;
		}
		private static void Internal_FillGeographyBuilder(SqlGeographyBuilder gb, MultiLineString multiLineString)
		{
			gb.BeginGeography(OpenGisGeographyType.MultiLineString);
			foreach (var lineString in multiLineString.Coordinates)
			{
				gb.BeginGeography(OpenGisGeographyType.LineString);
				bool beginFigureCalled = false;
				foreach (var ipos in lineString.Coordinates)
				{
					Position pos = ipos as Position;
					if (!beginFigureCalled)
					{
						gb.BeginFigure(pos.Latitude, pos.Longitude);
						beginFigureCalled = true;
					}
					else
					{
						gb.AddLine(pos.Latitude, pos.Longitude);
					}
				}
				gb.EndFigure();
				gb.EndGeography();
			}
			gb.EndGeography();
		}

		/// <summary>
		/// Converts a GeoJSON Polygon to an SqlGeography
		/// </summary>
		/// <param name="polygon"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this Polygon polygon, int srid = 4326)
		{
			SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);
			Internal_FillGeographyBuilder(gb, polygon);
			return gb.ConstructedGeography;
		}
		private static void Internal_FillGeographyBuilder(SqlGeographyBuilder gb, Polygon polygon)
		{
			gb.BeginGeography(OpenGisGeographyType.Polygon);
			foreach (var lineString in polygon.Coordinates)
			{
				bool beginFigureCalled = false;
				foreach (var ipos in lineString.Coordinates)
				{
					Position pos = ipos as Position;
					if (!beginFigureCalled)
					{
						gb.BeginFigure(pos.Latitude, pos.Longitude);
						beginFigureCalled = true;
					}
					else
					{
						gb.AddLine(pos.Latitude, pos.Longitude);
					}
				}
				gb.EndFigure();
			}
			gb.EndGeography();
		}

		/// <summary>
		/// Converts a GeoJSON MultiPolygon to an SqlGeography
		/// </summary>
		/// <param name="multiPolygon"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this MultiPolygon multiPolygon, int srid = 4326)
		{
			SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);
			gb.BeginGeography(OpenGisGeographyType.MultiPolygon);
			foreach (var polygon in multiPolygon.Coordinates)
			{
				Internal_FillGeographyBuilder(gb, polygon);
			}
			gb.EndGeography();
			return gb.ConstructedGeography;
		}
		private static void Internal_FillGeographyBuilder(SqlGeographyBuilder gb, MultiPolygon multiPolygon)
		{
			gb.BeginGeography(OpenGisGeographyType.MultiPolygon);
			foreach (var polygon in multiPolygon.Coordinates)
			{
				Internal_FillGeographyBuilder(gb, polygon);
			}
			gb.EndGeography();
		}

		/// <summary>
		/// Converts a GeoJSON GeographyCollection to an SqlGeography
		/// </summary>
		/// <param name="geometryCollection"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this GeometryCollection geometryCollection, int srid = 4326)
		{
			SqlGeographyBuilder gb = new SqlGeographyBuilder();
			gb.SetSrid(srid);
			gb.BeginGeography(OpenGisGeographyType.GeometryCollection);
			foreach (var geom in geometryCollection.Geometries)
			{
				switch (geom.Type)
				{
					case GeoJSONObjectType.LineString:
						Internal_FillGeographyBuilder(gb, geom as LineString);
						break;
					case GeoJSONObjectType.MultiLineString:
						Internal_FillGeographyBuilder(gb, geom as MultiLineString);
						break;
					case GeoJSONObjectType.Point:
						Internal_FillGeographyBuilder(gb, geom as Point);
						break;
					case GeoJSONObjectType.MultiPoint:
						Internal_FillGeographyBuilder(gb, geom as MultiPoint);
						break;
					case GeoJSONObjectType.Polygon:
						Internal_FillGeographyBuilder(gb, geom as Polygon);
						break;
					case GeoJSONObjectType.MultiPolygon:
						Internal_FillGeographyBuilder(gb, geom as MultiPolygon);
						break;
					default:
						throw new NotSupportedException("Geography conversion is not supported for " + geom.Type.ToString());
				}
			}
			gb.EndGeography();
			return gb.ConstructedGeography;
		}

		/// <summary>
		/// Converts a GeoJSON Feature to an SqlGeography
		/// </summary>
		/// <param name="feature"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static SqlGeography ToSqlGeography(this Feature feature, int srid = 4326)
		{
			switch (feature.Geometry.Type)
			{
				case GeoJSONObjectType.LineString:
					return ((LineString)feature.Geometry).ToSqlGeography(srid);

				case GeoJSONObjectType.MultiLineString:
					return ((MultiLineString)feature.Geometry).ToSqlGeography(srid);

				case GeoJSONObjectType.Point:
					return ((Point)feature.Geometry).ToSqlGeography(srid);

				case GeoJSONObjectType.MultiPoint:
					return ((MultiPoint)feature.Geometry).ToSqlGeography(srid);

				case GeoJSONObjectType.Polygon:
					return ((Polygon)feature.Geometry).ToSqlGeography(srid);

				case GeoJSONObjectType.MultiPolygon:
					return ((MultiPolygon)feature.Geometry).ToSqlGeography(srid);

				default:
					throw new NotSupportedException("Geography conversion is not supported for " + feature.Type.ToString());
			}
		}

		/// <summary>
		/// Converts a GeoJSON FeatureCollection to a list of SqlGeography, each SqlGeography matching each feature in the collection
		/// </summary>
		/// <param name="featureCollection"></param>
		/// <param name="srid"></param>
		/// <returns></returns>
		public static List<SqlGeography> ToSqlGeography(this FeatureCollection featureCollection, int srid = 4326)
		{
			List<SqlGeography> retList = new List<SqlGeography>();
			foreach (var feature in featureCollection.Features)
			{
				var sqlGeom = feature.ToSqlGeography(srid);
				if (sqlGeom != null)
					retList.Add(sqlGeom);
			}
			return retList;
		}

		#endregion

	}
}
