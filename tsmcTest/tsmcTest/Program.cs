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
    class Program
    {
        private static string url = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<JsonDataModel> list = JsonData();
            Thread.Sleep(500);
            if (list != null)
            {
                Console.WriteLine("成功取得資料");
            }
            else
            {
                Console.WriteLine("取得資料失敗");
            }

            //取得最後一筆
            var lastData = list[list.Count - 1];
            while (true)
            {
                //手動輸入tsm價格
                Console.WriteLine("\n請輸入tsm價格");
                var inputTSMPrice = Console.ReadLine();

                //驗證
                var auth=CheckInputData(inputTSMPrice);
                if (!auth)
                {
                    Console.WriteLine("輸入錯誤請重新輸入");
                    continue;
                }
                //計算換算結果
                decimal answer = CalculateNTDPrice(inputTSMPrice, lastData);


                Console.WriteLine("計算完成\n");
                //價錢
                Console.WriteLine("參考日期：" + lastData.Date);
                Console.WriteLine("台幣匯率：" + lastData.USDNTD);
                Console.WriteLine("\n換算結果如下");

                //換算結果
                Console.WriteLine(answer);


            }

            
        }

        private static bool CheckInputData(string inputTSMPrice)
        {
            decimal input2;            
            var check = decimal.TryParse(inputTSMPrice, out input2);
            if (!check)
            {                
                return false;
            }

            return true;
            
        }

        private static decimal CalculateNTDPrice(string price, JsonDataModel abc)
        {
            //開始計算
            Console.WriteLine("計算中.....");
            Thread.Sleep(2000);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();

            }
            var answer = Decimal.Round(Convert.ToDecimal(price) / 5 * Convert.ToDecimal(abc.USDNTD), 2);
            return answer;
        }

        private static List<JsonDataModel> JsonData()
        {
            //tsm price https://query1.finance.yahoo.com/v8/finance/chart/tsm
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.GetAsync(url).Result;
            var body = result.Content.ReadAsStringAsync().Result.Replace("/", "");

            //TODO:反序列化
            var list = JsonConvert.DeserializeObject<List<JsonDataModel>>(body);
            return list;
        }
    }
}
