using System;
using UnityEngine;

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

        // alternate phases
        playerInputActions.Player.InteractAlternate.started += ctx => OnInteractAlternateStarted?.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.InteractAlternate.performed += ctx => OnInteractAlternatePerformed?.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.InteractAlternate.canceled += ctx => OnInteractAlternateCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable() {
        playerInputActions.Player.Interact.performed -= OnInteractPerformed;

        // Unsubscribe: use explicit methods or disable whole action map (here we disable everything)
        playerInputActions.Player.InteractAlternate.started -= ctx => OnInteractAlternateStarted?.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.InteractAlternate.performed -= ctx => OnInteractAlternatePerformed?.Invoke(this, EventArgs.Empty);
        playerInputActions.Player.InteractAlternate.canceled -= ctx => OnInteractAlternateCanceled?.Invoke(this, EventArgs.Empty);

        playerInputActions.Player.Disable();
    }

    private void OnInteractPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // Dead zone to ignore controller drift
        if (inputVector.magnitude < 0.2f) return Vector2.zero;

        return inputVector.normalized;
    }

    // Return the raw stick/keyboard vector (not normalized). Used when you need to
    // distinguish between zero / small analog input and intentional input.
    public Vector2 GetMovementVector() {
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }
}