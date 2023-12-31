using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using libAstroGrep;
using libAstroGrep.Plugin;

namespace AstroGrep.Plugins.MicrosoftWord
{
	/// <summary>
	/// Used to search a Microsoft Word file for a specified string.
	/// </summary>
	/// <remarks>
	/// AstroGrep File Searching Utility. Written by Theodore L. Ward Copyright (C) 2002 AstroComma Incorporated.
	///
	/// This program is free software; you can redistribute it and/or modify it under the terms of
	/// the GNU General public License as published by the Free Software Foundation; either version 2
	/// of the License, or (at your option) any later version.
	///
	/// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
	/// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See
	/// the GNU General public License for more details.
	///
	/// You should have received a copy of the GNU General public License along with this program; if
	/// (not, write to the Free Software Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
	/// 02111-1307, USA.
	///
	/// The author may be contacted at: ted@astrocomma.com or curtismbeard@gmail.com
	/// </remarks>
	/// <history>
	/// [Curtis_Beard] 07/28/2006 Created
	/// [Curtis_Beard] 05/25/2007 CHG: properly call methods (makes it work with Word 2007)
	/// [Curtis_Beard] 08/21/2007 CHG: use const string for extensions
	/// [Curtis_Beard] 03/31/2015 CHG: rework Grep/Matches
	/// [Curtis_Beard] 09/09/2019 CHG: remove support for docx as OpenXML plugin will support it going forward
	/// [Curtis_Beard] 02/28/2020 CHG: .Net 4.5 cleanup
	/// </history>
	public class MicrosoftWordPlugin : IDisposable, IAstroGrepPlugin
	{
		private const string PLUGIN_AUTHOR = "The AstroGrep Team";
		private const string PLUGIN_DESCRIPTION = "Searches Microsoft Word documents for specified text.  Line numbers are shown as (Line,Page).  Currently doesn't support Regular Expressions or Context lines.";
		private const string PLUGIN_EXTENSIONS = ".doc";
		private const string PLUGIN_NAME = "Microsoft Word";
		private const string PLUGIN_VERSION = "1.2.2";
		private readonly Type __WordType;
		private readonly object MISSING_VALUE = Missing.Value;
		private object __WordApplication;
		private object __WordDocuments;
		private object __WordSelection;
		private object currentSecurityValue;

		/// <summary>
		/// Initializes a new instance of the MicrosoftWordPlugin class.
		/// </summary>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		public MicrosoftWordPlugin()
		{
			try
			{
				__WordType = Type.GetTypeFromProgID("Word.Application");

				if (__WordType != null)
					IsAvailable = true;
			}
			catch (Exception ex)
			{
				IsAvailable = false;
				Trace(ex.ToString());
			}
		}

		/// <summary>
		/// Handles destruction of the object.
		/// </summary>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		~MicrosoftWordPlugin()
		{
			Dispose();
		}

		/// <summary>
		/// Information enum [for selection]
		/// </summary>
		private enum WdInformation
		{
			wdActiveEndPageNumber = 3,
			wdFirstCharacterColumnNumber = 9,
			wdFirstCharacterLineNumber = 10
		}

		/// <summary>
		/// MovementType enum [for line movement]
		/// </summary>
		private enum WdMovementType
		{
			wdMove = 0,
			wdExtend = 1
		}

		/// <summary>
		/// Units enum [for line selection]
		/// </summary>
		private enum WdUnits
		{
			wdLine = 5
		}

		/// <summary>
		/// Handles disposing of the object.
		/// </summary>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// [Curtis_Beard] 05/25/2007 CHG: properly call methods (makes it work with Word 2007)
		/// [Curtis_Beard] 08/15/2017 FIX: 101/85, use unload method with try/catch
		/// </history>
		public void Dispose()
		{
			Unload();
		}

		#region Properties

		/// <summary>
		/// Gets the author of the plugin.
		/// </summary>
		public string Author
		{
			get { return PLUGIN_AUTHOR; }
		}

		/// <summary>
		/// Gets the description of the plugin.
		/// </summary>
		public string Description
		{
			get { return PLUGIN_DESCRIPTION; }
		}

		/// <summary>
		/// Gets the valid extensions for this grep type.
		/// </summary>
		/// <remarks>
		/// Comma separated list of strings.
		/// </remarks>
		public string Extensions
		{
			get { return PLUGIN_EXTENSIONS; }
		}

		/// <summary>
		/// Checks to see if the plugin is available on this system.
		/// </summary>
		public bool IsAvailable { get; }

		/// <summary>
		/// Determines whether the plug-in skipped the file and it should be read by another plug-in or the default search.
		/// </summary>
		public bool IsFileSkipped => false;

		/// <summary>
		/// Gets the name of the plugin.
		/// </summary>
		public string Name
		{
			get { return PLUGIN_NAME; }
		}

		/// <summary>
		/// Gets the version of the plugin.
		/// </summary>
		public string Version
		{
			get { return PLUGIN_VERSION; }
		}

		/// <summary>
		/// Checks to see if the plugin is available to use for searching.
		/// </summary>
		private bool IsUsable { get; set; }

		#endregion Properties

		/// <summary>
		/// Searches the given file for the given search text.
		/// </summary>
		/// <param name="file">
		/// FileInfo object
		/// </param>
		/// <param name="searchSpec">
		/// ISearchSpec interface value
		/// </param>
		/// <param name="ex">
		/// Exception holder if error occurs
		/// </param>
		/// <returns>
		/// Hitobject containing grep results, null if on error
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// [Curtis_Beard] 05/25/2007 ADD: support for Exception object
		/// [Curtis_Beard] 03/31/2015 CHG: rework Grep/Matches
		/// [Curtis_Beard] 01/16/2019 FIX: 103, CHG: 122, trim long lines support
		/// </history>
		public MatchResult Grep(FileInfo file, ISearchSpec searchSpec, ref Exception ex)
		{
			// initialize Exception object to null
			ex = null;

			if (IsAvailable && IsUsable)
			{
				try
				{
					if (file.Exists)
					{
						int count = 0;
						MatchResult match = null;
						int prevLine = 0;
						int prevPage = 0;

						// Open a given Word document as readonly
						// Note: Word 2003+ requires write mode since reading mode doesn't allow use
						// of home/end keys to select text)
						object appversion = __WordApplication.GetType().InvokeMember("Version", BindingFlags.GetProperty, null, __WordApplication, null);
						double.TryParse(appversion.ToString(), out double version);
						bool useReadOnly = version >= 12.00 ? false : true;
						object wordDocument = OpenDocument(file.FullName, useReadOnly);

						// Get Selection Property
						__WordSelection = __WordApplication.GetType().InvokeMember("Selection", BindingFlags.GetProperty,
						   null, __WordApplication, null);

						// create range and find objects
						object range = GetProperty(wordDocument, "Content");
						object find = GetProperty(range, "Find");

						// setup find
						RunRoutine(find, "ClearFormatting", null);
						SetProperty(find, "Forward", true);
						SetProperty(find, "Text", searchSpec.SearchText);
						SetProperty(find, "MatchWholeWord", searchSpec.UseWholeWordMatching);
						SetProperty(find, "MatchCase", searchSpec.UseCaseSensitivity);

						// start find
						FindExecute(find);

						// keep finding text
						while ((bool)GetProperty(find, "Found") == true)
						{
							count += 1;

							if (count == 1)
							{
								// create hit object
								match = new MatchResult(file);
							}

							// since a hit was found and only displaying file names, quickly exit
							if (searchSpec.ReturnOnlyFileNames)
								break;

							// retrieve find information
							int start = (int)GetProperty(range, "Start");
							int colNum = (int)Information(range, WdInformation.wdFirstCharacterColumnNumber);
							int lineNum = (int)Information(range, WdInformation.wdFirstCharacterLineNumber);
							int pageNum = (int)Information(range, WdInformation.wdActiveEndPageNumber);
							string line = GetFindTextLine(start);

							// don't add a hit if on same line
							if (!(prevLine == lineNum && prevPage == pageNum))
							{
								// remove any odd characters from the text
								line = RemoveSpecialCharacters(line);

								// add line
								MatchResultLine matchLine = new MatchResultLine()
								{
									HasMatch = true,
									ColumnNumber = colNum,
									LineNumber = lineNum,
									Line = line,
									LongLineCharCount = searchSpec.LongLineCharCount,
									BeforeAfterCharCount = searchSpec.BeforeAfterCharCount
								};
								var lineMatches = libAstroGrep.Grep.RetrieveLineMatches(line, searchSpec);
								match.SetHitCount(lineMatches.Count);
								matchLine.Matches = lineMatches;
								match.Matches.Add(matchLine);
							}

							prevLine = lineNum;
							prevPage = pageNum;

							// find again
							FindExecute(find);
						}

						ReleaseSelection();
						CloseDocument(wordDocument);

						return match;
					}
					else
					{
						string msg = string.Format("File does not exist: {0}", file.FullName);
						ex = new Exception(msg);
						Trace(msg);
					}
				}
				catch (Exception mainEx)
				{
					ex = mainEx;
					Trace(mainEx.ToString());
				}
			}
			else
			{
				ex = new Exception("Plugin not available or usable.");
				Trace("Plugin not available or usable.");
			}

			return null;
		}

		/// <summary>
		/// Determines if given file is supported by current plugin.
		/// </summary>
		/// <param name="file">
		/// Current FileInfo object
		/// </param>
		/// <returns>
		/// True if supported, False if not supported
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 10/17/2012 Created
		/// </history>
		public bool IsFileSupported(FileInfo file)
		{
			string ext = file.Extension.ToLower();

			string[] supportedExtensions = PLUGIN_EXTENSIONS.Split(new char[1] { ',' });
			foreach (string supportedExtension in supportedExtensions)
			{
				if (ext.Equals(supportedExtension))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Loads the plugin and prepares it for a grep.
		/// </summary>
		/// <returns>
		/// returns true if (successfully loaded or false otherwise
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		public bool Load()
		{
			return Load(false);
		}

		/// <summary>
		/// Loads the plugin and prepares it for a grep.
		/// </summary>
		/// <param name="visible">
		/// true makes underlying application visible, false is make it hidden
		/// </param>
		/// <returns>
		/// returns true if (successfully loaded or false otherwise
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// [Curtis_Beard] 05/25/2007 CHG: properly call methods (makes it work with Word 2007)
		/// [Curtis_Beard] 05/18/2020 FIX: 120, save and set AutomationSecurity property
		/// </history>
		public bool Load(bool visible)
		{
			try
			{
				if (IsAvailable)
				{
					if (!IsUsable)
					{
						// load word
						__WordApplication = Activator.CreateInstance(__WordType);

						// set visible state
						__WordApplication.GetType().InvokeMember("Visible", BindingFlags.SetProperty, null,
						   __WordApplication, new object[1] { visible });

						// get and set security setting (prevents macros from running)
						try
						{
							currentSecurityValue = GetProperty(__WordApplication, "AutomationSecurity");
							SetProperty(__WordApplication, "AutomationSecurity", 3);
						}
						catch (Exception ex)
						{
							Trace(ex.ToString());
						}

						// get Documents Property
						__WordDocuments = __WordApplication.GetType().InvokeMember("Documents", BindingFlags.GetProperty,
						   null, __WordApplication, null);

						// if all is good, then say we are usable
						if (__WordDocuments != null)
						{
							IsUsable = true;
							return true;
						}
					}
					else
					{
						// probably already loaded, but check anyways
						if (__WordApplication != null && __WordDocuments != null)
							return true;
					}
				}
			}
			catch (Exception ex)
			{
				Trace(ex.ToString());
			}

			IsUsable = false;
			return false;
		}

		/// <summary>
		/// Unloads Microsoft Word.
		/// </summary>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// [Curtis_Beard] 05/25/2007 CHG: properly call methods (makes it work with Word 2007)
		/// [Curtis_Beard] 05/18/2020 FIX: 120, reset AutomationSecurity property
		/// </history>
		public void Unload()
		{
			if (__WordApplication != null)
			{
				// Close the application.
				try
				{
					// reset security setting back to original value
					try
					{
						SetProperty(__WordApplication, "AutomationSecurity", currentSecurityValue);
					}
					catch (Exception ex)
					{
						Trace(ex.ToString());
					}

					__WordApplication.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, null,
					   __WordApplication, new object[] { });
				}
				catch (Exception ex)
				{
					Trace(ex.ToString());
				}
			}

			if (__WordApplication != null)
			{
				try
				{
					Marshal.ReleaseComObject(__WordApplication);
				}
				catch (Exception ex)
				{
					Trace(ex.ToString());
				}
			}

			__WordApplication = null;
			IsUsable = false;
		}

		/// <summary>
		/// Closes the given Word Document object.
		/// </summary>
		/// <param name="doc">
		/// Word Document object
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void CloseDocument(object doc)
		{
			if (IsAvailable && doc != null)
				doc.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, doc, new object[] { });
		}

		/// <summary>
		/// Executes the Word find method.
		/// </summary>
		/// <param name="find">
		/// Word's find object
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void FindExecute(object find)
		{
			if (IsAvailable && find != null)
			{
				find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, find, new object[] { });
			}
		}

		/// <summary>
		/// Returns the next line that contains the text to find.
		/// </summary>
		/// <param name="start">
		/// start position in line
		/// </param>
		/// <returns>
		/// line containing the search string
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private string GetFindTextLine(int start)
		{
			try
			{
				SetProperty(__WordSelection, "Start", start);
				SelectionHomeKey(WdUnits.wdLine);
				SelectionEndKey(WdUnits.wdLine, WdMovementType.wdExtend);

				return GetProperty(__WordSelection, "Text").ToString();
			}
			catch (Exception ex)
			{
				Trace(ex.ToString());
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the specified property from the given object.
		/// </summary>
		/// <param name="obj">
		/// Object to get property from
		/// </param>
		/// <param name="prop">
		/// name of property to retrieve
		/// </param>
		/// <returns>
		/// Property object
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private object GetProperty(object obj, string prop)
		{
			if (IsAvailable && obj != null)
				return obj.GetType().InvokeMember(prop, BindingFlags.GetProperty, null, obj, new object[] { });

			return null;
		}

		/// <summary>
		/// Returns the Information object from the given object.
		/// </summary>
		/// <param name="obj">
		/// Object to retrieve information object from
		/// </param>
		/// <param name="type">
		/// Information type to retrieve
		/// </param>
		/// <returns>
		/// Information object
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private object Information(object obj, WdInformation type)
		{
			if (IsAvailable && obj != null)
				return obj.GetType().InvokeMember("Information", BindingFlags.GetProperty, null, obj, new object[1] { (int)type });

			return null;
		}

		/// <summary>
		/// Opens and returns the Word's document object for the given file.
		/// </summary>
		/// <param name="path">
		/// Full path to file.
		/// </param>
		/// <param name="bReadOnly">
		/// True for readonly, False for full access.
		/// </param>
		/// <returns>
		/// Word's Document object if success, null otherwise
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private object OpenDocument(string path, bool bReadOnly)
		{
			if (IsAvailable && __WordDocuments != null)
			{
				return __WordDocuments.GetType().InvokeMember("Open", BindingFlags.InvokeMethod,
				   null, __WordDocuments, new object[3] { path, MISSING_VALUE, bReadOnly });
			}

			return null;
		}

		/// <summary>
		/// Releases the selection object from memory.
		/// </summary>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void ReleaseSelection()
		{
			if (__WordSelection != null)
			{
				Marshal.ReleaseComObject(__WordSelection);
			}
			__WordSelection = null;
		}

		/// <summary>
		/// Removes extra and unneeded characters from the given line.
		/// </summary>
		/// <param name="line">
		/// line to clean
		/// </param>
		/// <returns>
		/// cleaned line
		/// </returns>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private string RemoveSpecialCharacters(string line)
		{
			string cleanLine = line;

			if (cleanLine.EndsWith("\r\n"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r\n"));
			else if (cleanLine.EndsWith("\r"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r"));
			else if (cleanLine.EndsWith("\n"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\n"));

			if (cleanLine.EndsWith("\a"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\a"));

			if (cleanLine.EndsWith("\r\n"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r\n"));
			else if (cleanLine.EndsWith("\r"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\r"));
			else if (cleanLine.EndsWith("\n"))
				cleanLine = cleanLine.Substring(0, cleanLine.LastIndexOf("\n"));

			return cleanLine;
		}

		/// <summary>
		/// Runs the given routine on the object.
		/// </summary>
		/// <param name="obj">
		/// object to run routine on
		/// </param>
		/// <param name="routine">
		/// name of routine
		/// </param>
		/// <param name="parms">
		/// any parameters to routine
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void RunRoutine(object obj, string routine, object[] parms)
		{
			if (IsAvailable && obj != null)
				obj.GetType().InvokeMember(routine, BindingFlags.InvokeMethod, null, obj, parms);
		}

		/// <summary>
		/// Simulates pressing the end key.
		/// </summary>
		/// <param name="unit">
		/// Unit to select on
		/// </param>
		/// <param name="extend">
		/// Movement type
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void SelectionEndKey(WdUnits unit, WdMovementType extend)
		{
			RunRoutine(__WordSelection, "EndKey", new object[2] { (int)unit, (int)extend });
		}

		/// <summary>
		/// Simulates pressing the home key.
		/// </summary>
		/// <param name="unit">
		/// Unit to select on
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void SelectionHomeKey(WdUnits unit)
		{
			RunRoutine(__WordSelection, "HomeKey", new object[1] { (int)unit });
		}

		/// <summary>
		/// Sets the given object's property to the given value.
		/// </summary>
		/// <param name="obj">
		/// object to set property
		/// </param>
		/// <param name="prop">
		/// name of property
		/// </param>
		/// <param name="value">
		/// value to set
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void SetProperty(object obj, string prop, object value)
		{
			if (IsAvailable && obj != null)
				obj.GetType().InvokeMember(prop, BindingFlags.SetProperty, null, obj, new object[1] { value });
		}

		/// <summary>
		/// Outputs the given message to any external tracing applications.
		/// </summary>
		/// <param name="message">
		/// Message to trace
		/// </param>
		/// <history>
		/// [Curtis_Beard] 07/28/2006 Created
		/// </history>
		private void Trace(string message)
		{
			System.Diagnostics.Debug.WriteLine(PLUGIN_NAME + " Plugin: " + message);
		}
	}
}