using System;
using Caliburn.Micro;
using NLog;

namespace MarkPad.Framework
{
    public class NLogAdapter : ILog
    {
        private readonly Logger _Logger;

        public NLogAdapter(Type type)
        {
            _Logger = NLog.LogManager.GetLogger(type.FullName);
        }

        #region ILog Members

        public void Error(Exception exception) => _Logger.ErrorException(exception.Message, exception);

        public void Info(string format, params object[] args) => _Logger.Info(format, args);

        public void Warn(string format, params object[] args) => _Logger.Warn(format, args);

        #endregion
    }
}