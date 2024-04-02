using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UILog : MonoBehaviour
{
    [SerializeField] ScrollRect[] scrollRects;
    [SerializeField] UILogEntry logPrefab;
    private void OnEnable()
    {
        Logger.onLog += OnLogReception;
    }

    private void OnDisable()
    {
        Logger.onLog -= OnLogReception;
    }

    void OnLogReception(object senderLog, Logger.LogEventArgs args)
    {
        Logger.LogDisplayParams displayParams = args.logDisplayParams;
        if (displayParams == null)
        {
            displayParams = Logger.LogDisplayParams.displayNone;
        }

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(displayParams.displayTitle ? args.hero.title : "");
        stringBuilder.Append(" ");
        stringBuilder.Append(displayParams.displayName ? args.hero.firstName + " " + args.hero.lastName : "Some hero");
        stringBuilder.Append(" ");
        stringBuilder.Append(displayParams.displayAction ? args.actionString : "has done something");
        stringBuilder.Append(" ");
        stringBuilder.Append(displayParams.displayContext ? args.contextString : "somewhere");
        stringBuilder.Append(".");
        string msg = stringBuilder.ToString();

        Debug.Log(msg);

        foreach (var scrollRect in scrollRects)
		{
            UILogEntry logEntry = Instantiate(logPrefab, scrollRect.content);
            logEntry.SetImage(displayParams.displayPortrait, args.hero.portrait);
            logEntry.SetText(msg);
        }
    }
}
