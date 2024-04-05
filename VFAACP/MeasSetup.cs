using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace VFAACP
{
	public static class MeasSetup
	{
		private static Recipe _recipe;
		private static string _recipeFile;
		private static string _promptOutputFile;
		private static string _designFile;
		private static string _designName;
		private static string _publishName;
		private static string _protocolNumber;
		private static string _lotNumber;
		private static string _moldPlateID;
		private static string _operatorName;
		private static string _trayFile;
		private static string _trayName;
		public static ToleranceMgr _toleranceMgr;
		private static string _toleranceFile;

		private static string _protocolNumber_Saved;
		private static string _lotNumber_Saved;
		private static string _operatorName_Saved;

		private static string _lastDirEngineeringDesignFiles = null;
		private static string _lastDirProductionDesignFiles = null;
		private static string _lastDirToolingCalDesignFiles = null;
		private static string _lastDirRecipeFiles = null;


		public static Recipe CurrentRecipe
		{
			get { return _recipe; }
		}

		public static string RecipeFile
		{
			get { return _recipeFile; }
		}

		public static string RecipeNameRoot
		{
			get { return Path.GetFileNameWithoutExtension(_recipeFile); }
		}

		public static string PromptOutputFile
		{
			get { return _promptOutputFile; }
		}

		public static string DesignFile
		{
			get { return _designFile; }
		}

		public static string DesignName
		{
			get { return _designName; }
		}

		public static string PublishName
		{
			get { return _publishName; }
		}

		private static bool SplitDesignName(string root, out string prefix, out string remainder)
		{
			// Most design names begin with a two-char prefix followed by a dash.
			// Example: "FI-XYZ100+0000"
			// This function splits the name into the prefix and the remainder
			prefix = null;
			remainder = null;
			if ((root == null) || (root.Length < 4))
				return false;
			// If 3rd char is a dash
			if (root[2] == '-')
			{
				prefix = root.Substring(0, 2);
				remainder = root.Substring(3);
				return true;
			}
			return false;
		}

		public static string ProtocolNumber
		{
			get
			{
				if (_protocolNumber != null)
					return _protocolNumber;
				else
					return "";
			}
		}

		public static string LotNumber
		{
			get
			{
				if (_lotNumber != null)
					return _lotNumber;
				else
					return "";
			}
		}

		public static string MoldPlateID
		{
			get
			{
				if (_moldPlateID != null)
					return _moldPlateID;
				else
					return "";
			}
		}

		public static string OperatorName
		{
			get
			{
				if (_operatorName != null)
					return _operatorName;
				else
					return "";
			}
		}

		public static bool IsToolingCalibrationSetup
		{   // Find out if measurement setup is a "tooling calibration protocol run"
			// This will have both protocol number and lot number
			get
			{
				if (!string.IsNullOrEmpty(_protocolNumber) && !string.IsNullOrEmpty(_lotNumber))
					return true;
				else
					return false;
			}
		}

		public static bool IsMoldServiceSetup
		{   // Find out if measurement setup is a "mold service setup"
			// This will have a mold plate ID
			get
			{
				if (!string.IsNullOrEmpty(_moldPlateID))
					return true;
				else
					return false;
			}
		}

		public static string PresetComment
		{
			get
			{
				string comment = "";
				if (IsToolingCalibrationSetup)
				{
					string protocolNumber = ProtocolNumber;
					if (!string.IsNullOrEmpty(protocolNumber))
					{
						if (comment.Length > 0)
							comment += Environment.NewLine;
						comment += "Protocol = " + protocolNumber;
					}
					string lotNumber = LotNumber;
					if (!string.IsNullOrEmpty(lotNumber))
					{
						if (comment.Length > 0)
							comment += Environment.NewLine;
						comment += "Lot = " + lotNumber;
					}
					string operatorName = OperatorName;
					if (!string.IsNullOrEmpty(operatorName))
					{
						if (comment.Length > 0)
							comment += Environment.NewLine;
						comment += "Operator = " + operatorName;
					}
				}
				if (IsMoldServiceSetup)
				{
					string moldPlateID = MoldPlateID;
					if (!string.IsNullOrEmpty(moldPlateID))
					{
						if (comment.Length > 0)
							comment += Environment.NewLine;
						comment += "Mold Plate = " + moldPlateID;
					}
					string operatorName = OperatorName;
					if (!string.IsNullOrEmpty(operatorName))
					{
						if (comment.Length > 0)
							comment += Environment.NewLine;
						comment += "Operator = " + operatorName;
					}
				}
				return comment;
			}
		}

		public static string TrayFile
		{
			get { return _trayFile; }
			set { _trayFile = value; }
		}

		public static string TrayName
		{
			get { return _trayName; }
			set { _trayName = value; }
		}

		public static ToleranceMgr ToleranceMgr
		{
			get { return _toleranceMgr; }
		}

		public static string ToleranceFile
		{
			get { return _toleranceFile; }
		}

		public static string ToleranceFileRoot
		{
			get
			{
				if (_toleranceFile == null)
					return null;
				return Path.GetFileName(_toleranceFile);
			}
		}

		public static void ResetDesign()
		{
			_recipe = null;
			_recipeFile = null;
			_designFile = null;
			_designName = null;
			_publishName = null;
			_protocolNumber = null;
			_lotNumber = null;
			_moldPlateID = null;
			_operatorName = null;
			_toleranceMgr = null;
			_toleranceFile = null;
		}

		public static void ResetTray()
		{
			_trayFile = null;
			_trayName = null;
		}

		public static bool Setup_New_Measurement(UIForm uiForm, out string newTrayFile)
		{
			FileSystemFuncs.AppendToLogFile("Setup_New_Measurement");
			newTrayFile = null;
			ConfigurationFile config = ProgramSettings.ActiveConfiguration;
			DialogResult dr;
			bool wasTopMost = uiForm.TopMost;
			ResetDesign();
			string msg = "";
			Recipe newRecipe = null;
			try
			{
				string designName = null;
				string designFile = null;
				string recipeFile = null;
				string promptOutputFile = null;
				string protocolNumber = null;
				string lotNumber = null;
				string moldPlateID = null;
				string operatorName = null;

				if ((config.ToolingCalibrationProtocolRun == 1) && config.OnlyOneSetupNewMeasurementOption)
				{
					protocolNumber = _protocolNumber_Saved;
					lotNumber = _lotNumber_Saved;
					operatorName = _operatorName_Saved;
					if (!Tooling_Calibration_Setup(uiForm, ref protocolNumber, ref lotNumber, ref operatorName, out designFile))
					{
						uiForm.Set_CP_Status("Setup New Measurement canceled");
						return false;
					}
				}
				else if ((config.MoldServiceSetup > 0) && config.OnlyOneSetupNewMeasurementOption)
				{
					bool runMoldServiceSetupProgram = (config.MoldServiceSetup == 2);
					if (!Mold_Service_Setup(uiForm, runMoldServiceSetupProgram, out designName, out designFile, out moldPlateID, out recipeFile, out operatorName, out newTrayFile))
					{
						uiForm.Set_CP_Status("Setup New Measurement canceled");
						return false;
					}
				}
				else
				{
					SetupNewMeasDialog snmd = new SetupNewMeasDialog(uiForm);
					snmd.TopMost = true;
					if (!Debugger.IsAttached)
						uiForm.TopMost = true;
					dr = snmd.ShowDialog();
					uiForm.TopMost = wasTopMost;
					if (dr == DialogResult.Cancel)
					{
						uiForm.Set_CP_Status("Setup New Measurement canceled");
						return false;
					}
					designName = snmd.DesignName;
					designFile = snmd.DesignFile;
					recipeFile = snmd.RecipeFile;
					protocolNumber = snmd.ProtocolNumber;
					lotNumber = snmd.LotNumber;
					moldPlateID = snmd.MoldPlateID;
					operatorName = snmd.OperatorName;
					newTrayFile = snmd.TrayFile;
				}
				if ((designFile != null) && (recipeFile == null))
				{
					// Get a recipe file based on design file
					recipeFile = Get_Recipe_File_For_Design(designFile);
					if (recipeFile == null)
					{
						bool ok = Browse_For_Recipe_File(uiForm, out recipeFile);
						if (!ok)
						{
							uiForm.Set_CP_Status("Setup New Measurement canceled");
							return false;
						}
					}
					try
					{
						newRecipe = Recipe.ReadFromFile(recipeFile);
					}
					catch (Exception ex)
					{
						msg = "Error reading recipe file " + recipeFile + ": " + ex.Message;
						throw new Exception(msg);
					}
					if (newRecipe.Validate(out msg, designIsKnown: true) == false)
					{
						throw new Exception("Recipe is invalid: " + msg);
					}
				}
				else if ((designFile == null) && (recipeFile != null))
				{
					try
					{
						newRecipe = Recipe.ReadFromFile(recipeFile);
					}
					catch (Exception ex)
					{
						msg = "Error reading recipe file " + recipeFile + ": " + ex.Message;
						throw new Exception(msg);
					}
					if (newRecipe.Validate(out msg, designIsKnown: false) == false)
					{
						throw new Exception("Recipe is invalid: " + msg);
					}
					if (!string.IsNullOrEmpty(newRecipe.DesignPath))
					{
						FileSystemFuncs.AppendToLogFile("Recipe specified design file " + newRecipe.DesignFile);
						FileSystemFuncs.AppendToLogFile("Design file path " + newRecipe.DesignPath);
						designFile = newRecipe.DesignPath;
					}
					else if (!string.IsNullOrEmpty(newRecipe.JobFile))
					{
						FileSystemFuncs.AppendToLogFile("Recipe specified job file " + newRecipe.JobFile);
						designName = Path.GetFileNameWithoutExtension(newRecipe.JobFile);
					}
					else if (!string.IsNullOrEmpty(newRecipe.PromptFile))
					{
						FileSystemFuncs.AppendToLogFile("Recipe specified prompt file " + newRecipe.PromptFile);
						// Prompt operator to enter values
						OperatorPrompt op = new OperatorPrompt();
						op.TopMost = true;
						op.ReadPromptFile(ProgramSettings.RecipeFileDirectory + "\\" + newRecipe.PromptFile);
						op.Text = "Enter control values for recipe " + Path.GetFileNameWithoutExtension(recipeFile);
						if (!Debugger.IsAttached)
							uiForm.TopMost = true;
						dr = op.ShowDialog();
						uiForm.TopMost = wasTopMost;
						if (dr == DialogResult.OK)
						{
							designName = op.PartName;
							promptOutputFile = op.PromptOutputFile;
						}
						else
						{
							throw new Exception("Enter control values canceled");
						}
					}
				}
				else if ((designFile != null) && (recipeFile != null))
				{
					try
					{
						newRecipe = Recipe.ReadFromFile(recipeFile);
					}
					catch (Exception ex)
					{
						msg = "Error reading recipe file " + recipeFile + ": " + ex.Message;
						throw new Exception(msg);
					}
					if (newRecipe.Validate(out msg, designIsKnown: true) == false)
					{
						throw new Exception("Recipe is invalid: " + msg);
					}
				}
				if (string.IsNullOrEmpty(designName))
				{
					if (!string.IsNullOrEmpty(designFile))
					{
						string s = Path.GetFileNameWithoutExtension(designFile);
						if (s.EndsWith("-CSVMET"))
							s = s.Replace("-CSVMET", "");
						else if (s.EndsWith("-MET"))
							s = s.Replace("-MET", "");
						designName = s;
					}
					else
					{
						designName = "UNKNOWN";
					}
				}

				_designFile = designFile;
				_designName = designName;
				_recipe = newRecipe;
				_promptOutputFile = promptOutputFile;
				_recipeFile = recipeFile;
				_protocolNumber = protocolNumber;
				_lotNumber = lotNumber;
				_moldPlateID = moldPlateID;
				_operatorName = operatorName;

				_publishName = null;
				if (IsToolingCalibrationSetup)
				{
					string prefix, remainder;
					if (SplitDesignName(_designName, out prefix, out remainder))
					{
						_publishName = prefix + "-" + _protocolNumber + "-" + _lotNumber + "-" + remainder;
					}
				}
				if (string.IsNullOrEmpty(_publishName))
				{
					if (!string.IsNullOrEmpty(_designName))
						_publishName = _designName;
					else
						_publishName = Path.GetFileNameWithoutExtension(recipeFile);
				}

				if (ProgramSettings.ActiveConfiguration.MonitoringMeasurementResultsAndTolerances == 1)
				{
					Setup_Tolerancing(_designName);
				}
				FileSystemFuncs.WriteMeasSetupFile(newRecipe);
				if ((config.ToolingCalibrationProtocolRun == 1) && config.OnlyOneSetupNewMeasurementOption)
				{
					_protocolNumber_Saved = _protocolNumber;
					_lotNumber_Saved = _lotNumber;
					_operatorName_Saved = _operatorName;
				}
				else
				{
					_protocolNumber_Saved = null;
					_lotNumber_Saved = null;
					_operatorName_Saved = null;
				}
			}
			catch (Exception ex)
			{
				msg = "Setup New Measurement error: " + ex.Message;
				FileSystemFuncs.AppendToLogFile(msg);
				uiForm.Set_CP_Status(msg);
				return false;
			}
			return true;
		}	// Setup_New_Measurement()

		public static bool Tooling_Calibration_Setup(Form owner, ref string protocolNumber, ref string lotNumber, ref string operatorName, out string designFile)
		{
			FileSystemFuncs.AppendToLogFile("Tooling_Calibration_Setup");
			ToolingCalibrationDialog dlg = new ToolingCalibrationDialog();
			dlg.ProtocolNumber = protocolNumber;
			dlg.LotNumber = lotNumber;
			dlg.OperatorName = operatorName;
			designFile = null;
			DialogResult r = dlg.ShowDialog();
			if (r == DialogResult.OK)
			{
				bool ok = Browse_For_Tooling_Cal_Design_File(owner, out designFile);
				if (ok)
				{
					protocolNumber = dlg.ProtocolNumber;
					lotNumber = dlg.LotNumber;
					operatorName = dlg.OperatorName;
					return true;
				}
			}
			return false;
		}

		public static bool Mold_Service_Setup(Form owner, bool runMoldServiceSetupProgram, out string designName, out string designFile, out string moldPlateID, out string recipeFile, out string operatorName, out string trayFile)
		{
			FileSystemFuncs.AppendToLogFile("Mold_Service_Setup");
			designName = null;
			designFile = null;
			moldPlateID = null;
			recipeFile = null;
			operatorName = null;
			trayFile = null;
			string msg;
			if (runMoldServiceSetupProgram)
			{
				string exeFile = ProgramSettings.MoldServiceSetupExecutable;
				if (exeFile.Length == 0)
				{
					msg = "The MoldServiceSetupExecutable setting is not valid.";
					FileSystemFuncs.AppendToLogFile(msg);
					MessageBox.Show(owner, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				string outFile = ProgramSettings.MoldServiceSetupOutputFile;
				if (exeFile.Length == 0)
				{
					msg = "The MoldServiceSetupOutputFile setting is not valid.";
					FileSystemFuncs.AppendToLogFile(msg);
					MessageBox.Show(owner, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				if (!File.Exists(exeFile))
				{
					msg = "Cannot access MoldServiceSetupExecutable file " + exeFile;
					FileSystemFuncs.AppendToLogFile(msg);
					MessageBox.Show(owner, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				string ext = Path.GetExtension(exeFile).ToLower();
				if ((ext != ".exe") && (ext != ".bat"))
				{
					msg = "MoldServiceSetupExecutable file " + exeFile + " must have extension .exe or .bat";
					FileSystemFuncs.AppendToLogFile(msg);
					MessageBox.Show(owner, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
				File.Delete(outFile);
				ProcessStartInfo pinfo;
				if (ext == ".exe")
				{	// Exe file
					FileSystemFuncs.AppendToLogFile("ProcessStartInfo: " + exeFile + " " + outFile);
					pinfo = new ProcessStartInfo(exeFile, outFile);
				}
				else
				{	// Batch file
					string args = "/c " + exeFile + " " + outFile;
					FileSystemFuncs.AppendToLogFile("ProcessStartInfo: cmd.exe " + args);
					pinfo = new ProcessStartInfo("cmd.exe", args);
				}
				pinfo.CreateNoWindow = true;
				pinfo.UseShellExecute = false;
				int exitCode;
				using (Process p = new Process())
				{
					p.StartInfo = pinfo;
					p.Start();
					p.WaitForExit();
					exitCode = p.ExitCode;
				}
				FileSystemFuncs.AppendToLogFile("ExitCode = " + exitCode);
				if ((exitCode == 0) || (exitCode == 1))
				{
					string errorMessage = null;
					FileSystemFuncs.AppendToLogFile("Reading file " + outFile);
					try
					{
						MoldServiceSetupInfo info = MoldServiceSetupInfo.ReadFromFile(outFile);
						FileSystemFuncs.AppendToLogFile("\n" + info.AsTextLines);
						info.Validate();
						designName = info.DesignCode; // Note rename
						designFile = info.DesignFile;
						moldPlateID = info.MoldPlateID;
						recipeFile = info.RecipeFile;
						trayFile = info.TrayFile;
						operatorName = info.OperatorName;
						errorMessage = info.ErrorMessage;
						return true;
					}
					catch (Exception ex)
					{
						errorMessage = ex.Message;
						FileSystemFuncs.AppendToLogFile("Exception: " + ex.Message);
					}
					if ((exitCode == 1) || !string.IsNullOrEmpty(errorMessage))
					{
						msg = "";
						if (!string.IsNullOrEmpty(errorMessage))
						{
							FileSystemFuncs.AppendToLogFile("Error: " + errorMessage);
							msg = errorMessage + "\n";
						}
						msg = msg + "Click OK to continue with manual setup.\n";
						msg = msg + "Or click Cancel to cancel setup.";
						DialogResult dr = MessageBox.Show(owner, msg, "Mold Service Setup Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
						if (dr == DialogResult.Cancel)
							return false;
					}
				}
				else // other exitCode
				{
					msg = Path.GetFileName(exeFile) + " returned " + exitCode;
					MessageBox.Show(owner, msg, "Mold Service Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
			}
			SelectDesignFileDialog dlg = new SelectDesignFileDialog();
			DialogResult r = dlg.ShowDialog();
			if (r == DialogResult.OK)
			{
				designFile = dlg.DesignFile;
				return true;
			}
			else
			{
				return false;
			}
		}

		public static bool Design_From_Lot_Num_Setup(Form owner, out string designFile, out string lotNumber)
		{
			FileSystemFuncs.AppendToLogFile("Design_From_Lot_Num_Setup");
			designFile = null;
			lotNumber = null;
			string msg;
			string exeFile = ProgramSettings.DesignFromLotNumExecutable;
			if (exeFile.Length == 0)
			{
				msg = "The DesignFromLotNumExecutable setting is not valid.";
				FileSystemFuncs.AppendToLogFile(msg);
				MessageBox.Show(owner, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			if (!File.Exists(exeFile))
			{
				msg = "Cannot access DesignFromLotNumExecutable file " + exeFile;
				FileSystemFuncs.AppendToLogFile(msg);
				MessageBox.Show(owner, msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			ProcessStartInfo pinfo = new ProcessStartInfo();
			string status = ""; // 'OK' or 'ERROR'
			msg = "";        // Design File or Error Msg
			string outFile = Path.Combine(Path.GetDirectoryName(exeFile), "output.txt");
			try
			{
				pinfo.FileName = exeFile;
				Process p = Process.Start(pinfo);
				// Wait for window to load
				p.WaitForInputIdle();
				// Wait for process to end
				p.WaitForExit();
				// Read the result file for status
				try
				{
					using (StreamReader sr = new StreamReader(outFile))
					{
						status = sr.ReadLine();
						msg = sr.ReadLine();
						if (status.Trim() == "OK")
						{
							lotNumber = sr.ReadLine();
						}
					}
				}
				catch (Exception ex)
				{
					status = "ERROR";
					msg = ex.Message;
				}
				finally
				{
					File.Delete(outFile);
				}
			}
			catch (Exception ex)
			{
				status = "ERROR";
				msg = ex.Message;
			}
			// Test if user canceled the form, in this case we just return
			if (status == "ERROR" && msg == "User Cancel")
			{
				return false;
			}
			// If output file was not created, just return
			if (status == "ERROR" && msg.Contains("Could not find file") && msg.Contains(outFile))
			{
				return false;
			}
			if (status.Trim() == "OK")
			{
				designFile = msg;
				FileSystemFuncs.AppendToLogFile("Design File Selection Tool returned design file " + designFile + " for lot number " + lotNumber);
				return true;
			}
			else // status = "ERROR"
			{
				FileSystemFuncs.AppendToLogFile("Design File Selection Tool error: " + msg);
				MessageBox.Show(owner, msg, "Design File Selection Tool Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		public static bool Browse_For_Engineering_Design_File(Form owner, out string designFile)
		{
			FileSystemFuncs.AppendToLogFile("Browse_For_Engineering_Design_File_Cmd");
			designFile = null;
			if (string.IsNullOrEmpty(_lastDirEngineeringDesignFiles))
				_lastDirEngineeringDesignFiles = ProgramSettings.EngineeringDesignFileDirectory;
			bool ok = MeasSetup.Browse_For_Design_File(owner, _lastDirEngineeringDesignFiles, null, out designFile);
			if (ok)
			{
				FileSystemFuncs.AppendToLogFile("User selected design file " + designFile);
				_lastDirEngineeringDesignFiles = Path.GetDirectoryName(designFile);
			}
			return ok;
		}

		public static bool Browse_For_Production_Design_File(Form owner, out string designFile)
		{
			FileSystemFuncs.AppendToLogFile("Browse_For_Production_Design_File");
			designFile = null;
			if (string.IsNullOrEmpty(_lastDirProductionDesignFiles))
				_lastDirProductionDesignFiles = ProgramSettings.ProductionDesignFileDirectory;
			bool ok = MeasSetup.Browse_For_Design_File(owner, _lastDirProductionDesignFiles, ProgramSettings.ProductionDesignFileDirectory, out designFile);
			if (ok)
			{
				FileSystemFuncs.AppendToLogFile("User selected design file " + designFile);
				_lastDirProductionDesignFiles = Path.GetDirectoryName(designFile);
			}
			return ok;
		}

		public static bool Browse_For_Tooling_Cal_Design_File(Form owner, out string designFile)
		{
			FileSystemFuncs.AppendToLogFile("Browse_For_Tooling_Cal_Design_File");
			designFile = null;
			if (string.IsNullOrEmpty(_lastDirToolingCalDesignFiles))
				_lastDirToolingCalDesignFiles = ProgramSettings.ToolingCalDesignFileDirectory;
			bool ok = MeasSetup.Browse_For_Design_File(owner, _lastDirToolingCalDesignFiles, ProgramSettings.ToolingCalDesignFileDirectory, out designFile);
			if (ok)
			{
				FileSystemFuncs.AppendToLogFile("User selected design file " + designFile);
				_lastDirToolingCalDesignFiles = Path.GetDirectoryName(designFile);
			}
			return ok;
		}

		public static bool Browse_For_Design_File(Form owner, string initialDirectory, string restrictToDirectory, out string designFile)
		{
			designFile = null;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".csv";
			ofd.Filter = "Design Files (*.csv;*.sdefx;*.asj)|*.csv;*.sdefx;*.asj";
			ofd.InitialDirectory = initialDirectory;
			ofd.Multiselect = false;
			ofd.Title = "Select a Design File";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				if ((restrictToDirectory != null) && !ofd.FileName.StartsWith(restrictToDirectory))
				{
					string msg = "Please choose a design file under this folder: \n\n";
					msg += restrictToDirectory;

					bool strict = true;
					if (ProgramSettings.TestWithoutInstrument > 0)
					{
						msg += "\n\n(Ignore since ProgramSettings.TestWithoutInstrument > 0)";
						strict = false;
					}
					else if (Debugger.IsAttached)
					{
						msg += "\n\n(Ignore since Debugger.IsAttached)";
						strict = false;
					}

					MessageBox.Show(owner, msg, "Invalid Design File Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					if (strict)
						return false;
				}
				designFile = ofd.FileName;
				return true;
			}
			return false;
		}

		public static bool Browse_For_Recipe_File(Form owner, out string recipeFile)
		{
			recipeFile = null;
			FileSystemFuncs.AppendToLogFile("Browse_For_Recipe_File");
			if (string.IsNullOrEmpty(_lastDirRecipeFiles))
				_lastDirRecipeFiles = ProgramSettings.RecipeFileDirectory;
			bool ok = MeasSetup.Browse_For_Recipe_File(owner, _lastDirRecipeFiles, out recipeFile);
			if (ok)
			{
				FileSystemFuncs.AppendToLogFile("User selected recipe file " + recipeFile);
				_lastDirRecipeFiles = Path.GetDirectoryName(recipeFile);
			}
			return ok;
		}

		public static bool Browse_For_Recipe_File(Form owner, string initialDirectory, out string recipeFile)
		{
			recipeFile = null;
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.DefaultExt = ".rec";
			ofd.Filter = "Recipe files (*.rec)|*.rec";
			ofd.InitialDirectory = initialDirectory;
			ofd.Multiselect = false;
			ofd.Title = "Select a Recipe File";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				recipeFile = ofd.FileName;
				return true;
			}
			return false;
		}

		public static bool ValidateDesignCsvFile(Form owner, string designFile)
		{
			try
			{
				DesignFile df = new DesignFile();
				df.ReadDesignCsvFile(designFile);
			}
			catch (Exception ex)
			{
				string msg;
				FileSystemFuncs.AppendToLogFile("Design file " + designFile + " failed validation: " + ex.Message);
				msg = "The selected design file failed validation.\n\n" + ex.Message;
				MessageBox.Show(owner, msg, "Design File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		public static bool ValidateRecipeFile(Form owner, string recipeFile, bool designIsKnown)
		{
			Recipe recipe;
			string msg;
			try
			{
				recipe = Recipe.ReadFromFile(recipeFile);
			}
			catch (Exception ex)
			{
				FileSystemFuncs.AppendToLogFile("Error reading recipe file " + recipeFile + ": " + ex.Message);
				msg = "Error reading recipe file.\n\n" + ex.Message;
				MessageBox.Show(owner, msg, "Recipe File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			if (recipe.Validate(out msg, designIsKnown) == false)
			{
				MessageBox.Show(owner, msg, "Recipe File Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			return true;
		}

		private static string Get_Recipe_File_For_Design(string designFilePath)
		{
			// Look for Recipe LUT
			//   If found, look for match to DesignFile
			//      If found, read Recipe file
			// In all other cases, prompt user for RecipeFile

			FileSystemFuncs.AppendToLogFile("Get_Recipe_For_Design");

			string recipeFile = null;
			string recipeLUTPath = Path.Combine(ProgramSettings.RecipeLookupTableDirectory, ProgramSettings.RecipeLUT);

			if (File.Exists(recipeLUTPath))
			{
				// Search for match for targetDesignFile
				string targetDesignFile = Path.GetFileName(designFilePath);
				try
				{
					using (StreamReader sr = new StreamReader(recipeLUTPath))
					{
						bool found = false;
						string line;
						while ((line = sr.ReadLine()) != null)
						{
							if (!line.StartsWith(";") && line.Trim() != "") // Ignore comment and empty lines
							{
								string[] parsedLine = line.Split(new string[] { "," }, StringSplitOptions.None);

								string tableDesignFile = parsedLine[0].Trim();

								int compareResult = String.Compare(tableDesignFile, targetDesignFile, true /* ignore case */);
								if (compareResult == 0)
								{
									// A recipe file was found in the LUT
									found = true;
									recipeFile = parsedLine[1].Trim();
									recipeFile = Path.Combine(ProgramSettings.RecipeFileDirectory, recipeFile);
									FileSystemFuncs.AppendToLogFile("Recipe " + recipeFile + " obtained from recipe lookup table");
									break;
								}
							}
						}

						if (found)
						{
							if (!File.Exists(recipeFile))
							{
								// Recipe file does not exist
								FileSystemFuncs.AppendToLogFile("Design file " + targetDesignFile + " found in recipe lookup table, but recipe file " + recipeFile + " does not exist");
							}
						}
						else
						{
							// Design file not found in LUT
							FileSystemFuncs.AppendToLogFile("Design file " + targetDesignFile + " not found in recipe lookup table");
						}
					}

				}
				catch (Exception ex)
				{
					FileSystemFuncs.AppendToLogFile("Error reading recipe lookup table " + recipeLUTPath + ": " + ex.Message);
				}
			}
			else
			{
				// No Recipe LUT found.
				FileSystemFuncs.AppendToLogFile("Recipe lookup table " + recipeLUTPath + " not found.");
			}
			return recipeFile;
		} // Get_Recipe_File_For_Design()

		private static void Setup_Tolerancing(string designName)
		{
			string partType = CurrentRecipe.PartType;
			FileSystemFuncs.AppendToLogFile("Setup_Tolerancing: designName=" + designName + ", partType=" + partType);
			_toleranceMgr = new ToleranceMgr(designName, partType);
			_toleranceFile = _toleranceMgr.ToleranceFile;
			FileSystemFuncs.AppendToLogFile("Tolerance File = " + _toleranceFile);
		} // Setup_Tolerancing()

	}
}
