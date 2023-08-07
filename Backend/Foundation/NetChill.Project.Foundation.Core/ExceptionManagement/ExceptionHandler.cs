
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetChil.Project.Foundation.Core.ExceptionManagement
{
    public class ExceptionHandler : IExceptionHandler
    {

        public Exception Process(Exception exception)
        {
            throw exception;
        }
    }
}
