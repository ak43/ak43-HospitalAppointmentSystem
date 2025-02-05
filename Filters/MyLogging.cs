using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalAppointmentSystem.Filters
{
    public class MyLogging : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Filter executed before");        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Filter executed after");        }
    }
}
