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

        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        pluginInstance.Call("Initialize", activity);
#endif
    }

    [ContextMenu("SendTimeLog")]
    public void SendTimeLog()
    {
        Debug.Log("Unity - SendTimeLog");
#if UNITY_ANDROID || UNITY_EDITOR
        pluginInstance.Call("SendLog", Time.time.ToString());
#endif
        UpdateLogs();
        SaveLogs();
    }

    public void SendLog(string text)
    {
        Debug.Log("Unity - SendLog");
#if UNITY_ANDROID || UNITY_EDITOR
        pluginInstance.Call("SendLog", text);
#endif
        UpdateLogs();
        SaveLogs();
    }

    public void ReadLogsFromFile()
    {
        Debug.Log("Unity - SendLog");
#if UNITY_ANDROID || UNITY_EDITOR
        label.text = pluginInstance.Call<string>("ReadFromFile");
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

    public void SaveLogs()
    {
        Debug.Log("Unity - SaveLogs");
#if UNITY_ANDROID || UNITY_EDITOR
        pluginInstance.Call("WriteToFile");
#endif
    }

    public void DeleteLogs()
    {
        Debug.Log("Unity - DeleteLogs");
#if UNITY_ANDROID || UNITY_EDITOR
        pluginInstance.Call("DeleteLogs");
#endif
    }
}