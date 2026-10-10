using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroGrep.Common
{
	/// <summary>
	/// Contains common application related information details (like Name, Version, etc.)
	/// </summary>
	/// <remarks>
	///   AstroGrep File Searching Utility. Written by Theodore L. Ward
	///   Copyright (C) 2002 AstroComma Incorporated.
	///
	///   This program is free software; you can redistribute it and/or
	///   modify it under the terms of the GNU General Public License
	///   as published by the Free Software Foundation; either version 2
	///   of the License, or (at your option) any later version.
	///
	///   This program is distributed in the hope that it will be useful,
	///   but WITHOUT ANY WARRANTY; without even the implied warranty of
	///   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	///   GNU General Public License for more details.
	///
	///   You should have received a copy of the GNU General Public License
	///   along with this program; if not, write to the Free Software
	///   Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
	///
	///   The author may be contacted at:
	///   ted@astrocomma.com or curtismbeard@gmail.com
	/// </remarks>
	/// <history>
	/// [Curtis_Beard]		06/02/2015	Initial, moved some from Core\Common to here
	/// </history>
	public sealed class ProductInformation
	{
		/// <summary>The application's desired color</summary>
		public static Color ApplicationColor = Color.FromArgb(251, 127, 6);

		/// <summary>The application's display name</summary>
		public static string ApplicationName = "AstroGrep";

		/// <summary>The URL to the donation page</summary>
		public static string DonationUrl = "https://sourceforge.net/p/astrogrep/donate/";

		/// <summary>The URL to the download page</summary>
		public static string DownloadUrl = "https://astrogrep.sourceforge.net/download/";

		/// <summary>The URL to the help page</summary>
		public static string HelpUrl = "https://astrogrep.sourceforge.net/help/";

		/// <summary>The URL to the current license</summary>
		public static string LicenseUrl = "https://www.gnu.org/copyleft/gpl.html";

		/// <summary>The URL to the regular expressions help page</summary>
		public static string RegExHelpUrl = "https://msdn.microsoft.com/en-us/library/az24scfc.aspx";

		/// <summary>The URL to the current version</summary>
		public static string VersionUrl = "https://astrogrep.sourceforge.net/version.html";

		/// <summary>The URL to the current website</summary>
		public static string WebsiteUrl = "https://astrogrep.sourceforge.net";

		/// <summary>
		/// The application's current version.
		/// </summary>
		public static Version ApplicationVersion
		{
			get
			{
				System.Reflection.Assembly _assembly = System.Reflection.Assembly.GetEntryAssembly();
				return _assembly.GetName().Version;
			}
		}

		/// <summary>Determines if application is in portable mode</summary>
		public static bool IsPortable
		{
			get
			{
#if PORTABLE
				return true;
#else
				return false;
#endif
			}
		}

		/// <summary>
		/// Opens a URL in the default browser. Works on both .NET Framework and .NET Core.
		/// </summary>
		/// <param name="url">The URL to open</param>
		public static void OpenUrl(string url)
		{
			try
			{
				// Try the modern approach first that works on all platforms
				var psi = new ProcessStartInfo
				{
					FileName = url,
					UseShellExecute = true,  // Use shell to handle the URL
					CreateNoWindow = true   // Don't show a console window
				};
				Process.Start(psi);
			}
			catch
			{
				// If that fails, try platform-specific fallbacks
				try
				{
					if (System.Runtime.InteropServices.RuntimeInformation
						.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
					{
						// Windows fallback: use explorer or start command
						Process.Start(new ProcessStartInfo
						{
							FileName = "cmd",
							Arguments = $"/c start {url}",
							CreateNoWindow = true,
							UseShellExecute = false
						});
					}
					else if (System.Runtime.InteropServices.RuntimeInformation
						.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
					{
						// Linux fallback
						Process.Start(new ProcessStartInfo
						{
							FileName = "xdg-open",
							Arguments = url,
							UseShellExecute = false,
							CreateNoWindow = true
						});
					}
					else if (System.Runtime.InteropServices.RuntimeInformation
						.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
					{
						// macOS fallback
						Process.Start(new ProcessStartInfo
						{
							FileName = "open",
							Arguments = url,
							UseShellExecute = false,
							CreateNoWindow = true
						});
					}
				}
				catch (Exception fallbackEx)
				{
					// Log for debugging - only in debug builds
					System.Diagnostics.Debug.WriteLine($"Failed to open URL '{url}': {fallbackEx.Message}");
				}
			}
		}
	}
}