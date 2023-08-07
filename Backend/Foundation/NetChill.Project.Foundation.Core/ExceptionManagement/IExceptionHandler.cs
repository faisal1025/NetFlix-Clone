using System;
using System.Collections.Generic;
using System.Text;

namespace NetChil.Project.Foundation.Core.ExceptionManagement
{
    public interface IExceptionHandler
    {
        Exception Process(Exception exception);
    }
}
