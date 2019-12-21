using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessSize : MonoBehaviour
{
    SpriteRenderer sr;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        sr.size = new Vector2(width,height);
    }
}
