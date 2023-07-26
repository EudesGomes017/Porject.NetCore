using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hvex.Exception.ExceptionBase {
    public class ErroValidatorException : HvexException {
        public List<string> MesssageError { get; set; }

        public ErroValidatorException(List<string> messsageError) {
            MesssageError = messsageError;
        }
    }
}
