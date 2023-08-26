using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")] 
    [SerializeField] [Range(0.1f, 15f)] private float speed = 5;
    [SerializeField] [Range(0f, 10f)] private float velocity = 2;
    [SerializeField] [Range(0.005f, 0.1f)] private float rotateSpeed = 0.01f;

    [Header("Dash")]
    [SerializeField] [Range(0f, 5)] private float forcePower = 1;
    [SerializeField] [Range(0f, 1f)] private float dashTime = 1f;
    [SerializeField] [Range(0f, 2f)] private float dashCooldown = 0.5f;

    private Vector2 _direction = Vector2.down;
    private Vector2 _inputDirection;
    private float _axisVertical = 0;
    private float _axisHorizontal = 0;
    private float _horizontalInput;
    private float _verticalInput;
    
    private Rigidbody2D _rb;

    private float _elapsedTime;
    private float _percentageComplete;

    private bool _canDash = true;
    private bool _isDash;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        print(_canDash);

        if (Input.GetKeyDown(KeyCode.Space) && _canDash)
            _isDash = true;
        
        Dash();
    }

    private void Move()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (_horizontalInput != 0 && _verticalInput == 0)
            _axisVertical = Mathf.MoveTowards(_axisVertical, 0, rotateSpeed);
        if (_verticalInput != 0 && _horizontalInput == 0)
            _axisHorizontal = Mathf.MoveTowards(_axisHorizontal, 0, rotateSpeed);

        _axisHorizontal = Mathf.Clamp(_axisHorizontal + _horizontalInput * rotateSpeed, -1.0f, 1.0f);
        _axisVertical = Mathf.Clamp(_axisVertical + _verticalInput * rotateSpeed, -1.0f, 1.0f);

        if (_axisHorizontal != 0 || _axisVertical != 0)
            _inputDirection = new Vector2(_axisHorizontal, _axisVertical);

        _direction = Vector2.Lerp(_direction, _inputDirection, rotateSpeed);

        transform.position += (Vector3)_direction.normalized * (speed * Time.deltaTime);
        speed += velocity / 1000f;
    }

    private void Dash()
    {
        if (_percentageComplete > 1.0f) 
            _isDash = false;
        
        if (!_isDash || _percentageComplete > 1.0f || (_horizontalInput == 0 && _verticalInput == 0)) return;
        
        _canDash = false;
        _elapsedTime += Time.deltaTime;
        _percentageComplete = _elapsedTime / dashTime;
        
        StartCoroutine(DashCooldown());
        
        Vector2 dashDirection = new Vector2(_horizontalInput, _verticalInput);
        transform.position = Vector3.Lerp(transform.position, transform.position + (Vector3)(dashDirection * forcePower), _percentageComplete);
        _direction = dashDirection;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        
        _percentageComplete = 0;
        _elapsedTime = 0;
        _canDash = true;
    }
}