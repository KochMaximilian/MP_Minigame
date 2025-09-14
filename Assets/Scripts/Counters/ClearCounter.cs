using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        //TODO: Drop Item on Counter
        if (!HasKitchenObject()) {
            // There is no kitchen object
            if (player.HasKitchenObject()) {
                // Player is carrying something
                player.GetKitchenObject().setKitchenObjectParent(this); // Drop kitchenObject on empty Counter
            } else { 
                // Player not carrying anything

            }
        } else {
            // There is kitchen object here
            if (player.HasKitchenObject()) {
                // Player is carrying something
            } else {
                // Player is not carrying anything 
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }
}
