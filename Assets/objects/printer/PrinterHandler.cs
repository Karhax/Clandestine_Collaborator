using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterHandler : MonoBehaviour
{
    TextMesh printerID;
    [SerializeField] string printerName;
    bool printing = false;
    float timer = 0;
    [SerializeField] float printTime = 5;
    Animation anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animation>();
        printerID = GetComponentInChildren<TextMesh>();
        printerID.text = printerName;
    }
    public string ID()
    {
        return printerName.ToLower();
    }
    EnemyGuardHandler GetEnemyClosestGuardHandler()
    {
        EnemyGuardHandler[] enemyGuards;
        enemyGuards = FindObjectsOfType<EnemyGuardHandler>();
        EnemyGuardHandler closestEnemy = null;
        float distance = 0;
        float tempDistance = 0;
        foreach (EnemyGuardHandler egh in enemyGuards)
        {
            tempDistance = Vector3.Distance(egh.transform.position, this.transform.position);
            if (tempDistance > distance)
            {
                distance = tempDistance;
                closestEnemy = egh;
            }
        }
        return closestEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (printing)
        {
            timer += Time.deltaTime;
            if(timer > printTime)
            {
                printing = false;
            }
        }
    }

    public void Print()
    {
        printing = true;
        anim.Play();
        GetEnemyClosestGuardHandler().Distract(this);
    }
    public void StopPrint()
    {
        printing = false;
        anim.Stop();
    }
}
