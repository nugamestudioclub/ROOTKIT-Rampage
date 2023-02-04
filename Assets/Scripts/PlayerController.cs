using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform rotator;
    public Transform barrel;
    
    private Rigidbody2D _rb;

    [SerializeField]
    private Vector2 _moveInput;
    [SerializeField]
    private Vector2 _lookInput;
    [SerializeField]
    private float _lookAngle;

    [SerializeField]
    private float _moveSpeed;

    private float _xMoveInput;
    private float _yMoveInput;
    private float _xLookInput;
    private float _yLookInput;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _lookInput = new Vector2(0, 1);
        GameState.Instance.EnemyTarget = gameObject;
    }

    private void Update()
    {
        #region GETTING MOVEMENT INPUTS
        if(Input.GetKey(KeyCode.W))
        {
            _yMoveInput = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            _yMoveInput = -1;
        }
        else
        {
            _yMoveInput = 0;
        }

        if(Input.GetKey(KeyCode.D))
        {
            _xMoveInput = 1;
        }
        else if(Input.GetKey(KeyCode.A))
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

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            _lookInput = new Vector2(_xLookInput, _yLookInput).normalized;
        }
        #endregion
    }

    // Update is called once per framew
    void FixedUpdate()
    {
        #region MOVEMENT
        _rb.AddForce(_moveInput * _moveSpeed);
        #endregion

        #region LOOKING
        _lookAngle = Mathf.Atan2(_lookInput.x, -_lookInput.y) * Mathf.Rad2Deg;
        rotator.rotation = Quaternion.Euler(0, 0, _lookAngle);
        #endregion

    }

    public void Shoot()
    {

    }
}
