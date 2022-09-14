using HelloDot.models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace HelloDot.Settings
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {


        private readonly ServiceSettings settings;

        public ExternalEndpointHealthCheck(IOptions<ServiceSettings> options)
        {
            settings = options.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {


            Ping ping = new(); //same as new Ping();
            var reply = await ping.SendPingAsync(settings.OpenWeatherHost);

            if (reply.Status != IPStatus.Success) //if it was a healthy result.
            {
                return HealthCheckResult.Unhealthy();
            }


            return HealthCheckResult.Healthy();




        }
    }
}
