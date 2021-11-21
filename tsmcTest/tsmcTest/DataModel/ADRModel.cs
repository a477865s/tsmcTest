using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace tsmcTest.DataModel
{
    public class UsStock
    {
        public chart chart { get; set; }
    }

    public class Pre
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Regular
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    public class Post
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    public class CurrentTradingPeriod
    {
        public Pre pre { get; set; }
        public Regular regular { get; set; }
        public Post post { get; set; }
    }

    public class meta
    {
        public string currency { get; set; }
        public string symbol { get; set; }
        public string exchangeName { get; set; }
        public string instrumentType { get; set; }
        public int firstTradeDate { get; set; }
        public int regularMarketTime { get; set; }
        public int gmtoffset { get; set; }
        public string timezone { get; set; }
        public string exchangeTimezoneName { get; set; }
        public double regularMarketPrice { get; set; }
        public double chartPreviousClose { get; set; }
        public double previousClose { get; set; }
        public int scale { get; set; }
        public int priceHint { get; set; }
        public CurrentTradingPeriod currentTradingPeriod { get; set; }

        //public List<List<>> tradingPeriods { get; set; }
        public string dataGranularity { get; set; }

        public string range { get; set; }
        public List<string> validRanges { get; set; }
    }

    public class Quote
    {
        public List<int?> volume { get; set; }
        public List<double?> high { get; set; }
        public List<double?> low { get; set; }
        public List<double?> open { get; set; }
        public List<double?> close { get; set; }
    }

    public class Indicators
    {
        public List<Quote> quote { get; set; }
    }

    public class result
    {
        public meta meta { get; set; }
        public List<int> timestamp { get; set; }
        public Indicators indicators { get; set; }
    }

    public class chart
    {
        public List<result> result { get; set; }
        public object error { get; set; }
    }
}