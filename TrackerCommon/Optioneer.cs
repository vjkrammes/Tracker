using System;
using System.Collections;
using System.Collections.Generic;

namespace TrackerCommon
{
    public class Optioneer : IEnumerable, IEnumerable<KeyValuePair<string, object>>
    {
        //
        // parses command line parameters. The following formats are supported:
        //
        //  -option                                                     stored as option, true
        //  option                                                      stored as option, true
        //  -option=value                                               stored as option, value (string)
        //  -option value                                               stored as option, string
        //  option=value                                                stored as option, string
        //
        //  note that the signifier character can be overridden, is "-" by default
        //  the delimeter can also be overridden, is space by default
        //

        #region Dictionary

        private readonly Dictionary<string, object> _options = new Dictionary<string, object>();

        #endregion

        #region Constructors

        public Optioneer(string[] options, char signifier = '-') => Parse(options, signifier);

        public Optioneer(string commandline, char signifier = '-', params char[] delimiters)
        {
            string[] parts = commandline.Split((delimiters == null || delimiters.Length == 0)
                ? new char[] { ' ' }
                : delimiters,
                StringSplitOptions.RemoveEmptyEntries);
            Parse(parts, signifier);
        }

        #endregion

        #region Public Methods

        public object this[string key] { get { return _options.ContainsKey(key) ? _options[key] : null; } }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_options).GetEnumerator();

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            ((IEnumerable<KeyValuePair<string, object>>)_options).GetEnumerator();

        public bool Contains(string key) => _options.ContainsKey(key);

        public object GetOption(string key) => Contains(key) ? _options[key] : null;

        public bool GetOption<T>(string key, ref T value)
        {
            if (!Contains(key))
                return false;
            Type[] types = new Type[] { typeof(string), typeof(T).MakeByRefType() };
            var tp = typeof(T).GetMethod("TryParse", types);
            if (tp == null)
                return false;
            object[] args = new object[] { _options[key], null };
            bool pr = (bool)tp.Invoke(null, args);
            if (!pr)
                return false;
            value = (T)Convert.ChangeType(args[1], typeof(T));
            return true;
        }

        public bool? GetBooleanOption(string key)
        {
            if (!_options.ContainsKey(key))
                return null;
            if (_options[key] is bool value)
                return value;
            return null;
        }

        public string GetStringOption(string key)
        {
            if (!_options.ContainsKey(key))
                return null;
            if (_options[key] is string value)
                return value;
            return null;
        }

        public int? GetIntOption(string key)
        {
            if (!_options.ContainsKey(key))
                return null;
            if (!(_options[key] is string value))
                return null;
            if (!int.TryParse(value, out int val))
                return null;
            return val;
        }

        public long? GetLongOption(string key)
        {
            if (!_options.ContainsKey(key))
                return null;
            if (!(_options[key] is string value))
                return null;
            if (!long.TryParse(value, out long val))
                return null;
            return val;
        }

        public float? GetFloatOption(string key)
        {
            if (!_options.ContainsKey(key))
                return null;
            if (!(_options[key] is string value))
                return null;
            if (!float.TryParse(value, out float val))
                return null;
            return val;
        }

        public double? GetDoubleOption(string key)
        {
            if (!_options.ContainsKey(key))
                return null;
            if (!(_options[key] is string value))
                return null;
            if (!double.TryParse(value, out double val))
                return null;
            return val;
        }

        #endregion

        #region Private Methods

        private void Parse(string[] options, char signifier)
        {
            string sig = new string(signifier, 1);
            int ix = 0;
            while (ix < options.Length)
            {
                if (options[ix].StartsWith(sig))
                {
                    if (_options.ContainsKey(options[ix].Substring(1)))
                    {
                        throw new OptionParseException(options[ix], "Duplicate option", null);
                    }
                    if (options[ix].Contains("="))
                    {
                        string[] parts = options[ix].Split(new char[] { ' ', '=' },
                            StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length != 2)
                        {
                            throw new OptionParseException(options[ix], "Option contains multiple equals signs",
                                null);
                        }
                        _options.Add(parts[0].Substring(1), parts[1]);
                        ix++;
                    }
                    else
                    {
                        if (ix < options.Length - 1)
                        {
                            if (options[ix + 1].StartsWith(sig))
                            {
                                _options.Add(options[ix].Substring(1), true);
                                ix++;
                            }
                            else
                            {
                                _options.Add(options[ix].Substring(1), options[ix + 1]);
                                ix += 2;
                            }
                        }
                        else
                        {
                            _options.Add(options[ix].Substring(1), true);
                            ix++;
                        }
                    }
                }
                else
                {
                    if (_options.ContainsKey(options[ix]))
                    {
                        throw new OptionParseException(options[ix], "Duplicate Option", null);
                    }
                    if (options[ix].Contains("="))
                    {
                        string[] parts = options[ix].Split(new char[] { '=' },
                            StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length != 2)
                        {
                            throw new OptionParseException(options[ix], "Option contains multiple equals signs",
                                null);
                        }
                        _options.Add(parts[0], parts[1]);
                        ix++;
                    }
                    else
                    {
                        _options.Add(options[ix], true);
                        ix++;
                    }
                }
            }
        }

        #endregion
    }
}
