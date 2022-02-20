namespace Maikelsoft.Monads
{
    public interface IOptional<T> where T : notnull
    {
        bool HasValue { get; }
        T Value { get; }
        T GetValueOrDefault(T defaultValue);
    }
}