using System;
using UnityEngine;

public class CuttingCounter : BaseCounter {



    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public event EventHandler<OnProgressChangedEventArgs> OnProgressChange;
    public class OnProgressChangedEventArgs : EventArgs {
        public float progressNormalized;
    }

    public event EventHandler OnCut;

    public override void Interact(Player player) { // F Key
        //TODO: Drop Item on Counter
        if (!HasKitchenObject()) {
            // There is no kitchen object
            if (player.HasKitchenObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player carrying something that can be cut
                    player.GetKitchenObject().setKitchenObjectParent(this); // Drop kitchenObject on empty Counter
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChange?.Invoke(this, new OnProgressChangedEventArgs {

                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
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
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChange?.Invoke(this, new OnProgressChangedEventArgs {

                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        } else {
            return null;
        }
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKichenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKichenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
