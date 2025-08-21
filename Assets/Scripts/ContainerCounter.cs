using UnityEngine;

public class ContainerCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {
        //Debug.Log("Interacted");
        if (!HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().setKitchenObjectParent(this); // TODO: change to player
        } else {
            // Give object to player
            GetKitchenObject().setKitchenObjectParent(player);

        }
        //Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);


        // Better Logic saved for later:
        /* Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
         kitchenObjectTransform.GetComponent<KitchenObject>().setKitchenObjectParent(player); */
    }

}
