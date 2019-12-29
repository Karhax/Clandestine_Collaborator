using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.RaiseSuspicion(50 * Time.deltaTime);
        }
    }
}
