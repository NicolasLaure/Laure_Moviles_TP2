using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoggerManager : MonoBehaviour
{
    public TextMeshProUGUI label;
    
    private const string pluginClassName = "com.laure.loggerplugin.LaureLogger";

#if UNITY_ANDROID || UNITY_EDITOR
    private AndroidJavaClass pluginClass;
    private AndroidJavaObject pluginInstance;
#endif
    void Start()
    {
        label.text = "Start";
        Debug.Log("Unity - " + pluginClassName);
#if UNITY_ANDROID || UNITY_EDITOR
        pluginClass = new AndroidJavaClass(pluginClassName);
        pluginInstance = pluginClass.CallStatic<AndroidJavaObject>("getInstance");
#endif
    }

    [ContextMenu("SendLog")]
    public void SendLog()
    {
        Debug.Log("Unity - SendLog");
#if UNITY_ANDROID || UNITY_EDITOR
        pluginInstance.Call("SendLog", Time.time.ToString());
#endif
    }

    [ContextMenu("UpdateLog")]
    public void UpdateLogs()
    {
        Debug.Log("Unity - UpdateLogs");
#if UNITY_ANDROID || UNITY_EDITOR
        label.text = pluginInstance.Call<string>("GetAllLogs");
#endif
    }
}