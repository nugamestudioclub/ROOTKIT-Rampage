using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileObstacle : MonoBehaviour
{

    [SerializeField]
    private float hackingTime = 3.0f;
    [SerializeField]
    private GameObject corruptExplosion;

    private float currentCountdownTime;


    public bool IsHacked { get; private set; }

    private void Awake()
    {
        IsHacked = false;
        currentCountdownTime = hackingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHacked)
        {
            //countdown
            currentCountdownTime -= Time.deltaTime;
            if (currentCountdownTime < 0)
            {
                Corrupt();
            }
        }
    }

    public void Hack() => IsHacked = true;

    public void Corrupt()
    {
        // create explosion
        Instantiate(corruptExplosion, 
            transform.position, 
            transform.rotation);
        // delete file
        Destroy(gameObject);
    }
}
