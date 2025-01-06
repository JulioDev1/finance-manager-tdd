using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Shared.Dtos
{
    public enum FinanceType
    {
        Profit,
        Debt
    }
    public class CreateFinanceDto
    {
        public string Name { get; set; } = null!;
        public FinanceType Type { get; set; }
        public decimal Quantity { get; set; }
        public bool Status { get; set; } = true;
        public int UserId { get; set; }
    }
}
