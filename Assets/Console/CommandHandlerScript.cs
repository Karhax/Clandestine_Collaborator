using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CommandHandlerScript : MonoBehaviour
{
    string[] inputList;

    [SerializeField] ConsoleHistoryController consoleHistory;

    Dictionary<string, string> commandDescription;

    bool enableReboot = false;

    void Start()
    {
        commandDescription = new Dictionary<string, string>();

        commandDescription.Add("help", "Lists possible commands. Append -? to a command to get a detailed description");

        AddRebootCommand();
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
            case string a when a.Equals("reboot") && enableReboot:
                Reboot();
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
        foreach (KeyValuePair<string, string> entry in commandDescription)
        {
            consoleHistory.AddOutput(entry.Key.ToUpper() + "  " + entry.Value);
        }
    }
    void Reboot()
    {
        EnemyCameraHandler[] cam;
        bool finished = false;

        cam = FindObjectsOfType<EnemyCameraHandler>();

        for (int i = 0; i < inputList.Length; i++)
        {
            if (inputList[i].ToLower().Contains("camera"))
            {
                if (i + 1 == inputList.Length)
                {
                    consoleHistory.AddOutput("Could not reboot camera, no ID specified.");
                    finished = true;
                }
                else
                {
                    for (int j = 0; j < cam.Length; j++)
                    {
                        if (inputList[i + 1].ToLower().Equals(cam[j].ID()))
                        {
                            cam[j].DisableCamera();
                            consoleHistory.AddOutput("Rebooted camera with ID '" + inputList[i + 1] + "'.");
                            finished = true;
                        }
                    }
                    if (!finished)
                    {
                        consoleHistory.AddOutput("Could not find camera with ID '" + inputList[i + 1] + "'.");
                        finished = true;
                    }
                }
            }
            if (inputList[i].ToLower().Contains("-?"))
            {
                consoleHistory.AddOutput("Reboot command. Follow reboot with the type of device you wish to reboot.\nTypes are camera.\nExample:\nreboot camera cameraid");
                finished = true;
            }
            }
        if (!finished)
        {
            consoleHistory.AddOutput("Could not complete reboot command, no type given.");
        }

    }

    public void AddRebootCommand()
    {
        enableReboot = true;
        commandDescription.Add("reboot", "Reboots a device on the network.");
    }
}
