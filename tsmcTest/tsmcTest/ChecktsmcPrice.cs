using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using tsmcTest.DataModel;

namespace tsmcTest
{
    class ChecktsmcPrice
    {
        //期交所各式資料來源網站        
        //private static string DataSource = "https://openapi.taifex.com.tw/";
        private static string ForexList = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<JsonDataModel> result = JsonData();
            while (true)
            {
                if (result != null)
                {
                    Console.WriteLine("成功取得資料\n");
                    break;
                }
                else
                {
                    Console.WriteLine("連線失敗，請輸入 R 再次嘗試，或輸入任意鍵離開系統");
                    var inputRetry = Console.ReadLine();

                    if (inputRetry.ToUpper().Equals("R"))
                    {
                        continue;
                    }
                    Environment.Exit(0);
                }
            }
            

            

            //取得最後一筆
            var lastData = result[result.Count - 1];




            //日期格式為"20210606"，需求輸出為 2021-06-06
            //解法1
            var InsertDate = lastData.Date.Insert(4, "-").Insert(7, "-");
            //解法2
            var SubstringDate = lastData.Date.Substring(0, 4) + "-" + lastData.Date.Substring(4, 2) + "-" + lastData.Date.Substring(6, 2);



            //價錢
            Console.WriteLine("參考日期　　　：" + lastData.Date.Insert(4, "-").Insert(7, "-")+"\n");
            Console.WriteLine("台幣兌美金　　：" + lastData.USDNTD);
            Console.WriteLine("人民幣兌台幣　：" + lastData.RMBNTD);
            Console.WriteLine("歐元兌美金　　：" + lastData.EURUSD);
            Console.WriteLine("美金兌日幣　　：" + lastData.USDJPY);
            
            while (true)
            {
                //手動輸入tsm價格
                Console.WriteLine("\n請輸入tsm價格，或輸入 'E'  離開系統\n");
                var inputTSMPrice = Console.ReadLine();

                if (inputTSMPrice.ToUpper().Equals("E"))
                {
                    Console.WriteLine("離開系統~~~~~掰掰");
                    Thread.Sleep(1000);

                    break;
                }
                //驗證
                var auth=CheckInputData(inputTSMPrice);


                if (!auth)
                {
                    Console.WriteLine("輸入錯誤請重新輸入");
                    continue;
                }

                //計算換算結果
                decimal answer = CalculateNTDPrice(inputTSMPrice, lastData);             



                Console.WriteLine("\n換算結果如下\n");

                //換算結果
                Console.WriteLine(answer);
                Console.WriteLine("***********************************************************************");

            }

            
        }

        private static bool CheckInputData(string inputTSMPrice)
        {
            var check = decimal.TryParse(inputTSMPrice, out _);
            if (!check)
            {                
                return false;
            }

            return true;
            
        }

        private static decimal CalculateNTDPrice(string price, JsonDataModel abc)
        {
            //開始計算
            Console.WriteLine("計算中.....\n");
            
            var answer = Decimal.Round(Convert.ToDecimal(price) / 5 * Convert.ToDecimal(abc.USDNTD), 2);
            return answer;
        }

        private static List<JsonDataModel> JsonData()
        {
            //tsm price https://query1.finance.yahoo.com/v8/finance/chart/tsm
            Console.WriteLine("連線中..........");
            var client = new HttpClient();
            client.BaseAddress = new Uri(ForexList);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.GetAsync(ForexList).Result;
            var body = result.Content.ReadAsStringAsync().Result.Replace("/", "");
            if (string.IsNullOrEmpty(body))
            {
                Console.WriteLine("連線失敗");
            }
            //TODO:反序列化
            var list = JsonConvert.DeserializeObject<List<JsonDataModel>>(body);
            return list;
        }
    }
}
