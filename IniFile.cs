using System;
using System.Collections.Generic;
using System.IO;


namespace Polyriser {
	sealed class IniReader {
		readonly List<IniSection> _sections;
		readonly Stream _stream;
		readonly StreamReader _in;

		public IniReader(Stream stream) {
			_stream = stream;
			_in = new StreamReader(stream);
			_sections = new List<IniSection>();
		}

		public IniSection GetSection(string name) {
			for(int s = 0; s < _sections.Count; s++)
				if(_sections[s].Name == name)
					return _sections[s];
			return null;
		}


		public void Read() {
			IniSection curSection;

			curSection = new IniSection(null);
			_sections.Add(curSection);

			while(true) {
				var line = _in.ReadLine();
				if(line == null)
					break;

				if(line.Length == 0 || line[0] == ';')
					continue;

				if(line[0] == '[' && line[line.Length - 1] == ']') {
					var name = line.Substring(1, line.Length - 2);
					curSection = new IniSection(name);
					_sections.Add(curSection);
					continue;
				}

				var eqIndex = line.IndexOf('=');
				if(eqIndex == -1) {
					curSection.Add(line, null);
				} else {
					var key = line.Substring(0, eqIndex);
					var value = line.Substring(eqIndex + 1);
					curSection.Add(key, value);
				}
			}
		}
	}


	sealed class IniWriter {
		readonly Stream _stream;
		readonly StreamWriter _out;

		public IniWriter(Stream stream) {
			_stream = stream;
			_out = new StreamWriter(stream);
		}

		public void Close() {
			_out.Close();
		}

		public void BeginSection(string name) {
			_out.Write('[');
			_out.Write(name);
			_out.WriteLine(']');
		}

		public void WriteKeyValue(string key, string value) {
			if(key[0] == '[' || key.IndexOf('=') != -1)
				throw new ArgumentException("Invalid key");

			_out.Write(key);
			if(value != null) {
				_out.Write('=');
				_out.Write(value);
			}
			_out.WriteLine();
		}
	}


	sealed class IniSection {
		readonly string _name;
		public string Name {get {return _name;}}
		readonly Dictionary<string, string> _pairs;

		public IniSection(string name) {
			_name = name;
			_pairs = new Dictionary<string, string>();
		}

		public void Add(string key, string value) {
			_pairs.Add(key, value);
		}

		public string GetValue(string key) {
			string ret;
			if(_pairs.TryGetValue(key, out ret))
				return ret;
			return null;
		}
	}
}