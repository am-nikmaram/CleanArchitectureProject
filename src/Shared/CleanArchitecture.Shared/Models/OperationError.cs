using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Shared.Models
{
    public class OperationError(string message)
    {
        public string Message { get; set; } = message;
    }
}
