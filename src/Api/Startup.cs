using Api.FluentValidation.ResponseFactories;
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
            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(setup =>
                {
                    setup.InvalidModelStateResponseFactory = ValidationErrorResponseFactory.Create;
                });

            services.AddTransient<StudentRepository>();
            services.AddTransient<CourseRepository>();
            services.AddTransient<StatesRepository>();
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
            services.AddFluentValidationAutoValidation(config =>
            {
                config.OverrideDefaultResultFactoryWith<CustomResultFactory>();
            });

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
