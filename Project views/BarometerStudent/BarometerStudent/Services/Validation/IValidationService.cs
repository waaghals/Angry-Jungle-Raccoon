using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarometerDomain.Repositories;

namespace BarometerStudent.Services.Validation
{
    interface IValidationService<T>
    {
        IRepository<T> Repository { get; set; }
        bool Validate(T item);
    }
}
