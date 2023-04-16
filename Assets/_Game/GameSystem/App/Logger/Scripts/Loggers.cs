using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Utils
{
    [System.Serializable]
    public class LoggerContainer
    {
        public ELoggerGroup Group;
        public Color Color;
        public bool IsActivated;
    }

    [System.Serializable]
    public class Loggers
    {
        public Dictionary<ELoggerGroup, Logger> Value { get; private set; } = new Dictionary<ELoggerGroup, Logger>();

        public Loggers(Dictionary<ELoggerGroup, Logger> loggers)
        {
            Value = loggers;
        }

        public Loggers(LoggerContainer[] containers)
        {
            SwapLoggers(containers);
        }

        public void SwapLoggers(LoggerContainer[] containers)
        {
            Value.Clear();

            foreach (var container in containers)
            {
                Value.Add(container.Group, new Logger(container.Group, container.IsActivated, container.Color));
            }
        }

        public void SwapLoggers(Dictionary<ELoggerGroup, Logger> newloggers)
        {
            Value = newloggers;
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
