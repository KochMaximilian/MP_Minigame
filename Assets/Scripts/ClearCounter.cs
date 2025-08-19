using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform objectSpawnPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update() {
        if (testing && Input.GetKeyDown(KeyCode.T)) {
            if (kitchenObject != null) {
                kitchenObject.setClearCounter(secondClearCounter);
            }
        }
    }

    public void Interact() {
       //Debug.Log("Interacted");
       if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, objectSpawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().setClearCounter(this);
        } else {

            Debug.Log(kitchenObject.GetClearCounter());

        }
       //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
    }


    public Transform GetKitchenObjectFollowTransform() {
        return objectSpawnPoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}
