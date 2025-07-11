using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInputAction;

    private InputAction _move;

    public static event Action<Vector2> OnMoveInput;


    private void Awake()
    {
        _playerInputAction = new PlayerInput();
    }

    private void OnEnable()
    {
        _move = _playerInputAction.Player.Move;

        _move.performed += ctx => OnMoveInput?.Invoke(ctx.ReadValue<Vector2>());
        _move.canceled += ctx => OnMoveInput?.Invoke(Vector2.zero);

        _move.Enable();
    }
    private void OnDisable()
    {
        _move.Disable();

        OnMoveInput = null;
    }

}
