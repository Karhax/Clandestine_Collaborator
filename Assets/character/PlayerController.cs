using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(1,20)] float speed = 5f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2();
        pos.x += Input.GetAxis("Horizontal")*Time.deltaTime*speed;
        pos.y += Input.GetAxis("Vertical") * Time.deltaTime*speed;

        rb.AddForce(pos, ForceMode2D.Impulse);

        Vector2 v = rb.velocity;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        if (v.magnitude > 0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), 0.1f);
        }
    }
}
