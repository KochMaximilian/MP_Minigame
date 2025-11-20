using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnPlayerPickUp;

    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask CountersLayerMask;
    [SerializeField] private Transform objectHoldPoint;

    // Hold/repeat tuning (tweak in inspector)
    [Header("Hold / Repeat Tuning")]
    [Tooltip("Time after press before repeating begins (seconds). If triggerImmediateOnStarted is false and you configured a Hold interaction, the 'performed' event can be the first call.")]
    [SerializeField] private float interactAlternateInitialDelay = 0.35f; // time before repeats start

    [Tooltip("Interval between repeated alternate actions while holding (seconds). Also used as the minimum interval between any two alternate invokes when 'Enforce Min Interval' is enabled.")]
    [SerializeField] private float interactAlternateRepeatDelay = 0.12f;  // repeat interval

    [Tooltip("When true, the first action happens immediately on button-down (started). If false, the first action happens on performed (useful when binding uses Hold interaction).")]
    [SerializeField] private bool triggerImmediateOnStarted = true;       // immediate action on button down

    [Header("Fairness / Anti-Spam")]
    [Tooltip("If true, taps cannot invoke alternate action faster than the repeat delay. Makes tapping and holding produce the same max rate.")]
    [SerializeField] private bool enforceMinIntervalBetweenInvokes = true;

    [Tooltip("Minimum time allowed between two alternate invokes when enforcement is enabled (seconds). Typically set equal to Repeat Delay.")]
    [SerializeField] private float minInvokeInterval = 0.12f;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    // hold state
    private bool isInteractAlternateHeld;
    private bool invokedForThisPress; // prevents double invoke (started + performed)

    // timing for fairness (shared between taps and hold repeats)
    private float lastInvokeTime = -Mathf.Infinity;

    // coroutine handle for hold-repeat
    private Coroutine interactAlternateCoroutine;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("There is more than 1 Player Instance");
        }
        Instance = this;

        // sensible defaults if unset
        if (minInvokeInterval <= 0f) minInvokeInterval = 0.12f;
        if (interactAlternateRepeatDelay <= 0f) interactAlternateRepeatDelay = 0.12f;
        if (interactAlternateInitialDelay <= 0f) interactAlternateInitialDelay = 0.35f;
    }

    private void OnEnable() {
        if (gameInput == null) return;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateStarted += OnAlternateStarted;
        gameInput.OnInteractAlternatePerformed += OnAlternatePerformed;
        gameInput.OnInteractAlternateCanceled += OnAlternateCanceled;
    }

    private void OnDisable() {
        if (gameInput == null) return;
        gameInput.OnInteractAction -= GameInput_OnInteractAction;
        gameInput.OnInteractAlternateStarted -= OnAlternateStarted;
        gameInput.OnInteractAlternatePerformed -= OnAlternatePerformed;
        gameInput.OnInteractAlternateCanceled -= OnAlternateCanceled;
    }

    private void OnAlternateStarted(object s, EventArgs e) {
        // mark held and reset per-press flag
        isInteractAlternateHeld = true;
        invokedForThisPress = false;

        if (triggerImmediateOnStarted) {
            // immediate-first mode: invoke now and start repeat coroutine after the initial delay
            TryInvokeInteractAlternate();
            invokedForThisPress = true;

            // Start repeating: wait interactAlternateInitialDelay before the first repeat
            StartHoldRepeatCoroutine(interactAlternateInitialDelay);
        } else {
            // hold-first mode: do NOT start coroutine here.
            // Wait for 'performed' (Input System Hold) to invoke the first action,
            // then OnAlternatePerformed will start the repeat coroutine.
        }
    }

    private void OnAlternatePerformed(object s, EventArgs e) {
        // If Input Action uses Hold and performed is first activation
        if (!invokedForThisPress) {
            TryInvokeInteractAlternate();
            invokedForThisPress = true;

            // Start repeating after the configured initial delay so first gap == initial delay
            StartHoldRepeatCoroutine(interactAlternateInitialDelay);
        } else {
            // If we already invoked on started, ensure coroutine is running with correct timing
            if (interactAlternateCoroutine == null) {
                StartHoldRepeatCoroutine(interactAlternateInitialDelay);
            }
        }

        isInteractAlternateHeld = true;
    }

    private void OnAlternateCanceled(object s, EventArgs e) {
        isInteractAlternateHeld = false;
        invokedForThisPress = false;
        StopHoldRepeatCoroutine();
    }

    private void StartHoldRepeatCoroutine(float firstWait) {
        StopHoldRepeatCoroutine();
        interactAlternateCoroutine = StartCoroutine(HoldRepeatCoroutine(firstWait));
    }

    private void StopHoldRepeatCoroutine() {
        if (interactAlternateCoroutine != null) {
            StopCoroutine(interactAlternateCoroutine);
            interactAlternateCoroutine = null;
        }
    }

    private IEnumerator HoldRepeatCoroutine(float firstWait) {
        // wait before first repeat (firstWait can be interactAlternateInitialDelay)
        if (firstWait > 0f) yield return new WaitForSeconds(firstWait);

        // loop while held
        while (isInteractAlternateHeld) {
            // TryInvoke checks the global cooldown
            TryInvokeInteractAlternate();
            // wait the stable repeat interval
            yield return new WaitForSeconds(interactAlternateRepeatDelay);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteraction();
        // no longer using manual timer logic for repeats (coroutine handles it)
    }

    private bool CanInvokeNow() {
        if (!enforceMinIntervalBetweenInvokes) return true;
        return Time.time - lastInvokeTime >= minInvokeInterval - 0.0001f;
    }

    private void TryInvokeInteractAlternate() {
        if (selectedCounter == null) return;

        if (!CanInvokeNow()) {
            // optional: debug only
            // Debug.Log($"[Player] Skipping invoke (cooldown) @ {Time.time:F3}");
            return;
        }

        try {
            // optional debug:
            // Debug.Log($"[Player] Invoking InteractAlternate on '{selectedCounter.name}' @ {Time.time:F3}");
            selectedCounter.InteractAlternate(this);
            lastInvokeTime = Time.time;
        } catch (Exception ex) {
            Debug.LogError($"Exception in InteractAlternate on '{selectedCounter.name}': {ex}");
            lastInvokeTime = Time.time;
        }
    }

    private void HandleInteraction() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDirection = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactionDistance, CountersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = movementSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float PlayerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * PlayerHeight,
            playerRadius,
            moveDir,
            moveDistance
        );

        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * PlayerHeight,
                playerRadius,
                moveDirX,
                moveDistance
            );

            if (canMove) {
                moveDir = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * PlayerHeight,
                    playerRadius,
                    moveDirZ,
                    moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                } else {
                    moveDir = Vector3.zero;
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotationSpeed = 10f;
        if (moveDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }
    }


    public bool IsWalking() {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return objectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
   
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPlayerPickUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}