using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class MoveInputEvent : UnityEvent<float> { }
[Serializable]
public class JumpInputEvent : UnityEvent<float> { }
[Serializable]
public class PauseInputEvent : UnityEvent<float> { }
public class InputController : MonoBehaviour
{
    [Header("Components")]
    private PlayerControls controls;
    public MoveInputEvent moveInputEvent;
    public JumpInputEvent jumpInputEvent;
    public PauseInputEvent pauseInputEvent;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.PlayerController.Enable();
        controls.PlayerController.Movement.performed += OnMovement;
        controls.PlayerController.Jump.performed += OnJump;
        controls.PlayerController.Pause.performed += OnPause;

        controls.PlayerController.Movement.canceled += OnMovement;
        controls.PlayerController.Jump.canceled += OnJump;
        controls.PlayerController.Pause.canceled += OnPause;
    }

    private void OnDisable()
    {
        controls.PlayerController.Disable();
    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        float moveInput = context.ReadValue<float>();
        moveInputEvent.Invoke(moveInput);
        Debug.Log($"Move input: {moveInput}");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        float jumpInput = context.ReadValue<float>();
        jumpInputEvent.Invoke(jumpInput);
        Debug.Log($"Jump input: {jumpInput}");
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        float pauseInput = context.ReadValue<float>();
        pauseInputEvent.Invoke(pauseInput);
        Debug.Log($"Pause input: {pauseInput}");
    }
}
