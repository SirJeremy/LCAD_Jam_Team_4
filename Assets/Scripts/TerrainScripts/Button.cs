using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IUseable {
    [SerializeField]
    private GameObject iInteractable;

    private bool pressed = false;

    [SerializeField]
    private Door endDoor;
    [SerializeField]
    private int conditionNum;
    public void Use() {
        IInteractable interactable = iInteractable.GetComponent<IInteractable>();
        if (interactable != null) {
            endDoor.conditions[conditionNum] = true;
            pressed = !pressed;
            interactable.Interact(pressed);
        }
    }

}
