using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{

    [SerializeField]
    private float lifetime = 5f;
    private float remainingLifetime;
    // Start is called before the first frame update
    void Start()
    {
        GameState.Instance.EnemyTarget = gameObject;
        remainingLifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingLifetime -= Time.deltaTime;
        if (remainingLifetime <= 0)
        {
            Expire();
        }
    }

    void Expire()
    {
        GameState.Instance.EnemyTarget = GameState.Instance.FindPlayer();
        Destroy(gameObject);
    }
}
