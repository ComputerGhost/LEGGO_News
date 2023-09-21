using DataAccess.Repositories.Interfaces;
using System.Collections.Concurrent;
using System.Reflection;

namespace DataAccess.Repositories.Extensions
{
    internal static class RepositoryExtensions
    {
        private static readonly ConcurrentDictionary<Type, List<string>> _paramNamesCache = new();

        public static IEnumerable<string> GetParameters<T>(this IRepository _)
        {
            var type = typeof(T);
            if (!_paramNamesCache.TryGetValue(type, out var paramNames))
            {
                var properties = type
                    .GetProperties(BindingFlags.Public)
                    .Where(p => p.GetGetMethod(false) != null);
                paramNames = properties
                    .Select(p => p.Name)
                    .ToList();
                _paramNamesCache[type] = paramNames;
            }
            return paramNames;
        }
    }
}
