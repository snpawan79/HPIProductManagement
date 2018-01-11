using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPI.BusinessServices
{
    public class ValidationModel
    {
        public ValidationStatus Status { get; set; }
        public string ValidationMessage { get; set; }
    }
    public enum ValidationStatus
    {
        OK = 1,
        ValidationError = 2
    }
}

