using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCameraHandler : MonoBehaviour
{
    // Start is called before the first frame update

    TextMesh cameraID;
    Transform cameraIDtransform;
    Collider2D cld;
    [SerializeField] string CameraName;
    SpriteMask mask;
    float timerBlink;
    float timer;
    float disabledTimer;
    [SerializeField] float timeToEnable = 8f;
    enum CameraState
    {
        disabled,
        flashing,
        enabled
    }
    CameraState state;

    void Start()
    {
        cameraID = GetComponentInChildren<TextMesh>();
        cameraIDtransform = GetComponentInChildren<TextMesh>().transform;
        cameraID.text = CameraName;
        cld = GetComponent<Collider2D>();
        mask = GetComponentInChildren<SpriteMask>();
        state = CameraState.enabled;
        timerBlink = 0;
        timer = 0;
        disabledTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DisableCamera();
        }

        cameraIDtransform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        
        switch (state)
        {
            case CameraState.enabled:
                mask.enabled = true;
                cld.enabled = true;
                timerBlink = 0;
                timer = 0;
                disabledTimer = 0;
                break;
            case CameraState.flashing:
                Flash();
                cld.enabled = false;
                disabledTimer += Time.deltaTime;
                if (disabledTimer >= timeToEnable)
                {
                    state = CameraState.enabled;
                }
                break;
            case CameraState.disabled:
                mask.enabled = false;
                cld.enabled = false;
                disabledTimer += Time.deltaTime;
                if(disabledTimer >= timeToEnable/2)
                {
                    state = CameraState.flashing;
                }
                break;

        }
    }

    public string ID()
    {
        return CameraName;
    }

    void Flash()
    {
        timer += Time.deltaTime;

        timerBlink = (timeToEnable - disabledTimer) /2;

        if (timer > timerBlink)
        {
            timer = 0;
            if(mask.enabled)
            {
                mask.enabled = false;
            }
            else
            {
                mask.enabled = true;
            }
        }
    }

    public void DisableCamera()
    {
        state = CameraState.disabled;
        disabledTimer = 0;
    }
}
