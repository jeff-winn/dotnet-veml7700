using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp.Infrastructure.Extensions {
    static class SwaggerGenOptionsExtensions {
        public static void ScanForXmlComments(this SwaggerGenOptions opts, string pattern) {
            var files = Directory.EnumerateFiles(AppContext.BaseDirectory, pattern);

            foreach (var file in files) {
                opts.IncludeXmlComments(file, true);
            }
        }
    }
}