using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_4sem
{
    public class BindingException : Exception
    {
        public BindingException(string field, string? message) : base(message)
        {
            ErrorField = field;
        }

        public string ErrorField { get; set; }
    }
}
