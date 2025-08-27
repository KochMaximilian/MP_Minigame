using UnityEngine;

public class CuttingCounter : BaseCounter {

    

    [SerializeField]  private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player) { // F Key
        //TODO: Drop Item on Counter
        if (!HasKitchenObject()) {
            // There is no kitchen object
            if (player.HasKitchenObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player carryiong someting that can be cut
                    player.GetKitchenObject().setKitchenObjectParent(this); // Drop kitchenObject on empty Counter
                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // There is a KitchenObject here AND it can be cut
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }
        return false;
     }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {

        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
  
}
