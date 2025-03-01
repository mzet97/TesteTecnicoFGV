﻿using Elastic.Channels;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Serilog;

namespace DesafioFGV.API.Configuration;

public static class SerilogConfig
{
    public static void ConfigureSerilog(this IHostBuilder builder, IConfiguration configuration)
    {
        var elasticUri = configuration["ElasticSettings:Uri"]
                      ?? "http://localhost:9200";
        var elasticUsername = configuration["ElasticSettings:Username"]
                      ?? "elastic";
        var elasticPassword = configuration["ElasticSettings:Password"]
                      ?? "changeme";

        builder.UseSerilog((ctx, loggerConfig) =>
        {
            loggerConfig
                .ReadFrom.Configuration(ctx.Configuration)
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Information)
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                .WriteTo.Elasticsearch(new[] { new Uri(elasticUri) }, opts =>
                {
                    opts.DataStream = new DataStreamName("logs", "DesafioFGV", "all");
                    opts.BootstrapMethod = BootstrapMethod.Failure;
                    opts.ConfigureChannel = channelOpts =>
                    {
                        channelOpts.BufferOptions = new BufferOptions();
                    };
                }, transport =>
                {
                    transport.Authentication(new BasicAuthentication(elasticUsername, elasticPassword));
                });
        });
    }
}
