using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Model
{
    public enum TypeFinance
    {
        Profit, 
        Debt
    }
    public class Finance
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public TypeFinance Type { get; set; }
        public decimal Quantity { get; set; }
        public bool Status { get; set; } = true;
        public User User { get; set; } = null!;
    }
}
