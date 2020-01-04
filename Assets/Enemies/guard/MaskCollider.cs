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
        SpriteRenderer guard = transform.parent.GetComponentInChildren<SpriteRenderer>();

        if (player != null)
        {
            LayerMask mask = LayerMask.GetMask("Enviroment");
            Vector2 direction = (player.transform.position - guard.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(guard.transform.position,direction,direction.magnitude,mask);
            Debug.DrawRay(guard.transform.position, direction, Color.red);
            if (hit.transform == null)
            {
                player.RaiseSuspicion(50 * Time.deltaTime);
            }
        }
    }
}
