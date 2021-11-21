using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsStockPriceRepository.Interface;
using UsStockPriceRepository.Model;

namespace UsStockPriceRepository.Implement
{
    public class UsStockRepository : IUsStockRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public UsStockRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public UsStock GetStock(string stockName)
        {
            throw new NotImplementedException();
        }

        public UsStock GetTsm()
        {
            throw new NotImplementedException();
        }
    }
}