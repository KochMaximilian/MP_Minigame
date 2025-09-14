using UnityEngine;

public class ObjectSpawnVisual : MonoBehaviour {

    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    private const string ON_OPEN = "onOpen";

    private void Awake() {
        animator = GetComponent<Animator>();

    }

    private void Start() {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;

    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e) {
        animator.SetTrigger(ON_OPEN);
    }
}
