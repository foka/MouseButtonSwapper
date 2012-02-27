using System;

namespace MouseButtonsSwapper.Tools
{
	public static class EnumExtenstion
	{
		public static bool HasFlag(this Enum variable, Enum value)
		{
			if (variable == null)
				return false;

			if (value == null)
				throw new ArgumentNullException("value");

			if (!Enum.IsDefined(variable.GetType(), value))
			{
				throw new ArgumentException(string.Format(
					"Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.",
					value.GetType(), variable.GetType()));
			}

			ulong num = Convert.ToUInt64(value);
			return ((Convert.ToUInt64(variable) & num) == num);
		}
	}
}