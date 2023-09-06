//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using pkuBite.Models.Models;

//namespace pkuBite.Filters
//{
//	public class ModelValidationFilter : ActionFilterAttribute
//    {
//        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//        {
//            if (context.ModelState.IsValid)
//            {
//                await next();
//            }
//            else
//            {
//                var response = new
//                {
//                    Message = "User logged in succesfully",
//                };
//                context.Result =  new ObjectResult(context.ModelState. );
//                    //JsonResult(ResponseHelper.CreateValidationErrorResponse(context.ModelState.AllErrors().Select(r => r.Reason).ToList()));
//            }
//        }

//    }
//}

