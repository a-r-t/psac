using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PSACompressor
{
	public class EdOvrForm : Form
	{
		private int eo1;

		private int eob;

		private int[] eoal = new int[512];

		private IContainer components = null;

		private Button EOvrDone;

		private Button EOvrCancel;

		private TextBox EOvrName;

		private ListBox EOvrList;

		private Label EOvrIDLabel;

		private TextBox EOvrID;

		private Button EOvrAdd;

		public int[] eodt
		{
			get;
			set;
		}

		public int eow
		{
			get;
			set;
		}

		public int eoh
		{
			get;
			set;
		}

		public string eonm
		{
			get;
			set;
		}

		private void EdSave()
		{
			if (eob >= 500 || EOvrID.Text.Length < 1)
			{
				return;
			}
			for (eo1 = 0; eo1 < EOvrID.Text.Length; eo1++)
			{
				if (!Uri.IsHexDigit(EOvrID.Text[eo1]))
				{
					eo1 = 32;
				}
			}
			if (eo1 >= 4)
			{
				return;
			}
			eo1 = Convert.ToInt32(EOvrID.Text, 16);
			if (eoal[eob] != eo1)
			{
				eoal[eob] = eo1;
				eob = EOvrList.SelectedIndex;
				EOvrList.Enabled = false;
				EOvrList.Items.Clear();
				eo1 = 0;
				while (eo1 < 500 && eoal[eo1] >= 0)
				{
					EOvrList.Items.Add(eoal[eo1].ToString("X"));
					eo1++;
				}
				EOvrList.SelectedIndex = eob;
				EOvrList.Enabled = true;
			}
		}

		public EdOvrForm()
		{
			InitializeComponent();
		}

		private void EdOvrForm_Load(object sender, EventArgs e)
		{
			base.Width = eow;
			base.Height = eoh;
			EOvrName.Text = eonm;
			eob = 1000;
			for (eo1 = 0; eo1 < 500; eo1++)
			{
				eoal[eo1] = eodt[eo1];
				if (eoal[eo1] < 0)
				{
					break;
				}
				EOvrList.Items.Add(eoal[eo1].ToString("X"));
			}
		}

		private void EOvrList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (EOvrList.Enabled && EOvrList.SelectedIndex != -1)
			{
				EdSave();
				eo1 = EOvrList.SelectedIndex;
				EOvrID.Text = eoal[eo1].ToString("X");
				eob = eo1;
			}
		}

		private void EOvrAdd_Click(object sender, EventArgs e)
		{
			if (EOvrList.SelectedIndex != -1)
			{
				EdSave();
			}
			eo1 = EOvrList.Items.Count;
			if (eo1 < 500)
			{
				eoal[eo1] = 0;
				eoal[eo1 + 1] = -1;
				EOvrList.Items.Add(eoal[eo1].ToString("X"));
				EOvrList.SelectedIndex = eo1;
				EOvrList.Enabled = false;
				EOvrList.SelectedIndex = eo1;
				EOvrList.Enabled = true;
			}
		}

		private void EOvrDone_Click(object sender, EventArgs e)
		{
			if (EOvrList.SelectedIndex != -1)
			{
				EdSave();
			}
			eoal[510] = EOvrList.Items.Count;
			eodt = eoal;
			eonm = EOvrName.Text;
			Close();
		}

		private void EOvrCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EdOvrForm_ResizeEnd(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				eow = base.Width;
				eoh = base.Height;
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSACompressor.EdOvrForm));
			EOvrDone = new System.Windows.Forms.Button();
			EOvrCancel = new System.Windows.Forms.Button();
			EOvrName = new System.Windows.Forms.TextBox();
			EOvrList = new System.Windows.Forms.ListBox();
			EOvrIDLabel = new System.Windows.Forms.Label();
			EOvrID = new System.Windows.Forms.TextBox();
			EOvrAdd = new System.Windows.Forms.Button();
			SuspendLayout();
			EOvrDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			EOvrDone.Location = new System.Drawing.Point(3, 136);
			EOvrDone.Name = "EOvrDone";
			EOvrDone.Size = new System.Drawing.Size(75, 23);
			EOvrDone.TabIndex = 0;
			EOvrDone.TabStop = false;
			EOvrDone.Text = "Done";
			EOvrDone.UseVisualStyleBackColor = true;
			EOvrDone.Click += new System.EventHandler(EOvrDone_Click);
			EOvrCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			EOvrCancel.Location = new System.Drawing.Point(194, 136);
			EOvrCancel.Name = "EOvrCancel";
			EOvrCancel.Size = new System.Drawing.Size(75, 23);
			EOvrCancel.TabIndex = 1;
			EOvrCancel.TabStop = false;
			EOvrCancel.Text = "Cancel";
			EOvrCancel.UseVisualStyleBackColor = true;
			EOvrCancel.Click += new System.EventHandler(EOvrCancel_Click);
			EOvrName.Dock = System.Windows.Forms.DockStyle.Top;
			EOvrName.Location = new System.Drawing.Point(0, 0);
			EOvrName.MaxLength = 79;
			EOvrName.Name = "EOvrName";
			EOvrName.Size = new System.Drawing.Size(273, 19);
			EOvrName.TabIndex = 2;
			EOvrList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			EOvrList.FormattingEnabled = true;
			EOvrList.ItemHeight = 12;
			EOvrList.Location = new System.Drawing.Point(3, 22);
			EOvrList.Name = "EOvrList";
			EOvrList.Size = new System.Drawing.Size(75, 112);
			EOvrList.TabIndex = 3;
			EOvrList.SelectedIndexChanged += new System.EventHandler(EOvrList_SelectedIndexChanged);
			EOvrIDLabel.AutoSize = true;
			EOvrIDLabel.Location = new System.Drawing.Point(80, 52);
			EOvrIDLabel.Name = "EOvrIDLabel";
			EOvrIDLabel.Size = new System.Drawing.Size(53, 12);
			EOvrIDLabel.TabIndex = 4;
			EOvrIDLabel.Text = "Action ID";
			EOvrID.Location = new System.Drawing.Point(135, 49);
			EOvrID.MaxLength = 3;
			EOvrID.Name = "EOvrID";
			EOvrID.Size = new System.Drawing.Size(50, 19);
			EOvrID.TabIndex = 5;
			EOvrAdd.Location = new System.Drawing.Point(83, 22);
			EOvrAdd.Name = "EOvrAdd";
			EOvrAdd.Size = new System.Drawing.Size(103, 23);
			EOvrAdd.TabIndex = 6;
			EOvrAdd.Text = "Add";
			EOvrAdd.UseVisualStyleBackColor = true;
			EOvrAdd.Click += new System.EventHandler(EOvrAdd_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(273, 166);
			base.Controls.Add(EOvrAdd);
			base.Controls.Add(EOvrID);
			base.Controls.Add(EOvrIDLabel);
			base.Controls.Add(EOvrList);
			base.Controls.Add(EOvrName);
			base.Controls.Add(EOvrCancel);
			base.Controls.Add(EOvrDone);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(208, 157);
			base.Name = "EdOvrForm";
			Text = "Action Override Edit";
			base.Load += new System.EventHandler(EdOvrForm_Load);
			base.ResizeEnd += new System.EventHandler(EdOvrForm_ResizeEnd);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
