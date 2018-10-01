using UnityEngine;

public class WeightedButton : MonoBehaviour {
    [SerializeField]
    private GameObject iInteractable;

    private bool pressed = false;

    public void Use() {
        IInteractable interactable = iInteractable.GetComponent<IInteractable>();
        if (interactable != null) {
            interactable.Interact(pressed);
        }
    }
    private void OnCollisionEnter(Collision collision) {
        GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        if(go != null) {
            if(!pressed) {
                pressed = true;
                Use();
            }
        }
    }
    private void OnCollisionExit(Collision collision) {
        GrabbableObject go = collision.gameObject.GetComponent<GrabbableObject>();
        if (go != null) {
            if (pressed) {
                pressed = false;
                Use();
            }
        }
    }
}
