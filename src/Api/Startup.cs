using Api.FluentValidation;
using Api.FluentValidation.Validators;
using Api.Repositories;
using Api.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<StudentRepository>();
            services.AddTransient<CourseRepository>();
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddFluentValidationAutoValidation();

            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
