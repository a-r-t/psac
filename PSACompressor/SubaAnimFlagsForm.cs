using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PSACompressor
{
	public class SubaAnimFlagsForm : Form
	{
		private int afi;

		private int afr;

		private IContainer components = null;

		private Label InTransitionLabel;

		private TextBox InTransition;

		private CheckBox checkBox1;

		private CheckBox checkBox2;

		private CheckBox checkBox3;

		private CheckBox checkBox4;

		private Button FlagDone;

		private CheckBox checkBox5;

		private CheckBox checkBox6;

		private CheckBox checkBox7;

		private CheckBox checkBox8;

		private Button FlagCancel;

		private TextBox Animtxt;

		private Label AnimtxtLabel;

		private CheckedListBox checklistBox;

		public int afdat
		{
			get;
			set;
		}

		public int afrn
		{
			get;
			set;
		}

		public string afanm
		{
			get;
			set;
		}

		public SubaAnimFlagsForm()
		{
			InitializeComponent();
		}

		private void SubaAnimFlagsForm_Load(object sender, EventArgs e)
		{
			afr = afrn;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			if (afr < 2)
			{
				MaximumSize = new Size(500, 199);
				InTransition.Text = ((afdat >> 24) & 0xFF).ToString("X");
				InTransition.CharacterCasing = CharacterCasing.Upper;
				afi = ((afdat >> 16) & 0xFF);
				Animtxt.Text = this.afanm;
				if (afr == 1)
				{
					AnimtxtLabel.Visible = false;
					Animtxt.Visible = false;
				}
				if ((afi & 1) > 0)
				{
					checkBox1.Checked = true;
				}
				if ((afi & 2) > 0)
				{
					checkBox2.Checked = true;
				}
				if ((afi & 4) > 0)
				{
					checkBox3.Checked = true;
				}
				if ((afi & 8) > 0)
				{
					checkBox4.Checked = true;
				}
				if ((afi & 0x10) > 0)
				{
					checkBox5.Checked = true;
				}
				if ((afi & 0x20) > 0)
				{
					checkBox6.Checked = true;
				}
				if ((afi & 0x40) > 0)
				{
					checkBox7.Checked = true;
				}
				if ((afi & 0x80) > 0)
				{
					checkBox8.Checked = true;
				}
				return;
			}
			AnimtxtLabel.Visible = false;
			checkBox3.Visible = false;
			checkBox4.Visible = false;
			checkBox5.Visible = false;
			checkBox6.Visible = false;
			checkBox7.Visible = false;
			checkBox8.Visible = false;
			FlagDone.Left = 12;
			FlagCancel.Left = 94;
			if (afr == 5)
			{
				Text = "Search / Replace";
				checkBox1.Text = "Replace";
				checkBox2.Text = "Replace All";
				MinimumSize = new Size(173, 394);
				base.Size = new Size(197, 403);
				checkBox1.Top = 165;
				Animtxt.Enabled = false;
				checkBox1.Left = 5;
				checkBox2.Top = 165;
				checkBox2.Left = 77;
				checkBox2.Enabled = false;
				InTransition.Left = 0;
				InTransition.Top = 0;
				InTransition.Multiline = true;
				InTransition.ScrollBars = ScrollBars.Both;
				InTransition.Width = 181;
				InTransition.Height = 165;
				InTransition.MaxLength = 9999;
				Animtxt.Left = 0;
				Animtxt.Top = 181;
				Animtxt.Multiline = true;
				Animtxt.ScrollBars = ScrollBars.Both;
				Animtxt.Width = 181;
				Animtxt.Height = 150;
				Animtxt.MaxLength = 9999;
				FlagDone.Left = 1;
				FlagCancel.Left = 101;
				FlagDone.Top = 333;
				FlagCancel.Top = 333;
				InTransition.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				Animtxt.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				checkBox1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
				checkBox2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
				FlagDone.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
				FlagCancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				InTransition.Text = this.afanm;
				InTransition.CharacterCasing = CharacterCasing.Upper;
				Animtxt.CharacterCasing = CharacterCasing.Upper;
				FlagDone.Text = "Go";
			}
			else if (afr == 4)
			{
				checkBox1.Visible = false;
				Text = "Equalize";
				InTransitionLabel.Visible = false;
				InTransition.Visible = false;
				Animtxt.Visible = false;
				checklistBox.Visible = true;
				checklistBox.Width = 181;
				checklistBox.Height = 158;
				checkBox2.Text = "Selected Value Increase";
				checkBox2.Top = 162;
				MinimumSize = new Size(180, 153);
				base.Size = new Size(197, 250);
				checklistBox.Left = 0;
				checklistBox.Top = 0;
				FlagDone.Top = 180;
				FlagCancel.Top = 180;
				FlagDone.Left = 5;
				FlagCancel.Left = 98;
				checklistBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
				checkBox2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
				FlagDone.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
				FlagCancel.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
				afi = afdat;
				string afanm = this.afanm;
				if (afanm != "")
				{
					int num = afanm.Length - 1;
					afr = 0;
					int num2 = 0;
					byte b = 0;
					for (int i = 0; i < num; i++)
					{
						if (afanm[i] != '\r')
						{
							continue;
						}
						if (afanm[i + 1] == '\n')
						{
							if (b == 0)
							{
								checklistBox.Items.Add(afanm.Substring(num2, i - num2));
								b = 1;
								afr++;
								i += 2;
								num2 = i;
								if (afr >= afi)
								{
									break;
								}
							}
							else
							{
								b = 0;
								i += 2;
								num2 = i;
							}
						}
						else
						{
							i++;
						}
					}
					if (afr < afi)
					{
						while (afr < afi)
						{
							checklistBox.Items.Add("Parameter " + afr);
							afr++;
						}
					}
				}
				else
				{
					for (afr = 0; afr < afi; afr++)
					{
						checklistBox.Items.Add("Parameter " + afr);
					}
				}
				afr = 4;
			}
			else
			{
				checkBox1.Visible = false;
				checkBox2.Visible = false;
				Animtxt.Left = 3;
				Animtxt.Top = 5;
				Animtxt.Width = 295;
				if (afr == 2)
				{
					Animtxt.Top = 5;
					FlagDone.Top = 30;
					FlagCancel.Top = 30;
					InTransitionLabel.Visible = false;
					InTransition.Visible = false;
					Animtxt.MaxLength = 80;
					Text = "Name";
					MinimumSize = new Size(197, 102);
					MaximumSize = new Size(555, 102);
					base.Size = new Size(300, 102);
					Animtxt.Text = this.afanm;
				}
				else
				{
					FlagDone.Top = 52;
					FlagCancel.Top = 52;
					Animtxt.MaxLength = 1000;
					Text = "Size";
					MinimumSize = new Size(197, 124);
					MaximumSize = new Size(1200, 124);
					base.Size = new Size(300, 124);
					InTransitionLabel.Left = 12;
					InTransitionLabel.Top = 31;
					InTransitionLabel.Text = "Sub Routine:";
					InTransition.Left = 83;
					InTransition.Top = 28;
					InTransition.Width = 85;
					InTransition.MaxLength = 8;
					InTransition.Text = afdat.ToString("X");
					InTransition.CharacterCasing = CharacterCasing.Upper;
					Animtxt.Text = this.afanm;
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (afr == 5)
			{
				if (checkBox1.Checked)
				{
					Animtxt.Enabled = true;
					checkBox2.Enabled = true;
				}
				else
				{
					Animtxt.Enabled = false;
					checkBox2.Enabled = false;
				}
			}
		}

		private void FlagDone_Click(object sender, EventArgs e)
		{
			if (afr < 2)
			{
				if (InTransition.Text.Length < 1)
				{
					InTransition.Text = "0";
				}
				if (!Uri.IsHexDigit(InTransition.Text[0]))
				{
					InTransition.Text = "0";
				}
				if (InTransition.Text.Length == 2 && !Uri.IsHexDigit(InTransition.Text[1]))
				{
					InTransition.Text = "0";
				}
				afr = 7;
				afi = Convert.ToInt32(InTransition.Text, 16) * 16777216;
				if (checkBox1.Checked)
				{
					afi += 65536;
				}
				if (checkBox2.Checked)
				{
					afi += 131072;
				}
				if (checkBox3.Checked)
				{
					afi += 262144;
				}
				if (checkBox4.Checked)
				{
					afi += 524288;
				}
				if (checkBox5.Checked)
				{
					afi += 1048576;
				}
				if (checkBox6.Checked)
				{
					afi += 2097152;
				}
				if (checkBox7.Checked)
				{
					afi += 4194304;
				}
				if (checkBox8.Checked)
				{
					afi += 8388608;
				}
				afanm = Animtxt.Text;
				afdat = afi;
				afrn = afr;
				Close();
			}
			else if (afr == 2)
			{
				if (Animtxt.Text != "")
				{
					afr = 7;
					afi = 1;
					afanm = Animtxt.Text;
					afdat = afi;
					afrn = afr;
					Close();
				}
				else
				{
					MessageBox.Show("Name cannot be null!", "Error");
				}
			}
			else if (afr == 3)
			{
				if (InTransition.Text.Length >= 4)
				{
					for (afi = 0; afi < InTransition.Text.Length; afi++)
					{
						if (!Uri.IsHexDigit(InTransition.Text[afi]))
						{
							afi = 1000;
						}
					}
					if (afi < 10)
					{
						if (Animtxt.Text != "")
						{
							afi = Convert.ToInt32(InTransition.Text, 16);
						}
					}
					else
					{
						afi = 1;
					}
				}
				else
				{
					afi = 1;
				}
				afr = 7;
				afanm = Animtxt.Text;
				afdat = afi;
				afrn = afr;
				Close();
			}
			else if (afr == 4)
			{
				if (checklistBox.CheckedItems.Count > 0)
				{
					Animtxt.Text = "";
					afi = checklistBox.Items.Count;
					for (afr = 0; afr < afi; afr++)
					{
						if (checklistBox.GetItemChecked(afr))
						{
							Animtxt.Text += "1";
						}
						else
						{
							Animtxt.Text += "0";
						}
					}
					if (checkBox2.Checked)
					{
						afr = 7;
					}
					else
					{
						afr = 8;
					}
					afanm = Animtxt.Text;
					afdat = afi;
					afrn = afr;
				}
				Close();
			}
			else if (InTransition.Text.Length >= 11)
			{
				afi = InTransition.Text.Length;
				if (checkBox1.Checked)
				{
					InTransition.Text += Animtxt.Text;
					if (checkBox2.Checked)
					{
						afr = 9;
					}
					else
					{
						afr = 8;
					}
				}
				else
				{
					afr = 7;
				}
				afanm = InTransition.Text;
				afdat = afi;
				afrn = afr;
				Close();
			}
			else
			{
				MessageBox.Show("Cannot be searched.", "Error");
			}
		}

		private void FlagCancel_Click(object sender, EventArgs e)
		{
			Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSACompressor.SubaAnimFlagsForm));
			InTransitionLabel = new System.Windows.Forms.Label();
			InTransition = new System.Windows.Forms.TextBox();
			checkBox1 = new System.Windows.Forms.CheckBox();
			checkBox2 = new System.Windows.Forms.CheckBox();
			checkBox3 = new System.Windows.Forms.CheckBox();
			checkBox4 = new System.Windows.Forms.CheckBox();
			FlagDone = new System.Windows.Forms.Button();
			checkBox5 = new System.Windows.Forms.CheckBox();
			checkBox6 = new System.Windows.Forms.CheckBox();
			checkBox7 = new System.Windows.Forms.CheckBox();
			checkBox8 = new System.Windows.Forms.CheckBox();
			FlagCancel = new System.Windows.Forms.Button();
			Animtxt = new System.Windows.Forms.TextBox();
			AnimtxtLabel = new System.Windows.Forms.Label();
			checklistBox = new System.Windows.Forms.CheckedListBox();
			SuspendLayout();
			InTransitionLabel.AutoSize = true;
			InTransitionLabel.Location = new System.Drawing.Point(10, 15);
			InTransitionLabel.Name = "InTransitionLabel";
			InTransitionLabel.Size = new System.Drawing.Size(69, 12);
			InTransitionLabel.TabIndex = 0;
			InTransitionLabel.Text = "In Transition";
			InTransition.Location = new System.Drawing.Point(85, 12);
			InTransition.MaxLength = 2;
			InTransition.Name = "InTransition";
			InTransition.Size = new System.Drawing.Size(30, 19);
			InTransition.TabIndex = 1;
			InTransition.TabStop = false;
			checkBox1.AutoSize = true;
			checkBox1.Location = new System.Drawing.Point(12, 37);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(115, 16);
			checkBox1.TabIndex = 2;
			checkBox1.TabStop = false;
			checkBox1.Text = "No Out Transition";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
			checkBox2.AutoSize = true;
			checkBox2.Location = new System.Drawing.Point(12, 59);
			checkBox2.Name = "checkBox2";
			checkBox2.Size = new System.Drawing.Size(48, 16);
			checkBox2.TabIndex = 3;
			checkBox2.TabStop = false;
			checkBox2.Text = "Loop";
			checkBox2.UseVisualStyleBackColor = true;
			checkBox3.AutoSize = true;
			checkBox3.Location = new System.Drawing.Point(12, 81);
			checkBox3.Name = "checkBox3";
			checkBox3.Size = new System.Drawing.Size(111, 16);
			checkBox3.TabIndex = 4;
			checkBox3.TabStop = false;
			checkBox3.Text = "Moves Character";
			checkBox3.UseVisualStyleBackColor = true;
			checkBox4.AutoSize = true;
			checkBox4.Location = new System.Drawing.Point(12, 103);
			checkBox4.Name = "checkBox4";
			checkBox4.Size = new System.Drawing.Size(76, 16);
			checkBox4.TabIndex = 5;
			checkBox4.TabStop = false;
			checkBox4.Text = "Unknown3";
			checkBox4.UseVisualStyleBackColor = true;
			FlagDone.Location = new System.Drawing.Point(12, 125);
			FlagDone.Name = "FlagDone";
			FlagDone.Size = new System.Drawing.Size(75, 23);
			FlagDone.TabIndex = 6;
			FlagDone.TabStop = false;
			FlagDone.Text = "Done";
			FlagDone.UseVisualStyleBackColor = true;
			FlagDone.Click += new System.EventHandler(FlagDone_Click);
			checkBox5.AutoSize = true;
			checkBox5.Location = new System.Drawing.Point(133, 37);
			checkBox5.Name = "checkBox5";
			checkBox5.Size = new System.Drawing.Size(76, 16);
			checkBox5.TabIndex = 7;
			checkBox5.TabStop = false;
			checkBox5.Text = "Unknown4";
			checkBox5.UseVisualStyleBackColor = true;
			checkBox6.AutoSize = true;
			checkBox6.Location = new System.Drawing.Point(133, 59);
			checkBox6.Name = "checkBox6";
			checkBox6.Size = new System.Drawing.Size(76, 16);
			checkBox6.TabIndex = 8;
			checkBox6.TabStop = false;
			checkBox6.Text = "Unknown5";
			checkBox6.UseVisualStyleBackColor = true;
			checkBox7.AutoSize = true;
			checkBox7.Location = new System.Drawing.Point(133, 81);
			checkBox7.Name = "checkBox7";
			checkBox7.Size = new System.Drawing.Size(156, 16);
			checkBox7.TabIndex = 9;
			checkBox7.TabStop = false;
			checkBox7.Text = "Transition Out From Start";
			checkBox7.UseVisualStyleBackColor = true;
			checkBox8.AutoSize = true;
			checkBox8.Location = new System.Drawing.Point(133, 103);
			checkBox8.Name = "checkBox8";
			checkBox8.Size = new System.Drawing.Size(76, 16);
			checkBox8.TabIndex = 10;
			checkBox8.TabStop = false;
			checkBox8.Text = "Unknown7";
			checkBox8.UseVisualStyleBackColor = true;
			FlagCancel.Location = new System.Drawing.Point(133, 125);
			FlagCancel.Name = "FlagCancel";
			FlagCancel.Size = new System.Drawing.Size(75, 23);
			FlagCancel.TabIndex = 11;
			FlagCancel.TabStop = false;
			FlagCancel.Text = "Cancel";
			FlagCancel.UseVisualStyleBackColor = true;
			FlagCancel.Click += new System.EventHandler(FlagCancel_Click);
			Animtxt.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			Animtxt.Location = new System.Drawing.Point(193, 12);
			Animtxt.MaxLength = 31;
			Animtxt.Name = "Animtxt";
			Animtxt.Size = new System.Drawing.Size(96, 19);
			Animtxt.TabIndex = 12;
			Animtxt.TabStop = false;
			AnimtxtLabel.AutoSize = true;
			AnimtxtLabel.Location = new System.Drawing.Point(131, 15);
			AnimtxtLabel.Name = "AnimtxtLabel";
			AnimtxtLabel.Size = new System.Drawing.Size(56, 12);
			AnimtxtLabel.TabIndex = 13;
			AnimtxtLabel.Text = "Animation";
			checklistBox.CheckOnClick = true;
			checklistBox.FormattingEnabled = true;
			checklistBox.Location = new System.Drawing.Point(12, 12);
			checklistBox.Name = "checklistBox";
			checklistBox.Size = new System.Drawing.Size(19, 18);
			checklistBox.TabIndex = 14;
			checklistBox.TabStop = false;
			checklistBox.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(301, 160);
			base.Controls.Add(checklistBox);
			base.Controls.Add(AnimtxtLabel);
			base.Controls.Add(Animtxt);
			base.Controls.Add(FlagCancel);
			base.Controls.Add(checkBox8);
			base.Controls.Add(checkBox7);
			base.Controls.Add(checkBox6);
			base.Controls.Add(checkBox5);
			base.Controls.Add(FlagDone);
			base.Controls.Add(checkBox4);
			base.Controls.Add(checkBox3);
			base.Controls.Add(checkBox2);
			base.Controls.Add(checkBox1);
			base.Controls.Add(InTransition);
			base.Controls.Add(InTransitionLabel);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(300, 185);
			base.Name = "SubaAnimFlagsForm";
			Text = "Animation Flags";
			base.Load += new System.EventHandler(SubaAnimFlagsForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
