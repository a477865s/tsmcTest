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
        Task<UsStock> GetStock(string stockName);

        Task<UsStock> GetTsm();
    }
}