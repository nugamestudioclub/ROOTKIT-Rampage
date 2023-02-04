using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargePlayer : MonoBehaviour
{
    [SerializeField]
    private float _chargeSpeed = 2;
    [SerializeField]
    private float _cooldownTime = 3;
    [SerializeField]
    private ChargeState _state = ChargeState.Charging;

    [SerializeField]
    private float _timer;
    private Vector2 _playerPos;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(_state);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStateMachine();
    }

    void UpdateStateMachine()
    {
        switch (_state)
        {
            case ChargeState.Charging:
                if (Vector2.Distance(_playerPos, transform.position) < Mathf.Epsilon)
                {
                    ChangeState(ChargeState.Cooldown);
                }
                else
                {
                    Charge();
                }
                break;
            case ChargeState.Cooldown:
                if (Timer())
                {
                    ChangeState(ChargeState.Charging);
                }
                break;
        }
    }

    void Charge()
    {
        Vector2 thisPos = this.transform.position;
        Vector2 nextPos = Vector2.MoveTowards(thisPos, GetPlayerPosition(), _chargeSpeed * Time.deltaTime);
        this.transform.Translate(nextPos - thisPos);
    }

    // Counts down, returns true if time is finished
    bool Timer()
    {
        this._timer += Time.deltaTime;
        return _cooldownTime < _timer;
    }

    void ChangeState(ChargeState newState)
    {
        this._state = newState;
        switch (_state)
        {
            case ChargeState.Charging:
                this._playerPos = GetPlayerPosition();
                break;
            case ChargeState.Cooldown:
                this._timer = 0;
                break;
        }

    }

    Vector2 GetPlayerPosition()
    {
        // TODO actually get player position
        return new Vector2();
    }
}

enum ChargeState
{
    Charging, Cooldown
}