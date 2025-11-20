using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPlayerPickUp += Player_OnPlayerPickUp;
        BaseCounter.OnPlayerDroppedObject += BaseCounter_OnPlayerDroppedObject;
        TrashCounter.OnThrowAway += TrashCounter_OnThrowAway;
    }

    private void TrashCounter_OnThrowAway(object sender, System.EventArgs e) {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnPlayerDroppedObject(object sender, System.EventArgs e) {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPlayerPickUp(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume) {
        PlaySound(audioClipRefSO.footsteps, position,  volume);
    }
}
