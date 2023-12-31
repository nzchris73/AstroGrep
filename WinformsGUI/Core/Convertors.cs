﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AstroGrep.Common;

namespace AstroGrep.Core
{
	/// <summary>
	/// Contains common methods to convert to string/from string.
	/// </summary>
	/// <remarks>
	///   AstroGrep File Searching Utility. Written by Theodore L. Ward
	///   Copyright (C) 2002 AstroComma Incorporated.
	///
	///   This program is free software; you can redistribute it and/or
	///   modify it under the terms of the GNU General public License
	///   as published by the Free Software Foundation; either version 2
	///   of the License, or (at your option) any later version.
	///
	///   This program is distributed in the hope that it will be useful,
	///   but WITHOUT ANY WARRANTY; without even the implied warranty of
	///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	///   GNU General public License for more details.
	///
	///   You should have received a copy of the GNU General public License
	///   along with this program; if not, write to the Free Software
	///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
	///
	///   The author may be contacted at:
	///   ted@astrocomma.com or curtismbeard@gmail.com
	/// </remarks>
	/// <history>
	/// [Curtis_Beard]      11/07/2012  Initial
	/// </history>
	public static class Convertors
	{
		/// <summary>
		/// Calculates the width of the drop down list of the given combo box
		/// </summary>
		/// <param name="combo">Combo box to base calculate from</param>
		/// <param name="maxWidth">The maximum width of the drop down (defaults to 600)</param>
		/// <returns>Width of longest string in combo box items</returns>
		/// <history>
		/// [Curtis_Beard]    11/21/2005	Created
		/// [Curtis_Beard]    09/16/2019	CHG: rework to have a max and support extended combobox
		/// [Curtis_Beard]    03/05/2020	CHG: add maxWidth parameter to be able to override
		/// </history>
		public static int CalculateDropDownWidth(ComboBox combo, int maxWidth = 600)
		{
			maxWidth += SystemInformation.VerticalScrollBarWidth;
			int defaultWidth = combo.Width + SystemInformation.VerticalScrollBarWidth;
			int calculatedWidth = defaultWidth;
			bool isComboBoxEx = combo is AstroGrep.Windows.Controls.ComboBoxEx;

			using (Graphics g = combo.CreateGraphics())
			{
				string _itemValue = string.Empty;
				SizeF _size;

				foreach (object _item in combo.Items)
				{
					_itemValue = _item.ToString();
					if (isComboBoxEx)
					{
						_itemValue = (_item as AstroGrep.Windows.ComboBoxExEntry).Display;
					}

					_size = g.MeasureString(_itemValue, combo.Font);

					if (_size.Width > calculatedWidth)
						calculatedWidth = Convert.ToInt32(_size.Width);
				}

				// keep original width if no item longer
				if (calculatedWidth != defaultWidth)
					calculatedWidth += SystemInformation.VerticalScrollBarWidth;
			}

			return Math.Min(calculatedWidth, maxWidth);
		}

		/// <summary>
		/// Converts a Color to a string.
		/// </summary>
		/// <param name="color">Color</param>
		/// <returns>color values as a string</returns>
		/// <history>
		/// [Curtis_Beard]		11/03/2006	Created
		/// </history>
		public static string ConvertColorToString(System.Drawing.Color color)
		{
			return string.Format("{0}{4}{1}{4}{2}{4}{3}", color.R.ToString(), color.G.ToString(), color.B.ToString(), color.A.ToString(), Constants.COLOR_SEPARATOR);
		}

		/// <summary>
		/// Converts given file size in bytes as string to the display type as a string.
		/// </summary>
		/// <param name="bytes">File size in bytes</param>
		/// <param name="displayType">byte,kb,mb,gb</param>
		/// <returns>file size in given display type</returns>
		/// <history>
		/// [Curtis_Beard]        11/17/2014  Initial
		/// [Curtis_Beard]        03/05/2020  CHG: .Net 4.5 improvements
		/// </history>
		public static string ConvertFileSizeForDisplay(string bytes, string displayType)
		{
			// convert bytes value to selected display size
			long value = long.Parse(bytes);
			switch (displayType.ToLower())
			{
				case "byte":
					break;

				case "kb":
					value /= 1024;
					break;

				case "mb":
					value /= (1024 * 1024);
					break;

				case "gb":
					value /= (1024 * 1024 * 1024);
					break;
			}

			return value.ToString();
		}

		/// <summary>
		/// Converts given file size to long for use in comparison of file sizes.
		/// </summary>
		/// <param name="textValue">TextBox value entered by user</param>
		/// <param name="sizeType">The selected size type (byte,kb,mb,gb)</param>
		/// <param name="defaultValue">The default value</param>
		/// <returns>long representing number of bytes user selected</returns>
		/// <history>
		/// [Curtis_Beard]        02/09/2012  ADD: 3424156, size drop down selection
		/// [Curtis_Beard]        03/05/2020  CHG: .Net 4.5 improvements
		/// </history>
		public static long ConvertFileSizeFromDisplay(string textValue, string sizeType, long defaultValue)
		{
			long retVal = defaultValue;

			if (double.TryParse(textValue, out double size))
			{
				switch (sizeType.ToLower())
				{
					case "byte":
						break;

					case "kb":
						size *= 1024;
						break;

					case "mb":
						size *= 1024 * 1024;
						break;

					case "gb":
						size *= 1024 * 1024 * 1024;
						break;
				}

				retVal = (long)size;
			}

			return retVal;
		}

		/// <summary>
		/// Converts a font to a string.
		/// </summary>
		/// <param name="font">Font</param>
		/// <returns>font values as a string</returns>
		/// <history>
		/// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
		/// [Curtis_Beard]		10/22/2012	FIX: 36, use invariant culture to always have same float decimal separator
		/// </history>
		public static string ConvertFontToString(System.Drawing.Font font)
		{
			return string.Format("{0}{3}{1}{3}{2}", font.Name, font.Size.ToString(System.Globalization.CultureInfo.InvariantCulture), font.Style.ToString(), Constants.FONT_SEPARATOR);
		}

		/// <summary>
		/// Converts a string to a Color.
		/// </summary>
		/// <param name="color">color values as a string</param>
		/// <returns>Color</returns>
		/// <history>
		/// [Curtis_Beard]		11/03/2006	Created
		/// </history>
		public static System.Drawing.Color ConvertStringToColor(string color)
		{
			string[] rgba = color.Split(char.Parse(Constants.COLOR_SEPARATOR));

			return System.Drawing.Color.FromArgb(byte.Parse(rgba[3]), byte.Parse(rgba[0]), byte.Parse(rgba[1]), byte.Parse(rgba[2]));
		}

		/// <summary>
		/// Converts a string to a Font.
		/// </summary>
		/// <param name="font">font values as a string</param>
		/// <returns>Font</returns>
		/// <history>
		/// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
		/// [Curtis_Beard]		10/22/2012	FIX: 36, use invariant culture to always have same float decimal separator
		/// </history>
		public static System.Drawing.Font ConvertStringToFont(string font)
		{
			string[] fontValues = Utils.SplitByString(font, Constants.FONT_SEPARATOR);

			return new System.Drawing.Font(fontValues[0], float.Parse(fontValues[1], System.Globalization.CultureInfo.InvariantCulture), (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), fontValues[2], true), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
		}

		/// <summary>
		/// Converts a string to a SolidColorBrush.
		/// </summary>
		/// <param name="color">color values as a string</param>
		/// <returns>System.Windows.Media.SolidColorBrush</returns>
		/// <history>
		/// [Curtis_Beard]		04/15/2015	Created
		/// </history>
		public static System.Windows.Media.SolidColorBrush ConvertStringToSolidColorBrush(string color)
		{
			System.Drawing.Color dColor = ConvertStringToColor(color);

			return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(dColor.R, dColor.G, dColor.B));
		}

		/// <summary>
		/// Retrieves all the ComboBox entries as a string.
		/// </summary>
		/// <param name="combo">ComboBox</param>
		/// <returns>string of entries</returns>
		/// <history>
		/// [Curtis_Beard]		11/03/2006	Created
		/// </history>
		public static string GetComboBoxEntriesAsString(System.Windows.Forms.ComboBox combo)
		{
			string[] entries = new string[combo.Items.Count];

			for (int i = 0; i < combo.Items.Count; i++)
			{
				entries[i] = combo.Items[i].ToString();
			}

			return string.Join(Constants.SEARCH_ENTRIES_SEPARATOR, entries);
		}

		/// <summary>
		/// Retrieves the values as an array of strings.
		/// </summary>
		/// <param name="values">ComboBox values as a string</param>
		/// <returns>Array of strings</returns>
		/// <history>
		/// [Curtis_Beard]		11/03/2006	Created
		/// </history>
		public static string[] GetComboBoxEntriesFromString(string values)
		{
			string[] entries = Utils.SplitByString(values, Constants.SEARCH_ENTRIES_SEPARATOR);

			return entries;
		}

		/// <summary>
		/// Get the hit count from the hit/line count display text (assumes in format hit / line)
		/// </summary>
		/// <param name="displayText">The display text to parse (format hit / line)</param>
		/// <returns>hit count</returns>
		/// <history>
		/// [Curtis_Beard]		01/08/2019	CHG: 119, add line hit count
		/// </history>
		public static int GetHitCountFromCountDisplay(string displayText)
		{
			string text = displayText;

			int pos = text.IndexOf(" /");
			if (pos == -1)
			{
				pos = text.Length;
			}
			text = text.Substring(0, pos);

			return int.Parse(text);
		}

		/// <summary>
		/// Get the line count from the hit/line count display text (assumes in format hit / line)
		/// </summary>
		/// <param name="displayText">The display text to parse (format hit / line)</param>
		/// <returns>hit count</returns>
		/// <history>
		/// [Curtis_Beard]		01/08/2019	CHG: 119, add line hit count
		/// </history>
		public static int GetLineCountFromCountDisplay(string displayText)
		{
			string text = displayText;

			int pos = text.IndexOf("/ ");
			if (pos == -1)
			{
				pos = -2;
			}
			text = text.Substring(pos + 2);

			return int.Parse(text);
		}

		/// <summary>
		/// Invoke action delegate on main thread if required.
		/// </summary>
		/// <param name="obj">Object to check for InvokeRequired</param>
		/// <param name="action">Action delegate to perform (either on the current thread or invoked).</param>
		/// <remarks>
		/// Extension for any object that supports the ISynchronizeInvoke interface (such as WinForms
		/// controls). This will handle the InvokeRequired check and call the action delegate from
		/// the appropriate thread.
		/// </remarks>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// private void DB_OfflineModeChanged(object sender, Lib.DB.OfflineModeEventArgs e)
		/// {
		/// // This code could be ran from a background thread
		/// this.InvokeIfRequired(() =>
		/// {
		/// // Code to run after invoking if required
		/// OfflineStatusLabel.Visible = e.OfflineMode;
		/// });
		/// }
		/// ]]>
		/// </code>
		/// </example>
		/// <history>
		/// [Curtis_Beard]		03/05/2020	CHG: use async BeginInvoke for performance
		/// [Curtis_Beard]		05/18/2020	CHG: switch back to Invoke due to UI update issues
		/// </history>
		public static void InvokeIfRequired(this ISynchronizeInvoke obj, System.Windows.Forms.MethodInvoker action)
		{
			if (obj.InvokeRequired)
			{
				var args = new object[0];
				try
				{
					obj.Invoke(action, args);
				}
				catch { }
			}
			else
			{
				action();
			}
		}
	}
}