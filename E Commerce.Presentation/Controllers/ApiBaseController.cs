using E_Commerce.Shared.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ApiBaseController : ControllerBase
    {
        // Handle result without value 
        // if succuss return noContent 204 
        // if failure return problem with status code and details 
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else 
              return HandleProblem(result.Errors);
        }


        // Handle result with value 
        // if succuss return Ok 200 with value 
        // if failure return problem with status code and details 
        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
              return HandleProblem(result.Errors);
        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            // If no errors are provided , return 500 error 
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An UnExpected Error Occured");

            // If there is inly one error , Handle it as a single error problem 
            // If All errors are validation error , Handle the validation problem 
            if(errors.All(e=> e.ErrorType == ErrorType.Validation))
                return HandleValidationProblem(errors);
            return HandleSingleErrorProblem(errors[0]);
        }
        private ActionResult HandleSingleErrorProblem(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.ErrorType.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.ErrorType)
                );
        }
        private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthrized => StatusCodes.Status401Unauthorized,
            ErrorType.forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.InvaidCredentials => StatusCodes.Status401Unauthorized,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        }; 

        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var ModelState = new ModelStateDictionary();
            foreach (var error in errors)
                ModelState.AddModelError(error.Code , error.Description);
            return ValidationProblem(ModelState);  
        }
    }
}
