using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsStockPriceRepository.Model;

namespace UsStockPriceRepository.Interface
{
    public interface IUsStockRepository
    {
        UsStock GetStock(string stockName);

        UsStock GetTsm();
    }
}