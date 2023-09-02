using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
public class OperationNotAllowedExceptionFilter : IActionFilter
{
  // This method is called before the action method is invoked.
  public void OnActionExecuting(ActionExecutingContext context) { }

  // This method is called after the action method is invoked.
  public void OnActionExecuted(ActionExecutedContext context)
  {
    // If the exception is of type OperationNotAllowedException, then set the result to an ObjectResult with the exception message and status code.
    if (context.Exception is OperationNotAllowedException httpResponseException)
    {
      // Set the result to an ObjectResult with the exception message and status code
      context.Result = new ObjectResult(httpResponseException.Message)
      {
        StatusCode = httpResponseException.StatusCode
      };

      // Set the exception as handled
      context.ExceptionHandled = true;
    }
  }
}