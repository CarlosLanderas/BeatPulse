﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;

namespace BeatPulse
{
    class BeatPulseFilter
        : IStartupFilter
    {
        private readonly BeatPulseOptions _options;

        public BeatPulseFilter(BeatPulseOptions options)
        {
            _options = options;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.MapWhen(context => context.IsBeatPulseRequest(_options), appBuilder =>
                {
                    appBuilder.UseMiddleware<BeatPulseMiddleware>(_options);
                });

                if (_options.EnableMetadata)
                {
                    builder.Map($"/{BeatPulseKeys.BEATPULSE_METADATA_ENDPOINT}", appBuilder =>
                    {
                        appBuilder.UseMiddleware<BeatPulseMetadataMiddleware>(_options);
                    });
                }

                next(builder);
            };
        }
    }
}
