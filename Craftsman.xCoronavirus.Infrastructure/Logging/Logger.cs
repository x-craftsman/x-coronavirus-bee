using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using CoreLog = Craftsman.Core.Infrastructure.Logging;

namespace Craftsman.Mercury.Infrastructure.Logging
{
    public class Logger : CoreLog.ILogger, IDisposable
    {
        private NLog.Logger _logger;
        public Logger()
            : this("Logger")
        {

        }
        public Logger(string name)
            : this(LogManager.GetLogger(name))
        {

        }

        private Logger(NLog.Logger logger)
        {
            this._logger = logger;
        }

        #region Debug
        public void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }
        #endregion

        #region Info
        public void Info(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }
        #endregion

        #region Warn
        public void Warn(string msg, params object[] args)
        {
            _logger.Warn(msg, args);
        }
        #endregion

        #region Trace
        public void Trace(string msg, params object[] args)
        {
            _logger.Trace(msg, args);
        }
        #endregion

        #region Error
        public void Error(string msg, params object[] args)
        {
            _logger.Error(msg, args);
        }
        #endregion

        #region Fatal
        public void Fatal(string msg, params object[] args)
        {
            _logger.Fatal(msg, args);
        }
        #endregion

        public void Dispose()
        {
            NLog.LogManager.Flush();
        }
    }
}
