using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsoleController : MonoBehaviour
{
    Text text;
    [SerializeField] ConsoleHistoryController historyController;
    [SerializeField] CommandHandlerScript commandHandler;
    int AgeRequest = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (text.text.Length != 0)
                {
                    text.text = text.text.Substring(0, text.text.Length - 1);
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                if (text.text.Length > 0)
                {
                    historyController.AddInput(text.text);
                    commandHandler.Input(text.text);

                    text.text = null;
                    AgeRequest = 0;
                }
            }
            else
            {
                text.text += c;
            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (AgeRequest < historyController.GetCommandHistoryCount())
            {
                AgeRequest += 1;
            }
            text.text = historyController.GetCommandHistory(AgeRequest);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (AgeRequest > 1)
            {
                AgeRequest -= 1;
            }
            text.text = historyController.GetCommandHistory(AgeRequest);
        }
    }
}
