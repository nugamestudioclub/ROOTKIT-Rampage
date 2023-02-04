using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private Vector2 _moveInput;
    [SerializeField]
    private Vector2 _lookInput;

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
    }

    private void Update()
    {
        #region GETTING INPUTS
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
    }

    // Update is called once per framew
    void FixedUpdate()
    {
        #region MOVEMENT
        _rb.AddForce(_moveInput * _moveSpeed);
        #endregion
    }

    public void Shoot()
    {

    }
}
