using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarometerStudent.Services.Validation
{
    interface IValidationDictionary
    {
        Boolean IsValid { get; set; }
        void AddError(string subject, string message);
    }
}
