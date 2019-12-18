using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandlerScript : MonoBehaviour
{
    string[] inputList;

    [SerializeField]ConsoleHistoryController consoleHistory;

    Dictionary<string, string> commandDescription;

    void Start()
    {
        commandDescription = new Dictionary<string, string>();

        commandDescription.Add("help", "Lists possible commands. Append -? to a command to get a detailed description");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Input(string s)
    {
        inputList = s.Split(' ');

        switch (inputList[0].ToLower())
        {
            case string a when a.Equals("help"):
                Help();
                break;
            default:
                UnknownCommand();
                break;
        }
    }
    void UnknownCommand()
    {
        consoleHistory.AddOutput(inputList[0] + " is not a known command. Try using 'help' to see a list of commands.");
    }
    void Help()
    {
        foreach(KeyValuePair<string, string> entry in commandDescription)
        {
            consoleHistory.AddOutput(entry.Key.ToUpper() + "  " + entry.Value);
        }
    }
}
