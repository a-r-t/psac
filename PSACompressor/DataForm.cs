using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PSACompressor
{
	public class DataForm : Form
	{
		private int di1;

		private int di2;

		private string drd;

		private IContainer components = null;

		private Button DFClose;

		private ListView DFList;

		private ContextMenuStrip DFStrip;

		private ToolStripMenuItem DFNoSelect;

		private ToolStripMenuItem DFAllSelect;

		private ToolStripMenuItem DFCopytxt;

		public string[] dstr
		{
			get;
			set;
		}

		public DataForm()
		{
			InitializeComponent();
		}

		private void DataForm_Load(object sender, EventArgs e)
		{
			di1 = 0;
			while (di1 < 2 && Uri.IsHexDigit(dstr[0][di1]))
			{
				di1++;
			}
			string[] array;
			if (di1 != 2)
			{
				base.Width = 562;
				array = new string[3];
				di2 = dstr.Length;
				Text = "Error Datas";
				DFList.Columns.Add("Error");
				DFList.Columns.Add("Event");
				DFList.Columns.Add("Event List");
				DFList.Columns[0].Width = 170;
				DFList.Columns[1].Width = 130;
				DFList.Columns[2].Width = 225;
				for (di1 = 0; di1 < di2; di1++)
				{
					int num = dstr[di1].Length - 5;
					int i;
					for (i = 1; i < num; i++)
					{
						if (dstr[di1][i] == '\r')
						{
							if (dstr[di1][i + 1] == '\n')
							{
								break;
							}
							di1++;
						}
					}
					array[0] = dstr[di1].Substring(0, i);
					i += 2;
					int j;
					for (j = i; j < num; j++)
					{
						if (dstr[di1][j] == '\r')
						{
							if (dstr[di1][j + 1] == '\n')
							{
								break;
							}
							di1++;
						}
					}
					if (j == i)
					{
						array[1] = "";
					}
					else
					{
						array[1] = dstr[di1].Substring(i, j - i);
					}
					array[2] = dstr[di1].Substring(j + 2);
					num = array[2].Length;
					if (num > 15)
					{
						drd = array[2].Substring(num - 14);
						if (drd == " - Offset:0000")
						{
							drd = array[2].Substring(0, num - 14);
							array[2] = drd;
						}
					}
					DFList.Items.Add(new ListViewItem(array));
				}
				return;
			}
			di2 = dstr[0].Length - 20;
			for (di1 = 8; di1 < di2; di1++)
			{
				if (dstr[0][di1] == '\r')
				{
					if (dstr[0][di1 + 1] == '\n')
					{
						break;
					}
					di1++;
				}
			}
			if (di1 == di2)
			{
				base.Width = 332;
				string[] array2 = new string[2];
				di2 = dstr.Length;
				Text = "Unknown Events";
				DFList.Columns.Add("Event");
				DFList.Columns.Add("Event List");
				DFList.Columns[0].Width = 70;
				DFList.Columns[1].Width = 225;
				for (di1 = 0; di1 < di2; di1++)
				{
					array2[0] = dstr[di1].Substring(0, 8);
					array2[1] = dstr[di1].Substring(8);
					DFList.Items.Add(new ListViewItem(array2));
				}
				return;
			}
			base.Width = 484;
			array = new string[3];
			di2 = dstr.Length;
			Text = "Variables";
			DFList.Columns.Add("Variable");
			DFList.Columns.Add("Event");
			DFList.Columns.Add("Event List");
			DFList.Columns[0].Width = 92;
			DFList.Columns[1].Width = 130;
			DFList.Columns[2].Width = 225;
			for (di1 = 0; di1 < di2; di1++)
			{
				int j = dstr[di1].Length - 20;
				drd = dstr[di1].Substring(0, 8);
				int i = Convert.ToInt32(drd, 16);
				if (dstr[di1][0] == '0')
				{
					drd = "IC-";
				}
				else if (dstr[di1][0] == '1')
				{
					drd = "LA-";
				}
				else if (dstr[di1][0] == '2')
				{
					drd = "RA-";
				}
				else
				{
					drd = "(" + ((i >> 28) & 0xF) + ")-";
				}
				if (dstr[di1][1] == '0')
				{
					drd += "Basic[";
				}
				else if (dstr[di1][1] == '1')
				{
					drd += "Float[";
				}
				else if (dstr[di1][1] == '2')
				{
					drd += "Bit[";
				}
				else
				{
					drd = drd + "(" + ((i >> 24) & 0xF) + ")[";
				}
				drd = drd + (i & 0xFFFFFF) + "]";
				for (i = 8; i < j; i++)
				{
					if (dstr[di1][i] == '\r')
					{
						if (dstr[di1][i + 1] == '\n')
						{
							break;
						}
						di1++;
					}
				}
				array[0] = drd;
				array[1] = dstr[di1].Substring(8, i - 8);
				array[2] = dstr[di1].Substring(i + 2);
				DFList.Items.Add(new ListViewItem(array));
			}
		}

		private void DFClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void DFNoSelect_Click(object sender, EventArgs e)
		{
			if (DFList.SelectedItems.Count > 0)
			{
				DFList.SelectedItems.Clear();
			}
		}

		private void DFAllSelect_Click(object sender, EventArgs e)
		{
			if (DFList.SelectedItems.Count < DFList.Items.Count)
			{
				if (DFList.SelectedItems.Count > 0)
				{
					DFList.SelectedItems.Clear();
				}
				di2 = DFList.Items.Count;
				for (di1 = 0; di1 < di2; di1++)
				{
					DFList.Items[di1].Selected = true;
				}
			}
		}

		private void DFCopytxt_Click(object sender, EventArgs e)
		{
			if (DFList.SelectedItems.Count <= 0)
			{
				return;
			}
			di2 = DFList.Items.Count;
			drd = "";
			if (DFList.Columns.Count == 2)
			{
				for (di1 = 0; di1 < di2; di1++)
				{
					if (DFList.Items[di1].Selected)
					{
						string text = drd;
						drd = text + DFList.Items[di1].SubItems[0].Text + "\t" + DFList.Items[di1].SubItems[1].Text + "\r\n";
					}
				}
			}
			else
			{
				for (di1 = 0; di1 < di2; di1++)
				{
					if (DFList.Items[di1].Selected)
					{
						string text = drd;
						drd = text + DFList.Items[di1].SubItems[0].Text + "\t" + DFList.Items[di1].SubItems[1].Text + "\t" + DFList.Items[di1].SubItems[2].Text + "\r\n";
					}
				}
			}
			Clipboard.SetText(drd);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSACompressor.DataForm));
			DFClose = new System.Windows.Forms.Button();
			DFList = new System.Windows.Forms.ListView();
			DFStrip = new System.Windows.Forms.ContextMenuStrip(components);
			DFNoSelect = new System.Windows.Forms.ToolStripMenuItem();
			DFAllSelect = new System.Windows.Forms.ToolStripMenuItem();
			DFCopytxt = new System.Windows.Forms.ToolStripMenuItem();
			DFStrip.SuspendLayout();
			SuspendLayout();
			DFClose.Dock = System.Windows.Forms.DockStyle.Bottom;
			DFClose.Location = new System.Drawing.Point(0, 238);
			DFClose.Name = "DFClose";
			DFClose.Size = new System.Drawing.Size(284, 23);
			DFClose.TabIndex = 1;
			DFClose.Text = "Close";
			DFClose.UseVisualStyleBackColor = true;
			DFClose.Click += new System.EventHandler(DFClose_Click);
			DFList.ContextMenuStrip = DFStrip;
			DFList.Dock = System.Windows.Forms.DockStyle.Fill;
			DFList.FullRowSelect = true;
			DFList.HideSelection = false;
			DFList.Location = new System.Drawing.Point(0, 0);
			DFList.Name = "DFList";
			DFList.Size = new System.Drawing.Size(284, 238);
			DFList.TabIndex = 2;
			DFList.UseCompatibleStateImageBehavior = false;
			DFList.View = System.Windows.Forms.View.Details;
			DFStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				DFNoSelect,
				DFAllSelect,
				DFCopytxt
			});
			DFStrip.Name = "DFStrip";
			DFStrip.Size = new System.Drawing.Size(180, 70);
			DFNoSelect.Name = "DFNoSelect";
			DFNoSelect.ShortcutKeys = (System.Windows.Forms.Keys)131141;
			DFNoSelect.Size = new System.Drawing.Size(179, 22);
			DFNoSelect.Text = "No Select";
			DFNoSelect.Click += new System.EventHandler(DFNoSelect_Click);
			DFAllSelect.Name = "DFAllSelect";
			DFAllSelect.ShortcutKeys = (System.Windows.Forms.Keys)131137;
			DFAllSelect.Size = new System.Drawing.Size(179, 22);
			DFAllSelect.Text = "Select All";
			DFAllSelect.Click += new System.EventHandler(DFAllSelect_Click);
			DFCopytxt.Name = "DFCopytxt";
			DFCopytxt.ShortcutKeys = (System.Windows.Forms.Keys)131139;
			DFCopytxt.Size = new System.Drawing.Size(179, 22);
			DFCopytxt.Text = "Copy Text";
			DFCopytxt.Click += new System.EventHandler(DFCopytxt_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(284, 261);
			base.Controls.Add(DFList);
			base.Controls.Add(DFClose);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "DataForm";
			Text = "Data";
			base.Load += new System.EventHandler(DataForm_Load);
			DFStrip.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
