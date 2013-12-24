using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BarometerDomain.Repositories;

namespace BarometerStudent.Services.Validation
{
    public class EvaluationValidator : IValidationService<Evaluation>
    {
        public IRepository<Evaluation> Repository
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Validate(Evaluation item)
        {
            throw new NotImplementedException();
        }
    }
}