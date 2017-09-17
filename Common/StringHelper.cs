using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions; 


namespace HarrierGroup.Common
{
	public partial class StringHelper
	{
		public static List<string> StringToList(string s, bool trimStrings = false, string separator = ",")
		{
			var list = new List<string>(s.Split(new[] { separator }, StringSplitOptions.None));

			if (trimStrings)
				TrimAll(list);

			return list;
		}


		public static List<int> StringToIntegerList(string s, string separator = ",")
		{
			var list = new List<int>();

			foreach (var numberString in StringToList(s))
				list.Add(ParseInt(numberString));

			return list;
		}

        
		/// <summary>
		/// Converts a string to an integer number without any chance of failing.
		/// It is indestructible (good for converting QueryString arguments).
		/// </summary>
		/// <param name="number">Non-digit characters are ignored.</param>
		/// <returns>The converted number.</returns>
		public static int ParseInt(string number)
		{
			return ParseInt(number, 0);
		}


		public static int ParseInt(string number, int defaultValue)
		{
			if (string.IsNullOrEmpty(number))
				return defaultValue;

			number = DigitsOnly(number, "-");
			if (number.Length == 0)
				return 0;

			var value = 0;

			try
			{
				value = Int32.Parse(number);
			}
			catch (OverflowException)
			{
			}
			catch (FormatException)
			{
			}

			return value;
		}

		public static void TrimAll(List<string> list)
		{
			for (var i = 0; i < list.Count; ++i)
				list[i] = list[i].Trim();
		}

		

		public static string DigitsOnly(string number)
		{
			return DigitsOnly(number, string.Empty);
		}


		/// <summary>
		/// DigitsOnly returns the digits (0-9) found in the input string.
		/// </summary>
		/// <param name="number">Non-digit characters are ignored.</param>
		/// <param name="exceptions">
		/// Characters in this string will be returned in addition to any digits found.
		/// </param>
		/// <returns>
		/// The digits found in the input string, in addition to any 
		/// characters in the second param.
		/// </returns>
		public static string DigitsOnly(string number, string exceptions)
		{
			if (number == null)
				return string.Empty;

			var s = new StringBuilder();

			foreach (var c in number)
			{
				if ((c >= '0' && c <= '9') || exceptions.IndexOf(c) != -1)
					s.Append(c);
			}

			return s.ToString();
		}

    }
}