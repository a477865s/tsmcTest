using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using tsmcTest.DataModel;
using tsmcTestAPI.Controllers.ViewModel;

namespace tsmcTestAPI.Controllers
{
    /// <summary>
    /// tsmc Get Price
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class tsmcPriceController : ControllerBase
    {
        private static readonly string ForexUrl = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";
        private static readonly string tsmPriceUrl = " https://query1.finance.yahoo.com/v8/finance/chart/tsm";

        /// <summary>
        /// 取得所有的資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<TsmcPriceViewModel> GetTsmcprice()
        {
            var lastData = JsonData();

            VerifyData(lastData);

            //日期格式為"20210606"，需求輸出為 2021-06-06
            //解法1
            var InsertDate = lastData.Date.Insert(4, "-").Insert(7, "-");
            //解法2
            var SubstringDate = lastData.Date.Substring(0, 4) + "-" + lastData.Date.Substring(4, 2) + "-" + lastData.Date.Substring(6, 2);

            //價錢
            //Console.WriteLine("參考日期　　　：" + lastData.Date.Insert(4, "-").Insert(7, "-") + "\n");
            //Console.WriteLine("台幣兌美金　　：" + lastData.USDNTD);
            //Console.WriteLine("人民幣兌台幣　：" + lastData.RMBNTD);
            //Console.WriteLine("歐元兌美金　　：" + lastData.EURUSD);
            //Console.WriteLine("美金兌日幣　　：" + lastData.USDJPY);

            //驗證
            //var auth = CheckInputData(inputPrice);

            //if (!auth)
            //{
            //    throw new Exception("輸入錯誤請重新輸入");
            //}

            //計算換算結果
            decimal answer = CalculateNTDPrice(lastData.AdrPrice, lastData);

            return new TsmcPriceViewModel
            {
                Date = Convert.ToDateTime(InsertDate),
                USDNTD = lastData.USDNTD,
                RMBNTD = lastData.RMBNTD,
                EURUSD = lastData.EURUSD,
                USDJPY = lastData.USDJPY,
                ConvertResult = answer,
                USstock = new UsStock
                {
                    stockName = lastData.StockName,
                    regularMarketPrice = lastData.AdrPrice
                }
            };
        }

        /// <summary>
        /// 檢查是否有成功取得資料
        /// </summary>
        /// <param name="result"></param>
        private static void VerifyData(JsonDataModel result)
        {
            while (true)
            {
                if (result != null)
                {
                    //Console.WriteLine("成功取得資料\n");
                    break;
                }
                else
                {
                    throw new Exception("連線失敗，請輸入 R 再次嘗試，或輸入任意鍵離開系統");
                    //var inputRetry = Console.ReadLine();

                    //if (inputRetry.ToUpper().Equals("R"))
                    //{
                    //    continue;
                    //}
                    //Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// 檢查輸入資料是否正確
        /// </summary>
        /// <param name="inputTSMPrice"></param>
        /// <returns></returns>
        private static bool CheckInputData(string inputTSMPrice)
        {
            var check = decimal.TryParse(inputTSMPrice, out _);
            if (!check)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 計算合理價值
        /// </summary>
        /// <param name="price"></param>
        /// <param name="abc"></param>
        /// <returns></returns>
        private static decimal CalculateNTDPrice(double price, JsonDataModel abc)
        {
            return Decimal.Round(Convert.ToDecimal(price) / 5 * Convert.ToDecimal(abc.USDNTD), 2);
        }

        /// <summary>
        /// 轉換json
        /// </summary>
        /// <returns></returns>
        private static JsonDataModel JsonData()
        {
            var forexClient = new HttpClient();
            var tsmCient = new HttpClient();
            var tsmResult = GetForexResult(tsmCient, tsmPriceUrl);

            var forexResult = GetForexResult(forexClient, ForexUrl).Replace("/", "");

            if (string.IsNullOrEmpty(forexResult) || string.IsNullOrEmpty(tsmResult))
            {
                throw new Exception("連線失敗");
            }

            var answer = JsonConvert.DeserializeObject<List<JsonDataModel>>(forexResult)[^1];

            var tsmPriceObject = JsonConvert.DeserializeObject<tsmcTest.DataModel.UsStock>(tsmResult);

            answer.AdrPrice = tsmPriceObject.chart.result.result[0].meta.regularMarketPrice;
            answer.StockName = tsmPriceObject.result[0].meta.symbol;

            //TODO:反序列化
            return answer;
        }

        private static string GetForexResult(HttpClient forexClient, string url)
        {
            forexClient.BaseAddress = new Uri(url);
            forexClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = forexClient.GetAsync(url).Result;
            return result.Content.ReadAsStringAsync().Result;
        }
    }
}