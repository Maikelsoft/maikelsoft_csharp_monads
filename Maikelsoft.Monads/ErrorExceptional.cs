using System;

namespace Maikelsoft.Monads
{
	internal sealed class ErrorExceptional<T> : IExceptional<T>
		where T : IEquatable<T>
	{
		private readonly string _errorMessage;
		public T Value => throw new InvalidOperationException();
		public bool HasError => true;
		public string? ErrorMessage => _errorMessage;

		public ErrorExceptional(string errorMessage)
		{
			_errorMessage = errorMessage;
		}

		public override int GetHashCode()
		{
			return _errorMessage.GetHashCode();
		}

		public bool Equals(IExceptional<T>? other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _errorMessage.Equals(other.ErrorMessage, StringComparison.Ordinal);
		}
	}
}