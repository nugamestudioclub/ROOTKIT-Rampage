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

    public SpriteRenderer sr;
    public SpriteRenderer stunRenderer;

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
        {
            EnemyUpdate();
            if (stunRenderer != null)
            {
                stunRenderer.enabled = false;
            }
        }
        else
        {
            stunTimer += Time.deltaTime;
            if (stunTimer >= stunDuration)
            {
                isStunned = false;
            }
            if(stunRenderer != null)
            {
                stunRenderer.enabled = true;
            }
        }
    }

    protected abstract void EnemyUpdate();

    public void Stun()
    {
        stunTimer = 0;
        isStunned = true;
        AudioManager.Instance.Stun();
    }

    public void Die()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
