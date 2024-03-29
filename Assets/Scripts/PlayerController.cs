using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform rotator;
    public Transform barrel;
    public Transform self;

    public GameObject bullet;
    public GameObject decoy;
    public GameObject barrier;

    private Rigidbody2D _rb;

    [SerializeField]
    private Vector2 _moveInput;
    [SerializeField]
    private Vector2 _lookInput;
    [SerializeField]
    private float _lookAngle;

    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _fireSpeed;
    [SerializeField]
    private float _fireCooldown;

    private float _xMoveInput;
    private float _yMoveInput;
    private float _xLookInput;
    private float _yLookInput;
    private float _lastFireTime;

    [SerializeField]
    private float _hackCooldown = 10;
    private float _lastHackTime;
    [SerializeField]
    private float _decoyCooldown = 15;
    private float _lastDecoyTime;
    [SerializeField]
    private float _barrierCooldown = 5;
    private float _lastBarrierTime;

    private Animator animator;

    private bool _dead = false;
    private bool _aiming = false;
    private bool _shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        self = gameObject.GetComponent<Transform>();
        animator = gameObject.GetComponent<Animator>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _lookInput = new Vector2(0, 1);
        GameState.Instance.EnemyTarget = gameObject;
        GameState.Instance.ResetPlayer();
        _lastBarrierTime = -_barrierCooldown;
        _lastDecoyTime = -_decoyCooldown;
        _lastHackTime = -_hackCooldown;
    }

    private void Update()
    {
        #region GETTING MOVEMENT INPUTS
        if (Input.GetKey(KeyCode.W))
        {
            _yMoveInput = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _yMoveInput = -1;
        }
        else
        {
            _yMoveInput = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _xMoveInput = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _xMoveInput = -1;
        }
        else
        {
            _xMoveInput = 0;
        }

        _moveInput = new Vector2(_xMoveInput, _yMoveInput).normalized;

        #endregion

        #region GETTING LOOK INPUTS
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _yLookInput = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _yLookInput = -1;
        }
        else
        {
            _yLookInput = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _xLookInput = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _xLookInput = -1;
        }
        else
        {
            _xLookInput = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow) 
            || Input.GetKey(KeyCode.DownArrow) 
            || Input.GetKey(KeyCode.RightArrow) 
            || Input.GetKey(KeyCode.LeftArrow))
        {
            _lookInput = new Vector2(_xLookInput, _yLookInput).normalized;
            if (Time.time > _lastFireTime + _fireCooldown)
            {
                _aiming = true;
            }
        }
        #endregion

        #region GETTING SHOOT INPUTS

        #endregion

        #region GETTING ABILITY INPUTS
        GameState.Instance.DecoyCooldown =
            Mathf.Max(0, _decoyCooldown - (Time.time - _lastDecoyTime));
        GameState.Instance.HackCooldown =
            Mathf.Max(0, _hackCooldown - (Time.time - _lastHackTime));
        GameState.Instance.BarrierCooldown =
            Mathf.Max(0, _barrierCooldown - (Time.time - _lastBarrierTime));
        if (Input.GetKey(KeyCode.Q) && Time.time > _lastDecoyTime + _decoyCooldown)
        {
            Decoy();
        }

        if (Input.GetKey(KeyCode.E) && Time.time > _lastBarrierTime + _barrierCooldown)
        {
            Barrier();
        }

        #endregion
    }

    public void Die()
    {
        _dead = true;
        Debug.Log("die");
        animator.Play("Dying");
    }

    // Update is called once per framew
    void FixedUpdate()
    {
        if (_dead) return;

        #region MOVEMENT
        _rb.AddForce(_moveInput * _moveSpeed);
        #endregion


        #region LOOKING
        _lookAngle = Mathf.Atan2(_lookInput.y, _lookInput.x) * Mathf.Rad2Deg + 90;
        rotator.rotation = Quaternion.Euler(0, 0, _lookAngle);
        self.rotation = Quaternion.Euler(0, 0, _lookAngle + 180);

        if (_aiming)
        {
            if (_shooting)
            {
                Shoot();
                _aiming = false;
                _shooting = false;
            }
            else
            {
                _shooting = true;
            }
        }
        #endregion

        if (_moveInput.magnitude > 0.1)
        {
            animator.Play("Running");
            PlayMoveSound();
        }
        else
        {
            animator.Play("Idle");
        }
    }

    [SerializeField]
    private int moveWaitMs = 1000;

    private DateTime lastMoveTime;

    private void PlayMoveSound()
    {
        var now = DateTime.Now;
        if (now >= lastMoveTime.AddMilliseconds(moveWaitMs))
        {
            AudioManager.Instance.Move();
            lastMoveTime = now;
        }
    }

    public void Shoot()
    {
        GameObject firedBullet = Instantiate(bullet, barrel.position, Quaternion.identity);
        firedBullet.GetComponent<Rigidbody2D>().velocity = barrel.up * -_fireSpeed;
        AudioManager.Instance.Shoot();
        _lastFireTime = Time.time;
    }

    public void Decoy()
    {
        Instantiate(decoy, transform.position, Quaternion.identity);
        AudioManager.Instance.DecoyBlip();
        _lastDecoyTime = Time.time;
    }

    public void Barrier()
    {
        Instantiate(barrier, transform.position, Quaternion.identity);
        _lastBarrierTime = Time.time;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        FileObstacle file;
        Debug.Log($"should hack?: {ShouldHack()}");
        if (ShouldHack() && collision.TryGetComponent(out file))
        {
            _lastHackTime = Time.time;
            file.Hack();
            AudioManager.Instance.DialUp();
        }
        Enemy enemy;
        if (collision.TryGetComponent(out enemy))
        {
            GameState.Instance.DamagePlayer(1);
        }
    }

    private bool ShouldHack()
    {
        if (Input.GetKey(KeyCode.R)
            && Time.time > _lastHackTime + _hackCooldown)
        {

            return true;
        }
        return false;
    }
}
