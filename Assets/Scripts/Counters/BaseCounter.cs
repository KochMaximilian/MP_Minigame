using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnPlayerDroppedObject;

    [SerializeField] private Transform objectSpawnPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) { Debug.LogError("BaseCounter.Interact();"); }

    public virtual void InteractAlternate(Player player) {
        #if UNITY_EDITOR
            Debug.LogWarning($"BaseCounter.InteractAlternate() called on '{gameObject.name}'. Override this in derived counters if needed.");
        #endif
    }

    public Transform GetKitchenObjectFollowTransform() {
        return objectSpawnPoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPlayerDroppedObject?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void ClearKitchenObject() { kitchenObject = null; }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }


}
