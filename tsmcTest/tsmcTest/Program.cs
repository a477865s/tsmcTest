using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using tsmcTest.DataModel;

namespace tsmcTest
{
    class Program
    {
        private static string url = "https://openapi.taifex.com.tw/v1/DailyForeignExchangeRates";
        static void Main(string[] args)
        {
            //tsm price https://query1.finance.yahoo.com/v8/finance/chart/tsm
            Console.WriteLine("Hello World!");
            var client = new HttpClient();
            client.BaseAddress=new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = client.GetAsync(url).Result;
            var body = result.Content.ReadAsStringAsync().Result.Replace("/","");

            //TODO:反序列化
            var list = JsonConvert.DeserializeObject<List<JsonDataModel>>(body);

            //手動輸入tsm價格
            Console.WriteLine("請輸入tsm價格");
            var price = Console.ReadLine();
            
            //取得最後一筆
            var abc = list[list.Count-1];

            //開始計算
            Console.WriteLine("計算中");
            var answer = Decimal.Round(Convert.ToDecimal(price) / 5 * Convert.ToDecimal(abc.USDNTD),2);

            //價錢
            Console.WriteLine("以日期："+ abc.Date);
            Console.WriteLine("匯率如下\n"+abc.USDNTD);
            Console.WriteLine("換算結果如下");

            //日期
            Console.WriteLine(answer);

            
            Console.ReadLine();
        }
    }
}
