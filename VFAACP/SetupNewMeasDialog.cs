using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace VFAACP
{
	public partial class SetupNewMeasDialog : Form
	{
		private static string _lastLotNumber = "";
		private static string _lastProtocolNumber = "";
		private static string _lastOperatorName = "";

		private UIForm _uiForm;
		private string _designName;
		private string _designFile;
		private string _recipeFile;
		private string _protocolNumber;
		private string _lotNumber;
		private string _moldPlateID;
		private string _operatorName;
		private string _trayFile;

		const int btnWidth = 186;
		const int btnSpacing = 14;

		public SetupNewMeasDialog(UIForm f)
		{
			_uiForm = f;
			InitializeComponent();
			this.Icon = Properties.Resources.VFA_ACP;
			_designName = null;
			_designFile = null;
			_recipeFile = null;
			_protocolNumber = _lastProtocolNumber;
			_lotNumber = _lastLotNumber;
			_operatorName = _lastOperatorName;
			_trayFile = null;
		}

		private void SetupNewMeasDialog_Load(object sender, EventArgs e)
		{
			List<Button> visibleButtonList = new List<Button>();

			ConfigurationFile config = ProgramSettings.ActiveConfiguration;

			// Set button visibility and make list of visible buttons

			if (config.ToolingCalibrationProtocolRun == 1)
			{
				btnToolingCalibrationProtocolRun.Visible = true;
				visibleButtonList.Add(btnToolingCalibrationProtocolRun);
			}
			else
			{
				btnToolingCalibrationProtocolRun.Visible = false;
			}

			if (config.MoldServiceSetup > 0)
			{
				btnMoldServiceSetup.Visible = true;
				visibleButtonList.Add(btnMoldServiceSetup);
			}
			else
			{
				btnMoldServiceSetup.Visible = false;
			}

			if (config.UseLotNumberToLoadDesignFile == 1)
			{
				btnUseLotNumberToLoadDesignFile.Visible = true;
				visibleButtonList.Add(btnUseLotNumberToLoadDesignFile);
			}
			else
			{
				btnUseLotNumberToLoadDesignFile.Visible = false;
			}

			if (config.BrowseForEngineeringDesignFile == 1)
			{
				btnBrowseForEngineeringDesignFile.Visible = true;
				visibleButtonList.Add(btnBrowseForEngineeringDesignFile);
			}
			else
			{
				btnBrowseForEngineeringDesignFile.Visible = false;
			}

			if (config.BrowseForProductionDesignFile == 1)
			{
				btnBrowseForProductionDesignFile.Visible = true;
				visibleButtonList.Add(btnBrowseForProductionDesignFile);
			}
			else
			{
				btnBrowseForProductionDesignFile.Visible = false;
			}

			if (config.BrowseForRecipeFile == 1)
			{
				btnBrowseForRecipeFile.Visible = true;
				visibleButtonList.Add(btnBrowseForRecipeFile);
			}
			else
			{
				btnBrowseForRecipeFile.Visible = false;
			}

			if (config.MeasureVerificationParts == 1)
			{
				btnMeasureVerificationPart.Visible = true;
				visibleButtonList.Add(btnMeasureVerificationPart);
			}
			else
			{
				btnMeasureVerificationPart.Visible = false;
			}

			// Layout the buttons
			int indx = 0;
			foreach (Button b in visibleButtonList)
			{
				b.Location = new Point(btnSpacing + ((btnWidth + btnSpacing) * indx), b.Location.Y);
				++indx;
			}
			int numVisible = visibleButtonList.Count;
			// Set form size
			this.ClientSize = new Size((numVisible * btnWidth) + ((numVisible + 1) * btnSpacing), this.ClientSize.Height);
		} // SetupNewMeasDialog_Load()

		public string DesignName
		{
			get { return _designName; }
		}

		public string DesignFile
		{
			get { return _designFile; }
		}

		public string RecipeFile
		{
			get { return _recipeFile; }
		}

		public string ProtocolNumber
		{
			get { return _protocolNumber; }
		}

		public string LotNumber
		{
			get { return _lotNumber; }
		}

		public string MoldPlateID
		{
			get { return _moldPlateID; }
		}

		public string OperatorName
		{
			get { return _operatorName; }
		}

		public string TrayFile
		{
			get { return _trayFile; }
		}

		private void btnToolingCalibrationProtocolRun_Click(object sender, EventArgs e)
		{
			Tooling_Calibration_Setup_Cmd();
		}

		private void btnMoldServiceSetup_Click(object sender, EventArgs e)
		{
			Mold_Service_Setup_Cmd();
		}

		private void btnUseLotNumberToLoadDesignFile_Click(object sender, EventArgs e)
		{
			Design_From_Lot_Num_Cmd();
		}

		private void btnBrowseForEngineeringDesignFile_Click(object sender, EventArgs e)
		{
			Browse_For_Engineering_Design_File_Cmd();
		}

		private void btnBrowseForProductionDesignFile_Click(object sender, EventArgs e)
		{
			Browse_For_Production_Design_File_Cmd();
		}

		private void btnBrowseForRecipeFile_Click(object sender, EventArgs e)
		{
			Browse_For_Recipe_File_Cmd();
		}

		private void btnMeasureVerificationParts_Click(object sender, EventArgs e)
		{
			Measure_Verification_Part_Cmd();
		}

		private void Tooling_Calibration_Setup_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Tooling_Calibration_Setup_Cmd");
			string protocolNumber = _protocolNumber;
			string lotNumber = _lotNumber;
			string operatorName = _operatorName;
			string designFile = null;
			bool ok = MeasSetup.Tooling_Calibration_Setup(this, ref protocolNumber, ref lotNumber, ref operatorName, out designFile);
			if (ok)
				SetDialogOutputs(designFile: designFile, protocolNumber: protocolNumber, lotNumber: lotNumber, operatorName: operatorName);
		}

		private void Mold_Service_Setup_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Mold_Service_Setup_Cmd");
			string designName, designFile, moldPlateID, recipeFile, operatorName, trayFile;
			bool runMoldServiceSetupExecutable = (ProgramSettings.ActiveConfiguration.MoldServiceSetup == 2);
			bool ok = MeasSetup.Mold_Service_Setup(this, runMoldServiceSetupExecutable, out designName, out designFile, out moldPlateID, out recipeFile, out operatorName, out trayFile);
			if (ok)
				SetDialogOutputs(designName: designName, designFile: designFile, moldPlateID: moldPlateID, recipeFile: recipeFile, operatorName: operatorName, trayFile: trayFile);
		}

		private void Design_From_Lot_Num_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Design_From_Lot_Num_Cmd");
			string designFile, lotNumber;
			bool ok = MeasSetup.Design_From_Lot_Num_Setup(this, out designFile, out lotNumber);
			if (ok)
				SetDialogOutputs(designFile: designFile, lotNumber: lotNumber);
		}   // Design_From_Lot_Num_Cmd()


		private void Browse_For_Engineering_Design_File_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Browse_For_Engineering_Design_File_Cmd");
			string designFile;
			bool ok = MeasSetup.Browse_For_Engineering_Design_File(this, out designFile);
			if (ok)
			{
				SetDialogOutputs(designFile: designFile);
			}
		}

		private void Browse_For_Production_Design_File_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Browse_For_Production_Design_File_Cmd");
			string designFile;
			bool ok = MeasSetup.Browse_For_Production_Design_File(this, out designFile);
			if (ok)
			{
				SetDialogOutputs(designFile: designFile);
			}
		}

		private void Browse_For_Recipe_File_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Browse_For_Recipe_File_Cmd");
			string recipeFile;
			bool ok = MeasSetup.Browse_For_Recipe_File(this, out recipeFile);
			if (ok)
			{
				SetDialogOutputs(recipeFile: recipeFile);
			}
		}

		private void Measure_Verification_Part_Cmd()
		{
			FileSystemFuncs.AppendToLogFile("Measure_Verification_Part_Cmd");
			StandardsPickerDialog spd = new StandardsPickerDialog();
			spd.TopMost = true;
			bool wasTopMost = _uiForm.TopMost;
			if (!Debugger.IsAttached)
				_uiForm.TopMost = true;
			DialogResult dr = spd.ShowDialog();
			_uiForm.TopMost = wasTopMost;
			if (dr == DialogResult.OK)
			{
				if (spd.VerificationPartRecipeFile != "")
				{
					FileSystemFuncs.AppendToLogFile("User selected recipe file " + spd.VerificationPartRecipeFile);
					SetDialogOutputs(recipeFile: spd.VerificationPartRecipeFile);
				}
			}
		} // Measure_Verification_Part_Cmd()

		private void SetDialogOutputs(string designName = null, string designFile=null, string recipeFile=null, string protocolNumber=null, string lotNumber=null, string moldPlateID=null, string operatorName=null, string trayFile=null)
		{
			_designName = null;
			_designFile = null;
			_recipeFile = null;
			_protocolNumber = null;
			_lotNumber = null;
			_moldPlateID = null;
			_operatorName = null;
			bool ok;
			bool designIsKnown = false;
			if (!string.IsNullOrEmpty(designFile))
			{
				string extension = Path.GetExtension(designFile).ToUpper();
				if (string.Compare(extension, ".CSV") == 0)
				{
					ok = MeasSetup.ValidateDesignCsvFile(this, designFile);
					if (!ok)
						return;
				}
				designIsKnown = true;
			}
			if (recipeFile != null)
			{
				ok = MeasSetup.ValidateRecipeFile(this, recipeFile, designIsKnown);
				if (!ok)
					return;
			}
			_designName = designName;
			_designFile = designFile;
			_recipeFile = recipeFile;
			_protocolNumber = protocolNumber;
			_lotNumber = lotNumber;
			_moldPlateID = moldPlateID;
			_operatorName = operatorName;
			_trayFile = trayFile;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

	}
}
