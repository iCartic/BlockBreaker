﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SkillzSDK
{
	namespace Extensions {

		/// <summary>
		/// Skillz helper extensions. Internal only, do not rely on for game code
		/// </summary>
		public static class SkillzExtensions
		{
			public static bool? SafeGetBoolValue(
					this Dictionary<string, object> dict,
					string key,
					string trueStr = "True",
					string falseStr = "False")
			{
				string val = dict.SafeGetStringValue(key);
				return Helpers.SafeBoolParse(val, trueStr, falseStr);
			}

			public static double? SafeGetDoubleValue(
					this Dictionary<string, object> dict,
					string key)
			{
				string val = dict.SafeGetStringValue(key);
				return Helpers.SafeDoubleParse(val);
			}

			public static DateTime? SafeGetUnixDateTimeValue(
					this Dictionary<string, object> dict,
					string key)
			{
				double? val = dict.SafeGetDoubleValue(key);
				return Helpers.SafeParseUnixTime(val);
			}

			public static int? SafeGetIntValue(
					this Dictionary<string, object> dict,
					string key)
			{
				string val = dict.SafeGetStringValue(key);
				return Helpers.SafeIntParse(val);
			}

			public static uint? SafeGetUintValue(
					this Dictionary<string, object> dict,
					string key)
			{
				string val = dict.SafeGetStringValue(key);
				return Helpers.SafeUintParse(val);
			}

			public static string SafeGetStringValue(
					this Dictionary<string, object> dict,
					string key)
			{
				object val = dict.SafeGetValue(key);
				return val != null ? val.ToString() : null;
			}

			public static object SafeGetValue(
					this Dictionary<string, object> dict,
					string key)
			{
				return dict.ContainsKey(key) ? dict[key] : null;
			}

			// put specific type parsing in here?
		}
	} // namespace Extensions

	/// <summary>
	/// Skillz helper methods. Internal only, do not rely on for game code
	/// </summary>
	public static class Helpers
	{
		public static bool? SafeBoolParse(
				string str, 
				string trueStr = "True",
				string falseStr = "False")
		{
			if (str == trueStr)
			{
				return true;
			}
			if (str == falseStr)
			{
				return false;
			}
			return null;
		}

		public static DateTime? SafeParseUnixTime(double? unixTime)
		{
			if (unixTime == null)
			{
				return null;
			}

			try 
			{
				DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
				return epoch.AddSeconds((double) unixTime);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static double? SafeDoubleParse(string str)
		{
			double result;
			bool success = double.TryParse(str, out result);
			return success ? result : (double?) null;
		}

		public static int? SafeIntParse(string str)
		{
			int result;
			bool success = int.TryParse(str, out result);
			return success ? result : (int?) null;
		}

		public static uint? SafeUintParse(string str)
		{
			uint result;
			bool success = uint.TryParse(str, out result);
			return success ? result : (uint?) null;
		}
	}
}
