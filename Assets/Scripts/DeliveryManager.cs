using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;

    public static DeliveryManager Instance { get; private set; }


    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private bool enableDeliveryDebugLogs = false;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 6;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
              //  Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);


                OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        if (enableDeliveryDebugLogs) {
            Debug.Log("=== Attempting Delivery ===");
            Debug.Log($"Plate has {plateKitchenObject.GetKitchenObjectSOList().Count} ingredients:");
            foreach (KitchenObjectSO item in plateKitchenObject.GetKitchenObjectSOList()) {
                Debug.Log($"  - {item.objectName}");
            }
        }

        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (enableDeliveryDebugLogs) {
                Debug.Log($"Checking recipe: {waitingRecipeSO.recipeName} ({waitingRecipeSO.kitchenObjectSOList.Count} ingredients)");
                foreach (KitchenObjectSO item in waitingRecipeSO.kitchenObjectSOList) {
                    Debug.Log($"  - {item.objectName}");
                }
            }

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                bool plateContentMatchRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        if (enableDeliveryDebugLogs) {
                            Debug.Log($"  Missing ingredient: {recipeKitchenObjectSO.objectName}");
                        }
                        plateContentMatchRecipe = false;
                    }
                }
                if (plateContentMatchRecipe) { // Player delivers correct dish
                    if (enableDeliveryDebugLogs) {
                        Debug.Log("Player delivered the correct recipe");
                    }
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        // No matches found - Player did not deliver correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        if (enableDeliveryDebugLogs) {
            Debug.Log("Player did not deliver the correct recipe");
        }
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }
}