using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogManager : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private int logLimit = 5;
    [SerializeField] private Toggle logToggle;

    private string myLog;
    private GameObject logPanel;
    private Queue myLogQueue = new Queue();

    void Start()
    {
        logPanel = GameObject.Find("LogPanel");
        EnableLog();
    }

    public void EnableLog()
    {
        if (logToggle.isOn)
        {
            Application.logMessageReceived += HandleLog;
            logPanel.SetActive(true);
        }
        else
        {
            Application.logMessageReceived -= HandleLog;
            logPanel.SetActive(false);
            myLogQueue.Clear();
            myLog = "";
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "[" + type + "] : " + myLog + "\n";
        if (myLogQueue.Count >= logLimit)
            myLogQueue.Dequeue();
        myLogQueue.Enqueue(newString);

        if (type == LogType.Exception)
        {
            newString = "\n" + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;

        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    void OnGUI()
    {
        text.text = myLog;
    }
}
