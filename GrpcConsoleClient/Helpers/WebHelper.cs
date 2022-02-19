using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using static GrpcServer.Customers;
using static GrpcServer.Greeter;

namespace GrpcConsoleClient.Helpers
{
    public static class WebHelper
    {
        /// <summary>
        /// GreeterService Client
        /// </summary>
        public static GreeterClient GetGreeterClient(IConfigurationRoot config)
        {
            var grpcServerUrl = config.GetRequiredSection("Settings").GetSection("GrpcServerUrl").Value;

            var channel = GrpcChannel.ForAddress(grpcServerUrl);

            var client = new GreeterClient(channel);

            return client;
        }

        /// <summary>
        /// CustomerService Client
        /// </summary>
        public static CustomersClient GetCustomersClient(IConfigurationRoot config)
        {
            var grpcServerUrl = config.GetRequiredSection("Settings").GetSection("GrpcServerUrl").Value;

            var channel = GrpcChannel.ForAddress(grpcServerUrl);

            var client = new CustomersClient(channel);

            return client;
        }
    }
}