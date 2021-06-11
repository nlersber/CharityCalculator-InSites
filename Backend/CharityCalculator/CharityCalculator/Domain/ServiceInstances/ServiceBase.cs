using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CharityCalculator.Data;

namespace CharityCalculator.Domain.ServiceInstances
{
    public class ServiceBase
    {
        protected readonly Context context;

        public ServiceBase(Context context)
        {
            this.context = context;
        }
    }
}
