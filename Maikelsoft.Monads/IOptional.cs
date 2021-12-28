namespace Maikelsoft.Monads
{
    public interface IOptional<T> where T : notnull
    {
        /// <summary>
        /// 
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// 
        /// </summary>
        T Value { get; }

        T GetValueOrDefault(T defaultValue);
    }
}