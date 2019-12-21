using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PSACompressor
{
	public class EvChangeForm : Form
	{
		private int ec1;

		private int ec2;

		private string ectx;

		private IContainer components = null;

		private ListBox EvChList;

		private CheckBox EvChReset;

		private Button EvChDone;

		private Button EvChCancel;

		private TextBox EvChID;

		public string[] eclis
		{
			get;
			set;
		}

		public int ecid
		{
			get;
			set;
		}

		public int ecdn
		{
			get;
			set;
		}

		public int ecwf
		{
			get;
			set;
		}

		public int echf
		{
			get;
			set;
		}

		public EvChangeForm()
		{
			InitializeComponent();
		}

		private void EvChangeForm_Load(object sender, EventArgs e)
		{
			EvChID.ReadOnly = true;
			if (ecdn == 0)
			{
				EvChID.CharacterCasing = CharacterCasing.Upper;
				EvChID.Text = ecid.ToString("X8");
				ec1 = 0;
				ec2 = 10000;
				while (eclis[ec1] != null)
				{
					EvChList.Items.Add(eclis[ec1 + 1]);
					if (eclis[ec1] == EvChID.Text)
					{
						ec2 = ec1 / 4;
					}
					ec1 += 4;
					if (ec1 > 1997)
					{
						break;
					}
				}
				if (ec2 < 2000)
				{
					EvChID.Enabled = false;
					EvChList.SelectedIndex = ec2;
					EvChID.Enabled = true;
				}
				EvChID.ReadOnly = false;
				base.Width = ecwf;
				base.Height = echf;
			}
			else
			{
				EvChID.Enabled = false;
				EvChID.Visible = false;
				EvChReset.Visible = false;
				EvChList.Height = 160;
				MinimumSize = new Size(180, 147);
				EvChCancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				EvChDone.Width = 75;
				EvChCancel.Width = 75;
				EvChCancel.Left = 128;
				if (ecdn == 5)
				{
					Text = "Select External Sub Routine";
					EvChList.Items.AddRange(eclis);
					EvChList.Items.Add("No Select");
					ec1 = ecid;
					EvChList.SelectedIndex = ec1;
				}
				else
				{
					EvChDone.Text = "Start";
					Text = "Paste Preview";
					EvChList.Items.AddRange(eclis);
				}
				base.Width = ecwf;
				base.Height = echf;
			}
		}

		private void EvChList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (EvChID.Enabled)
			{
				EvChID.ReadOnly = true;
				if (EvChList.SelectedIndex != -1)
				{
					EvChID.Text = eclis[EvChList.SelectedIndex * 4];
				}
				EvChID.ReadOnly = false;
			}
		}

		private void EvChID_TextChanged(object sender, EventArgs e)
		{
			if (EvChID.ReadOnly || EvChID.Text.Length != 8)
			{
				return;
			}
			EvChID.ReadOnly = true;
			ec1 = 0;
			ec2 = 10000;
			while (eclis[ec1] != null)
			{
				if (eclis[ec1] == EvChID.Text)
				{
					ec2 = ec1 / 4;
				}
				ec1 += 4;
				if (ec1 > 1997)
				{
					break;
				}
			}
			if (ec2 < 2000)
			{
				EvChList.SelectedIndex = ec2;
			}
			EvChID.ReadOnly = false;
		}

		private void EvChDone_Click(object sender, EventArgs e)
		{
			if (EvChID.Visible)
			{
				if (EvChID.Text.Length != 8)
				{
					if (EvChID.Text.Length < 1)
					{
						EvChID.Text = "00020000";
					}
					else
					{
						ec1 = EvChID.Text.Length;
						if (ec1 != 8)
						{
							ectx = EvChID.Text;
							EvChID.Text = "";
							for (ec2 = ec1; ec2 < 8; ec2++)
							{
								EvChID.Text += "0";
							}
							EvChID.Text += ectx;
						}
						if (EvChID.Text.Length > 8)
						{
							EvChID.Text.Substring(0, 8);
						}
					}
				}
				for (ec1 = 0; ec1 < 8; ec1++)
				{
					if (!Uri.IsHexDigit(EvChID.Text[ec1]))
					{
						ec1 = 1000;
					}
				}
				if (ec1 == 8)
				{
					ec1 = Convert.ToInt32(EvChID.Text, 16);
					if (EvChReset.Checked)
					{
						ec2 = 3;
					}
					else
					{
						ec2 = 2;
					}
					ecid = ec1;
					ecdn = ec2;
					Close();
				}
				else
				{
					MessageBox.Show("Invalid Event ID", "Error");
				}
			}
			else
			{
				ec1 = EvChList.SelectedIndex;
				ec2 = 7;
				ecid = ec1;
				ecdn = ec2;
				Close();
			}
		}

		private void EvChCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EvChangeForm_ResizeEnd(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				ecwf = base.Width;
				echf = base.Height;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSACompressor.EvChangeForm));
			EvChList = new System.Windows.Forms.ListBox();
			EvChReset = new System.Windows.Forms.CheckBox();
			EvChDone = new System.Windows.Forms.Button();
			EvChCancel = new System.Windows.Forms.Button();
			EvChID = new System.Windows.Forms.TextBox();
			SuspendLayout();
			EvChList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			EvChList.FormattingEnabled = true;
			EvChList.ItemHeight = 12;
			EvChList.Location = new System.Drawing.Point(0, 0);
			EvChList.Name = "EvChList";
			EvChList.Size = new System.Drawing.Size(211, 136);
			EvChList.TabIndex = 0;
			EvChList.TabStop = false;
			EvChList.SelectedIndexChanged += new System.EventHandler(EvChList_SelectedIndexChanged);
			EvChReset.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			EvChReset.AutoSize = true;
			EvChReset.Checked = true;
			EvChReset.CheckState = System.Windows.Forms.CheckState.Checked;
			EvChReset.Location = new System.Drawing.Point(4, 141);
			EvChReset.Name = "EvChReset";
			EvChReset.Size = new System.Drawing.Size(96, 16);
			EvChReset.TabIndex = 1;
			EvChReset.TabStop = false;
			EvChReset.Text = "Reset Params";
			EvChReset.UseVisualStyleBackColor = true;
			EvChDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			EvChDone.Location = new System.Drawing.Point(3, 161);
			EvChDone.Name = "EvChDone";
			EvChDone.Size = new System.Drawing.Size(50, 23);
			EvChDone.TabIndex = 2;
			EvChDone.TabStop = false;
			EvChDone.Text = "Done";
			EvChDone.UseVisualStyleBackColor = true;
			EvChDone.Click += new System.EventHandler(EvChDone_Click);
			EvChCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			EvChCancel.Location = new System.Drawing.Point(56, 161);
			EvChCancel.Name = "EvChCancel";
			EvChCancel.Size = new System.Drawing.Size(50, 23);
			EvChCancel.TabIndex = 3;
			EvChCancel.TabStop = false;
			EvChCancel.Text = "Cancel";
			EvChCancel.UseVisualStyleBackColor = true;
			EvChCancel.Click += new System.EventHandler(EvChCancel_Click);
			EvChID.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			EvChID.Location = new System.Drawing.Point(131, 163);
			EvChID.MaxLength = 8;
			EvChID.Name = "EvChID";
			EvChID.Size = new System.Drawing.Size(75, 19);
			EvChID.TabIndex = 4;
			EvChID.TabStop = false;
			EvChID.TextChanged += new System.EventHandler(EvChID_TextChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(211, 192);
			base.Controls.Add(EvChID);
			base.Controls.Add(EvChCancel);
			base.Controls.Add(EvChDone);
			base.Controls.Add(EvChReset);
			base.Controls.Add(EvChList);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(205, 147);
			base.Name = "EvChangeForm";
			Text = "Events";
			base.Load += new System.EventHandler(EvChangeForm_Load);
			base.ResizeEnd += new System.EventHandler(EvChangeForm_ResizeEnd);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
