using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectList<T> : ScriptableObject where T:ScriptableObject
{
    public List<T> list = new List<T>();
}