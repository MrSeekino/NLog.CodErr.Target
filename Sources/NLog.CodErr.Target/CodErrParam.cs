namespace NLog.CodErr.Target
{
    class CodErrParam
    {
        public string Key { get; set; }
        public string Value { get; set; }

        private CodErrParam(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static CodErrParam New(string key, string value)
        {
            return new CodErrParam(key, value);
        }
    }
}
