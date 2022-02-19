using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService : Customers.CustomersBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            var model = new CustomerModel();

            if (request.UserId == 1)
            {
                model.FirstName = "Jamie";
                model.LastName = "Smiths";
            }
            else if (request.UserId == 2)
            {
                model.FirstName = "Jane";
                model.LastName = "Doe";
            }
            else
            {
                model.FirstName = "Arthas";
                model.LastName = "Menetil";
            }

            return Task.FromResult(model);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>()
            {
                new CustomerModel
                {
                    FirstName = "Cate",
                    LastName = "Blanchett",
                    EmailAddress = "cate@gmail.com",
                    Age = 52,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Cate",
                    LastName = "Blanchett",
                    EmailAddress = "cate@gmail.com",
                    Age = 52,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Cate",
                    LastName = "Blanchett",
                    EmailAddress = "cate@gmail.com",
                    Age = 52,
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}