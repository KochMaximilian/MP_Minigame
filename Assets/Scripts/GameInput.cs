using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateStarted;
    public event EventHandler OnInteractAlternatePerformed;
    public event EventHandler OnInteractAlternateCanceled;

    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += OnInteractPerformed;

        // Use named methods so we can unsubscribe in OnDisable
        playerInputActions.Player.InteractAlternate.started += OnInteractAlternateStartedPerformed;
        playerInputActions.Player.InteractAlternate.performed += OnInteractAlternatePerformedPerformed;
        playerInputActions.Player.InteractAlternate.canceled += OnInteractAlternateCanceledPerformed;
    }

    private void OnDisable() {
        if (playerInputActions == null) return;

        playerInputActions.Player.Interact.performed -= OnInteractPerformed;
        playerInputActions.Player.InteractAlternate.started -= OnInteractAlternateStartedPerformed;
        playerInputActions.Player.InteractAlternate.performed -= OnInteractAlternatePerformedPerformed;
        playerInputActions.Player.InteractAlternate.canceled -= OnInteractAlternateCanceledPerformed;

        playerInputActions.Player.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx) =>
        OnInteractAction?.Invoke(this, EventArgs.Empty);

    private void OnInteractAlternateStartedPerformed(InputAction.CallbackContext ctx) =>
        OnInteractAlternateStarted?.Invoke(this, EventArgs.Empty);

    private void OnInteractAlternatePerformedPerformed(InputAction.CallbackContext ctx) =>
        OnInteractAlternatePerformed?.Invoke(this, EventArgs.Empty);

    private void OnInteractAlternateCanceledPerformed(InputAction.CallbackContext ctx) =>
        OnInteractAlternateCanceled?.Invoke(this, EventArgs.Empty);

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.sqrMagnitude > 0f ? inputVector.normalized : Vector2.zero;
    }
}