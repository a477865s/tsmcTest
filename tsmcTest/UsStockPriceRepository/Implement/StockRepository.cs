using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsStockPriceRepository.Interface;
using UsStockPriceRepository.Model;

namespace UsStockPriceRepository.Implement
{
    public class StockRepository : IStockRepository
    {
        public UsStock GetStock(string stockName)
        {
            throw new NotImplementedException();
        }
    }
}