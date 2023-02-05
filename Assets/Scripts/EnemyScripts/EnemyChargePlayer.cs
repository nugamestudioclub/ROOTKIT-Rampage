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
                else
                {
                    Charge();
                }
                break;
            case ChargeState.Cooldown:
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
        return GameState.Instance.EnemyTarget.transform.position;
    }
}

enum ChargeState
{
    Charging, Cooldown
}