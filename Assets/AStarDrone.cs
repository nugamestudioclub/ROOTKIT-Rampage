using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarDrone : MonoBehaviour
{
    public Transform targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        Seeker seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);
    }

    public void OnPathComplete(Path p) {
            Debug.Log("epicly found path" + p.error);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
