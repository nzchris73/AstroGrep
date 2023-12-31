using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using AstroGrep.Common;
using AstroGrep.Common.Logging;
using AstroGrep.Core;
using libAstroGrep;
using libAstroGrep.EncodingDetection;

namespace AstroGrep.Windows.Forms
{
	/// <summary>
	/// Used to display the options dialog.
	/// </summary>
	/// <remarks>
	/// AstroGrep File Searching Utility. Written by Theodore L. Ward
	/// Copyright (C) 2002 AstroComma Incorporated.
	///
	/// This program is free software; you can redistribute it and/or
	/// modify it under the terms of the GNU General public License
	/// as published by the Free Software Foundation; either version 2
	/// of the License, or (at your option) any later version.
	///
	/// This program is distributed in the hope that it will be useful,
	/// but WITHOUT ANY WARRANTY; without even the implied warranty of
	/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	/// GNU General public License for more details.
	///
	/// You should have received a copy of the GNU General public License
	/// along with this program; if not, write to the Free Software
	/// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
	///
	/// The author may be contacted at:
	/// ted@astrocomma.com or curtismbeard@gmail.com
	/// </remarks>
	/// <history>
	/// [Curtis_Beard]		05/23/2007	Created
	/// [Curtis_Beard]		07/13/2007	ADD: system tray options
	/// [Curtis_Beard]	   03/07/2012	ADD: 3131609, exclusions, remove file extension exclusion list
	/// </history>
	public partial class frmOptions : BaseForm
	{
		/// <summary>
		/// Used to display encoding performance enum values.
		/// </summary>
		internal class EncodingPerformance
		{
			/// <summary>Display name of performance value</summary>
			public string Name { get; set; }

			/// <summary>Performance enum value</summary>
			public int Value { get; set; }
		}

		private Font __FileFont = Convertors.ConvertStringToFont(GeneralSettings.FilePanelFont);
		private bool __IsAdmin = API.UACHelper.HasAdminPrivileges();
		private bool __LanguageChange = false;
		private bool __RightClickEnabled = false;
		private bool __RightClickUpdate = false;
		private bool inhibitFileEncodingAutoCheck;

		/// <summary>
		/// Creates a new instance of the frmOptions class.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]		05/23/2007	Created
		/// [Curtis_Beard]      10/09/2012	CHG: 3575507, handle UAC request for right click option
		/// </history>
		public frmOptions()
		{
			InitializeComponent();

			__RightClickEnabled = Shortcuts.IsSearchOption();

			ForeColorButton.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
			BackColorButton.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
			btnResultsWindowForeColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
			btnResultsWindowBackColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
			btnResultsContextForeColor.ColorChange += new AstroGrep.Windows.Controls.ColorButton.ColorChangeHandler(NewColor);
			chkRightClickOption.CheckedChanged += new EventHandler(chkRightClickOption_CheckedChanged);

			API.ListViewExtensions.SetTheme(lstFiles);
			API.ListViewExtensions.SetTheme(TextEditorsList);
		}

		/// <summary>
		/// Checks to see if the language w changed.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]		07/25/2006	Created
		/// </history>
		public bool IsLanguageChange
		{
			get { return __LanguageChange; }
		}

		/// <summary>
		/// Determines if the theme changed.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]		08/26/2022	Created
		/// </history>
		public bool IsThemeChange { get; set; } = false;

		/// <summary>
		/// Add a new text editor.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
		/// </history>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (tbcOptions.SelectedTab == tabTextEditors)
			{
				frmAddEditTextEditor dlg = new frmAddEditTextEditor();
				dlg.IsAdd = true;
				dlg.IsAllTypesDefined = IsAllTypesDefined();
				dlg.ExistingFileTypes = GetExistingFileTypes();

				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					// create new entry
					ListViewItem item = new ListViewItem();
					item.Tag = dlg.Editor;
					item.Text = dlg.Editor.FileType;
					item.SubItems.Add(dlg.Editor.Editor);
					item.SubItems.Add(dlg.Editor.Arguments);
					item.SubItems.Add(dlg.Editor.TabSize.ToString());
					TextEditorsList.Items.Add(item);

					SetTextEditorsButtonState();
				}
			}

			this.DialogResult = DialogResult.None;
		}

		/// <summary>
		/// Clears the encoding cache.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
		/// </history>
		private void btnCacheClear_Click(object sender, EventArgs e)
		{
			libAstroGrep.EncodingDetection.Caching.EncodingCache.Instance.Clear(true);

			MessageBox.Show(this, Language.GetGenericText("FileEncoding.CacheCleared", "Cache cleared successfully."), ProductInformation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Closes the form
		/// </summary>
		/// <param name="sender">System parameter</param>
		/// <param name="e">System parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/19/2006	Created
		/// </history>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Edit the selected text editor.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// [Curtis_Beard]		08/13/2014	FIX: better detection of file types
		/// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
		/// </history>
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			if (tbcOptions.SelectedTab == tabTextEditors)
			{
				if (TextEditorsList.SelectedItems.Count > 0)
				{
					ListViewItem item = TextEditorsList.SelectedItems[0];
					frmAddEditTextEditor dlg = new frmAddEditTextEditor();

					// set values
					dlg.IsAdd = false;
					dlg.IsAllTypesDefined = IsAllTypesDefined();
					dlg.Editor = item.Tag as TextEditor;
					dlg.ExistingFileTypes = GetExistingFileTypes();

					if (dlg.ShowDialog(this) == DialogResult.OK)
					{
						// get values
						TextEditorsList.SelectedItems[0].Tag = dlg.Editor;
						TextEditorsList.SelectedItems[0].Text = dlg.Editor.FileType;
						TextEditorsList.SelectedItems[0].SubItems[1].Text = dlg.Editor.Editor;
						TextEditorsList.SelectedItems[0].SubItems[2].Text = dlg.Editor.Arguments;
						TextEditorsList.SelectedItems[0].SubItems[3].Text = dlg.Editor.TabSize.ToString();
					}

					SetTextEditorsButtonState();
				}
			}

			this.DialogResult = DialogResult.None;
		}

		private void btnFileEncodingAdd_Click(object sender, EventArgs e)
		{
			var dialog = new frmAddEditFileEncoding();
			if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				lstFiles.Items.Add(GetFileEncodingListViewItem(dialog.SelectedFileEncoding));

				SetFileEncodingButtonState();
			}

			this.DialogResult = System.Windows.Forms.DialogResult.None;
		}

		private void btnFileEncodingDelete_Click(object sender, EventArgs e)
		{
			// remove
			if (lstFiles.SelectedItems.Count > 0)
			{
				foreach (ListViewItem item in lstFiles.SelectedItems)
				{
					lstFiles.Items.Remove(item);
				}
				SetFileEncodingButtonState();
			}

			this.DialogResult = DialogResult.None;
		}

		private void btnFileEncodingEdit_Click(object sender, EventArgs e)
		{
			if (lstFiles.SelectedItems.Count > 0)
			{
				// get currently selected exclusion
				var item = lstFiles.SelectedItems[0].Tag as FileEncoding;
				item.Enabled = lstFiles.SelectedItems[0].Checked;

				var dialog = new frmAddEditFileEncoding();
				dialog.SelectedFileEncoding = item;
				if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					item = dialog.SelectedFileEncoding;
					var listItem = GetFileEncodingListViewItem(item);
					lstFiles.SelectedItems[0].Checked = item.Enabled;
					lstFiles.SelectedItems[0].Tag = item;

					lstFiles.SelectedItems[0].SubItems[1].Text = listItem.SubItems[1].Text;
					lstFiles.SelectedItems[0].SubItems[2].Text = listItem.SubItems[2].Text;

					SetFileEncodingButtonState();
				}
			}

			this.DialogResult = System.Windows.Forms.DialogResult.None;
		}

		/// <summary>
		/// Show font selection dialog.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   10/10/2012	ADD: 3479503, ability to change file list font
		/// </history>
		private void btnFileFindFont_Click(object sender, EventArgs e)
		{
			var dlg = new FontDialog()
			{
				ShowColor = false,
				ShowEffects = false,
				ShowHelp = false,
				Font = __FileFont
			};

			var result = dlg.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				DisplayFont(dlg.Font, lblFileCurrentFont);
				__FileFont = dlg.Font;
			}
		}

		/// <summary>
		/// Show font selection dialog.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
		/// </history>
		private void btnFindFont_Click(object sender, EventArgs e)
		{
			var dlg = new FontDialog()
			{
				ShowColor = false,
				ShowEffects = false,
				ShowHelp = false,
				Font = rtxtResultsPreview.Font
			};

			var result = dlg.ShowDialog(this);
			if (result == DialogResult.OK)
			{
				DisplayFont(dlg.Font, lblCurrentFont);
				rtxtResultsPreview.Font = dlg.Font;
			}
		}

		/// <summary>
		/// Handles saving the user specified options.
		/// </summary>
		/// <param name="sender">System parameter</param>
		/// <param name="e">System parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/19/2006	Created
		/// [Curtis_Beard]		07/21/2006	ADD: Custom colors for fore/back of results
		/// [Curtis_Beard]		07/25/2006	FIX: Add back Browse... if it was removed
		/// [Curtis_Beard]      07/28/2006  ADD: extension exclusion list
		/// [Curtis_Beard]      11/10/2006  FIX: Don't load new language, just set that it changed
		/// [Curtis_Beard]      11/13/2006  CHG: Only try and save the search option if enabled
		/// [Curtis_Beard]		10/11/2007	CHG: use language culture ids
		/// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
		/// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
		/// [Curtis_Beard]	   10/16/2012	CHG: Save search settings on exit
		/// [Curtis_Beard]	   10/28/2012	ADD: 3575509, results word wrap
		/// [Curtis_Beard]	   10/28/2012	CHG: 3575507, handle UAC request for right click option
		/// [Curtis_Beard]	   10/28/2012	ADD: 3479503, ability to change file list font
		/// [Curtis_Beard]	   02/04/2014	ADD: 66, option to detect file encoding
		/// [Curtis_Beard]	   09/16/2014	ADD: installer check for desktop/start menu options processing
		/// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
		/// [Curtis_Beard]      03/03/2015	CHG: 93, option to save messages form position
		/// [Curtis_Beard]	   04/08/2015	CHG: 81, remove old word wrap and white space options. now in frmMain.
		/// [Curtis_Beard]	   04/15/2015	CHG: add content forecolor
		/// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
		/// [Curtis_Beard]	   05/19/2017	CHG: 120, add option to use accent color
		/// [Curtis_Beard]	   01/10/2019  CHG: 136, add option to force encoding for all
		/// [Curtis_Beard]      01/16/2019	FIX: 103, CHG: 122, trim long lines support
		/// </history>
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			// Store the values in the globals
			GeneralSettings.MaximumMRUPaths = cboPathMRUCount.SelectedIndex + 1;
			GeneralSettings.HighlightForeColor = Convertors.ConvertColorToString(ForeColorButton.SelectedColor);
			GeneralSettings.HighlightBackColor = Convertors.ConvertColorToString(BackColorButton.SelectedColor);
			GeneralSettings.ResultsForeColor = Convertors.ConvertColorToString(btnResultsWindowForeColor.SelectedColor);
			GeneralSettings.ResultsBackColor = Convertors.ConvertColorToString(btnResultsWindowBackColor.SelectedColor);
			GeneralSettings.ResultsContextForeColor = Convertors.ConvertColorToString(btnResultsContextForeColor.SelectedColor);
			GeneralSettings.ResultsFont = Convertors.ConvertFontToString(rtxtResultsPreview.Font);
			GeneralSettings.ShowExclusionErrorMessage = chkShowExclusionErrorMessage.Checked;
			GeneralSettings.SaveSearchOptionsOnExit = chkSaveSearchOptions.Checked;
			GeneralSettings.FilePanelFont = Convertors.ConvertFontToString(__FileFont);
			GeneralSettings.DetectFileEncoding = chkDetectFileEncoding.Checked;
			GeneralSettings.EncodingPerformance = (int)cboPerformance.SelectedValue;
			GeneralSettings.UseEncodingCache = chkUseEncodingCache.Checked;
			GeneralSettings.ForcedEncoding = (int)cboForceEncoding.SelectedValue;
			GeneralSettings.LogDisplaySavePosition = chkSaveMessagesPosition.Checked;
			GeneralSettings.ExclusionsDisplaySavePosition = chkSaveExclusionsPosition.Checked;
			GeneralSettings.LongLineCharCount = (int)numResultsLongLineCount.Value;
			GeneralSettings.BeforeAfterCharCount = (int)numResultsBeforeAfterCount.Value;

			// determine if theme needs updated (theme type changed or accent color check-box toggled)
			if (cboTheme.SelectedIndex != GeneralSettings.ThemeType || GeneralSettings.UseAstroGrepAccentColor != chkLabelColor.Checked)
			{
				GeneralSettings.ThemeType = cboTheme.SelectedIndex;
				IsThemeChange = true;
			}

			GeneralSettings.UseAstroGrepAccentColor = chkLabelColor.Checked;

			// set default log display positions if save position is disabled
			if (!GeneralSettings.LogDisplaySavePosition)
			{
				// set log display window and column positions to default values
				GeneralSettingsReset.LogDisplaySetDefaultPositions();
			}

			// set default exclusions display positions if save position is disabled
			if (!GeneralSettings.ExclusionsDisplaySavePosition)
			{
				// set exclusions display window and column positions to default values
				GeneralSettingsReset.ExclusionsDisplaySetDefaultPositions();
			}

			// Only load new language on a change
			LanguageItem item = (LanguageItem)cboLanguage.SelectedItem;
			if (!GeneralSettings.Language.Equals(item.Culture))
			{
				GeneralSettings.Language = item.Culture;
				Language.Load(GeneralSettings.Language);
				__LanguageChange = true;
			}

			// set shortcuts
			if (!Registry.IsInstaller())
			{
				Shortcuts.SetDesktopShortcut(chkDesktopShortcut.Checked);
				Shortcuts.SetStartMenuShortcut(chkStartMenuShortcut.Checked);
			}

			SaveEditors();

			SaveFileEncodings();

			Core.PluginManager.Save();

			// handle right click search change
			if (__RightClickUpdate)
			{
				try
				{
					string path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "AstroGrep.AdminProcess.exe");
					string agPath = string.Format("\"{0}\"", Application.ExecutablePath);
					string explorerText = string.Format("\"{0}\"", Language.GetGenericText("SearchExplorerItem"));
					string args = string.Format("\"{0}\" {1} {2}", chkRightClickOption.Checked.ToString(), agPath, explorerText);

					API.UACHelper.AttemptPrivilegeEscalation(path, args, false);
				}
				catch (Exception ex)
				{
					LogClient.Instance.Logger.Error(ex, "An error occurred trying to call the privilege escaltion to set the right click search option.");
				}
			}

			this.Close();
		}

		/// <summary>
		/// Delete the selected text editor.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// </history>
		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if (tbcOptions.SelectedTab == tabTextEditors)
			{
				// remove
				if (TextEditorsList.SelectedItems.Count > 0)
				{
					TextEditorsList.Items.Remove(TextEditorsList.SelectedItems[0]);
					SetTextEditorsButtonState();
				}
			}

			this.DialogResult = DialogResult.None;
		}

		/// <summary>
		/// Resize drop down list if necessary
		/// </summary>
		/// <param name="sender">system parm</param>
		/// <param name="e">system parm</param>
		/// <history>
		/// [Curtis_Beard]	   01/10/2019  CHG: 136, initial
		/// </history>
		private void cboForceEncoding_DropDown(object sender, EventArgs e)
		{
			cboForceEncoding.DropDownWidth = Convertors.CalculateDropDownWidth(cboForceEncoding);
		}

		/// <summary>
		/// Enable/Disable settings based on if encoding detection is enabled.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   05/26/2015	FIX: 69, add performance setting for file detection
		/// </history>
		private void chkDetectFileEncoding_CheckedChanged(object sender, EventArgs e)
		{
			lblPerformance.Enabled = cboPerformance.Enabled = chkUseEncodingCache.Enabled = btnCacheClear.Enabled = lblForceEncoding.Enabled = cboForceEncoding.Enabled = chkDetectFileEncoding.Checked;
		}

		/// <summary>
		/// Update results preview display when remove leading white space checkbox changes.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
		/// </history>
		private void chkRemoveLeadingWhiteSpace_CheckedChanged(object sender, EventArgs e)
		{
			UpdateResultsPreview();
		}

		/// <summary>
		/// Handle change to the right click checkbox.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]      10/09/2012	Initial: 3575507, handle UAC request for right click option
		/// </history>
		private void chkRightClickOption_CheckedChanged(object sender, EventArgs e)
		{
			if (chkRightClickOption.Checked != __RightClickEnabled)
			{
				if (!__IsAdmin)
				{
					API.UACHelper.AddShieldToButton(btnOK);
				}
				__RightClickUpdate = true;
			}
			else
			{
				if (!__IsAdmin)
				{
					API.UACHelper.RemoveShieldFromButton(btnOK);
				}
				__RightClickUpdate = false;
			}
		}

		/// <summary>
		/// Displays the given font as a string on the given label.
		/// </summary>
		/// <param name="fnt">Font to display</param>
		/// <param name="lbl">Label to show font</param>
		/// <history>
		/// [Curtis_Beard]	   10/10/2012	ADD: 3479503, ability to change file list font
		/// [Curtis_Beard]	   10/26/2012	CHG: use / to separate values instead of comma which could be used in SizeInPoints
		/// </history>
		private void DisplayFont(Font fnt, Label lbl)
		{
			lbl.Text = string.Format("{0} / {1} / {2}", fnt.Name, fnt.SizeInPoints, fnt.Style.ToString());
		}

		/// <summary>
		/// Handles setting the user specified options into the correct controls for display.
		/// </summary>
		/// <param name="sender">System parameter</param>
		/// <param name="e">System parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/19/2006	Created
		/// [Curtis_Beard]		07/21/2006	ADD: Custom colors for fore/back of results
		/// [Curtis_Beard]		07/28/2006	ADD: extension exclusion list
		/// [Curtis_Beard]		10/11/2007	CHG: use language culture ids
		/// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
		/// [Curtis_Beard]		02/24/2012	CHG: 3488321, ability to change results font
		/// [Curtis_Beard]		10/16/2012	CHG: Save search settings on exit
		/// [Curtis_Beard]		10/28/2012	ADD: 3575509, results word wrap
		/// [Curtis_Beard]		10/28/2012	ADD: 3479503, ability to change file list font
		/// [Curtis_Beard]		02/04/2014	ADD: 66, option to detect file encoding
		/// [Curtis_Beard]		09/16/2014	ADD: installer check to hide desktop/start menu options
		/// [Curtis_Beard]      10/27/2014	CHG: 85, remove leading white space
		/// [Curtis_Beard]      03/03/2015	CHG: 93, option to save messages form position
		/// [Curtis_Beard]		04/08/2015	CHG: 81, remove old word wrap and white space options. now in frmMain.
		/// [Curtis_Beard]		04/15/2015	CHG: add content forecolor
		/// [Curtis_Beard]		05/26/2015	FIX: 69, add performance setting for file detection
		/// [Curtis_Beard]		06/15/2015	CHG: 57, support external language files
		/// [LinkNet]			04/29/2017  ADD: column widths scaled in accordance with windows DPI% setting
		/// [Curtis_Beard]		05/19/2017	CHG: 120, add option to use accent color
		/// [Curtis_Beard]		01/10/2019  CHG: 136, add option to force encoding for all
		/// [Curtis_Beard]      01/16/2019	FIX: 103, CHG: 122, trim long lines support
		/// [Curtis_Beard]      08/26/2022	CHG: 146, support dark theme/adjust backcolor of certain controls
		/// </history>
		private void frmOptions_Load(object sender, System.EventArgs e)
		{
			cboPathMRUCount.SelectedIndex = GeneralSettings.MaximumMRUPaths - 1;
			chkRightClickOption.Checked = Shortcuts.IsSearchOption();
			if (Registry.IsInstaller())
			{
				chkDesktopShortcut.Visible = false;
				chkStartMenuShortcut.Visible = false;
			}
			else
			{
				chkDesktopShortcut.Checked = Shortcuts.IsDesktopShortcut();
				chkStartMenuShortcut.Checked = Shortcuts.IsStartMenuShortcut();
			}
			chkShowExclusionErrorMessage.Checked = GeneralSettings.ShowExclusionErrorMessage;
			chkSaveSearchOptions.Checked = GeneralSettings.SaveSearchOptionsOnExit;
			chkDetectFileEncoding.Checked = GeneralSettings.DetectFileEncoding;
			chkUseEncodingCache.Checked = GeneralSettings.UseEncodingCache;
			chkSaveMessagesPosition.Checked = GeneralSettings.LogDisplaySavePosition;
			chkSaveExclusionsPosition.Checked = GeneralSettings.ExclusionsDisplaySavePosition;
			chkLabelColor.Checked = GeneralSettings.UseAstroGrepAccentColor;

			// ColorButton init
			ForeColorButton.SelectedColor = Convertors.ConvertStringToColor(GeneralSettings.HighlightForeColor);
			BackColorButton.SelectedColor = Convertors.ConvertStringToColor(GeneralSettings.HighlightBackColor);
			btnResultsWindowForeColor.SelectedColor = Convertors.ConvertStringToColor(GeneralSettings.ResultsForeColor);
			btnResultsWindowBackColor.SelectedColor = Convertors.ConvertStringToColor(GeneralSettings.ResultsBackColor);
			btnResultsContextForeColor.SelectedColor = Convertors.ConvertStringToColor(GeneralSettings.ResultsContextForeColor);

			// results font
			rtxtResultsPreview.Font = Convertors.ConvertStringToFont(GeneralSettings.ResultsFont);
			DisplayFont(rtxtResultsPreview.Font, lblCurrentFont);

			// file list font
			DisplayFont(__FileFont, lblFileCurrentFont);

			// character counts
			numResultsLongLineCount.Value = GeneralSettings.LongLineCharCount;
			numResultsBeforeAfterCount.Value = GeneralSettings.BeforeAfterCharCount;

			tbcOptions.SelectedTab = tabGeneral;

			LoadEditors(TextEditors.GetAll());
			LoadFileEncodings();

			// set window as tab background instead of base class default
			ForceBackColor(tabGeneral, Core.Theme.ThemeProvider.Theme.Colors.Window);
			ForceBackColor(tabFileEncoding, Core.Theme.ThemeProvider.Theme.Colors.Window);
			ForceBackColor(tabTextEditors, Core.Theme.ThemeProvider.Theme.Colors.Window);
			ForceBackColor(tabResults, Core.Theme.ThemeProvider.Theme.Colors.Window);

			// these specific controls need their backgrounds forces to transparent
			ForceBackColor(lblPerformance, Color.Transparent, false);
			ForceBackColor(chkUseEncodingCache, Color.Transparent, false);
			ForceBackColor(lblForceEncoding, Color.Transparent, false);

			//Language.GenerateXml(this, Application.StartupPath + "\\" + this.Name + ".xml");
			Language.ProcessForm(this);
			Language.LoadComboBox(cboLanguage);

			// set the user selected language
			if (cboLanguage.Items.Count > 0)
			{
				foreach (object oItem in cboLanguage.Items)
				{
					LanguageItem item = (LanguageItem)oItem;
					if (item.Culture.Equals(GeneralSettings.Language, StringComparison.OrdinalIgnoreCase))
					{
						cboLanguage.SelectedItem = item;
						break;
					}
				}
			}

			// setup the performance drop down list
			List<EncodingPerformance> performanceValues = new List<EncodingPerformance>();
			Array values = Enum.GetValues(typeof(EncodingOptions.Performance));
			foreach (EncodingOptions.Performance val in values)
			{
				performanceValues.Add(new EncodingPerformance() { Name = Language.GetGenericText(string.Format("FileEncoding.Performance.{0}", Enum.GetName(typeof(EncodingOptions.Performance), val))), Value = (int)val });
			}
			cboPerformance.DisplayMember = "Name";
			cboPerformance.ValueMember = "Value";
			cboPerformance.DataSource = performanceValues;
			cboPerformance.SelectedValue = GeneralSettings.EncodingPerformance;
			chkDetectFileEncoding_CheckedChanged(null, null);

			// setup the force encoding
			var encodings = System.Text.Encoding.GetEncodings().Select(ec => new Tuple<int, string>(ec.CodePage, ec.DisplayName)).ToList();
			encodings.Insert(0, new Tuple<int, string>(-1, ""));
			cboForceEncoding.DisplayMember = "Item2";
			cboForceEncoding.ValueMember = "Item1";
			cboForceEncoding.DataSource = encodings;
			cboForceEncoding.SelectedValue = GeneralSettings.ForcedEncoding;

			// set column text
			TextEditorsList.Columns[0].Text = Language.GetGenericText("TextEditorsColumnFileType");
			TextEditorsList.Columns[1].Text = Language.GetGenericText("TextEditorsColumnLocation");
			TextEditorsList.Columns[2].Text = Language.GetGenericText("TextEditorsColumnCmdLine");
			TextEditorsList.Columns[3].Text = Language.GetGenericText("TextEditorsColumnTabSize");
			lstFiles.Columns[0].Text = Language.GetGenericText("FileEncoding.Enabled", "Enabled");
			lstFiles.Columns[1].Text = Language.GetGenericText("FileEncoding.FilePath", "File Path");
			lstFiles.Columns[2].Text = Language.GetGenericText("FileEncoding.Encoding", "Encoding");

			// set column widths
			TextEditorsList.Columns[0].Width = Constants.OPTIONS_TEXT_EDITOR_COLUMN_0_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;
			TextEditorsList.Columns[1].Width = Constants.OPTIONS_TEXT_EDITOR_COLUMN_1_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;
			TextEditorsList.Columns[2].Width = Constants.OPTIONS_TEXT_EDITOR_COLUMN_2_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;
			TextEditorsList.Columns[3].Width = Constants.OPTIONS_TEXT_EDITOR_COLUMN_3_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;
			lstFiles.Columns[0].Width = Constants.OPTIONS_FILES_COLUMN_0_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;
			lstFiles.Columns[1].Width = Constants.OPTIONS_FILES_COLUMN_1_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;
			lstFiles.Columns[2].Width = Constants.OPTIONS_FILES_COLUMN_2_WIDTH * GeneralSettings.WindowsDPIPerCentSetting / 100;

			// setup the theme drop down list
			var themeValues = CreateList(new { Name = "", Value = 0 });
			themeValues.Clear();
			Array themeEnumValues = Enum.GetValues(typeof(Core.Theme.ThemeProvider.ThemeType));
			foreach (Core.Theme.ThemeProvider.ThemeType val in themeEnumValues)
			{
				themeValues.Add(new { Name = Language.GetGenericText($"Theme.{Enum.GetName(typeof(Core.Theme.ThemeProvider.ThemeType), val)}"), Value = (int)val });
			}
			cboTheme.DisplayMember = "Name";
			cboTheme.ValueMember = "Value";
			cboTheme.DataSource = themeValues;
			cboTheme.SelectedValue = GeneralSettings.ThemeType;
		}

		private List<T> CreateList<T>(params T[] elements)
		{
			return new List<T>(elements);
		}

		/// <summary>
		/// Retrieves an array of file types currently defined.
		/// </summary>
		/// <returns>String array of file types</returns>
		/// <history>
		/// [Curtis_Beard]		08/13/2014	FIX: better detection of file types
		/// [Curtis_Beard]	   08/21/2017	FIX: get each definition and check it in frmAddEditTextEditor.cs instead
		/// </history>
		private List<string> GetExistingFileTypes()
		{
			List<string> types = new List<string>();

			for (int i = 0; i < TextEditorsList.Items.Count; i++)
			{
				types.Add(TextEditorsList.Items[i].Text);
			}

			return types;
		}

		/// <summary>
		/// Get the list view item from the given FileEncoding object.
		/// </summary>
		/// <param name="item">FileEncoding object</param>
		/// <returns>ListViewItem object</returns>
		/// <history>
		/// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
		/// </history>
		private ListViewItem GetFileEncodingListViewItem(FileEncoding item)
		{
			ListViewItem listItem = new ListViewItem();
			listItem.Tag = item;
			listItem.Checked = item.Enabled;
			listItem.SubItems.Add(item.FilePath);
			listItem.SubItems.Add(System.Text.Encoding.GetEncoding(item.CodePage).EncodingName);

			return listItem;
		}

		/// <summary>
		/// Determines if a text editor is defined for all file types.
		/// </summary>
		/// <returns>Returns true if an all file types text editor is defined, false otherwise</returns>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// </history>
		private bool IsAllTypesDefined()
		{
			foreach (ListViewItem item in TextEditorsList.Items)
			{
				if (item.Text.Equals("*"))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Loads the given text editors.
		/// </summary>
		/// <param name="editors">TextEditor array, can be nothing</param>
		/// <history>
		/// [Curtis_Beard]	   07/10/2006	Created
		/// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
		/// </history>
		private void LoadEditors(TextEditor[] editors)
		{
			if (editors != null)
			{
				TextEditorsList.BeginUpdate();
				foreach (TextEditor editor in editors)
				{
					ListViewItem item = new ListViewItem();
					item.Text = editor.FileType;
					item.SubItems.Add(editor.Editor);
					item.SubItems.Add(editor.Arguments);
					item.SubItems.Add(editor.TabSize.ToString());
					item.Selected = true;
					item.Tag = editor;
					TextEditorsList.Items.Add(item);
				}
				TextEditorsList.EndUpdate();
			}
		}

		private void LoadFileEncodings()
		{
			var fileEncodings = FileEncoding.ConvertStringToFileEncodings(GeneralSettings.FileEncodings);
			if (fileEncodings != null && fileEncodings.Count > 0)
			{
				lstFiles.BeginUpdate();

				foreach (var file in fileEncodings)
				{
					var item = GetFileEncodingListViewItem(file);
					lstFiles.Items.Add(item);
				}

				lstFiles.EndUpdate();
			}

			SetFileEncodingButtonState();
		}

		/// <summary>
		/// Handles ListView Column Click event to allow Enabled column to toggle all checkboxes.
		/// </summary>
		/// <param name="sender">lstFiles listview</param>
		/// <param name="e">column click arguments</param>
		/// <history>
		/// [Curtis_Beard]	   08/13/2014	ADD: 79, allow Enabled column to toggle all checkboxes
		/// </history>
		private void lstFiles_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			// enabled column
			if (e.Column == 0)
			{
				bool allChecked = (lstFiles.CheckedItems.Count == lstFiles.Items.Count);
				foreach (ListViewItem item in lstFiles.Items)
				{
					item.Checked = !allChecked;
				}
			}
		}

		/// <summary>
		/// Edit the selected file entry.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
		/// </history>
		private void lstFiles_DoubleClick(object sender, EventArgs e)
		{
			Point clientPoint = lstFiles.PointToClient(Control.MousePosition);
			ListViewItem item = lstFiles.GetItemAt(clientPoint.X, clientPoint.Y);

			if (item != null)
			{
				item.Selected = true;
				btnFileEncodingEdit_Click(null, null);
			}
		}

		/// <summary>
		/// Handle not changing checked state of item when double clicking to edit it.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
		/// </history>
		private void lstFiles_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if (inhibitFileEncodingAutoCheck)
			{
				e.NewValue = e.CurrentValue;
			}
		}

		/// <summary>
		/// Handles the key down event (supports ctrl-a, del).
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
		/// </history>
		private void lstFiles_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.A && e.Control) //ctrl+a  Select All
			{
				foreach (ListViewItem item in lstFiles.Items)
				{
					item.Selected = true;
				}
			}

			if (e.KeyCode == Keys.Delete) //delete
			{
				btnFileEncodingDelete_Click(sender, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Handle not changing checked state of item when double clicking to edit it.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
		/// </history>
		private void lstFiles_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			inhibitFileEncodingAutoCheck = true;
		}

		/// <summary>
		/// Handle not changing checked state of item when double clicking to edit it.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]	   03/04/2014	FIX: 52, don't change check state when double clicking to edit
		/// </history>
		private void lstFiles_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			inhibitFileEncodingAutoCheck = false;
		}

		/// <summary>
		/// Update the button states.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
		/// </history>
		private void lstFiles_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetFileEncodingButtonState();
		}

		/// <summary>
		/// Handle when a new color has been selected.
		/// </summary>
		/// <param name="newColor">new Color</param>
		/// <history>
		/// [Curtis_Beard]		07/21/2006	Created
		/// </history>
		private void NewColor(Color newColor)
		{
			UpdateResultsPreview();
		}

		/// <summary>
		/// Saves the defined text editors.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]	   07/10/2006	Created
		/// [Curtis_Beard]		03/06/2015	FIX: 65, support use quotes around file name
		/// </history>
		private void SaveEditors()
		{
			if (TextEditorsList.Items.Count == 0)
			{
				TextEditors.Save(null);
				return;
			}

			TextEditor[] editors = new TextEditor[TextEditorsList.Items.Count];
			int index = 0;
			foreach (ListViewItem item in TextEditorsList.Items)
			{
				editors[index] = item.Tag as TextEditor;
				index += 1;
			}

			TextEditors.Save(editors);
		}

		private void SaveFileEncodings()
		{
			string encodings = string.Empty;

			if (lstFiles.Items.Count > 0)
			{
				var fileEncodings = new List<FileEncoding>();
				foreach (ListViewItem item in lstFiles.Items)
				{
					var encoding = item.Tag as FileEncoding;
					encoding.Enabled = item.Checked;
					fileEncodings.Add(encoding);
				}

				encodings = FileEncoding.ConvertFileEncodingsToString(fileEncodings);
			}

			GeneralSettings.FileEncodings = encodings;
		}

		/// <summary>
		/// Sets the FileEncoding's button states depending on if one is selected.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]      02/09/2015	CHG: 92, support for specific file encodings
		/// </history>
		private void SetFileEncodingButtonState()
		{
			if (lstFiles.SelectedItems.Count > 0)
			{
				btnFileEncodingDelete.Enabled = true;
				btnFileEncodingEdit.Enabled = true;
			}
			else
			{
				btnFileEncodingDelete.Enabled = false;
				btnFileEncodingEdit.Enabled = false;
			}
		}

		/// <summary>
		/// Sets the TextEditor's button states depending on if one is selected.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// </history>
		private void SetTextEditorsButtonState()
		{
			if (TextEditorsList.SelectedItems.Count > 0)
			{
				btnRemove.Enabled = true;
				btnEdit.Enabled = true;
			}
			else
			{
				btnRemove.Enabled = false;
				btnEdit.Enabled = false;
			}
		}

		/// <summary>
		/// Setup tab pages when selected.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]		05/22/2007	Created
		/// </history>
		private void tbcOptions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (tbcOptions.SelectedTab == tabResults)
			{
				UpdateResultsPreview();
			}
		}

		/// <summary>
		/// Edit the selected text editor entry.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// </history>
		private void TextEditorsList_DoubleClick(object sender, EventArgs e)
		{
			Point clientPoint = TextEditorsList.PointToClient(Control.MousePosition);
			ListViewItem item = TextEditorsList.GetItemAt(clientPoint.X, clientPoint.Y);

			if (item != null)
			{
				item.Selected = true;
				btnEdit_Click(null, null);
			}
		}

		/// <summary>
		/// Update the text editor button states.
		/// </summary>
		/// <param name="sender">system parameter</param>
		/// <param name="e">system parameter</param>
		/// <history>
		/// [Curtis_Beard]		07/20/2006	Created
		/// </history>
		private void TextEditorsList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetTextEditorsButtonState();
		}

		/// <summary>
		/// Update the results preview.
		/// </summary>
		/// <history>
		/// [Curtis_Beard]		07/21/2006	Created
		/// [Curtis_Beard]		01/24/2012	CHG: allow back color use again since using .Net v2+
		/// [Curtis_Beard]	   02/24/2012	CHG: 3488321, ability to change results font
		/// [Curtis_Beard]	   09/11/2014	FIX, don't play windows chime if matching text is at end of line
		/// [Curtis_Beard]	   04/08/2015	CHG: 81, remove old word wrap and white space options. now in frmMain.
		/// </history>
		private void UpdateResultsPreview()
		{
			string PREVIEW_TEXT = Language.GetGenericText("ResultsPreviewText");
			string PREVIEW_SPACER_TEXT = Language.GetGenericText("ResultsPreviewSpacerText");
			string PREVIEW_MATCH_TEXT = Language.GetGenericText("ResultsPreviewTextMatch");

			string _textToSearch = string.Empty;
			string _searchText = PREVIEW_MATCH_TEXT;
			string _tempLine = string.Empty;

			string _begin = string.Empty;
			string _text = string.Empty;
			string _end = string.Empty;
			int _pos = 0;
			bool _highlight = false;

			if (tbcOptions.SelectedTab == tabResults)
			{
				// Clear the contents
				rtxtResultsPreview.Text = string.Empty;
				rtxtResultsPreview.ForeColor = btnResultsWindowForeColor.SelectedColor;
				rtxtResultsPreview.BackColor = btnResultsWindowBackColor.SelectedColor;

				// Retrieve hit text
				_textToSearch = PREVIEW_TEXT;

				_textToSearch = string.Format("{0}{1}", PREVIEW_SPACER_TEXT, _textToSearch);

				// Set default font
				rtxtResultsPreview.SelectionFont = rtxtResultsPreview.Font;

				_tempLine = _textToSearch;

				// attempt to locate the text in the line
				_pos = _tempLine.IndexOf(_searchText);

				if (_pos > -1)
				{
					do
					{
						_highlight = false;

						//
						// retrieve parts of text
						_begin = _tempLine.Substring(0, _pos);
						_text = _tempLine.Substring(_pos, _searchText.Length);
						_end = _tempLine.Substring(_pos + _searchText.Length);

						// set default color for starting text
						rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
						rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
						rtxtResultsPreview.SelectedText = _begin;

						// do a check to see if begin and end are valid for wholeword searches
						_highlight = true;

						// set highlight color for searched text
						if (_highlight)
						{
							rtxtResultsPreview.SelectionColor = ForeColorButton.SelectedColor;
							rtxtResultsPreview.SelectionBackColor = BackColorButton.SelectedColor;
						}
						rtxtResultsPreview.SelectedText = _text;

						// Check remaining string for other hits in same line
						_pos = _end.IndexOf(_searchText);

						// set default color for end, if no more hits in line
						_tempLine = _end;
						if (_pos < 0)
						{
							rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
							rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
							if (_end.Length > 0)
							{
								rtxtResultsPreview.SelectedText = _end;
							}
						}
					} while (_pos > -1);
				}
				else
				{
					// set default color, no search text found
					rtxtResultsPreview.SelectionColor = btnResultsWindowForeColor.SelectedColor;
					rtxtResultsPreview.SelectionBackColor = btnResultsWindowBackColor.SelectedColor;
					rtxtResultsPreview.SelectedText = _textToSearch;
				}
			}
		}
	}
}