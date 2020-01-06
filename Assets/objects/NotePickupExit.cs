using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePickupExit : NotePickupParent
{
    protected override string Note()
    {
        return "You found the secret data and can take down the evil organization. You win.";
    }
    protected override void Effect()
    {
        ConsoleHistoryController consoleHistory = FindObjectOfType<ConsoleHistoryController>();
        consoleHistory.AddOutput("YOU WIN!");
    }
}
