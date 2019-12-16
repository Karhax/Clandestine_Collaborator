using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandlerScript : MonoBehaviour
{
    string[] commandList;
    string command;
    [SerializeField]ConsoleHistoryController consoleHistory;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Input(string s)
    {
        commandList = s.Split(' ');

        switch (commandList[0].ToLower())
        {
            case string a when a.Equals("help"):
                Help();
                break;
        }
    }
    void Help()
    {
        consoleHistory.AddOutput("HELP REQUESTED");
        for (int i = 1;i < commandList.Length;i++)
        {
            consoleHistory.AddOutput("command: "+commandList[i]);
        }
    }
}
