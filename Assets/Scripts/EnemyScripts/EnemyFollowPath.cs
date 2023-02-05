using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyFollowPath : Enemy
{

    public PathCreator pathCreator;
    [SerializeField]
    private PathCreation.EndOfPathInstruction _pathInstruction = PathCreation.EndOfPathInstruction.Reverse;
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private float _distanceTraveled;

    private bool reversed = false;

    // Update is called once per frame
    protected override void EnemyUpdate()
    {
        _distanceTraveled += _speed * Time.deltaTime;
        if (pathCreator == null)
        {
            Debug.Log($"path creator of {name} isnt set");
        }
        switch (_pathInstruction)
        {
            case EndOfPathInstruction.Reverse:
                if (_distanceTraveled >= 2 * pathCreator.path.length)
                {
                    _distanceTraveled -= 2 * pathCreator.path.length;
                }
                if (_distanceTraveled >= pathCreator.path.length)
                {
                    reversed = true;
                }
                else
                {
                    reversed = false;
                }
                break;
        }
        this.transform.position = pathCreator.path.GetPointAtDistance(_distanceTraveled, _pathInstruction);
        Vector3 dir = pathCreator.path.GetDirectionAtDistance(_distanceTraveled, _pathInstruction);
        float deg;
        if (reversed)
        {
            deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        }
        else
        {
            deg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        }
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, deg);
    }
}
