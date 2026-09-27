using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AstroGrep.Windows.Forms
{
	/// <summary>
	/// Base form for all forms to inherit from with built-in dark mode support.
	/// </summary>
	public class BaseForm : Form
	{
			/// <summary>
			/// Determines if the common logic for color changes is applied at the form level.
			/// </summary>
			[System.ComponentModel.Browsable(false)]
			public bool ProcessColorChange { get; set; } = true;

		#if NET10_0_WINDOWS
			/// <summary>
			/// Gets the link color for the current theme (light or dark mode).
			/// </summary>
			private static Color LinkColor
				=> Application.IsDarkModeEnabled
					? Color.FromArgb(0x5BA3E6)  // Light blue for dark mode
					: Color.FromArgb(0x0563C1); // Dark blue for light mode

			/// <summary>
			/// Gets the active link color for the current theme (light or dark mode).
			/// </summary>
			private static Color ActiveLinkColor
				=> Application.IsDarkModeEnabled
					? Color.FromArgb(0x64B4FF)  // Brighter blue for dark mode
					: Color.FromArgb(0x0A66CC); // Brighter blue for light mode
		#else
			/// <summary>
			/// Gets the link color for net48 (fixed to light mode colors).
			/// </summary>
			private static Color LinkColor => Color.FromArgb(0x0563C1); // Dark blue

			/// <summary>
			/// Gets the active link color for net48 (fixed to light mode colors).
			/// </summary>
			private static Color ActiveLinkColor => Color.FromArgb(0x0A66CC); // Brighter blue
		#endif

		/// <summary>
		/// Force a <see cref="Color"/> for the <paramref name="ctrl"/> and its children.
		/// </summary>
		/// <param name="ctrl"><see cref="Control"/> to set</param>
		/// <param name="backColor"><see cref="Color"/> to set</param>
		/// <param name="processChildren">true (default) to process children controls, false to only do given <paramref name="ctrl"/></param>
		public void ForceBackColor(Control ctrl, Color backColor, bool processChildren = true)
		{
			// Set color
			ctrl.BackColor = backColor;

			// Process the child controls
			if (processChildren && ctrl.HasChildren)
			{
				foreach (Control child in ctrl.Controls)
				{
					ForceBackColor(child, backColor);
				}
			}
		}

		/// <summary>
		/// Load the current theme for the <paramref name="ctrl"/> and its children.
		/// </summary>
		/// <param name="ctrl"><see cref="Control"/> to set</param>
		public void LoadTheme(Control ctrl)
		{
			if (ProcessColorChange)
			{
				// Don't change these strip based controls here (they have their own rendering)
				if (ctrl is ToolStrip || ctrl is MenuStrip || ctrl is StatusStrip)
				{
					return;
				}

				// Button - keep visual style for consistency
				if (ctrl is Button btn)
				{
					// Special changes just for main form
					if (ctrl.FindForm() is frmMain)
					{
						btn.UseVisualStyleBackColor = true;
						btn.FlatStyle = FlatStyle.System;
					}
					else
					{
						return;
					}
				}

				// Standard control colors using SystemColors (auto-adapts to dark mode)
				ctrl.ForeColor = SystemColors.ControlText;
				ctrl.BackColor = SystemColors.Control;

				// TextBox and ComboBox get Window colors
				if (ctrl is TextBox || ctrl is ComboBox)
				{
					ctrl.BackColor = SystemColors.Window;
				}

				// frmMain special handling
				if (ctrl.FindForm() is frmMain)
				{
					if (GetControlStyle(ctrl, ControlStyles.SupportsTransparentBackColor))
					{
						ctrl.BackColor = Color.Transparent;
					}
					else if (ctrl is Label || ctrl is ComboBox || ctrl is Controls.ComboBoxEx)
					{
						ctrl.BackColor = SystemColors.Window;
					}
				}

				// CheckBox special changes (use standard style with dark mode support)
				if (ctrl is CheckBox chk)
				{
					if (chk.FlatStyle != FlatStyle.Standard)
					{
						chk.FlatStyle = FlatStyle.Standard;
					}

					if (GetControlStyle(ctrl, ControlStyles.SupportsTransparentBackColor))
					{
						ctrl.BackColor = Color.Transparent;
					}
				}

				// Labels in frmOptions
				if (ctrl is Label && ctrl.FindForm() is frmOptions)
				{
					if (GetControlStyle(ctrl, ControlStyles.SupportsTransparentBackColor))
					{
						ctrl.BackColor = Color.Transparent;
					}
				}

				// LinkLabel with custom link colors for dark/light mode
				if (ctrl is LinkLabel lnk)
				{
					lnk.ForeColor = SystemColors.ControlText;
					lnk.LinkColor = LinkColor;
					lnk.ActiveLinkColor = ActiveLinkColor;
				}

				// ListView gets window background
				if (ctrl is ListView lsv)
				{
					lsv.BackColor = SystemColors.Window;
					lsv.ForeColor = SystemColors.ControlText;
				}

				// Panel handling for frmMain
				if (ctrl is Panel pnl)
				{
					if (pnl.FindForm() is frmMain)
					{
						pnl.BackColor = SystemColors.Window;
					}
				}

				// Recursively process child controls
				if (ctrl.HasChildren)
				{
					foreach (Control child in ctrl.Controls)
					{
						LoadTheme(child);
					}
				}
			}
		}

		/// <summary>
		/// Load event for all forms
		/// </summary>
		/// <param name="e">system parameter</param>
		protected override void OnLoad(EventArgs e)
		{
			if (!DesignMode)
			{
				LoadTheme(this);
			}

			base.OnLoad(e);
		}

		/// <summary>
		/// Determines if given <see cref="ControlStyles"/> <paramref name="flags"/> are available on the given <paramref name="control"/>.
		/// </summary>
		/// <param name="control"><see cref="Control"/> to check</param>
		/// <param name="flags"><see cref="ControlStyles"/> to check</param>
		/// <returns>true if found, false otherwise</returns>
		private bool GetControlStyle(Control control, ControlStyles flags)
		{
			Type type = control.GetType();
			BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
			MethodInfo method = type.GetMethod("GetStyle", bindingFlags);
			object[] param = { flags };
			return (bool)method.Invoke(control, param);
		}
	}
}