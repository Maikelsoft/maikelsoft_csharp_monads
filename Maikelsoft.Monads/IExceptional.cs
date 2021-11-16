using System;

namespace Maikelsoft.Monads
{
	public interface IExceptional<T> : IEquatable<IExceptional<T>> 
		where T : IEquatable<T>
	{
		/// <summary>
		/// 
		/// </summary>
		T Value { get; }

		/// <summary>
		/// 
		/// </summary>
		bool HasError { get; }

		/// <summary>
		/// 
		/// </summary>
		string? ErrorMessage { get; }
	}
}