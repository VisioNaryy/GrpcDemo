using Grpc.Core;
using GrpcConsoleClient.Helpers;
using GrpcServer;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GrpcConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", false)
                        .AddEnvironmentVariables()
                        .Build();

            Console.WriteLine("Communicating with GreeterService");

            var greeterClient = WebHelper.GetGreeterClient(config);

            var greeterRequest = new HelloRequest { Name = "Tim" };

            var greeterResponse = await greeterClient.SayHelloAsync(greeterRequest);

            Console.WriteLine("Communicating with CustomerService");

            var customersClient = WebHelper.GetCustomersClient(config);

            var customersRequest = new CustomerLookupModel { UserId = 1 };

            var customersResponse = await customersClient.GetCustomerInfoAsync(customersRequest);

            Console.WriteLine($"{customersResponse.FirstName} : {customersResponse.LastName}");

            using (var client = customersClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while (await client.ResponseStream.MoveNext())
                {
                    var newCustomer = client.ResponseStream.Current;

                    Console.WriteLine($"{newCustomer.FirstName} {newCustomer.LastName} {newCustomer.Age} {newCustomer.EmailAddress}");
                }
            }         

            Console.ReadKey();
        }
    }
}