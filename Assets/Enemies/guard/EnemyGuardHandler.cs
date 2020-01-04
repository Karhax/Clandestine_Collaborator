using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardHandler : MonoBehaviour
{
    Collider2D cld;
    SpriteMask mask;
    PrinterHandler printerHandler;
    [SerializeField] List<Transform> patrolPositions;

    Vector3 targetPosition;

    int currentPoint = 0;
    bool waiting = false;
    bool distracted = false;
    [SerializeField] float waitTime = 3f;
    float timer = 0f;
    [SerializeField] float speed = 2f;
    [SerializeField] float rotSpeed = 45f;

    // Start is called before the first frame update
    void Start()
    {
        cld = GetComponentInChildren<Collider2D>();
        mask = GetComponentInChildren<SpriteMask>();
    }

    public void Distract(PrinterHandler printer)
    {
        targetPosition = printer.transform.position;
        distracted = true;
        printerHandler = printer;

        timer = 0;
        waiting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!distracted)
        {
            targetPosition = patrolPositions[currentPoint].position;
        }
        if (waiting)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                timer = 0;
                waiting = false;
                if (distracted)
                {
                    printerHandler.StopPrint();
                    Debug.Log("stopped print" + printerHandler.name);
                    distracted = false;
                }
            }
        }
        else
        {
            Vector3 dir = targetPosition - transform.position;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            Quaternion qto = Quaternion.AngleAxis(angle, Vector3.back);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, qto, rotSpeed * Time.deltaTime);

            if (Vector3.Distance(targetPosition, transform.position) < 0.1f)
            {
                waiting = true;
                currentPoint++;
                if (currentPoint >= patrolPositions.Count)
                {
                    currentPoint = 0;
                }
            }
            else if (qto == transform.rotation)
            {
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }
        }
    }
}
