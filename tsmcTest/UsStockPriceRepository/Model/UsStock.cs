using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsStockPriceRepository.Model
{
    public class UsStock
    {
        private chart chart { get; set; }
    }

    internal class Pre
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    internal class Regular
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    internal class Post
    {
        public string timezone { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int gmtoffset { get; set; }
    }

    internal class CurrentTradingPeriod
    {
        public Pre pre { get; set; }
        public Regular regular { get; set; }
        public Post post { get; set; }
    }

    internal class meta
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

    internal class Quote
    {
        public List<int?> volume { get; set; }
        public List<double?> high { get; set; }
        public List<double?> low { get; set; }
        public List<double?> open { get; set; }
        public List<double?> close { get; set; }
    }

    internal class Indicators
    {
        public List<Quote> quote { get; set; }
    }

    internal class result
    {
        public meta meta { get; set; }
        public List<int> timestamp { get; set; }
        public Indicators indicators { get; set; }
    }

    internal class chart
    {
        public List<result> result { get; set; }
        public object error { get; set; }
    }
}