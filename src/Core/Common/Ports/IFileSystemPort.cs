﻿using Core.Common.Ports.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Ports;
public interface IFileSystemPort
{
    public Task<Stream> LoadFile(string fileName);
    public Task SaveFile(string fileName, Stream stream);
}

internal static class FileSystemPortExtensions
{
    public static IServiceCollection AddFileStorage(this IServiceCollection services)
    {
        return services.AddScoped<IFileSystemPort, FileSystemAdapter>();
    }
}
