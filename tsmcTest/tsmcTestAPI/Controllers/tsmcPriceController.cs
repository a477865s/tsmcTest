using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        private readonly static string ForexList = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";

        /// <summary>
        /// 取得所有的資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{inputPrice}")]
        public async Task<TsmcPriceViewModel> GetTsmcprice(string inputPrice)
        {
            List<JsonDataModel> result = JsonData();

            VerifyData(result);

            //取得最後一筆
            var lastData = result[result.Count - 1];

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
            var auth = CheckInputData(inputPrice);

            if (!auth)
            {
                throw new Exception("輸入錯誤請重新輸入");
            }

            //計算換算結果
            decimal answer = CalculateNTDPrice(inputPrice, lastData);

            return new TsmcPriceViewModel
            {
                Date = Convert.ToDateTime(InsertDate),
                USDNTD = lastData.USDNTD,
                RMBNTD = lastData.RMBNTD,
                EURUSD = lastData.EURUSD,
                USDJPY = lastData.USDJPY,
                ConvertResult = answer
            };
        }

        /// <summary>
        /// 檢查是否有成功取得資料
        /// </summary>
        /// <param name="result"></param>
        private static void VerifyData(List<JsonDataModel> result)
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
        private static decimal CalculateNTDPrice(string price, JsonDataModel abc)
        {
            return Decimal.Round(Convert.ToDecimal(price) / 5 * Convert.ToDecimal(abc.USDNTD), 2);
        }

        /// <summary>
        /// 轉換json
        /// </summary>
        /// <returns></returns>
        private static List<JsonDataModel> JsonData()
        {
            //tsm price https://query1.finance.yahoo.com/v8/finance/chart/tsm
            var client = new HttpClient();
            client.BaseAddress = new Uri(ForexList);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.GetAsync(ForexList).Result;
            var body = result.Content.ReadAsStringAsync().Result.Replace("/", "");
            if (string.IsNullOrEmpty(body))
            {
                throw new Exception("連線失敗");
            }
            //TODO:反序列化
            return JsonConvert.DeserializeObject<List<JsonDataModel>>(body);
        }
    }
}