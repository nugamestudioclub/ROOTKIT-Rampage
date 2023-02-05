using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargePlayer : Enemy
{
    [SerializeField]
    private float _chargeSpeed = 2;
    [SerializeField]
    private float _chargeTime = 3;

    [SerializeField]
    private float _cooldownTime = 3;
    [SerializeField]
    private ChargeState _state = ChargeState.Cooldown;

    private float _cooldownTimer; 
    private float _chargeTimer;
    private Vector2 _playerPos;
    [SerializeField]
    private float _rotationSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        this._cooldownTimer = 0;
        this._chargeTimer = 0;
        ChangeState(_state);
    }

    // Update is called once per frame
    protected override void EnemyUpdate()
    {
        UpdateStateMachine();
    }

    void UpdateStateMachine()
    {
        switch (_state)
        {
            case ChargeState.Charging:
                if (ChargingTimer())
                {
                    ChangeState(ChargeState.Cooldown);
                }
                else if (Vector2.Distance(_playerPos, transform.position) < Mathf.Epsilon)
                {
                    TurnToPlayer();
                }
                else 
                {
                    Charge();
                }
                break;
            case ChargeState.Cooldown:
                TurnToPlayer();
                if (CooldownTimer())
                {
                    ChangeState(ChargeState.Charging);
                }
                break;
        }
    }

    void Charge()
    {
        Vector2 thisPos = this.transform.position;
        Vector2 nextPos = Vector2.MoveTowards(thisPos, _playerPos, _chargeSpeed * Time.deltaTime);
        this.transform.Translate(nextPos - thisPos);
    }

    // Counts down, returns true if time is finished
    bool CooldownTimer()
    {
        this._cooldownTimer += Time.deltaTime;
        return _cooldownTime < _cooldownTimer;
    }

   bool ChargingTimer()
    {
        this._chargeTimer += Time.deltaTime;
        return _chargeTime < _chargeTimer;
    }

    void TurnToPlayer()
    {
        Vector2 thisPos = this.transform.position;
        Vector2 toPlayer = GetPlayerPosition() - thisPos;
        Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg - 90);
        sr.transform.rotation = Quaternion.Lerp(sr.transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }

    void ChangeState(ChargeState newState)
    {
        this._state = newState;
        switch (_state)
        {
            case ChargeState.Charging:
                this._chargeTimer = 0;
                this._playerPos = GetPlayerPosition();
                break;
            case ChargeState.Cooldown:
                this._cooldownTimer = 0;
                break;
        }

    }

    Vector2 GetPlayerPosition()
    {
        var target = GameState.Instance.EnemyTarget;
        return target == null
            ? transform.position
            : target.transform.position;
    }
}

enum ChargeState
{
    Charging, Cooldown
}