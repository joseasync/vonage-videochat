using OpenTokSDK;
using Microsoft.Extensions.DependencyInjection;

namespace Letsgetchecked.VideoChat.Vonage.Registration;

public static class ServiceCollection
{
    public static IServiceCollection AddVonage(this IServiceCollection services,
        OpenTokOptions options)
        {
            int apiKey = 0;
            string apiSecret = null;
            
            
            try
            {
                apiKey = options.ApiKey;
                apiSecret = options.ApiSecret;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (apiKey == 0 || apiSecret == null)
                {
                    Console.WriteLine("The OpenTok API Key and API Secret were not set in the application configuration. ");
                }
            }
            var openTok = new OpenTok(apiKey, apiSecret);
            Dictionary<string, Session> appSessions = new ();

            services.AddSingleton(openTok);
            services.AddSingleton<Dictionary<string, Session>>(appSessions);
            return services;
        }
}