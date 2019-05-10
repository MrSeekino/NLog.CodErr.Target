using NLog.Config;
using NLog.Targets;
using System;

namespace NLog.CodErr.Target
{
    [Target("CodErr")]
    public sealed class CodErrTarget : NLog.Targets.Target
    {
        [RequiredParameter]
        public string AppUrl { get; set; }
        [RequiredParameter]
        public string AppKey { get; set; }
        [RequiredParameter]
        public string AppSecret { get; set; }

        public int MaxAttempts { get; set; } = 3;
        public int MaxQueueSize { get; set; } = 10;
        public int RetryIntervalMinutes { get; set; } = 5;

        public bool Async { get; set; } = true;
        public bool OnlyExceptions { get; set; } = true;

        public CodErrTarget() : base() { }

        protected override void InitializeTarget()
        {
            CodErrConfig codErrConfig = new CodErrConfig
            {
                ApiUrl = AppUrl,
                AppKey = AppKey,
                AppSecret = AppSecret,
                MaxAttempts = MaxAttempts,
                MaxQueueSize = MaxQueueSize,
                RetryInterval = new TimeSpan(0, RetryIntervalMinutes, 0)
            };

            CodErrManager.Instance.Configure(codErrConfig);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            if (OnlyExceptions && logEvent.Exception == null)
                return;

            Exception logException = logEvent.Exception ?? new Exception(logEvent.FormattedMessage);
            string logMessage = logEvent.Message.Equals("{0}") || logEvent.Message.Length == 0 ? "N/A" : logEvent.Message;

            CodErrManager.Instance.SendError(Async, logException,
                    CodErrParam.New("LogLevel", logEvent.Level.Name),
                    CodErrParam.New("LoggerName", logEvent.LoggerName),
                    CodErrParam.New("ErrorDate", logEvent.TimeStamp.ToUniversalISO8601()),
                    CodErrParam.New("CustomMessage", logMessage));
        }
    }
}
