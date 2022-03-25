using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public static class ExtendIOptional
    {
        [Pure]
        public static T? GetValueOrDefault<T>(this IOptional<T> optional)
            where T : class
        {
            return optional.HasValue ? optional.Value : default;
        }

        [Pure]
        // ReSharper disable once ConvertNullableToShortForm
        public static Nullable<T> GetValueOrNull<T>(this IOptional<T> optional)
            where T : struct
        {
            return optional.HasValue ? optional.Value : null;
        }

        public static void DiposeValue<T>(this IOptional<T> optional)
            where T : IDisposable
        {
            if (optional.HasValue)
            {
                optional.Value.Dispose();
            }
        }
    }
}