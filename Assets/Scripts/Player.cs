
using System;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask CountersLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDirection;
    private ClearCounter selectedCounter;


    private void Awake() {
        if (Instance != null) {
            Debug.Log("There is more than 1 Player Instance");
        }
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;


    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {

        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HanldeMovement();
        HandeInteraction();
    }

    private void HandeInteraction() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);


        if (moveDir != Vector3.zero) {
            lastInteractDirection = moveDir;
        }

        float interactionDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactionDistance, CountersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // hit a clear counter
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);

                }
            } else {
                SetSelectedCounter(null);

            }
        } else {
            SetSelectedCounter(null);

        }

         Debug.Log(selectedCounter);
    }


    private void HanldeMovement() {

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
            // can not move torwards moveDir

            // try to move sideways x
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * PlayerHeight,
            playerRadius,
            moveDirX,
            moveDistance
        );

            if (canMove) {
                // can move only on x
                moveDir = moveDirX;
            } else {
                // can not move only on x
                // attemtd only to move on z

                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * PlayerHeight,
                playerRadius,
                moveDirZ,
                moveDistance);

                if (canMove) {
                    // can move only on z
                    moveDir = moveDirZ;
                } else {
                    // can not move at all

                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;


        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);

    }


    public bool IsWalking() {
        return isWalking;
    }


    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

}
