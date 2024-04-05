using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VFAACP
{
	class VerificationPart
	{
		private string _category;
		private string _recipeName;
		private string _recipeFile;

		public string Category
		{
			get
			{
				return _category;
			}
		}

		public string RecipeName
		{
			get
			{
				return _recipeName;
			}
		}

		public string RecipeFile
		{
			get
			{
				return _recipeFile;
			}
		}

		public VerificationPart(string category, string recipeFile)
		{
			_category = category;
			_recipeName = Path.GetFileNameWithoutExtension(recipeFile);
			_recipeFile = recipeFile;
		}

	} // class VerificationPart

	class VerificationParts
	{
		private static bool _isInitialized = false;
		private static List<string> _categories;
		private static List<VerificationPart> _parts;
		private static string _lastCategorySelected = null;

		public static List<string> GetCategories()
		{
			if (!_isInitialized)
			{
				Initialize();
			}
			return _categories;
		} // GetCategories()

		public static List<VerificationPart> GetAllParts()
		{
			if (!_isInitialized)
			{
				Initialize();
			}
			return _parts;
		} // GetAllParts()

		public static List<VerificationPart> GetPartsInCategory(string category)
		{
			if (!_isInitialized)
			{
				Initialize();
			}
			List<VerificationPart> result = new List<VerificationPart>();
			if (_parts == null)
				return result;
			foreach (VerificationPart v in _parts)
			{
				if ((category == "Any") || (category == v.Category))
				{
					result.Add(v);
				}
			}
			return result;
		} // GetPartsInCategory()

		public static void Reset()
		{
			_isInitialized = false;
		}

		public static string LastCategorySelected
		{
			get
			{
				return _lastCategorySelected;
			}
			set
			{
				_lastCategorySelected = value;
			}
		}

		private static void Initialize()
		{
			_categories = new List<string>();
			_parts = new List<VerificationPart>();
			string category;
			string[] recipeFiles;
			_categories.Add("Any");
			// Look for recipe files at top level
			category = null;
			recipeFiles = Directory.GetFiles(ProgramSettings.VerificationPartDirectory, "*.rec", SearchOption.TopDirectoryOnly);
			foreach (string recipeFile in recipeFiles)
			{
				try
				{
					Recipe recipe = Recipe.ReadFromFileAndValidate(recipeFile, false /*designIsKnown*/);
					if (category == null)
					{
						category = "Undefined";
						_categories.Add(category);
					}
					VerificationPart v = new VerificationPart(category, recipeFile);
					_parts.Add(v);
				}
				catch (Exception ex)
				{
					FileSystemFuncs.AppendToLogFile("File " + recipeFile + " is not a valid recipe: " + ex.Message);
				}
			}

			// Look for recipe files in subfolders
			// Each subfolder is a category
			string[] subfolders = Directory.GetDirectories(ProgramSettings.VerificationPartDirectory);
			int n = subfolders.Length;
			if (n > 0)
			{
				foreach (string subfolder in subfolders)
				{
					category = Path.GetFileName(subfolder);
					// Skip name if it begins with a special character
					char c = category[0];
					if ("!%#$%^&()_+-".Contains(c))
						continue;
					recipeFiles = Directory.GetFiles(subfolder, "*.rec", SearchOption.TopDirectoryOnly);
					foreach (string recipeFile in recipeFiles)
					{
						try
						{
							Recipe recipe = Recipe.ReadFromFileAndValidate(recipeFile, false /*designIsKnown*/);
							if (!_categories.Contains(category))
								_categories.Add(category);
							VerificationPart v = new VerificationPart(category, recipeFile);
							_parts.Add(v);
						}
						catch (Exception ex)
						{
							FileSystemFuncs.AppendToLogFile("File " + recipeFile + " is not a valid recipe: " + ex.Message);
						}
					}
				}
			}
			_isInitialized = true;
		} // Initialize()

	} // class VerificationParts
}
