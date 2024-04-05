using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace VFAACP
{
	class OperatorPromptLine
	{
		private string _name;
		private string _type;
		private string _value;
		private string _label;
		private string _initialValue;
		private int _maxLength;
		private double _minNumeric;
		private double _maxNumeric;

		public OperatorPromptLine(string Name, string InputType, string Label, string InitialValue, int MaxLength, double MinNumeric, double MaxNumeric)
		{
			_name = Name;
			_type = InputType;
			_label = Label;
			_initialValue = InitialValue;
			_value = _initialValue;
			_maxLength = MaxLength;
			_minNumeric = MinNumeric;
			_maxNumeric = MaxNumeric;

			// Validate Type
			if (!(_type.ToUpper() == "TEXT" || _type.ToUpper() == "NUMBER")) throw new Exception("OperatorPrompt: Type must be Text or Number; received " + _type);
            
			// Validate Name
			if (_name.Trim().Contains(" ")) throw new Exception("OperatorPrompt: Name must not contain any spaces; received " + _name);

			// Validate MaxLength
			if (_type.ToUpper() == "TEXT" && _maxLength < 1) throw new Exception("OperatorPrompt: MaxLength must be greater than 0; received " + _maxLength.ToString());

			// Validate Min/Max Numeric
			if (_type.ToUpper() == "NUMBER" && _minNumeric == _maxNumeric) throw new Exception("OperatorPrompt: MinNumeric must be less than MaxNumeric; received Min = " + _minNumeric.ToString() + " Max = " + _maxNumeric.ToString());
			if (_type.ToUpper() == "NUMBER" && !(_minNumeric < _maxNumeric)) throw new Exception("OperatorPrompt: MinNumeric must be less than MaxNumeric; received Min = " + _minNumeric.ToString() + " Max = " + _maxNumeric.ToString());
		}

		[Browsable(false)]
		public string Name
		{
			get { return _name; }
		}

		public string Label
		{
			get { return _label; }
		}

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		[Browsable(false)]
		public string InputType
		{
			get { return _type; }
		}

		[Browsable(false)]
		public string InitialValue
		{
			get { return _initialValue; }
		}

		[Browsable(false)]
		public int MaxLength
		{
			get { return _maxLength; }
		}

		[Browsable(false)]
		public double MaxNumeric
		{
			get { return _maxNumeric; }
		}

		[Browsable(false)]
		public double MinNumeric
		{
			get { return _minNumeric; }
		}
	}
}
