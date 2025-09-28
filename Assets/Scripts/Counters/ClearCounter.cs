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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject) ) {
                    // player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(this.GetKitchenObject().GetKitchenObjectSO()) ) {
                        GetKitchenObject().DestroySelf();
                    } 
                } else {
                    // player is not holding a plate but something else
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject playerPlateKitchenObject)) {
                        // counter is holding a plate
                        if(playerPlateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                // Player is not carrying anything 
                GetKitchenObject().setKitchenObjectParent(player);
            }
        }
    }
}
