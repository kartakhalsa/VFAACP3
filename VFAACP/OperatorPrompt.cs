using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace VFAACP
{
	public partial class OperatorPrompt : Form
	{
		private BindingList<OperatorPromptLine> _outputList = new BindingList<OperatorPromptLine>();
		private string _promptOutputFile;
		private string _partName;

		public string PromptOutputFile
		{
			get { return _promptOutputFile; }
		}
		public string PartName
		{
			get { return _partName; }
		}

		public OperatorPrompt()
		{
			InitializeComponent();
			dataGridView1.DataSource = _outputList;
			_promptOutputFile = null;
			_partName = null;
		}

		public void ReadPromptFile(string promptFile)
		{
			_outputList.Clear();
			try
			{
				using (StreamReader sr = new StreamReader(promptFile))
				{
					string line;
					int linenum = 0;
					while ((line = sr.ReadLine()) != null)
					{
						linenum++;
						if (line.Length > 0)
						{
							string[] parsedStr;
							parsedStr = line.Split(new string[] { "," }, StringSplitOptions.None);

							if (parsedStr.Length == 7)
							{
								try
								{
									OperatorPromptLine opl = new OperatorPromptLine(parsedStr[0].Trim().Replace("\"", ""), parsedStr[1].Trim(), parsedStr[2].Trim().Replace("\"", ""), parsedStr[3].Trim().Replace("\"", ""), int.Parse(parsedStr[4].Trim()), double.Parse(parsedStr[5].Trim()), double.Parse(parsedStr[6].Trim()));
									_outputList.Add(opl);
								}
								catch(Exception ex)
								{
									throw new Exception("Line " + linenum.ToString() + ", " + ex.Message);
								}
							}
							else
							{
								throw new Exception("Line " + linenum.ToString() + ", expecting 7 entries, got " + parsedStr.Length.ToString());
							}
						}
					}
					DataGridViewCellStyle bg = new DataGridViewCellStyle();
					bg.BackColor = Color.LightYellow;
					this.dataGridView1.Columns[1].DefaultCellStyle = bg;
					DataGridViewCell firstCell = dataGridView1.Rows[0].Cells[1];
					dataGridView1.CurrentCell = firstCell;
				}

			}
			catch (Exception ex)
			{
				throw new Exception("Error reading prompt file " + promptFile + ": " + ex.Message);
			}
		}

		private void WritePromptOutputFile()
		{
			string path = Path.Combine(ProgramSettings.MeasSetupDirectory, ProgramSettings.PromptOutputFile);
			try
			{
				// Write prompt output file
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.WriteLine("[PromptOutput]");
					foreach (OperatorPromptLine opl in _outputList)
					{
						string tmp = opl.Name.ToUpper();
						// Assign _partName
						if ((string.Compare(tmp, "NAME") == 0) || (string.Compare(tmp, "PART_NAME") == 0))
						{
							_partName = opl.Value;
						}
						sw.WriteLine(opl.Name + "=" + opl.Value);
					}
				}
				_promptOutputFile = path;
				FileSystemFuncs.AppendToLogFile("Wrote PromptOutputFile " + path);
			}
			catch (Exception ex)
			{
				FileSystemFuncs.AppendToLogFile("Error writing PromptOutputFile " + path + " : " + ex.Message);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			WritePromptOutputFile();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;
         
			// Abort validation if cell is not in the Value column
			if (!headerText.Equals("Value")) return;

			OperatorPromptLine opl = _outputList.ElementAt(e.RowIndex);

			// If Numeric expected, test that entry is numeric, and within range
			if (opl.InputType.ToUpper() == "NUMBER")
			{
				double result;
				string entry = (string)e.FormattedValue;
				bool eval = Double.TryParse(entry, out result);
				if (!eval)
				{
					dataGridView1.Rows[e.RowIndex].ErrorText = "Entry must be numeric";
					e.Cancel = true;
				}
				if (!(result >= opl.MinNumeric && result <= opl.MaxNumeric))
				{
					dataGridView1.Rows[e.RowIndex].ErrorText = "Numeric value is out of range [" + opl.MinNumeric.ToString("F4") + " .. " + opl.MaxNumeric.ToString("F4") + "]";
					e.Cancel = true;
				}
			}

			// If Text expected, test that entry is within length specification
			if (opl.InputType.ToUpper() == "TEXT")
			{
				string entry = (string)e.FormattedValue;
				if (entry.Length > opl.MaxLength)
				{
					dataGridView1.Rows[e.RowIndex].ErrorText = "Value must have less than " + opl.MaxLength.ToString() + " characters";
					e.Cancel = true;
				}
			}
		}

		private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			// Clear the row error in case the user presses ESC
			dataGridView1.Rows[e.RowIndex].ErrorText = String.Empty;
		}

		private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				DataGridViewCell currentCell = dataGridView1.CurrentCell;

				// Allow tabbing down a colum, ignoring Readonly cells
				// Not the most elegant solution but the alternative is more messy
				if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
				{
					SendKeys.Send("{TAB}");
				}
			}
			catch { }
		}

		private void OperatorPrompt_Load(object sender, EventArgs e)
		{
			this.Icon = Properties.Resources.VFA_ACP;
		}  
	}   
}
