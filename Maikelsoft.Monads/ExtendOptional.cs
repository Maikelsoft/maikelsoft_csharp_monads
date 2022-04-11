using System;
using System.Diagnostics.Contracts;

namespace Maikelsoft.Monads
{
    public static class ExtendOptional
    {
        [Pure]
        public static T? GetValueOrDefault<T>(this Optional<T> optional)
            //where T : notnull
            where T : struct
        {
            return optional.GetValueOrDefault(default);
            //return optional.Match(() => default, v => (T?)v);
        }
        
        //[Pure]
        //public static T? GetValueOrNull<T>(this Optional<T> optional)
        //    where T : struct
        //{
        //    return optional.Match(() => (T?)null, value => value);
        //}

        [Pure]
        public static T? GetValueOrNull<T>(this Optional<T> optional)
            where T : class
        {
            return optional.Match(() => default, value => (T?)value);
        }

        public static void DiposeValue<T>(this Optional<T> optional)
            where T : IDisposable
        {
            optional.Match(() => { }, value => value.Dispose());
        }
    }
}