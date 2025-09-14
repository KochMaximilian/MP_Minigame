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
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, this); // <player> if you want to spawn it on the player instead
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            } else {
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }

}
