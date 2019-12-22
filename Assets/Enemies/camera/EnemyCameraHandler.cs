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
    float timerFlash;
    float timerON;
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
        timerFlash = 0;
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
                timerFlash = 0;
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
        return CameraName.ToLower();
    }

    void Flash()
    {
        timerFlash += Time.deltaTime;

        timerBlink = (timeToEnable - disabledTimer)/2;

        if (timerFlash > timerBlink)
        {
            if (mask.enabled)
            {
                mask.enabled = false;
                timerFlash = 0;
            }
            else
            {
                mask.enabled = true;
                timerFlash -= timerFlash / 2;
            }
        }
    }

    public void DisableCamera()
    {
        if (state == CameraState.enabled)
        {
            state = CameraState.disabled;
            disabledTimer = 0;
        }
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
