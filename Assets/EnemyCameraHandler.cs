using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameraHandler : MonoBehaviour
{
    // Start is called before the first frame update

    TextMesh cameraID;
    Transform cameraIDtransform;
    [SerializeField] string CameraName;

    void Start()
    {
        cameraID = GetComponentInChildren<TextMesh>();
        cameraIDtransform = GetComponentInChildren<TextMesh>().transform;
        cameraID.text = CameraName;
    }

    // Update is called once per frame
    void Update()
    {
        cameraIDtransform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public string ID()
    {
        return CameraName;
    }
}
