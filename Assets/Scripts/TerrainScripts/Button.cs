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

    [SerializeField]
    private GameObject[] fires;

    public void Use() {
        IInteractable interactable = iInteractable.GetComponent<IInteractable>();
        if (interactable != null) {
            endDoor.conditions[conditionNum] = true;
            pressed = !pressed;
            interactable.Interact(pressed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.layer == 12)
        {
            endDoor.conditions[conditionNum] = true;
            if (conditionNum == 2)
            {
                for (int i = 0; i < fires.Length; i++)
                {
                    fires[i].SetActive(true);
                }
            }
        }
    }

}
