using FinanceManager.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Application.Dto_s
{
    public class FinanceDto
    {
        public string Name { get; set; } = null!;
        public TypeFinance Type { get; set; }
        public decimal Quantity { get; set; }
        public bool Status { get; set; } = true;
        public int userId { get; set; }
    }
}
