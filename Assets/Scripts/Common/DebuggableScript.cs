using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggableScript
{
    // logs a debug message in the following format: '[{componentName}]: {message}'
    public static void DebugLog(string componentName, string message)
    {
        Debug.Log("[" + componentName + "]: " + message);
    }
}
