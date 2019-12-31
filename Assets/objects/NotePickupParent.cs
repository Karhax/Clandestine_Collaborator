using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class NotePickupParent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected RectTransform panel;
    protected Text txt;
    protected void Start()
    {
        txt = panel.gameObject.GetComponentInChildren<Text>();
        txt.text = "HELLO";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract string Note();
    protected virtual void Effect()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            panel.gameObject.SetActive(true);
            txt.text = Note();
            Effect();
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            panel.gameObject.SetActive(false);
        }
    }
}
