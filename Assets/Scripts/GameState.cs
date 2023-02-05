using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{

    public GameObject EnemyTarget
    {
        get => target;
        set => target = value;
    }

    public float HackCooldown { get; set; }
    public float DecoyCooldown { get; set; }
    public float BarrierCooldown { get; set; }

    [SerializeField]
    private FolderExit exit;

    [SerializeField]
    private int keysRequired;
    public int KeysRequired {
        get => keysRequired;  
    }

    public int KeyCount { get; private set; }

    [SerializeField]
    private float deathFadeoutTimer;

    public void CollectKey()
    {
        ++KeyCount;
        if (exit != null)
            exit.Locked = KeyCount < keysRequired;
    }

    public int PlayerHealth { get; private set; }
    [SerializeField]
    private int maxPlayerHealth = 3;

    [SerializeField]
    private float invincibilityTime = 1;
    private float lastDamage = 0;

    private bool playerDead;

    public static GameState Instance { get; private set; }

    private GameObject target;
    private GameObject player;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            ResetPlayer();
        }
    }

    public GameObject FindPlayer()
    {
        return GameObject.FindWithTag("Player");
    }

    public void ResetPlayerHealth()
    {
        PlayerHealth = maxPlayerHealth;
    }
    public void ResetPlayer()
    {
        ResetPlayerHealth();
        playerDead = false;
    }

    public void DamagePlayer(int value)
    {
        if (lastDamage + invincibilityTime < Time.time)
        {
            PlayerHealth -= value;
            if (PlayerHealth <= 0 && !playerDead)
            {
                playerDead = true;
                PlayerDie();
            }
            lastDamage = Time.time;
        }

    }

    public void PlayerDie()
    {
        //TODO Transition to Game over screen
        Debug.Log($"Player died, reseting in {deathFadeoutTimer}...");
        PlayerController player = FindPlayer().GetComponent<PlayerController>();
        player.Die();
        StartCoroutine(DeathFade());

    }

    IEnumerator DeathFade()
    {
        yield return new WaitForSeconds(deathFadeoutTimer);
        TransitionManager.ToGameOver();
    }


}
