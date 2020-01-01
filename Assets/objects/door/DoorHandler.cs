using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    TextMesh doorID;
    Transform doorIDTransform;
    SpriteRenderer doorSprite;

    [SerializeField] string doorName;
    [SerializeField] string password;
    [SerializeField] float speed = 2;
    bool open = false;

    Vector3 startPosition;
    Vector3 goalPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        doorID = GetComponentInChildren<TextMesh>();
        doorIDTransform = GetComponentInChildren<TextMesh>().transform;
        doorSprite = GetComponent<SpriteRenderer>();
        goalPosition = transform.position;
        goalPosition.x -= doorSprite.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        doorIDTransform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        if (open)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        }
    }
    public string ID()
    {
        return doorName.ToLower();
    }

    public string Password()
    {
        return password;
    }

    public void Open()
    {
        open = true;
    }
    public void Close()
    {
        open = false;
    }
}
