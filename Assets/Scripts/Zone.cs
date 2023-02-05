using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private readonly HashSet<Enemy> enemies = new HashSet<Enemy>();

    private bool hasPlayer;

    void Start()
    {
        StartCoroutine(DoStart());
    }

    private IEnumerator DoStart()
    {
        yield return new WaitForSeconds(1.5f);
        PutEnemiesToSleep(!hasPlayer);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            hasPlayer = true;
            PutEnemiesToSleep(false);
        }
        else
        {
            foreach (var enemy in obj.GetComponents<Enemy>())
                enemies.Add(enemy);
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            hasPlayer = false;
            PutEnemiesToSleep(true);
        }
    }

    private void PutEnemiesToSleep(bool value)
    {
        foreach (var enemy in enemies)
            enemy.Asleep = value;
    }
}