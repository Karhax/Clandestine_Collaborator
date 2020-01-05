using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CommandHandlerScript : MonoBehaviour
{
    string[] inputList;

    [SerializeField] ConsoleHistoryController consoleHistory;

    Dictionary<string, string> commandDescription;

    bool enableReboot = false;
    bool enablePrint = false;
    bool enableOpen = false;

    void Start()
    {
        commandDescription = new Dictionary<string, string>();

        commandDescription.Add("help", "Lists possible commands. Append -? to a command to get a detailed description");
        commandDescription.Add("quit", "Exits the game.");
        /*
        AddRebootCommand();
        AddPrintCommand();
        AddOpenCommand();
        */
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
            case string a when a.Equals("print") && enablePrint:
                Print();
                break;
            case string a when a.Equals("open") && enableOpen:
                Open();
                break;
            case string a when a.Equals("quit"):
                Application.Quit();
                break;
            default:
                UnknownCommand(inputList[0]);
                break;
        }
    }
    void UnknownCommand(string command)
    {
        consoleHistory.AddOutput(command + " is not a known command. Try using 'help' to see a list of commands.");
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
            if (inputList[i].ToLower().Equals("camera"))
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
                consoleHistory.AddOutput("Reboot command. Follow reboot with the type of device you wish to reboot.\nTypes are:\ncamera\nExample:\nreboot camera cameraid");
                finished = true;
            }
        }
        if (!finished)
        {
            consoleHistory.AddOutput("Could not complete reboot command, no type given.");
        }

    }
    void Print()
    {
        PrinterHandler[] printers;
        bool finished = false;

        printers = FindObjectsOfType<PrinterHandler>();

        for (int i = 0; i < inputList.Length; i++)
        {
            if (inputList[i].ToLower().Equals("print"))
            {
                if (i + 1 == inputList.Length && !finished)
                {
                    consoleHistory.AddOutput("Could not print, no ID specified.");
                    finished = true;
                }
                else
                {
                    for (int j = 0; j < printers.Length; j++)
                    {
                        Debug.Log("j:" + j + " i:" + i + " inputlist:" + inputList.Length);
                        if (inputList[i + 1].ToLower().Equals(printers[j].ID()))
                        {
                            printers[j].Print();
                            consoleHistory.AddOutput("Started printing on Printer with ID '" + inputList[i + 1] + "'.");
                            finished = true;
                        }
                    }
                    if (!finished && !inputList[i + 1].ToLower().Contains("-?"))
                    {
                        consoleHistory.AddOutput("Could not find printer with ID '" + inputList[i + 1] + "'.");
                        finished = true;
                    }
                }
                if (inputList[i].ToLower().Contains("-?"))
                {
                    consoleHistory.AddOutput("Printing command that causes a printer to continuously shake and make noise while\ntrying to print an incompatible number of pages.");
                    finished = true;
                }
            }
        }
        if (!finished)
        {
            consoleHistory.AddOutput("Could not complete print command");
        }

    }

    void Open()
    {
        DoorHandler[] doors;
        bool finished = false;

        doors = FindObjectsOfType<DoorHandler>();

        for (int i = 0; i < inputList.Length; i++)
        {
            if (inputList[i].ToLower().Contains("open"))
            {
                if (i + 1 == inputList.Length && !finished)
                {
                    consoleHistory.AddOutput("Could not open door, no ID specified.");
                    finished = true;
                }
                else
                {
                    for (int j = 0; j < doors.Length; j++)
                    {
                        if (inputList[i + 1].ToLower().Equals(doors[j].ID()))
                        {
                            if (doors[j].Password() == "")
                            {
                                doors[j].Open();
                                consoleHistory.AddOutput("Opened door with ID '" + inputList[i + 1] + "'.");
                                finished = true;
                            }
                            else
                            {
                                if (inputList.Length > i + 2)
                                {

                                    if (inputList[i + 2].ToLower().Equals(doors[j].Password()))
                                    {
                                        doors[j].Open();
                                        consoleHistory.AddOutput("Opened door with ID '" + inputList[i + 1] + "'.");
                                        finished = true;
                                    }
                                    else
                                    {
                                        consoleHistory.AddOutput("Could not open door with ID '" + inputList[i + 1] + "'. Wrong password specified.");
                                        finished = true;
                                    }
                                }
                                else
                                {
                                    consoleHistory.AddOutput("Could not open door with ID '" + inputList[i + 1] + "'. No password specified.");
                                    finished = true;
                                }

                            }
                        }
                    }
                    if (!finished && !inputList[i + 1].ToLower().Contains("-?"))
                    {
                        consoleHistory.AddOutput("Could not find door with ID '" + inputList[i + 1] + "'.");
                        finished = true;
                    }
                }
            }
            if (inputList[i].ToLower().Contains("-?"))
            {
                consoleHistory.AddOutput("Door opening command that will open any door in the facility.\nSome doors have a password which must be included after the ID");
                finished = true;
            }
        }
        if (!finished)
        {
            consoleHistory.AddOutput("Could not complete open command");
        }

    }
    public void AddRebootCommand()
    {
        if (!enableReboot)
        {
            enableReboot = true;
            commandDescription.Add("reboot", "Reboots a device on the network.");
        }
    }

    public void AddPrintCommand()
    {
        if (!enablePrint)
        {
            enablePrint = true;
            commandDescription.Add("print", "Cause a printer to print continuously");
        }
    }

    public void AddOpenCommand()
    {
        if (!enableOpen)
        {
            enableOpen = true;
            commandDescription.Add("open", "Opens a door");
        }
    }
}
