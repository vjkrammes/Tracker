using System;
using System.Runtime.Serialization;

namespace TrackerCommon
{
    public class OptionParseException : Exception
    {
        public readonly string _option;

        public OptionParseException(string option, string message, Exception inner) : base(message, inner) => _option = option;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Option", _option);
            base.GetObjectData(info, context);
        }
    }
}
