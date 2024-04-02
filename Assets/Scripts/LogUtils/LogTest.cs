using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class LogTest : MonoBehaviour
{
    public HeroID HeroID;
    public string ActionString = "has died";
    public string ContextString = "in some room";
    public Logger.LogDisplayParams displayParams;

    private void Start()
    {
        Logger.Log(HeroID, "has prouted", "intentionally", displayParams);
    }
}
