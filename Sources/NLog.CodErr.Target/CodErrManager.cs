using Coderr.Client;
using Coderr.Client.Uploaders;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace NLog.CodErr.Target
{
    sealed class CodErrManager
    {
        private static readonly Lazy<CodErrManager> _instance = new Lazy<CodErrManager>(() => new CodErrManager());
        public static CodErrManager Instance => _instance.Value;

        bool IsConfigured { get; set; }

        private CodErrManager()
        {
            IsConfigured = false;
        }

        public void Configure(CodErrConfig codErrConfig)
        {
            var uploaderInstance = GetUploader(codErrConfig);
            if (uploaderInstance != null)
            {
                Err.Configuration.QueueReports = true;
                Err.Configuration.Uploaders.Register(uploaderInstance);

                IsConfigured = true;
            }
        }

        public void SendError(bool useAsync, Exception ex, params CodErrParam[] paramList)
        {
            ExpandoObject context = new ExpandoObject();
            foreach (var param in paramList)
                context.SetProperty(param.Key, param.Value);

            SendErrorInternal(useAsync, ex, context);
        }

        public void SendError(bool useAsync, Exception ex)
        {
            SendErrorInternal(useAsync, ex);
        }

        private void SendErrorInternal(bool useAsync, Exception ex, object context = null)
        {
            if (!IsConfigured)
                return;

            if (!useAsync)
            {
                if (context == null)
                    Err.Report(ex);
                else
                    Err.Report(ex, context);
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    if (context == null)
                        Err.Report(ex);
                    else
                        Err.Report(ex, context);
                });
            }
        }

        private UploadToCoderr GetUploader(CodErrConfig codErrConfig)
        {
            if (codErrConfig != null && codErrConfig.IsConfigured())
            {
                var codErrUploader = new UploadToCoderr(new Uri(codErrConfig.ApiUrl), codErrConfig.AppKey, codErrConfig.AppSecret);

                codErrUploader.MaxQueueSize = codErrConfig.MaxQueueSize;
                codErrUploader.MaxAttempts = codErrConfig.MaxAttempts;
                codErrUploader.RetryInterval = codErrConfig.RetryInterval;

                return codErrUploader;
            }

            return null;
        }
    }
}
