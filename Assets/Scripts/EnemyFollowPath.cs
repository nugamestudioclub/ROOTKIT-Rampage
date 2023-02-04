using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyFollowPath : MonoBehaviour
{

    public PathCreator pathCreator;

    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private float _distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _distanceTraveled += _speed * Time.deltaTime;
        Vector2 thisPos = this.transform.position;
        Vector2 nextPos = pathCreator.path.GetPointAtDistance(_distanceTraveled);
        Vector2 alongPath = nextPos - thisPos;

        this.transform.Translate(alongPath);
        this.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(alongPath.normalized.y, alongPath.normalized.x) * Mathf.Rad2Deg);
        
    }
}
