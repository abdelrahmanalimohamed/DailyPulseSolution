﻿namespace DailyPulse.Application.Extensions
{
	public static class RemoveWhiteSpace
	{
		public static string RemoveWhitespace(this string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
	}
}