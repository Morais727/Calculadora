using System.Collections.Generic;

namespace Trabalho_final.Controller
{
    public static class OpPrecedence
    {
        private static readonly IDictionary<char, int> OperationPriority = new Dictionary<char, int>
            {
               { '√', 4 },
               { '^', 4 },
               { '*', 3 },
               { '/', 3 },               
               { '+', 2 },
               { '-', 2 },
               { '(', 1 },
               { ')', 1 }
            };

        public static bool IsPrecided(char operationA, char operationB)
        {
            return OperationPriority[operationA] <= OperationPriority[operationB];
        }
    }
}
