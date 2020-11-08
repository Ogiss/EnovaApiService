using log4net;
using EnovaApiService.Framework.Logging;
using System;

namespace EnovaApiService.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        private readonly ILog log;

        public Logger(string logName)
        {
            if (logName == null)
            {
                throw new ArgumentNullException(nameof(logName));
            }

            log = LogManager.GetLogger(logName);
        }

        public void Debug(object message)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        public void Error(object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public void Info(object message)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        public void Warning(object message)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        public void Fatal(object message)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
    }
}
