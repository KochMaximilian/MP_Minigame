using System;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnThrowAway;
    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnThrowAway?.Invoke(this, EventArgs.Empty);
        }
    }
}

