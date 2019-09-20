using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace properTech.Models
{
    public class HostedData
    {
        public string extraCriteria { get; set; }
        public List<object> columnNames { get; set; }
        public string tableName { get; set; }
    }

    public class DisplayLatLng
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class LatLng
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class Origin
    {
        public bool dragPoint { get; set; }
        public DisplayLatLng displayLatLng { get; set; }
        public string adminArea4 { get; set; }
        public string adminArea5 { get; set; }
        public string postalCode { get; set; }
        public string adminArea1 { get; set; }
        public string adminArea3 { get; set; }
        public string type { get; set; }
        public string sideOfStreet { get; set; }
        public string geocodeQualityCode { get; set; }
        public string adminArea4Type { get; set; }
        public int linkId { get; set; }
        public string street { get; set; }
        public string adminArea5Type { get; set; }
        public string geocodeQuality { get; set; }
        public string adminArea1Type { get; set; }
        public string adminArea3Type { get; set; }
        public LatLng latLng { get; set; }
    }

    public class Options
    {
        public string kmlStyleUrl { get; set; }
        public string shapeFormat { get; set; }
        public int maxMatches { get; set; }
        public int pageSize { get; set; }
        public bool ambiguities { get; set; }
        public string units { get; set; }
        public int currentPage { get; set; }
        public int radius { get; set; }
    }

    public class LatLng2
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class MqapGeography
    {
        public LatLng2 latLng { get; set; }
    }

    public class Fields
    {
        public string mqap_id { get; set; }
        public string country { get; set; }
        public string address { get; set; }
        public double lng { get; set; }
        public string city { get; set; }
        public string group_sic_code_name_ext { get; set; }
        public string group_sic_code { get; set; }
        public string side_of_street { get; set; }
        public double disp_lng { get; set; }
        public string phone { get; set; }
        public string group_sic_code_ext { get; set; }
        public string group_sic_code_name { get; set; }
        public string name { get; set; }
        public double disp_lat { get; set; }
        public string state { get; set; }
        public string id { get; set; }
        public string postal_code { get; set; }
        public MqapGeography mqap_geography { get; set; }
        public double lat { get; set; }
    }

    public class SearchResult
    {
        public string distanceUnit { get; set; }
        public double distance { get; set; }
        public string name { get; set; }
        public string sourceName { get; set; }
        public int resultNumber { get; set; }
        public Fields fields { get; set; }
        public string key { get; set; }
        public List<double> shapePoints { get; set; }
    }

    public class Copyright
    {
        public string imageAltText { get; set; }
        public string imageUrl { get; set; }
        public string text { get; set; }
    }

    public class Info
    {
        public Copyright copyright { get; set; }
        public List<object> messages { get; set; }
        public int statusCode { get; set; }
    }

    public class MapQuestLocationData
    {
        public List<HostedData> hostedData { get; set; }
        public int resultsCount { get; set; }
        public Origin origin { get; set; }
        public int totalPages { get; set; }
        public Options options { get; set; }
        public List<SearchResult> searchResults { get; set; }
        public Info info { get; set; }
    }
}
