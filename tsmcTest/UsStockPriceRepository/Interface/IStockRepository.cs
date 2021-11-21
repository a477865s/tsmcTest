using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsStockPriceRepository.Model;

namespace UsStockPriceRepository.Interface
{
    public interface IStockRepository
    {
        UsStock GetStock(string stockName);
    }
}