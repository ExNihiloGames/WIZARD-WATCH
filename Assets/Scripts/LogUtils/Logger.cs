using System;

public static class Logger
{
    public static event EventHandler<LogEventArgs> onLog;

    public class LogEventArgs : EventArgs
    {
        public HeroID hero { get; protected set; }
        public string actionString { get; protected set; }
        public string contextString { get; protected set; }
        public LogDisplayParams logDisplayParams { get; protected set;}
        public LogEventArgs(HeroID hero, string actionString, string contextString, LogDisplayParams logDisplayParams)
        {
            this.hero = hero;
            this.actionString = actionString;
            this.contextString = contextString;
            this.logDisplayParams = logDisplayParams;
        }
    }

    [Serializable]
    public class LogDisplayParams
    {
        public static LogDisplayParams displayAll = new LogDisplayParams(true, true, true, true, true, true, true);
        public static LogDisplayParams displayNone = new LogDisplayParams(false, false, false, false, false, false, false);
        public bool displayTitle;
        public bool displayName;
        public bool displayAge;
        public bool displayPortrait;
        public bool displayFactionName;
        public bool displayAction;
        public bool displayContext;
        public LogDisplayParams (bool displayTitle, bool displayName, bool displayAge, bool displayPortrait, bool displayFactionName, bool displayAction, bool displayContext)
        {
            this.displayTitle = displayTitle;
            this.displayName = displayName;
            this.displayAge = displayAge;
            this.displayPortrait = displayPortrait;
            this.displayFactionName = displayFactionName;
            this.displayAction = displayAction;
            this.displayContext = displayContext;
        }
    }
    
    public static void Log(HeroID heroID, string actionString, string contextString = "", LogDisplayParams logDisplayParams = null)
    {
        onLog?.Invoke(null, new LogEventArgs(heroID, actionString, contextString, logDisplayParams));
    }
}