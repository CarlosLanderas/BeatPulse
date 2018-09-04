using BeatPulse.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Net;

namespace BeatPulse
{
    class BeatPulseMetadataMiddleware
    {
        private readonly BeatPulseOptions _options;

        public BeatPulseMetadataMiddleware(RequestDelegate next, BeatPulseOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        public async Task Invoke(HttpContext context, BeatPulseContext beatpulseContext)
        {
            var registeredLiveness = beatpulseContext.GetAllLiveness();
            context.Response.ContentType = "application/json";

            var metadata = new BeatPulseMetadata
            {
                BeatPulsePath = _options.BeatPulsePath,
                Livenesses = registeredLiveness
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(metadata));
        }
    }
}
