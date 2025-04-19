using Microsoft.Data.SqlClient;
using sqlCopyExample.ConnectionFactory;
using sqlCopyExample.Interface;
using sqlCopyExample.Interface.IRepositories;
using sqlCopyExample.Interface.IService;
using sqlCopyExample.Repositories;
using sqlCopyExample.Service;

namespace WebApplication1
{
    public class Program
    {
        public static WebApplication CreateApp(string[] args)
        {
            var isTestMode = args.Contains("test");
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IProductModelRepository, ProductModelRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IDapperWarpper, DapperWarpper>();
            builder.Services.AddScoped<IDbConnectFactory, DbConnectionFactory>();
            builder.Services.AddScoped<ISqlBulkCopyFactory, SqlBulkCopyFactory>();
            builder.Services.AddScoped<Func<SqlConnection, ISqlBulkCopyWrapper>>(provider =>
    conn => new SqlBulkCopyWrapper(conn));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
        public static async Task Main(string[] args)
        {
            var app = CreateApp(args);

            if (!args.Contains("test"))
            {
                await app.RunAsync();
            }
        }
    }
}
