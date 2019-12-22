using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(1, 20)] float speed = 5f;
    Rigidbody2D rb;
    [SerializeField] [Range(1, 500)] float suspicion = 100f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(suspicion < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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

    public void RaiseSuspicion(float number)
    {
        suspicion -= number;
    }
    public float GetSuspicion()
    {
        return suspicion;
    }
}
