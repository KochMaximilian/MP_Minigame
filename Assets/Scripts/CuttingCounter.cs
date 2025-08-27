using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField]  private KitchenObjectSO cutKitchenObjectSO;
    public override void Interact(Player player) { // F Key
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

    public override void InteractAlternate(Player player) { // E Key
        if (HasKitchenObject()) {
            // There is a KitchenObject here
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
        }
    }
}
