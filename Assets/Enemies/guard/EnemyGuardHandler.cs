using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardHandler : MonoBehaviour
{
    Collider2D cld;
    SpriteMask mask;

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

    public void Distract(Vector3 pos)
    {
        targetPosition = pos;
        distracted = true;
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
            }
        }
        else
        {
            Vector3 dir = targetPosition - transform.position;

            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            Debug.Log(angle);

            Quaternion qto = Quaternion.AngleAxis(angle, Vector3.back);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, qto, rotSpeed * Time.deltaTime);


            if (qto == transform.rotation)
            {
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }


            if (Vector3.Distance(targetPosition, transform.position) < 0.1f)
            {
                waiting = true;
                currentPoint++;
                if (currentPoint >= patrolPositions.Count)
                {
                    currentPoint = 0;
                }
                distracted = false;
            }
        }
    }
}
