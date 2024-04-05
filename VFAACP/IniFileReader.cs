using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFAACP
{
	public class IniFileReader
	{
		const string _dflt_comment_delim = ";";
		string _comment_delim = null; // A one-character string
		bool _ignore_case = false;
		bool _allow_unnamed_values = false;
		string _file_path = null;

		// Dictionary will contain elements like this:
		// "[section]name"   -> "val1"
		// "[section]name~2" -> "val2"
		// "[section]name~3" -> "val3"
		Dictionary<string, string> _dictionary = null;

		public string CommentDelim
		{
			set
			{
				_comment_delim = value.Trim().Substring(0,1);
			}
			get
			{
				return _comment_delim;
			}
		}

		public bool IgnoreCase
		{
			set
			{
				_ignore_case = value;
			}
			get
			{
				return _ignore_case;
			}
		}

		public bool AllowUnnamedValues
		{
			set
			{
				_allow_unnamed_values = value;
			}
			get
			{
				return _allow_unnamed_values;
			}
		}

		public IniFileReader()
		{
			_comment_delim = _dflt_comment_delim;
			_dictionary = new Dictionary<string, string>();
		}

		public IniFileReader(string path) : this()
		{ 
			ReadFile(path);
		}

		public void ReadFile(string path)
		{
			string msg;
			if (path == null)
				throw new ArgumentNullException();
			StreamReader sr = null;
			try
			{
				sr = new StreamReader(path);
				_dictionary.Clear();
				_file_path = path;
				string section = "";
				string line;
				int unnamed_value_index = 0;
				while ((line = sr.ReadLine()) != null)
				{
					line = line.Trim();
					if (line.Length == 0)
						continue;  // empty line
					if (!String.IsNullOrEmpty(_comment_delim) && line.StartsWith(_comment_delim))
						continue;  // comment
					if (line.StartsWith("[") && line.Contains("]")) // Section name
					{
						int index = line.IndexOf(']');
						section = line.Substring(1, index - 1).Trim();
						unnamed_value_index = 0;
						continue;
					}
					if (line.Contains("=")) // name=value
					{
						int index = line.IndexOf('=');
						if (index == 0)
							continue;
						string name = line.Substring(0, index).Trim();
						string val = line.Substring(index + 1).Trim();
						string dictKey = String.Format("[{0}]{1}", section, name);
						if (_ignore_case)
							dictKey = dictKey.ToLower();

						if (val.StartsWith("\"") && val.EndsWith("\""))  // strip quote chars
							val = val.Substring(1, val.Length - 2);

						if (_dictionary.ContainsKey(dictKey))  // multiple values can share the same key
						{
							index = 1;
							string dictKey2;
							while (true)
							{
								dictKey2 = String.Format("{0}~{1}", dictKey, ++index);
								if (!_dictionary.ContainsKey(dictKey2))
								{
									_dictionary.Add(dictKey2, val);
									break;
								}
							}
						}
						else
						{
							_dictionary.Add(dictKey, val);
						}
					}
					else if (_allow_unnamed_values)
					{
						string dictKey = String.Format("[{0}]{1}", section, unnamed_value_index++);
						if (_ignore_case)
							dictKey = dictKey.ToLower();
						_dictionary.Add(dictKey, line);
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				msg = string.Format("Error reading file {0}", path);
				throw new IniFileReaderException(msg, ex);
			}
			catch (DirectoryNotFoundException ex)
			{
				msg = string.Format("Error reading file {0}", path);
				throw new IniFileReaderException(msg, ex);
			}
			catch (IOException)
			{
				throw;
			}
			finally
			{
				if (sr != null)
				{
					sr.Close();
					sr.Dispose();
				}
			}
		}

		private bool TryGetValue(string section, string name, out string dictKey, out string value)
		{
			if (section.StartsWith("["))
				dictKey = String.Format("{0}{1}", section, name);
			else
				dictKey = String.Format("[{0}]{1}", section, name);
			if (_ignore_case)
				dictKey = dictKey.ToLower();
			return _dictionary.TryGetValue(dictKey, out value);
		}

		public string GetValue(string section, string name, bool required = true, string defaultValue = "")
		{
			string dictKey, stringValue;
			if (!TryGetValue(section, name, out dictKey, out stringValue))
			{
				if (required)
					throw  new Exception(string.Format("{0} not found", dictKey));
				else
					return defaultValue;
			}
			return stringValue;
		}

		public int GetInteger(string section, string name, bool required = true, int defaultValue = 0,
								int minValue = int.MinValue, int maxValue = int.MaxValue)
		{
			string dictKey, stringValue;
			if (!TryGetValue(section, name, out dictKey, out stringValue))
			{
				if (required)
					throw  new Exception(string.Format("{0} not found", dictKey));
				else
					return defaultValue;
			}
			int value;
			if (!int.TryParse(stringValue, out value))
			{
				double dvalue;
				if (!double.TryParse(stringValue, out dvalue))
					return defaultValue;
				value = (int)dvalue;
			}
			if (value < minValue)
				value = minValue;
			if (value > maxValue)
				value = maxValue;
			return value;
		}

		public double GetDouble(string section, string name, bool required = true, double defaultValue = 0.0,
							double minValue = double.MinValue, double maxValue = double.MaxValue)
		{
			string dictKey, stringValue;
			if (!TryGetValue(section, name, out dictKey, out stringValue))
			{
				if (required)
					throw  new Exception(string.Format("{0} not found", dictKey));
				else
					return defaultValue;
			}
			double value;
			if (!double.TryParse(stringValue, out value))
				return defaultValue;
			if (value < minValue)
				value = minValue;
			if (value > maxValue)
				value = maxValue;
			return value;
		}

		public bool GetBoolean(string section, string name, bool required = true, bool defaultValue = false)
		{
			string dictKey, stringValue;
			if (!TryGetValue(section, name, out dictKey, out stringValue))
			{
				if (required)
					throw  new Exception(string.Format("{0} not found", dictKey));
				else
					return defaultValue;
			}
			if (stringValue == "0")
				return false;
			if (stringValue == "1")
				return true;
			stringValue = stringValue.ToUpper();
			if (stringValue.StartsWith("T"))
				return true;
			else
				return false;
		}

		public string[] GetSectionValues(string section)
		{
			List<string> valueList = new List<string>();
			string key;
			if (section.StartsWith("["))
				key = section;
			else
				key = String.Format("[{0}]", section);
			foreach (KeyValuePair<string,string> entry in _dictionary)
				if (entry.Key.StartsWith(key))
					valueList.Add(entry.Value);
			return valueList.ToArray();
		}

	}
	public class IniFileReaderException : Exception
	{
		public IniFileReaderException()
		{
		}
		public IniFileReaderException(string message)
			: base(message)
		{
		}
		public IniFileReaderException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

}
