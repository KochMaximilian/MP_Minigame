using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter {
    private enum State {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    private State currentState;
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] bool isDebugging = false;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;

    private void Start() {
        currentState = State.Idle;
    }

    private void Update() {

        if (HasKitchenObject()) {
            switch (currentState) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        if (isDebugging) {
                            Debug.Log(fryingRecipeSO.input + " Fried");
                        }
                      
                        currentState = State.Fried;
                    
                    }
                    break;
                case State.Fried:
                    break;
                case State.Burned:
                    break;
            }
            if (isDebugging) {
                Debug.Log("Frying Time: " + (int)fryingTimer);
                Debug.Log("Stove State: " + currentState);
            }
        }
    }

    public override void Interact(Player player) {

        if (!HasKitchenObject()) {
            // There is no KitchenObject
            if (player.HasKitchenObject()) {
                // Player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player carrying something that can be fried
                    player.GetKitchenObject().setKitchenObjectParent(this); // Drop KitchenObject on empty Counter
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    currentState = State.Frying;
                    fryingTimer = 0f;
                    // TODO Progress 
                }
            } else {
                // Player not carrying anything

            }
        } else {
            // There is a KitchenObject here 
            if (player.HasKitchenObject()) {
                // Player is carrying something 
            } else {
                // Player is not carrying anything 
                GetKitchenObject().setKitchenObjectParent(player);
                // TODO Progress
            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }


}
