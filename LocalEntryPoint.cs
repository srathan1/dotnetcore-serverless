using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.Json;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;

namespace dotnetcore_serverless
{
    /// <summary>
    /// This class extends from APIGatewayProxyFunction which contains the method FunctionHandlerAsync which is the 
    /// actual Lambda function entry point. The Lambda handler field should be set to
    /// It is now also the main entry point for the custom runtime.
    /// bingewatchers::bingewatchers.LambdaEntryPoint::FunctionHandlerAsync
    /// </summary>
    public class LocalEntryPoint
    {
        private static readonly LambdaEntryPoint LambdaEntryPoint = new LambdaEntryPoint();
        private static readonly Func<APIGatewayProxyRequest, ILambdaContext, Task<APIGatewayProxyResponse>> Func = LambdaEntryPoint.FunctionHandlerAsync;

        public static async Task Main(string[] args)
        {
#if DEBUG
            BuildWebHost(args).Run();
#else
            // Wrap the FunctionHandler method in a form that LambdaBootstrap can work with.
            using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(Func, new JsonSerializer()))

            // Instantiate a LambdaBootstrap and run it.
            // It will wait for invocations from AWS Lambda and call the handler function for each one.
            using (var bootstrap = new LambdaBootstrap(handlerWrapper))
            {
                await bootstrap.RunAsync();
            }
#endif
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}