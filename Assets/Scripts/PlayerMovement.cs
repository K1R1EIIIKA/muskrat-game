using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 25f)] private float speed = 5;
    [SerializeField] [Range(0f, 10f)] private float velocity = 2;
    [SerializeField] [Range(0.005f, 0.1f)] private float rotateSpeed = 0.01f;

    private Vector2 _direction = Vector2.down;
    private Vector2 _inputDirection;
    private float _axisVertical = 0;
    private float _axisHorizontal = 0;

    public static bool CanDash;

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && CanDash)
            Dash();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0 && verticalInput == 0)
            _axisVertical = Mathf.MoveTowards(_axisVertical, 0, rotateSpeed);
        if (verticalInput != 0 && horizontalInput == 0)
            _axisHorizontal = Mathf.MoveTowards(_axisHorizontal, 0, rotateSpeed);
        
        _axisHorizontal = Mathf.Clamp(_axisHorizontal + horizontalInput * rotateSpeed, -1.0f, 1.0f);
        _axisVertical = Mathf.Clamp(_axisVertical + verticalInput * rotateSpeed, -1.0f, 1.0f);
        
        if (_axisHorizontal != 0 || _axisVertical != 0)
            _inputDirection = new Vector2(_axisHorizontal, _axisVertical);

        _direction = Vector2.Lerp(_direction, _inputDirection, rotateSpeed);

        print(_direction);
        transform.position += (Vector3)_direction.normalized * (speed * Time.deltaTime);
        speed += velocity / 1000f;
    }

    private void Dash()
    {
    }
}