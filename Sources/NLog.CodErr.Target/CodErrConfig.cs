using System;

namespace NLog.CodErr.Target
{
    class CodErrConfig
    {
        public string ApiUrl { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }

        public int MaxQueueSize { get; set; }
        public int MaxAttempts { get; set; }
        public TimeSpan RetryInterval { get; set; }

        public CodErrConfig() { }

        public bool IsConfigured()
        {
            return !string.IsNullOrWhiteSpace(ApiUrl) && !string.IsNullOrWhiteSpace(AppKey) && !string.IsNullOrWhiteSpace(AppSecret);
        }
    }
}
