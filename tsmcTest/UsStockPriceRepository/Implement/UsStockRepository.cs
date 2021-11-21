using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UsStockPriceRepository.Interface;
using UsStockPriceRepository.Model;

namespace UsStockPriceRepository.Implement
{
    /// <summary>
    /// 取得美股
    /// </summary>
    public class UsStockRepository : IUsStockRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string UsStockUrl = "https://query1.finance.yahoo.com/v8/finance/chart/";

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public UsStockRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 取得美股個股資料
        /// </summary>
        /// <param name="stockName">美股名稱</param>
        /// <returns></returns>
        public async Task<UsStock> GetStock(string stockName)
        {
            var requestUri = UsStockUrl + stockName;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取得台積電資料TSM ADR股價
        /// </summary>
        /// <returns></returns>
        public async Task<UsStock> GetTsm()
        {
            var tsmUri = UsStockUrl + "tsm";
            var tsmClient = this._httpClientFactory.CreateClient();
            tsmClient.BaseAddress = new Uri(tsmUri);
            tsmClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await (await tsmClient.GetAsync(tsmUri)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UsStock>(result);
        }
    }
}