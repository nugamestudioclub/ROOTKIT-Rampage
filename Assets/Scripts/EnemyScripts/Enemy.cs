using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool isStunned = false;
    [SerializeField]
    private float stunDuration = 5;
    [SerializeField]
    private float stunTimer;

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            EnemyUpdate();
        }
        else
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                isStunned = false;
            }
        }
    }

    protected abstract void EnemyUpdate();

    public void Stun()
    {
        stunTimer = 0;
        isStunned = true;
    }
}
