using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PSACompressor
{
	public class EvParForm : Form
	{
		private int ep1;

		private int ep2;

		private int ep3;

		private int ep4;

		private int ep5;

		private int ecws;

		private int echs;

		private int eptx;

		private int epp;

		private int ept;

		private decimal epdcl;

		private int[] epps = new int[512];

		private IContainer components = null;

		private TextBox EventText;

		private TextBox EventID;

		private Button ChangeEvent;

		private TextBox ParamsOffsetLabel;

		private TextBox ParListLabel;

		private TextBox ParamsOffset;

		private ListBox ParList;

		private TextBox ParText;

		private Button ParDone;

		private Button ParCancel;

		private TextBox DataTypeLabel;

		private ComboBox DataType;

		private TextBox DataValueLabel;

		private TextBox DataValue;

		private ComboBox DataValueCb;

		private TextBox VariableTypeLabel;

		private ComboBox VariableType;

		private TextBox VariableIDLabel;

		private TextBox VariableID;

		private CheckBox NotReq;

		public int eid
		{
			get;
			set;
		}

		public int eof
		{
			get;
			set;
		}

		public int epw
		{
			get;
			set;
		}

		public int eph
		{
			get;
			set;
		}

		public int ecw
		{
			get;
			set;
		}

		public int ech
		{
			get;
			set;
		}

		public int[] epm
		{
			get;
			set;
		}

		public string[] epEveTx
		{
			get;
			set;
		}

		public string[] epParTx
		{
			get;
			set;
		}

		public string[] epReqTx
		{
			get;
			set;
		}

		public int[] epEveid
		{
			get;
			set;
		}

		public int[] epParid
		{
			get;
			set;
		}

		private void EvParReadin()
		{
			EventID.Text = ep2.ToString("X8");
			ParList.Items.Clear();
			if (((ep2 >> 8) & 0xFF) > 0)
			{
				ep1 = 0;
				while (epParid[ep1] != 0 && ep2 != epParid[ep1])
				{
					ep1++;
					if (ep1 >= 500)
					{
						ep1 = 0;
						break;
					}
				}
				if (ep2 == epParid[ep1])
				{
					eptx = ep1;
				}
				else
				{
					ep1 = 0;
					ep3 = 500;
					ep4 = (ep2 | 0xFFFF) - 65280;
					while (epParid[ep1] != 0)
					{
						if (epParid[ep1] > ep4 && epParid[ep1] < ep2)
						{
							ep4 = epParid[ep1];
							ep3 = ep1;
						}
						ep1++;
						if (ep1 >= 500)
						{
							break;
						}
					}
					eptx = ep3;
				}
				if (eptx < 500)
				{
					ep4 = 0;
					ep3 = ((ep2 >> 8) & 0xFF) * 2 - 1;
					ept = epParTx[eptx].Length - 1;
					epp = 0;
					for (ep1 = 0; ep1 < ept; ep1++)
					{
						if (epParTx[eptx][ep1] == '\r')
						{
							if (epParTx[eptx][ep1 + 1] == '\n')
							{
								if (ep4 % 2 == 0)
								{
									ParList.Items.Add(epParTx[eptx].Substring(epp, ep1 - epp));
									ep4++;
									ep1 += 2;
									epp = ep1;
									if (ep4 >= ep3)
									{
										break;
									}
								}
								else
								{
									ep4++;
									ep1 += 2;
									epp = ep1;
								}
							}
							else
							{
								ep1++;
							}
						}
					}
					if (ep4 < ep3)
					{
						ep3 = ((ep2 >> 8) & 0xFF);
						for (ep1 = ParList.Items.Count; ep1 < ep3; ep1++)
						{
							ParList.Items.Add("Parameter" + ep1);
						}
					}
				}
				else
				{
					ep3 = ((ep2 >> 8) & 0xFF);
					for (ep1 = 0; ep1 < ep3; ep1++)
					{
						ParList.Items.Add("Parameter" + ep1);
					}
				}
			}
			ep1 = 0;
			while (epEveid[ep1] != 0 && ep2 != epEveid[ep1])
			{
				ep1++;
				if (ep1 >= 500)
				{
					ep1 = 0;
					break;
				}
			}
			if (ep2 == epEveid[ep1])
			{
				EventText.Text = epEveTx[ep1 * 4 + 1];
				ParText.Text = epEveTx[ep1 * 4 + 2];
				return;
			}
			ep1 = 0;
			ep3 = 500;
			ep4 = (ep2 | 0xFFFF) - 65535;
			while (epEveid[ep1] != 0)
			{
				if (epEveid[ep1] >= ep4 && epEveid[ep1] < ep2)
				{
					ep4 = epEveid[ep1] + 1;
					ep3 = ep1;
				}
				ep1++;
				if (ep1 >= 500)
				{
					break;
				}
			}
			if (ep3 < 500)
			{
				EventText.Text = epEveTx[ep3 * 4 + 1] + "(+" + (ep2 - epEveid[ep3]).ToString("X") + ")";
				ParText.Text = epEveTx[ep3 * 4 + 2];
			}
			else
			{
				EventText.Text = ep2.ToString("X8");
				ParText.Text = "No Description Available.";
			}
			ep1 = ep3;
		}

		private void SelCh()
		{
			ep2 = DataType.SelectedIndex;
			if (ep2 == 5)
			{
				DataValue.Visible = false;
				DataValueCb.Visible = true;
				VariableTypeLabel.Visible = true;
				VariableType.Visible = true;
				VariableIDLabel.Visible = true;
				VariableID.Visible = true;
				NotReq.Visible = false;
				DataValueLabel.Text = "MemoryType";
				DataValueCb.Items.Clear();
				VariableType.Items.Clear();
				DataValueCb.Items.Add("IC");
				DataValueCb.Items.Add("LA");
				DataValueCb.Items.Add("RA");
				VariableType.Items.Add("Basic");
				VariableType.Items.Add("Float");
				VariableType.Items.Add("Bit");
				if (((ep3 >> 28) & 0xF) > 2)
				{
					DataValueCb.SelectedIndex = 0;
				}
				else
				{
					DataValueCb.SelectedIndex = ((ep3 >> 28) & 0xF);
				}
				if (((ep3 >> 24) & 0xF) > 2)
				{
					VariableType.SelectedIndex = 0;
				}
				else
				{
					VariableType.SelectedIndex = ((ep3 >> 24) & 0xF);
				}
				VariableID.Text = (ep3 & 0xFFFFFF).ToString();
				return;
			}
			if (ep2 == 6)
			{
				DataValue.Visible = false;
				DataValueCb.Visible = true;
				VariableTypeLabel.Visible = false;
				VariableType.Visible = false;
				VariableIDLabel.Visible = false;
				VariableID.Visible = false;
				NotReq.Visible = true;
				DataValueLabel.Text = "Requirement";
				if (((ep3 >> 16) & 0xFFFF) >= 32768)
				{
					NotReq.Checked = true;
					ep3 &= 65535;
				}
				else
				{
					NotReq.Checked = false;
				}
				DataValueCb.Items.Clear();
				for (ep4 = 0; ep4 < 151; ep4++)
				{
					DataValueCb.Items.Add(epReqTx[ep4]);
				}
				if (ep3 < 128)
				{
					DataValueCb.SelectedIndex = ep3;
				}
				else if (ep3 >= 9999 && ep3 <= 10021)
				{
					DataValueCb.SelectedIndex = ep3 - 9871;
				}
				else
				{
					DataValueCb.SelectedIndex = 0;
				}
				return;
			}
			if (ep2 == 3)
			{
				DataValue.Visible = false;
				DataValueCb.Visible = true;
				VariableTypeLabel.Visible = false;
				VariableType.Visible = false;
				VariableIDLabel.Visible = false;
				VariableID.Visible = false;
				NotReq.Visible = false;
				DataValueLabel.Text = "true/false";
				DataValueCb.Items.Clear();
				DataValueCb.Items.Add("False");
				DataValueCb.Items.Add("True");
				if (ep3 == 0)
				{
					DataValueCb.SelectedIndex = 0;
				}
				else
				{
					DataValueCb.SelectedIndex = 1;
				}
				return;
			}
			DataValue.Visible = true;
			DataValueCb.Visible = false;
			VariableTypeLabel.Visible = false;
			VariableType.Visible = false;
			VariableIDLabel.Visible = false;
			VariableID.Visible = false;
			NotReq.Visible = false;
			if (ep2 == 1)
			{
				DataValueLabel.Text = "Value";
				if (DataValue.MaxLength < 15)
				{
					DataValue.MaxLength = 15;
				}
				DataValue.Text = $"{(decimal)ep3 / 60000m:0.#######}";
				return;
			}
			if (DataValue.MaxLength > 8)
			{
				DataValue.MaxLength = 8;
			}
			if (ep2 == 2)
			{
				DataValueLabel.Text = "Address";
			}
			else
			{
				DataValueLabel.Text = "Value";
			}
			DataValue.Text = ep3.ToString("X8");
		}

		private void ParSave()
		{
			if (ept == 5)
			{
				if (int.TryParse(VariableID.Text, out ep4) && ep4 >= 0 && ep4 < 16777216)
				{
					if (DataValueCb.SelectedIndex == 1)
					{
						ep4 += 268435456;
					}
					else if (DataValueCb.SelectedIndex == 2)
					{
						ep4 += 536870912;
					}
					if (VariableType.SelectedIndex == 1)
					{
						ep4 += 16777216;
					}
					else if (VariableType.SelectedIndex == 2)
					{
						ep4 += 33554432;
					}
					epps[ep2] = ep4;
				}
			}
			else if (ept == 6)
			{
				if (DataValueCb.SelectedIndex < 128)
				{
					epps[ep2] = DataValueCb.SelectedIndex;
				}
				else
				{
					epps[ep2] = DataValueCb.SelectedIndex;
					epps[ep2] += 9871;
				}
				if (NotReq.Checked)
				{
					epps[ep2] -= 805306368;
					epps[ep2] -= 1342177280;
				}
			}
			else if (ept == 1)
			{
				if (!decimal.TryParse(DataValue.Text, out epdcl))
				{
					return;
				}
				ep3 = DataValue.Text.Length;
				if (ep3 > 8 && DataValue.Text[ep3 - 8] == '.')
				{
					if (epdcl >= 0m)
					{
						epdcl += 0.00000004m;
					}
					else
					{
						epdcl -= 0.00000004m;
					}
				}
				if (epdcl >= 0m)
				{
					if (epdcl <= 35791.394133m)
					{
						epdcl *= 60000m;
						epps[ep2] = (int)epdcl;
					}
				}
				else if (epdcl >= -35791.394149m)
				{
					epdcl *= 60000m;
					epps[ep2] = (int)epdcl;
				}
			}
			else if (ept == 3)
			{
				if (DataValueCb.SelectedIndex == 1)
				{
					epps[ep2] = 1;
				}
				else
				{
					epps[ep2] = 0;
				}
			}
			else
			{
				if (!(DataValue.Text != ""))
				{
					return;
				}
				for (ep3 = 0; ep3 < DataValue.Text.Length; ep3++)
				{
					if (!Uri.IsHexDigit(DataValue.Text[ep3]))
					{
						ep3 = 1000;
					}
				}
				if (ep3 < 9)
				{
					epps[ep2] = Convert.ToInt32(DataValue.Text, 16);
				}
			}
		}

		public EvParForm()
		{
			InitializeComponent();
		}

		private void EvParForm_Load(object sender, EventArgs e)
		{
			base.Width = epw;
			base.Height = eph;
			ecws = ecw;
			echs = ech;
			DataType.Items.Add("Value");
			DataType.Items.Add("Scalar");
			DataType.Items.Add("Pointer");
			DataType.Items.Add("Boolean");
			DataType.Items.Add("(4)");
			DataType.Items.Add("Variable");
			DataType.Items.Add("Requirement");
			ParamsOffset.Text = "0x" + eof.ToString("X");
			ep3 = ((eid >> 8) & 0xFF) * 2;
			for (ep2 = 0; ep2 < ep3; ep2++)
			{
				epps[ep2] = epm[ep2];
			}
			ep2 = eid;
			EvParReadin();
			epp = 500;
			ept = 500;
		}

		private void DataType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (DataType.Enabled && DataType.SelectedIndex != -1 && ParList.SelectedIndex != -1)
			{
				ep2 = ParList.SelectedIndex * 2 + 1;
				ParSave();
				ep1 = ParList.SelectedIndex * 2;
				ep3 = epps[ep1 + 1];
				SelCh();
				ept = DataType.SelectedIndex;
				epps[ep1] = ept;
			}
		}

		private void ChangeEvent_Click(object sender, EventArgs e)
		{
			ep1 = Convert.ToInt32(EventID.Text, 16);
			ep5 = 0;
			using (EvChangeForm evChangeForm = new EvChangeForm())
			{
				evChangeForm.ecid = ep1;
				evChangeForm.ecdn = ep5;
				evChangeForm.ecwf = ecws;
				evChangeForm.echf = echs;
				evChangeForm.eclis = epEveTx;
				evChangeForm.ShowDialog(this);
				ep2 = evChangeForm.ecid;
				ep5 = evChangeForm.ecdn;
				ecws = evChangeForm.ecwf;
				echs = evChangeForm.echf;
			}
			ecw = ecws;
			ech = echs;
			if (ep5 <= 1)
			{
				return;
			}
			ParList.ClearSelected();
			DataType.Text = "";
			DataValue.Text = "";
			DataType.Items.Clear();
			DataType.Items.Add("Value");
			DataType.Items.Add("Scalar");
			DataType.Items.Add("Pointer");
			DataType.Items.Add("Boolean");
			DataType.Items.Add("(4)");
			DataType.Items.Add("Variable");
			DataType.Items.Add("Requirement");
			DataValue.Visible = true;
			DataValueCb.Visible = false;
			VariableTypeLabel.Visible = false;
			VariableType.Visible = false;
			VariableIDLabel.Visible = false;
			VariableID.Visible = false;
			NotReq.Visible = false;
			if (ep1 != ep2)
			{
				EvParReadin();
			}
			else if (ep5 == 3)
			{
				ep1 = 0;
				while (epEveid[ep1] != 0 && ep2 != epEveid[ep1])
				{
					ep1++;
					if (ep1 >= 500)
					{
						ep1 = 0;
						break;
					}
				}
				if (ep2 != epEveid[ep1])
				{
					ep1 = 0;
					ep3 = 500;
					ep4 = (ep2 | 0xFFFF) - 65535;
					while (epEveid[ep1] != 0)
					{
						if (epEveid[ep1] >= ep4 && epEveid[ep1] < ep2)
						{
							ep4 = epEveid[ep1] + 1;
							ep3 = ep1;
						}
						ep1++;
						if (ep1 >= 500)
						{
							break;
						}
					}
					ep1 = ep3;
				}
			}
			if (ep5 == 3)
			{
				if (ep1 < 500)
				{
					ep3 = ep1 * 4 + 3;
					if (epEveTx[ep3] != null)
					{
						ep4 = epEveTx[ep3].Length;
						for (ep2 = 0; ep2 < ep4; ep2++)
						{
							if (epEveTx[ep3][ep2] == '1')
							{
								epps[ep2 * 2] = 1;
							}
							else if (epEveTx[ep3][ep2] == '2')
							{
								epps[ep2 * 2] = 2;
							}
							else if (epEveTx[ep3][ep2] == '3')
							{
								epps[ep2 * 2] = 3;
							}
							else if (epEveTx[ep3][ep2] == '4')
							{
								epps[ep2 * 2] = 4;
							}
							else if (epEveTx[ep3][ep2] == '5')
							{
								epps[ep2 * 2] = 5;
							}
							else if (epEveTx[ep3][ep2] == '6')
							{
								epps[ep2 * 2] = 6;
							}
							else
							{
								epps[ep2 * 2] = 0;
							}
							epps[ep2 * 2 + 1] = 0;
						}
						for (ep1 = ep2 * 2; ep1 < 512; ep1++)
						{
							epps[ep1] = 0;
						}
					}
					else
					{
						for (ep1 = 0; ep1 < 512; ep1++)
						{
							epps[ep1] = 0;
						}
					}
				}
				else
				{
					for (ep1 = 0; ep1 < 512; ep1++)
					{
						epps[ep1] = 0;
					}
				}
			}
			epp = 500;
			ept = 500;
		}

		private void ParList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ParList.SelectedIndex == -1)
			{
				return;
			}
			DataType.Enabled = false;
			if (epp < 300)
			{
				ep2 = epp * 2 + 1;
				ParSave();
			}
			if (eptx < 500)
			{
				ep1 = ParList.SelectedIndex * 2 + 1;
				ep3 = epParTx[eptx].Length - 1;
				for (ep2 = 0; ep2 < ep3; ep2++)
				{
					if (epParTx[eptx][ep2] == '\r')
					{
						if (epParTx[eptx][ep2 + 1] == '\n')
						{
							if (ep1 == 0)
							{
								ParText.Text = epParTx[eptx].Substring(ep4, ep2 - ep4);
								ep1 = 10000;
								break;
							}
							ep1--;
							ep2 += 2;
							ep4 = ep2;
						}
						else
						{
							ep2++;
						}
					}
				}
				if (ep2 >= ep3)
				{
					ParText.Text = "No Description Available.";
				}
			}
			else
			{
				ParText.Text = "No Description Available.";
			}
			ep1 = ParList.SelectedIndex * 2;
			if (epps[ep1] < 0 || epps[ep1] > 6)
			{
				epps[ep1] = 0;
			}
			DataType.SelectedIndex = epps[ep1];
			ep3 = epps[ep1 + 1];
			SelCh();
			epp = ParList.SelectedIndex;
			ept = DataType.SelectedIndex;
			DataType.Enabled = true;
		}

		private void ParDone_Click(object sender, EventArgs e)
		{
			if (epp < 300)
			{
				ep2 = epp * 2 + 1;
				ParSave();
			}
			ep1 = Convert.ToInt32(EventID.Text, 16);
			ep2 = -1;
			eid = ep1;
			eof = ep2;
			epm = epps;
			Close();
		}

		private void ParCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EvParForm_ResizeEnd(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				epw = base.Width;
				eph = base.Height;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PSACompressor.EvParForm));
			EventText = new System.Windows.Forms.TextBox();
			EventID = new System.Windows.Forms.TextBox();
			ChangeEvent = new System.Windows.Forms.Button();
			ParamsOffsetLabel = new System.Windows.Forms.TextBox();
			ParListLabel = new System.Windows.Forms.TextBox();
			ParamsOffset = new System.Windows.Forms.TextBox();
			ParList = new System.Windows.Forms.ListBox();
			ParText = new System.Windows.Forms.TextBox();
			ParDone = new System.Windows.Forms.Button();
			ParCancel = new System.Windows.Forms.Button();
			DataTypeLabel = new System.Windows.Forms.TextBox();
			DataType = new System.Windows.Forms.ComboBox();
			DataValueLabel = new System.Windows.Forms.TextBox();
			DataValue = new System.Windows.Forms.TextBox();
			DataValueCb = new System.Windows.Forms.ComboBox();
			VariableTypeLabel = new System.Windows.Forms.TextBox();
			VariableType = new System.Windows.Forms.ComboBox();
			VariableIDLabel = new System.Windows.Forms.TextBox();
			VariableID = new System.Windows.Forms.TextBox();
			NotReq = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			EventText.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			EventText.Location = new System.Drawing.Point(2, 4);
			EventText.MaxLength = 300;
			EventText.Name = "EventText";
			EventText.ReadOnly = true;
			EventText.Size = new System.Drawing.Size(182, 19);
			EventText.TabIndex = 0;
			EventText.TabStop = false;
			EventID.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			EventID.Location = new System.Drawing.Point(183, 4);
			EventID.MaxLength = 8;
			EventID.Name = "EventID";
			EventID.ReadOnly = true;
			EventID.Size = new System.Drawing.Size(72, 19);
			EventID.TabIndex = 1;
			EventID.TabStop = false;
			ChangeEvent.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ChangeEvent.Location = new System.Drawing.Point(255, 2);
			ChangeEvent.Name = "ChangeEvent";
			ChangeEvent.Size = new System.Drawing.Size(55, 23);
			ChangeEvent.TabIndex = 2;
			ChangeEvent.TabStop = false;
			ChangeEvent.Text = "Change";
			ChangeEvent.UseVisualStyleBackColor = true;
			ChangeEvent.Click += new System.EventHandler(ChangeEvent_Click);
			ParamsOffsetLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ParamsOffsetLabel.Location = new System.Drawing.Point(128, 25);
			ParamsOffsetLabel.MaxLength = 7;
			ParamsOffsetLabel.Name = "ParamsOffsetLabel";
			ParamsOffsetLabel.ReadOnly = true;
			ParamsOffsetLabel.Size = new System.Drawing.Size(43, 19);
			ParamsOffsetLabel.TabIndex = 4;
			ParamsOffsetLabel.TabStop = false;
			ParamsOffsetLabel.Text = "Offset";
			ParListLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ParListLabel.Location = new System.Drawing.Point(2, 25);
			ParListLabel.MaxLength = 11;
			ParListLabel.Name = "ParListLabel";
			ParListLabel.ReadOnly = true;
			ParListLabel.Size = new System.Drawing.Size(127, 19);
			ParListLabel.TabIndex = 3;
			ParListLabel.TabStop = false;
			ParListLabel.Text = "Parameters";
			ParamsOffset.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ParamsOffset.Location = new System.Drawing.Point(170, 25);
			ParamsOffset.MaxLength = 10;
			ParamsOffset.Name = "ParamsOffset";
			ParamsOffset.ReadOnly = true;
			ParamsOffset.Size = new System.Drawing.Size(139, 19);
			ParamsOffset.TabIndex = 5;
			ParamsOffset.TabStop = false;
			ParList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ParList.FormattingEnabled = true;
			ParList.ItemHeight = 12;
			ParList.Location = new System.Drawing.Point(2, 46);
			ParList.Name = "ParList";
			ParList.Size = new System.Drawing.Size(127, 136);
			ParList.TabIndex = 6;
			ParList.SelectedIndexChanged += new System.EventHandler(ParList_SelectedIndexChanged);
			ParText.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ParText.Location = new System.Drawing.Point(2, 184);
			ParText.Multiline = true;
			ParText.Name = "ParText";
			ParText.ReadOnly = true;
			ParText.Size = new System.Drawing.Size(307, 42);
			ParText.TabIndex = 7;
			ParText.TabStop = false;
			ParDone.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			ParDone.Location = new System.Drawing.Point(7, 228);
			ParDone.Name = "ParDone";
			ParDone.Size = new System.Drawing.Size(75, 23);
			ParDone.TabIndex = 8;
			ParDone.Text = "Done";
			ParDone.UseVisualStyleBackColor = true;
			ParDone.Click += new System.EventHandler(ParDone_Click);
			ParCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			ParCancel.Location = new System.Drawing.Point(229, 228);
			ParCancel.Name = "ParCancel";
			ParCancel.Size = new System.Drawing.Size(75, 23);
			ParCancel.TabIndex = 9;
			ParCancel.Text = "Cancel";
			ParCancel.UseVisualStyleBackColor = true;
			ParCancel.Click += new System.EventHandler(ParCancel_Click);
			DataTypeLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DataTypeLabel.Location = new System.Drawing.Point(130, 46);
			DataTypeLabel.MaxLength = 7;
			DataTypeLabel.Name = "DataTypeLabel";
			DataTypeLabel.ReadOnly = true;
			DataTypeLabel.Size = new System.Drawing.Size(70, 19);
			DataTypeLabel.TabIndex = 10;
			DataTypeLabel.TabStop = false;
			DataTypeLabel.Text = "Type";
			DataType.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DataType.FormattingEnabled = true;
			DataType.Location = new System.Drawing.Point(199, 46);
			DataType.Name = "DataType";
			DataType.Size = new System.Drawing.Size(110, 20);
			DataType.TabIndex = 11;
			DataType.SelectedIndexChanged += new System.EventHandler(DataType_SelectedIndexChanged);
			DataValueLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DataValueLabel.Location = new System.Drawing.Point(130, 67);
			DataValueLabel.MaxLength = 7;
			DataValueLabel.Name = "DataValueLabel";
			DataValueLabel.ReadOnly = true;
			DataValueLabel.Size = new System.Drawing.Size(70, 19);
			DataValueLabel.TabIndex = 12;
			DataValueLabel.TabStop = false;
			DataValue.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DataValue.Location = new System.Drawing.Point(199, 67);
			DataValue.Name = "DataValue";
			DataValue.Size = new System.Drawing.Size(110, 19);
			DataValue.TabIndex = 13;
			DataValue.TabStop = false;
			DataValueCb.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			DataValueCb.FormattingEnabled = true;
			DataValueCb.Location = new System.Drawing.Point(199, 67);
			DataValueCb.Name = "DataValueCb";
			DataValueCb.Size = new System.Drawing.Size(110, 20);
			DataValueCb.TabIndex = 14;
			DataValueCb.TabStop = false;
			DataValueCb.Visible = false;
			VariableTypeLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			VariableTypeLabel.Location = new System.Drawing.Point(130, 89);
			VariableTypeLabel.MaxLength = 7;
			VariableTypeLabel.Name = "VariableTypeLabel";
			VariableTypeLabel.ReadOnly = true;
			VariableTypeLabel.Size = new System.Drawing.Size(70, 19);
			VariableTypeLabel.TabIndex = 15;
			VariableTypeLabel.TabStop = false;
			VariableTypeLabel.Text = "DataType";
			VariableTypeLabel.Visible = false;
			VariableType.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			VariableType.FormattingEnabled = true;
			VariableType.Location = new System.Drawing.Point(199, 89);
			VariableType.Name = "VariableType";
			VariableType.Size = new System.Drawing.Size(110, 20);
			VariableType.TabIndex = 16;
			VariableType.TabStop = false;
			VariableType.Visible = false;
			VariableIDLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			VariableIDLabel.Location = new System.Drawing.Point(130, 110);
			VariableIDLabel.MaxLength = 7;
			VariableIDLabel.Name = "VariableIDLabel";
			VariableIDLabel.ReadOnly = true;
			VariableIDLabel.Size = new System.Drawing.Size(70, 19);
			VariableIDLabel.TabIndex = 17;
			VariableIDLabel.TabStop = false;
			VariableIDLabel.Text = "Dec ID";
			VariableIDLabel.Visible = false;
			VariableID.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			VariableID.Location = new System.Drawing.Point(199, 110);
			VariableID.MaxLength = 8;
			VariableID.Name = "VariableID";
			VariableID.Size = new System.Drawing.Size(110, 19);
			VariableID.TabIndex = 18;
			VariableID.TabStop = false;
			VariableID.Visible = false;
			NotReq.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			NotReq.AutoSize = true;
			NotReq.Location = new System.Drawing.Point(132, 92);
			NotReq.Name = "NotReq";
			NotReq.Size = new System.Drawing.Size(42, 16);
			NotReq.TabIndex = 19;
			NotReq.Text = "Not";
			NotReq.UseVisualStyleBackColor = true;
			NotReq.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(311, 256);
			base.Controls.Add(NotReq);
			base.Controls.Add(VariableID);
			base.Controls.Add(VariableIDLabel);
			base.Controls.Add(VariableType);
			base.Controls.Add(VariableTypeLabel);
			base.Controls.Add(DataValueCb);
			base.Controls.Add(DataValue);
			base.Controls.Add(DataValueLabel);
			base.Controls.Add(DataType);
			base.Controls.Add(DataTypeLabel);
			base.Controls.Add(ParCancel);
			base.Controls.Add(ParDone);
			base.Controls.Add(ParText);
			base.Controls.Add(ParList);
			base.Controls.Add(ParamsOffset);
			base.Controls.Add(ParamsOffsetLabel);
			base.Controls.Add(ParListLabel);
			base.Controls.Add(ChangeEvent);
			base.Controls.Add(EventID);
			base.Controls.Add(EventText);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(265, 247);
			base.Name = "EvParForm";
			Text = "Modify Event";
			base.Load += new System.EventHandler(EvParForm_Load);
			base.ResizeEnd += new System.EventHandler(EvParForm_ResizeEnd);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
