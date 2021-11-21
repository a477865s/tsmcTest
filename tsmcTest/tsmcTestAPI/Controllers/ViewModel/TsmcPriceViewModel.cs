using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tsmcTestAPI.Controllers.ViewModel
{
    public class TsmcPriceViewModel
    {
        public DateTime Date { get; set; }
        public string USDNTD { get; set; }
        public string RMBNTD { get; set; }
        public string EURUSD { get; set; }
        public string USDJPY { get; set; }
        public decimal ConvertResult { get; set; }
        public UsStock USstock { get; set; }
    }

    public class UsStock
    {
        public string stockName { get; set; }
        public double regularMarketPrice { get; set; }
    }
}