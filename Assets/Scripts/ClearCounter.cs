using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform objectSpawnPoint;
    [SerializeField] private ClearCounter secondClearCounter;
    [SerializeField] private bool testing;

    private KitchenObject kitchenObject;

    private void Update() {
        if (testing && Input.GetKeyDown(KeyCode.T)) {
            if (kitchenObject != null) {
                kitchenObject.setKitchenObjectParent(secondClearCounter);
            }
        }
    }

    public void Interact(Player player) {
       //Debug.Log("Interacted");
       if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, objectSpawnPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().setKitchenObjectParent(this);
        } else {
            // Give object to player
            kitchenObject.setKitchenObjectParent(player);
            Debug.Log(kitchenObject.GetKitchenObjectParent());

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
