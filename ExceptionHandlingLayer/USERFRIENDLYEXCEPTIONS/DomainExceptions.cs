using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandlingLayer.USERFRIENDLYEXCEPTIONS
{
    public class DomainExceptions : Exception
    {

        public DomainExceptions(string message) : base(message)
        {

        }
    }

    public class NotFoundException : Exception
    {

        public NotFoundException(string message) : base(message)
        {

        }
    }    
    
    public class DuplicateRecordExceptionn : Exception
    {

        public DuplicateRecordExceptionn(string message) : base(message)
        {

        }
    }
}
