using log4net;
using log4net.Config;
using System;

namespace chrismrgn.sdl.tridion.core.Logging
{
    public static class Logger
    {
        static Logger()
        {
            XmlConfigurator.Configure();
        }

        public static void Info(string format, params object[] args)
        {
            For(null).InfoFormat(format, args);
        }

        public static void Debug(string format, params object[] args)
        {
            For(null).DebugFormat(format, args);
        }

        public static void Warn(string format, params object[] args)
        {
            For(null).WarnFormat(format, args);
        }

        public static void Error(string format, Exception exception = null, params object[] args)
        {
            For(null).ErrorFormat(format, args);
            For(null).Error(format, exception);
        }

        public static void Fatal(string format, params object[] args)
        {
            For(null).FatalFormat(format, args);
        }

        private static ILog For(object LoggedObject)
        {
            if (LoggedObject != null)
                return For(LoggedObject.GetType());
            else
                return For(null);
        }

        private static ILog For(Type ObjectType)
        {
            if (ObjectType != null)
                return LogManager.GetLogger(ObjectType.Name);
            else
                return LogManager.GetLogger(string.Empty);
        }
    }
}
