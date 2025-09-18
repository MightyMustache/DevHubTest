using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;
    [SerializeField] private float _maxSpeed;
    private CharacterAnimation _characterAnimation;
    private CharacterRotation _characterRotation;
    private Vector3 _moveInput;
    private Vector3 _velocity;
    private bool _pause;
    private float _minX = -20f;
    private float _manX = 20f;
    private float _minZ = -8.5f;
    private float _maxZ = 4.5f;


    public Vector3 StartPosition { get; private set; }


    void Awake()
    {
        PlayerInputHandler.OnMoveInput += GetMoveInput;
        PauseManager.OnPause += GetPauseState;
    }

    private void Start()
    {
        StartPosition = transform.position;
        _characterAnimation = GetComponent<CharacterAnimation>();
        _characterRotation = GetComponent<CharacterRotation>();
    }


    void Update()
    {
        if (!_pause)
        {
            if (_moveInput != Vector3.zero)
            {
                _velocity = Vector3.MoveTowards(_velocity, _moveInput * _maxSpeed, _acceleration * Time.deltaTime);
            }
            else
            {
                _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, _deceleration * Time.deltaTime);
            }
            transform.position += _velocity * Time.deltaTime;
            _characterAnimation.ChangeVelocityParametr(Vector3.Magnitude(_velocity));
            _characterRotation.RotateCharacterTowards(_velocity);


            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, _minX, _manX);
            pos.z = Mathf.Clamp(pos.z, _minZ, _maxZ);
            transform.position = pos;
        }
    }

    private void GetMoveInput(Vector2 input)
    {
        _moveInput.x = input.x;
        _moveInput.z = input.y;
    }

    private void GetPauseState(bool pause)
    {
        _pause = pause;
    }

    public void DisableMovement(bool state)
    {
        _pause = state;
        if (state)
            _characterAnimation.ChangeVelocityParametr(0);
    }

    public void ResetPosition()
    {
        transform.position = StartPosition;
    }
}
