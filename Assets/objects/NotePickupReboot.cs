using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickupReboot : NotePickupParent
{
    protected override string Note()
    {
        return "You find a USB stick that was placed by an agent earlier. It contains a command for your handler back at base to use.";
    }
    protected override void Effect()
    {
        CommandHandlerScript commandHandler = FindObjectOfType<CommandHandlerScript>();
        commandHandler.AddRebootCommand();
    }
}
