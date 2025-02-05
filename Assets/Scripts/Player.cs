
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update() {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += moveDir * movementSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking() {
        return isWalking;
    }
}
