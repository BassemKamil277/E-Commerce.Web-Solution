using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {
        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }

        public string Code { get;}

        public string Description { get;}

        public ErrorType ErrorType { get;}

        public static Error Failure(string code = "General.Failure", string description = "General Failure Has Occured")
        {
            return new Error(code, description, ErrorType.Failure);
        }
        public static Error Validation(string code = "General.Validation", string description = "Validation Error Has Occured")
        {
            return new Error(code, description, ErrorType.Validation);
        }

        public static Error NotFound(string code = "General.NotFound", string description = "The Request Resource Has Not Found")
        {
            return new Error(code, description, ErrorType.NotFound);
        }

        public static Error Unauthrized(string code = "General.Unauthrized", string description = "You Are Not Authrized")
        {
            return new Error(code, description, ErrorType.Unauthrized);
        }

        public static Error forbidden(string code = "General.forbidden", string description = "You Do Not Have Permession")
        {
            return new Error(code, description, ErrorType.forbidden);
        }

        public static Error InvaidCredentials(string code = "General.InvaidCredentials", string description = "The Provided Credentials Not Valid")
        {
            return new Error(code, description, ErrorType.InvaidCredentials);
        }

      
    }
}
