using System.Collections.Generic;

namespace LOK1game.Utils
{
    public class Loggers
    {
        public Dictionary<ELoggerGroup, Logger> Value { get; private set; }

        public Loggers(Dictionary<ELoggerGroup, Logger> loggers)
        {
            Value = loggers;
        }

        public bool TryGetLogger(ELoggerGroup group, out Logger logger)
        {
            if (Value.ContainsKey(group))
            {
                logger = GetLogger(group);

                return true;
            }

            logger = null;

            return false;
        }

        public Logger GetLogger(ELoggerGroup group)
        {
            return Value[group];
        }
    }
}
