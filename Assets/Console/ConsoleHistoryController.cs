using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleHistoryController : MonoBehaviour
{
    Text text;
    List<string> stringList;
    List<string> commandHistory;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        stringList = new List<string>();
        commandHistory = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = null;
        if (stringList.Count > 50)
        {
            stringList.RemoveAt(0);
        }
        if (commandHistory.Count > 50)
        {
            commandHistory.RemoveAt(0);
        }
        foreach (string s in stringList)
        {
            text.text += "\n";
            text.text += s;
        }
    }

    public void AddInput(string line)
    {
        stringList.Add(line);
        commandHistory.Add(line);
    }

    public void AddOutput(string line)
    {
        stringList.Add(line);
    }
    public string GetCommandHistory(int age)
    {
        if (commandHistory.Count == 0)
        {
            return null;
        }
        if (commandHistory.Count-1 < age)
        {
            return commandHistory[0];
        }
        return commandHistory[commandHistory.Count - age];
    }
    public int GetCommandHistoryCount()
    {
        return commandHistory.Count;
    }
}
