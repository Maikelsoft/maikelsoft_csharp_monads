using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public static class ExtendTry
    {
        [Pure]
        public static T? GetValueOrDefault<T>(this Try<T> optional)
            where T : struct
        {
            return optional.GetValueOrDefault(default);
        }
        
        [Pure]
        public static T? GetValueOrNull<T>(this Try<T> optional)
            where T : class
        {
            return optional.Match(_ => null, value => (T?)value);
        }
    }
}