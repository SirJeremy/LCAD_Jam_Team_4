using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IUseable {
    [SerializeField]
    private GameObject iInteractable;

    private bool pressed = false;

    public void Use() {
        IInteractable interactable = iInteractable.GetComponent<IInteractable>();
        if (interactable != null) {
            pressed = !pressed;
            interactable.Interact(pressed);
        }
    }

}
