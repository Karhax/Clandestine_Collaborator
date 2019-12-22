using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSuspicion : MonoBehaviour
{
    [SerializeField]PlayerController playerController;
    RectTransform rectTf;
    // Start is called before the first frame update
    void Start()
    {
        rectTf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        RectTransform r = rectTf;
        r.localScale = new Vector3(playerController.GetSuspicion() / 100, 1f, 1f);
        rectTf = r;
    }
}
