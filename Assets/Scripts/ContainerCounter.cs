using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    // Animation
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // Player is not carrying anything
            if (!HasKitchenObject()) {
                // Counter is empty 
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
                kitchenObjectTransform.GetComponent<KitchenObject>().setKitchenObjectParent(this);
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            } else {
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }

}
