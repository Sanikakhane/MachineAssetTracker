using MachineAssetTracker.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MachineAssetTracker
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponce
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Something went wrong",
                ExceptionMessage = exception.Message
            };

            if (exception is FileNotFoundException)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Title = "File Not Found";
            }
            else if (exception is ArgumentException)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.Title = "Invalid Data";
            }

            httpContext.Response.StatusCode = response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
