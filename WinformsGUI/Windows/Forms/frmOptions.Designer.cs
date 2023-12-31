﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AstroGrep.Windows.Forms
{
   public partial class frmOptions
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.Container components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      protected override void Dispose(bool disposing)
      {
         if (disposing)
         {
            if (components != null)
            {
               components.Dispose();
            }
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code
      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
			this.tbcOptions = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.ThemeGroup = new System.Windows.Forms.GroupBox();
			this.cboTheme = new System.Windows.Forms.ComboBox();
			this.chkLabelColor = new System.Windows.Forms.CheckBox();
			this.chkSaveExclusionsPosition = new System.Windows.Forms.CheckBox();
			this.chkSaveMessagesPosition = new System.Windows.Forms.CheckBox();
			this.chkSaveSearchOptions = new System.Windows.Forms.CheckBox();
			this.chkShowExclusionErrorMessage = new System.Windows.Forms.CheckBox();
			this.ShortcutGroup = new System.Windows.Forms.GroupBox();
			this.chkStartMenuShortcut = new System.Windows.Forms.CheckBox();
			this.chkDesktopShortcut = new System.Windows.Forms.CheckBox();
			this.chkRightClickOption = new System.Windows.Forms.CheckBox();
			this.LanguageGroup = new System.Windows.Forms.GroupBox();
			this.cboLanguage = new System.Windows.Forms.ComboBox();
			this.cboPathMRUCount = new System.Windows.Forms.ComboBox();
			this.lblStoredPaths = new System.Windows.Forms.Label();
			this.tabFileEncoding = new System.Windows.Forms.TabPage();
			this.cboForceEncoding = new System.Windows.Forms.ComboBox();
			this.lblForceEncoding = new System.Windows.Forms.Label();
			this.btnCacheClear = new System.Windows.Forms.Button();
			this.chkUseEncodingCache = new System.Windows.Forms.CheckBox();
			this.cboPerformance = new System.Windows.Forms.ComboBox();
			this.lblPerformance = new System.Windows.Forms.Label();
			this.chkDetectFileEncoding = new System.Windows.Forms.CheckBox();
			this.btnFileEncodingDelete = new System.Windows.Forms.Button();
			this.btnFileEncodingEdit = new System.Windows.Forms.Button();
			this.btnFileEncodingAdd = new System.Windows.Forms.Button();
			this.lstFiles = new System.Windows.Forms.ListView();
			this.clhEnabled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clhFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.clhEncoding = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabTextEditors = new System.Windows.Forms.TabPage();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.TextEditorsList = new System.Windows.Forms.ListView();
			this.ColumnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnEditor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnArguments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ColumnTabSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tabResults = new System.Windows.Forms.TabPage();
			this.pnlResultsPreview = new System.Windows.Forms.Panel();
			this.lblResultPreview = new System.Windows.Forms.Label();
			this.rtxtResultsPreview = new System.Windows.Forms.RichTextBox();
			this.grpFileList = new System.Windows.Forms.GroupBox();
			this.lblFileCurrentFont = new System.Windows.Forms.Label();
			this.btnFileFindFont = new System.Windows.Forms.Button();
			this.grpResultWindow = new System.Windows.Forms.GroupBox();
			this.lblResultsBeforeAfterCount = new System.Windows.Forms.Label();
			this.lblResultsLongLineCount = new System.Windows.Forms.Label();
			this.numResultsBeforeAfterCount = new System.Windows.Forms.NumericUpDown();
			this.numResultsLongLineCount = new System.Windows.Forms.NumericUpDown();
			this.btnResultsContextForeColor = new AstroGrep.Windows.Controls.ColorButton();
			this.lblResultsContextForeColor = new System.Windows.Forms.Label();
			this.lblCurrentFont = new System.Windows.Forms.Label();
			this.btnFindFont = new System.Windows.Forms.Button();
			this.btnResultsWindowBackColor = new AstroGrep.Windows.Controls.ColorButton();
			this.btnResultsWindowForeColor = new AstroGrep.Windows.Controls.ColorButton();
			this.lblResultsWindowBack = new System.Windows.Forms.Label();
			this.lblResultsWindowFore = new System.Windows.Forms.Label();
			this.grpResultMatch = new System.Windows.Forms.GroupBox();
			this.BackColorButton = new AstroGrep.Windows.Controls.ColorButton();
			this.ForeColorButton = new AstroGrep.Windows.Controls.ColorButton();
			this.BackColorLabel = new System.Windows.Forms.Label();
			this.ForeColorLabel = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbcOptions.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.ThemeGroup.SuspendLayout();
			this.ShortcutGroup.SuspendLayout();
			this.LanguageGroup.SuspendLayout();
			this.tabFileEncoding.SuspendLayout();
			this.tabTextEditors.SuspendLayout();
			this.tabResults.SuspendLayout();
			this.pnlResultsPreview.SuspendLayout();
			this.grpFileList.SuspendLayout();
			this.grpResultWindow.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numResultsBeforeAfterCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numResultsLongLineCount)).BeginInit();
			this.grpResultMatch.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbcOptions
			// 
			this.tbcOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbcOptions.Controls.Add(this.tabGeneral);
			this.tbcOptions.Controls.Add(this.tabFileEncoding);
			this.tbcOptions.Controls.Add(this.tabTextEditors);
			this.tbcOptions.Controls.Add(this.tabResults);
			this.tbcOptions.Location = new System.Drawing.Point(8, 8);
			this.tbcOptions.Name = "tbcOptions";
			this.tbcOptions.SelectedIndex = 0;
			this.tbcOptions.Size = new System.Drawing.Size(561, 390);
			this.tbcOptions.TabIndex = 0;
			this.tbcOptions.SelectedIndexChanged += new System.EventHandler(this.tbcOptions_SelectedIndexChanged);
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.ThemeGroup);
			this.tabGeneral.Controls.Add(this.chkLabelColor);
			this.tabGeneral.Controls.Add(this.chkSaveExclusionsPosition);
			this.tabGeneral.Controls.Add(this.chkSaveMessagesPosition);
			this.tabGeneral.Controls.Add(this.chkSaveSearchOptions);
			this.tabGeneral.Controls.Add(this.chkShowExclusionErrorMessage);
			this.tabGeneral.Controls.Add(this.ShortcutGroup);
			this.tabGeneral.Controls.Add(this.LanguageGroup);
			this.tabGeneral.Controls.Add(this.cboPathMRUCount);
			this.tabGeneral.Controls.Add(this.lblStoredPaths);
			this.tabGeneral.Location = new System.Drawing.Point(4, 24);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(553, 362);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// ThemeGroup
			// 
			this.ThemeGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ThemeGroup.Controls.Add(this.cboTheme);
			this.ThemeGroup.Location = new System.Drawing.Point(281, 154);
			this.ThemeGroup.Name = "ThemeGroup";
			this.ThemeGroup.Size = new System.Drawing.Size(264, 60);
			this.ThemeGroup.TabIndex = 10;
			this.ThemeGroup.TabStop = false;
			this.ThemeGroup.Text = "Theme";
			// 
			// cboTheme
			// 
			this.cboTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTheme.FormattingEnabled = true;
			this.cboTheme.Location = new System.Drawing.Point(16, 24);
			this.cboTheme.Name = "cboTheme";
			this.cboTheme.Size = new System.Drawing.Size(144, 23);
			this.cboTheme.TabIndex = 5;
			// 
			// chkLabelColor
			// 
			this.chkLabelColor.AutoSize = true;
			this.chkLabelColor.Location = new System.Drawing.Point(8, 300);
			this.chkLabelColor.Name = "chkLabelColor";
			this.chkLabelColor.Size = new System.Drawing.Size(191, 19);
			this.chkLabelColor.TabIndex = 13;
			this.chkLabelColor.Text = "Use accent color on &headings.";
			this.chkLabelColor.UseVisualStyleBackColor = true;
			// 
			// chkSaveExclusionsPosition
			// 
			this.chkSaveExclusionsPosition.AutoSize = true;
			this.chkSaveExclusionsPosition.Location = new System.Drawing.Point(8, 276);
			this.chkSaveExclusionsPosition.Name = "chkSaveExclusionsPosition";
			this.chkSaveExclusionsPosition.Size = new System.Drawing.Size(205, 19);
			this.chkSaveExclusionsPosition.TabIndex = 12;
			this.chkSaveExclusionsPosition.Text = "Save exclusions window position";
			this.chkSaveExclusionsPosition.UseVisualStyleBackColor = true;
			// 
			// chkSaveMessagesPosition
			// 
			this.chkSaveMessagesPosition.AutoSize = true;
			this.chkSaveMessagesPosition.Location = new System.Drawing.Point(8, 252);
			this.chkSaveMessagesPosition.Name = "chkSaveMessagesPosition";
			this.chkSaveMessagesPosition.Size = new System.Drawing.Size(204, 19);
			this.chkSaveMessagesPosition.TabIndex = 11;
			this.chkSaveMessagesPosition.Text = "Save messages window position";
			this.chkSaveMessagesPosition.UseVisualStyleBackColor = true;
			// 
			// chkSaveSearchOptions
			// 
			this.chkSaveSearchOptions.AutoSize = true;
			this.chkSaveSearchOptions.Location = new System.Drawing.Point(8, 228);
			this.chkSaveSearchOptions.Name = "chkSaveSearchOptions";
			this.chkSaveSearchOptions.Size = new System.Drawing.Size(175, 19);
			this.chkSaveSearchOptions.TabIndex = 10;
			this.chkSaveSearchOptions.Text = "Save search options on exit";
			this.chkSaveSearchOptions.UseVisualStyleBackColor = true;
			// 
			// chkShowExclusionErrorMessage
			// 
			this.chkShowExclusionErrorMessage.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkShowExclusionErrorMessage.Location = new System.Drawing.Point(8, 324);
			this.chkShowExclusionErrorMessage.Name = "chkShowExclusionErrorMessage";
			this.chkShowExclusionErrorMessage.Size = new System.Drawing.Size(537, 36);
			this.chkShowExclusionErrorMessage.TabIndex = 14;
			this.chkShowExclusionErrorMessage.Text = "Show a &prompt when a search yields items being excluded or an error occurs";
			this.chkShowExclusionErrorMessage.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkShowExclusionErrorMessage.UseVisualStyleBackColor = true;
			// 
			// ShortcutGroup
			// 
			this.ShortcutGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ShortcutGroup.Controls.Add(this.chkStartMenuShortcut);
			this.ShortcutGroup.Controls.Add(this.chkDesktopShortcut);
			this.ShortcutGroup.Controls.Add(this.chkRightClickOption);
			this.ShortcutGroup.Location = new System.Drawing.Point(8, 44);
			this.ShortcutGroup.Name = "ShortcutGroup";
			this.ShortcutGroup.Size = new System.Drawing.Size(537, 104);
			this.ShortcutGroup.TabIndex = 4;
			this.ShortcutGroup.TabStop = false;
			this.ShortcutGroup.Text = "Shortcuts";
			// 
			// chkStartMenuShortcut
			// 
			this.chkStartMenuShortcut.AutoSize = true;
			this.chkStartMenuShortcut.BackColor = System.Drawing.Color.Transparent;
			this.chkStartMenuShortcut.Location = new System.Drawing.Point(6, 72);
			this.chkStartMenuShortcut.Name = "chkStartMenuShortcut";
			this.chkStartMenuShortcut.Size = new System.Drawing.Size(134, 19);
			this.chkStartMenuShortcut.TabIndex = 7;
			this.chkStartMenuShortcut.Text = "Start Menu Shortcut";
			this.chkStartMenuShortcut.UseVisualStyleBackColor = false;
			// 
			// chkDesktopShortcut
			// 
			this.chkDesktopShortcut.AutoSize = true;
			this.chkDesktopShortcut.BackColor = System.Drawing.Color.Transparent;
			this.chkDesktopShortcut.Location = new System.Drawing.Point(6, 48);
			this.chkDesktopShortcut.Name = "chkDesktopShortcut";
			this.chkDesktopShortcut.Size = new System.Drawing.Size(119, 19);
			this.chkDesktopShortcut.TabIndex = 6;
			this.chkDesktopShortcut.Text = "Desktop Shortcut";
			this.chkDesktopShortcut.UseVisualStyleBackColor = false;
			// 
			// chkRightClickOption
			// 
			this.chkRightClickOption.AutoSize = true;
			this.chkRightClickOption.BackColor = System.Drawing.Color.Transparent;
			this.chkRightClickOption.Location = new System.Drawing.Point(6, 24);
			this.chkRightClickOption.Name = "chkRightClickOption";
			this.chkRightClickOption.Size = new System.Drawing.Size(193, 19);
			this.chkRightClickOption.TabIndex = 5;
			this.chkRightClickOption.Text = "Set right-click option on folders";
			this.chkRightClickOption.UseVisualStyleBackColor = false;
			// 
			// LanguageGroup
			// 
			this.LanguageGroup.Controls.Add(this.cboLanguage);
			this.LanguageGroup.Location = new System.Drawing.Point(8, 154);
			this.LanguageGroup.Name = "LanguageGroup";
			this.LanguageGroup.Size = new System.Drawing.Size(267, 60);
			this.LanguageGroup.TabIndex = 8;
			this.LanguageGroup.TabStop = false;
			this.LanguageGroup.Text = "Language";
			// 
			// cboLanguage
			// 
			this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLanguage.Location = new System.Drawing.Point(16, 24);
			this.cboLanguage.Name = "cboLanguage";
			this.cboLanguage.Size = new System.Drawing.Size(144, 23);
			this.cboLanguage.TabIndex = 9;
			// 
			// cboPathMRUCount
			// 
			this.cboPathMRUCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPathMRUCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
			this.cboPathMRUCount.Location = new System.Drawing.Point(8, 12);
			this.cboPathMRUCount.Name = "cboPathMRUCount";
			this.cboPathMRUCount.Size = new System.Drawing.Size(56, 23);
			this.cboPathMRUCount.TabIndex = 3;
			// 
			// lblStoredPaths
			// 
			this.lblStoredPaths.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblStoredPaths.BackColor = System.Drawing.Color.Transparent;
			this.lblStoredPaths.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblStoredPaths.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblStoredPaths.Location = new System.Drawing.Point(80, 12);
			this.lblStoredPaths.Name = "lblStoredPaths";
			this.lblStoredPaths.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblStoredPaths.Size = new System.Drawing.Size(465, 21);
			this.lblStoredPaths.TabIndex = 32;
			this.lblStoredPaths.Text = "Number of most recently used paths to store";
			this.lblStoredPaths.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabFileEncoding
			// 
			this.tabFileEncoding.Controls.Add(this.cboForceEncoding);
			this.tabFileEncoding.Controls.Add(this.lblForceEncoding);
			this.tabFileEncoding.Controls.Add(this.btnCacheClear);
			this.tabFileEncoding.Controls.Add(this.chkUseEncodingCache);
			this.tabFileEncoding.Controls.Add(this.cboPerformance);
			this.tabFileEncoding.Controls.Add(this.lblPerformance);
			this.tabFileEncoding.Controls.Add(this.chkDetectFileEncoding);
			this.tabFileEncoding.Controls.Add(this.btnFileEncodingDelete);
			this.tabFileEncoding.Controls.Add(this.btnFileEncodingEdit);
			this.tabFileEncoding.Controls.Add(this.btnFileEncodingAdd);
			this.tabFileEncoding.Controls.Add(this.lstFiles);
			this.tabFileEncoding.Location = new System.Drawing.Point(4, 24);
			this.tabFileEncoding.Name = "tabFileEncoding";
			this.tabFileEncoding.Size = new System.Drawing.Size(553, 362);
			this.tabFileEncoding.TabIndex = 4;
			this.tabFileEncoding.Text = "File Encoding";
			this.tabFileEncoding.UseVisualStyleBackColor = true;
			// 
			// cboForceEncoding
			// 
			this.cboForceEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboForceEncoding.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboForceEncoding.FormattingEnabled = true;
			this.cboForceEncoding.Location = new System.Drawing.Point(144, 92);
			this.cboForceEncoding.Name = "cboForceEncoding";
			this.cboForceEncoding.Size = new System.Drawing.Size(254, 23);
			this.cboForceEncoding.TabIndex = 5;
			this.cboForceEncoding.DropDown += new System.EventHandler(this.cboForceEncoding_DropDown);
			// 
			// lblForceEncoding
			// 
			this.lblForceEncoding.AutoSize = true;
			this.lblForceEncoding.Location = new System.Drawing.Point(5, 95);
			this.lblForceEncoding.Name = "lblForceEncoding";
			this.lblForceEncoding.Size = new System.Drawing.Size(93, 15);
			this.lblForceEncoding.TabIndex = 44;
			this.lblForceEncoding.Text = "Force Encoding";
			// 
			// btnCacheClear
			// 
			this.btnCacheClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCacheClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCacheClear.Location = new System.Drawing.Point(415, 63);
			this.btnCacheClear.Name = "btnCacheClear";
			this.btnCacheClear.Size = new System.Drawing.Size(130, 25);
			this.btnCacheClear.TabIndex = 4;
			this.btnCacheClear.Text = "Clear Cache";
			this.btnCacheClear.UseVisualStyleBackColor = true;
			this.btnCacheClear.Click += new System.EventHandler(this.btnCacheClear_Click);
			// 
			// chkUseEncodingCache
			// 
			this.chkUseEncodingCache.AutoSize = true;
			this.chkUseEncodingCache.Location = new System.Drawing.Point(8, 67);
			this.chkUseEncodingCache.Name = "chkUseEncodingCache";
			this.chkUseEncodingCache.Size = new System.Drawing.Size(231, 19);
			this.chkUseEncodingCache.TabIndex = 3;
			this.chkUseEncodingCache.Text = "Enable cache for detected encodings.";
			this.chkUseEncodingCache.UseVisualStyleBackColor = true;
			// 
			// cboPerformance
			// 
			this.cboPerformance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPerformance.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cboPerformance.FormattingEnabled = true;
			this.cboPerformance.Location = new System.Drawing.Point(144, 36);
			this.cboPerformance.Name = "cboPerformance";
			this.cboPerformance.Size = new System.Drawing.Size(121, 23);
			this.cboPerformance.TabIndex = 2;
			// 
			// lblPerformance
			// 
			this.lblPerformance.AutoSize = true;
			this.lblPerformance.Location = new System.Drawing.Point(5, 40);
			this.lblPerformance.Name = "lblPerformance";
			this.lblPerformance.Size = new System.Drawing.Size(78, 15);
			this.lblPerformance.TabIndex = 40;
			this.lblPerformance.Text = "Performance";
			// 
			// chkDetectFileEncoding
			// 
			this.chkDetectFileEncoding.AutoSize = true;
			this.chkDetectFileEncoding.Location = new System.Drawing.Point(8, 10);
			this.chkDetectFileEncoding.Name = "chkDetectFileEncoding";
			this.chkDetectFileEncoding.Size = new System.Drawing.Size(137, 19);
			this.chkDetectFileEncoding.TabIndex = 1;
			this.chkDetectFileEncoding.Text = "Detect file encoding.";
			this.chkDetectFileEncoding.UseVisualStyleBackColor = true;
			this.chkDetectFileEncoding.CheckedChanged += new System.EventHandler(this.chkDetectFileEncoding_CheckedChanged);
			// 
			// btnFileEncodingDelete
			// 
			this.btnFileEncodingDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFileEncodingDelete.Location = new System.Drawing.Point(200, 322);
			this.btnFileEncodingDelete.Name = "btnFileEncodingDelete";
			this.btnFileEncodingDelete.Size = new System.Drawing.Size(90, 25);
			this.btnFileEncodingDelete.TabIndex = 9;
			this.btnFileEncodingDelete.Text = "&Delete";
			this.btnFileEncodingDelete.UseVisualStyleBackColor = true;
			this.btnFileEncodingDelete.Click += new System.EventHandler(this.btnFileEncodingDelete_Click);
			// 
			// btnFileEncodingEdit
			// 
			this.btnFileEncodingEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFileEncodingEdit.Location = new System.Drawing.Point(104, 322);
			this.btnFileEncodingEdit.Name = "btnFileEncodingEdit";
			this.btnFileEncodingEdit.Size = new System.Drawing.Size(90, 25);
			this.btnFileEncodingEdit.TabIndex = 8;
			this.btnFileEncodingEdit.Text = "&Edit...";
			this.btnFileEncodingEdit.UseVisualStyleBackColor = true;
			this.btnFileEncodingEdit.Click += new System.EventHandler(this.btnFileEncodingEdit_Click);
			// 
			// btnFileEncodingAdd
			// 
			this.btnFileEncodingAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFileEncodingAdd.Location = new System.Drawing.Point(8, 322);
			this.btnFileEncodingAdd.Name = "btnFileEncodingAdd";
			this.btnFileEncodingAdd.Size = new System.Drawing.Size(90, 25);
			this.btnFileEncodingAdd.TabIndex = 7;
			this.btnFileEncodingAdd.Text = "&Add...";
			this.btnFileEncodingAdd.UseVisualStyleBackColor = true;
			this.btnFileEncodingAdd.Click += new System.EventHandler(this.btnFileEncodingAdd_Click);
			// 
			// lstFiles
			// 
			this.lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstFiles.CheckBoxes = true;
			this.lstFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhEnabled,
            this.clhFile,
            this.clhEncoding});
			this.lstFiles.FullRowSelect = true;
			this.lstFiles.HideSelection = false;
			this.lstFiles.Location = new System.Drawing.Point(8, 121);
			this.lstFiles.Name = "lstFiles";
			this.lstFiles.Size = new System.Drawing.Size(537, 189);
			this.lstFiles.TabIndex = 6;
			this.lstFiles.UseCompatibleStateImageBehavior = false;
			this.lstFiles.View = System.Windows.Forms.View.Details;
			this.lstFiles.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstFiles_ColumnClick);
			this.lstFiles.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstFiles_ItemCheck);
			this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
			this.lstFiles.DoubleClick += new System.EventHandler(this.lstFiles_DoubleClick);
			this.lstFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstFiles_KeyDown);
			this.lstFiles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseDown);
			this.lstFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstFiles_MouseUp);
			// 
			// clhEnabled
			// 
			this.clhEnabled.Text = "Enabled";
			this.clhEnabled.Width = 69;
			// 
			// clhFile
			// 
			this.clhFile.Text = "File";
			this.clhFile.Width = 320;
			// 
			// clhEncoding
			// 
			this.clhEncoding.Text = "Encoding";
			this.clhEncoding.Width = 132;
			// 
			// tabTextEditors
			// 
			this.tabTextEditors.Controls.Add(this.btnEdit);
			this.tabTextEditors.Controls.Add(this.btnRemove);
			this.tabTextEditors.Controls.Add(this.btnAdd);
			this.tabTextEditors.Controls.Add(this.TextEditorsList);
			this.tabTextEditors.Location = new System.Drawing.Point(4, 24);
			this.tabTextEditors.Name = "tabTextEditors";
			this.tabTextEditors.Size = new System.Drawing.Size(553, 362);
			this.tabTextEditors.TabIndex = 1;
			this.tabTextEditors.Text = "Text Editors";
			this.tabTextEditors.UseVisualStyleBackColor = true;
			// 
			// btnEdit
			// 
			this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnEdit.Location = new System.Drawing.Point(104, 322);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(90, 25);
			this.btnEdit.TabIndex = 3;
			this.btnEdit.Text = "&Edit...";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRemove.Location = new System.Drawing.Point(200, 322);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(90, 25);
			this.btnRemove.TabIndex = 4;
			this.btnRemove.Text = "&Delete";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Location = new System.Drawing.Point(8, 322);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(90, 25);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "&Add...";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// TextEditorsList
			// 
			this.TextEditorsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TextEditorsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnType,
            this.ColumnEditor,
            this.ColumnArguments,
            this.ColumnTabSize});
			this.TextEditorsList.FullRowSelect = true;
			this.TextEditorsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.TextEditorsList.HideSelection = false;
			this.TextEditorsList.Location = new System.Drawing.Point(8, 8);
			this.TextEditorsList.MultiSelect = false;
			this.TextEditorsList.Name = "TextEditorsList";
			this.TextEditorsList.Size = new System.Drawing.Size(537, 302);
			this.TextEditorsList.TabIndex = 1;
			this.TextEditorsList.UseCompatibleStateImageBehavior = false;
			this.TextEditorsList.View = System.Windows.Forms.View.Details;
			this.TextEditorsList.SelectedIndexChanged += new System.EventHandler(this.TextEditorsList_SelectedIndexChanged);
			this.TextEditorsList.DoubleClick += new System.EventHandler(this.TextEditorsList_DoubleClick);
			// 
			// ColumnType
			// 
			this.ColumnType.Text = "File Type";
			this.ColumnType.Width = 100;
			// 
			// ColumnEditor
			// 
			this.ColumnEditor.Text = "Text Editor";
			this.ColumnEditor.Width = 240;
			// 
			// ColumnArguments
			// 
			this.ColumnArguments.Text = "Command Line";
			this.ColumnArguments.Width = 113;
			// 
			// ColumnTabSize
			// 
			this.ColumnTabSize.Text = "Tab Size";
			this.ColumnTabSize.Width = 75;
			// 
			// tabResults
			// 
			this.tabResults.Controls.Add(this.pnlResultsPreview);
			this.tabResults.Controls.Add(this.grpFileList);
			this.tabResults.Controls.Add(this.grpResultWindow);
			this.tabResults.Controls.Add(this.grpResultMatch);
			this.tabResults.Location = new System.Drawing.Point(4, 24);
			this.tabResults.Name = "tabResults";
			this.tabResults.Size = new System.Drawing.Size(553, 362);
			this.tabResults.TabIndex = 2;
			this.tabResults.Text = "Results";
			this.tabResults.UseVisualStyleBackColor = true;
			// 
			// pnlResultsPreview
			// 
			this.pnlResultsPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlResultsPreview.Controls.Add(this.lblResultPreview);
			this.pnlResultsPreview.Controls.Add(this.rtxtResultsPreview);
			this.pnlResultsPreview.Location = new System.Drawing.Point(8, 288);
			this.pnlResultsPreview.Name = "pnlResultsPreview";
			this.pnlResultsPreview.Size = new System.Drawing.Size(537, 61);
			this.pnlResultsPreview.TabIndex = 28;
			// 
			// lblResultPreview
			// 
			this.lblResultPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblResultPreview.Location = new System.Drawing.Point(0, 0);
			this.lblResultPreview.Name = "lblResultPreview";
			this.lblResultPreview.Size = new System.Drawing.Size(537, 16);
			this.lblResultPreview.TabIndex = 28;
			this.lblResultPreview.Text = "Results Preview";
			// 
			// rtxtResultsPreview
			// 
			this.rtxtResultsPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtxtResultsPreview.Location = new System.Drawing.Point(0, 21);
			this.rtxtResultsPreview.Name = "rtxtResultsPreview";
			this.rtxtResultsPreview.ReadOnly = true;
			this.rtxtResultsPreview.Size = new System.Drawing.Size(537, 40);
			this.rtxtResultsPreview.TabIndex = 27;
			this.rtxtResultsPreview.TabStop = false;
			this.rtxtResultsPreview.Text = "(21)  Example results line and, match, displayed";
			// 
			// grpFileList
			// 
			this.grpFileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpFileList.Controls.Add(this.lblFileCurrentFont);
			this.grpFileList.Controls.Add(this.btnFileFindFont);
			this.grpFileList.Location = new System.Drawing.Point(8, 3);
			this.grpFileList.Name = "grpFileList";
			this.grpFileList.Size = new System.Drawing.Size(537, 66);
			this.grpFileList.TabIndex = 1;
			this.grpFileList.TabStop = false;
			this.grpFileList.Text = "File List";
			// 
			// lblFileCurrentFont
			// 
			this.lblFileCurrentFont.AutoSize = true;
			this.lblFileCurrentFont.Location = new System.Drawing.Point(8, 34);
			this.lblFileCurrentFont.Name = "lblFileCurrentFont";
			this.lblFileCurrentFont.Size = new System.Drawing.Size(74, 15);
			this.lblFileCurrentFont.TabIndex = 1;
			this.lblFileCurrentFont.Text = "Current Font";
			// 
			// btnFileFindFont
			// 
			this.btnFileFindFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFileFindFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFileFindFont.Location = new System.Drawing.Point(396, 29);
			this.btnFileFindFont.Name = "btnFileFindFont";
			this.btnFileFindFont.Size = new System.Drawing.Size(120, 25);
			this.btnFileFindFont.TabIndex = 2;
			this.btnFileFindFont.Text = "Find Font";
			this.btnFileFindFont.UseVisualStyleBackColor = true;
			this.btnFileFindFont.Click += new System.EventHandler(this.btnFileFindFont_Click);
			// 
			// grpResultWindow
			// 
			this.grpResultWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpResultWindow.Controls.Add(this.lblResultsBeforeAfterCount);
			this.grpResultWindow.Controls.Add(this.lblResultsLongLineCount);
			this.grpResultWindow.Controls.Add(this.numResultsBeforeAfterCount);
			this.grpResultWindow.Controls.Add(this.numResultsLongLineCount);
			this.grpResultWindow.Controls.Add(this.btnResultsContextForeColor);
			this.grpResultWindow.Controls.Add(this.lblResultsContextForeColor);
			this.grpResultWindow.Controls.Add(this.lblCurrentFont);
			this.grpResultWindow.Controls.Add(this.btnFindFont);
			this.grpResultWindow.Controls.Add(this.btnResultsWindowBackColor);
			this.grpResultWindow.Controls.Add(this.btnResultsWindowForeColor);
			this.grpResultWindow.Controls.Add(this.lblResultsWindowBack);
			this.grpResultWindow.Controls.Add(this.lblResultsWindowFore);
			this.grpResultWindow.Location = new System.Drawing.Point(8, 140);
			this.grpResultWindow.Name = "grpResultWindow";
			this.grpResultWindow.Size = new System.Drawing.Size(537, 142);
			this.grpResultWindow.TabIndex = 6;
			this.grpResultWindow.TabStop = false;
			this.grpResultWindow.Text = "Results Window";
			// 
			// lblResultsBeforeAfterCount
			// 
			this.lblResultsBeforeAfterCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblResultsBeforeAfterCount.Location = new System.Drawing.Point(305, 75);
			this.lblResultsBeforeAfterCount.Name = "lblResultsBeforeAfterCount";
			this.lblResultsBeforeAfterCount.Size = new System.Drawing.Size(136, 27);
			this.lblResultsBeforeAfterCount.TabIndex = 30;
			this.lblResultsBeforeAfterCount.Text = "Before/After Count";
			this.lblResultsBeforeAfterCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblResultsLongLineCount
			// 
			this.lblResultsLongLineCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblResultsLongLineCount.Location = new System.Drawing.Point(305, 49);
			this.lblResultsLongLineCount.Name = "lblResultsLongLineCount";
			this.lblResultsLongLineCount.Size = new System.Drawing.Size(136, 27);
			this.lblResultsLongLineCount.TabIndex = 29;
			this.lblResultsLongLineCount.Text = "Long Line Count";
			this.lblResultsLongLineCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// numResultsBeforeAfterCount
			// 
			this.numResultsBeforeAfterCount.Location = new System.Drawing.Point(442, 78);
			this.numResultsBeforeAfterCount.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.numResultsBeforeAfterCount.Name = "numResultsBeforeAfterCount";
			this.numResultsBeforeAfterCount.Size = new System.Drawing.Size(74, 21);
			this.numResultsBeforeAfterCount.TabIndex = 11;
			this.numResultsBeforeAfterCount.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// numResultsLongLineCount
			// 
			this.numResultsLongLineCount.Location = new System.Drawing.Point(442, 53);
			this.numResultsLongLineCount.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
			this.numResultsLongLineCount.Name = "numResultsLongLineCount";
			this.numResultsLongLineCount.Size = new System.Drawing.Size(74, 21);
			this.numResultsLongLineCount.TabIndex = 10;
			this.numResultsLongLineCount.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			// 
			// btnResultsContextForeColor
			// 
			this.btnResultsContextForeColor.ForeColor = System.Drawing.Color.Silver;
			this.btnResultsContextForeColor.Location = new System.Drawing.Point(144, 53);
			this.btnResultsContextForeColor.Name = "btnResultsContextForeColor";
			this.btnResultsContextForeColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.btnResultsContextForeColor.Size = new System.Drawing.Size(75, 23);
			this.btnResultsContextForeColor.TabIndex = 9;
			// 
			// lblResultsContextForeColor
			// 
			this.lblResultsContextForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblResultsContextForeColor.Location = new System.Drawing.Point(8, 49);
			this.lblResultsContextForeColor.Name = "lblResultsContextForeColor";
			this.lblResultsContextForeColor.Size = new System.Drawing.Size(136, 27);
			this.lblResultsContextForeColor.TabIndex = 25;
			this.lblResultsContextForeColor.Text = "Context Fore Color";
			this.lblResultsContextForeColor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCurrentFont
			// 
			this.lblCurrentFont.AutoSize = true;
			this.lblCurrentFont.Location = new System.Drawing.Point(8, 110);
			this.lblCurrentFont.Name = "lblCurrentFont";
			this.lblCurrentFont.Size = new System.Drawing.Size(74, 15);
			this.lblCurrentFont.TabIndex = 24;
			this.lblCurrentFont.Text = "Current Font";
			// 
			// btnFindFont
			// 
			this.btnFindFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFindFont.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnFindFont.Location = new System.Drawing.Point(396, 105);
			this.btnFindFont.Name = "btnFindFont";
			this.btnFindFont.Size = new System.Drawing.Size(120, 25);
			this.btnFindFont.TabIndex = 12;
			this.btnFindFont.Text = "&Find Font";
			this.btnFindFont.UseVisualStyleBackColor = true;
			this.btnFindFont.Click += new System.EventHandler(this.btnFindFont_Click);
			// 
			// btnResultsWindowBackColor
			// 
			this.btnResultsWindowBackColor.Location = new System.Drawing.Point(441, 24);
			this.btnResultsWindowBackColor.Name = "btnResultsWindowBackColor";
			this.btnResultsWindowBackColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.btnResultsWindowBackColor.Size = new System.Drawing.Size(75, 23);
			this.btnResultsWindowBackColor.TabIndex = 8;
			// 
			// btnResultsWindowForeColor
			// 
			this.btnResultsWindowForeColor.Location = new System.Drawing.Point(144, 24);
			this.btnResultsWindowForeColor.Name = "btnResultsWindowForeColor";
			this.btnResultsWindowForeColor.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.btnResultsWindowForeColor.Size = new System.Drawing.Size(75, 23);
			this.btnResultsWindowForeColor.TabIndex = 7;
			// 
			// lblResultsWindowBack
			// 
			this.lblResultsWindowBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblResultsWindowBack.Location = new System.Drawing.Point(305, 22);
			this.lblResultsWindowBack.Name = "lblResultsWindowBack";
			this.lblResultsWindowBack.Size = new System.Drawing.Size(136, 27);
			this.lblResultsWindowBack.TabIndex = 20;
			this.lblResultsWindowBack.Text = "Back Color";
			this.lblResultsWindowBack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblResultsWindowFore
			// 
			this.lblResultsWindowFore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lblResultsWindowFore.Location = new System.Drawing.Point(8, 22);
			this.lblResultsWindowFore.Name = "lblResultsWindowFore";
			this.lblResultsWindowFore.Size = new System.Drawing.Size(136, 27);
			this.lblResultsWindowFore.TabIndex = 19;
			this.lblResultsWindowFore.Text = "Fore Color";
			this.lblResultsWindowFore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpResultMatch
			// 
			this.grpResultMatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpResultMatch.Controls.Add(this.BackColorButton);
			this.grpResultMatch.Controls.Add(this.ForeColorButton);
			this.grpResultMatch.Controls.Add(this.BackColorLabel);
			this.grpResultMatch.Controls.Add(this.ForeColorLabel);
			this.grpResultMatch.Location = new System.Drawing.Point(8, 78);
			this.grpResultMatch.Name = "grpResultMatch";
			this.grpResultMatch.Size = new System.Drawing.Size(537, 56);
			this.grpResultMatch.TabIndex = 3;
			this.grpResultMatch.TabStop = false;
			this.grpResultMatch.Text = "Results Match";
			// 
			// BackColorButton
			// 
			this.BackColorButton.Location = new System.Drawing.Point(441, 24);
			this.BackColorButton.Name = "BackColorButton";
			this.BackColorButton.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.BackColorButton.Size = new System.Drawing.Size(75, 23);
			this.BackColorButton.TabIndex = 5;
			// 
			// ForeColorButton
			// 
			this.ForeColorButton.Location = new System.Drawing.Point(144, 24);
			this.ForeColorButton.Name = "ForeColorButton";
			this.ForeColorButton.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.ForeColorButton.Size = new System.Drawing.Size(75, 23);
			this.ForeColorButton.TabIndex = 4;
			// 
			// BackColorLabel
			// 
			this.BackColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.BackColorLabel.Location = new System.Drawing.Point(305, 22);
			this.BackColorLabel.Name = "BackColorLabel";
			this.BackColorLabel.Size = new System.Drawing.Size(136, 27);
			this.BackColorLabel.TabIndex = 16;
			this.BackColorLabel.Text = "Back Color";
			this.BackColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ForeColorLabel
			// 
			this.ForeColorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.ForeColorLabel.Location = new System.Drawing.Point(8, 22);
			this.ForeColorLabel.Name = "ForeColorLabel";
			this.ForeColorLabel.Size = new System.Drawing.Size(136, 27);
			this.ForeColorLabel.TabIndex = 15;
			this.ForeColorLabel.Text = "Fore Color";
			this.ForeColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(370, 406);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(90, 25);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(475, 406);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 25);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmOptions
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(575, 443);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbcOptions);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.frmOptions_Load);
			this.tbcOptions.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.ThemeGroup.ResumeLayout(false);
			this.ShortcutGroup.ResumeLayout(false);
			this.ShortcutGroup.PerformLayout();
			this.LanguageGroup.ResumeLayout(false);
			this.tabFileEncoding.ResumeLayout(false);
			this.tabFileEncoding.PerformLayout();
			this.tabTextEditors.ResumeLayout(false);
			this.tabResults.ResumeLayout(false);
			this.pnlResultsPreview.ResumeLayout(false);
			this.grpFileList.ResumeLayout(false);
			this.grpFileList.PerformLayout();
			this.grpResultWindow.ResumeLayout(false);
			this.grpResultWindow.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numResultsBeforeAfterCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numResultsLongLineCount)).EndInit();
			this.grpResultMatch.ResumeLayout(false);
			this.ResumeLayout(false);

      }
      #endregion

      private System.Windows.Forms.TabControl tbcOptions;
      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.TabPage tabGeneral;
      private System.Windows.Forms.TabPage tabTextEditors;
      private System.Windows.Forms.TabPage tabResults;
      private System.Windows.Forms.GroupBox grpResultWindow;
      private AstroGrep.Windows.Controls.ColorButton btnResultsWindowBackColor;
      private AstroGrep.Windows.Controls.ColorButton btnResultsWindowForeColor;
      private System.Windows.Forms.Label lblResultsWindowBack;
      private System.Windows.Forms.Label lblResultsWindowFore;
      private System.Windows.Forms.GroupBox grpResultMatch;
      private AstroGrep.Windows.Controls.ColorButton BackColorButton;
      private AstroGrep.Windows.Controls.ColorButton ForeColorButton;
      private System.Windows.Forms.Label BackColorLabel;
      private System.Windows.Forms.Label ForeColorLabel;
      private System.Windows.Forms.GroupBox LanguageGroup;
      private System.Windows.Forms.ComboBox cboLanguage;
      private System.Windows.Forms.ComboBox cboPathMRUCount;
      private System.Windows.Forms.Label lblStoredPaths;
      private System.Windows.Forms.Button btnEdit;
      private System.Windows.Forms.Button btnRemove;
      private System.Windows.Forms.Button btnAdd;
      private System.Windows.Forms.ListView TextEditorsList;
      private System.Windows.Forms.ColumnHeader ColumnType;
      private System.Windows.Forms.ColumnHeader ColumnEditor;
      private System.Windows.Forms.ColumnHeader ColumnArguments;
      private System.Windows.Forms.GroupBox ShortcutGroup;
      private System.Windows.Forms.CheckBox chkStartMenuShortcut;
      private System.Windows.Forms.CheckBox chkDesktopShortcut;
      private System.Windows.Forms.CheckBox chkRightClickOption;
      private Label lblCurrentFont;
      private Button btnFindFont;
      private CheckBox chkShowExclusionErrorMessage;
      private CheckBox chkSaveSearchOptions;
      private GroupBox grpFileList;
      private Label lblFileCurrentFont;
      private Button btnFileFindFont;
      private ColumnHeader ColumnTabSize;
      private TabPage tabFileEncoding;
      private Button btnFileEncodingDelete;
      private Button btnFileEncodingEdit;
      private Button btnFileEncodingAdd;
      private ListView lstFiles;
      private ColumnHeader clhEnabled;
      private ColumnHeader clhFile;
      private ColumnHeader clhEncoding;
      private CheckBox chkDetectFileEncoding;
      private CheckBox chkSaveMessagesPosition;
      private Controls.ColorButton btnResultsContextForeColor;
      private Label lblResultsContextForeColor;
      private ComboBox cboPerformance;
      private Label lblPerformance;
      private CheckBox chkUseEncodingCache;
      private Button btnCacheClear;
      private CheckBox chkSaveExclusionsPosition;
      private CheckBox chkLabelColor;
      private Panel pnlResultsPreview;
      private Label lblResultPreview;
      private RichTextBox rtxtResultsPreview;
      private ComboBox cboForceEncoding;
      private Label lblForceEncoding;
      private Label lblResultsBeforeAfterCount;
      private Label lblResultsLongLineCount;
      private NumericUpDown numResultsBeforeAfterCount;
      private NumericUpDown numResultsLongLineCount;
		private GroupBox ThemeGroup;
		private ComboBox cboTheme;
	}
}
