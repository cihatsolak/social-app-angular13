namespace ServerApp.Helpers
{
    public class LastActiveActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            int id = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var socialRepository = resultContext.HttpContext.RequestServices.GetRequiredService<ISocialRepository>();

            var user = await socialRepository.GetUserByIdAsync(id);
            user.LastActive = DateTime.Now;

            socialRepository.Update(user);
        }
    }
}
