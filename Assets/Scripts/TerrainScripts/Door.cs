using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int swingDirection = 1;
    [SerializeField] float swingSpeed = 1;
    [SerializeField] int stepLimit = 47;

    private bool isOpen = false;

    public bool[] conditions;
    private bool conditionMet = false;

    public void Interact(bool interactState)
    {
        conditionMet = true;
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i])
            {
                conditionMet = false;
                break;
            }
        }
        if (conditionMet)
        {
            if (!isOpen)
                StartCoroutine(SwingOpen());
            isOpen = true;
        }
        
    }


    /*void Start()
    {
        StartCoroutine(SwingOpen());
    }*/

    IEnumerator SwingOpen()
    {
        int steps = 0;
        while (stepLimit >= steps)
        {
            steps++;
            transform.eulerAngles += new Vector3(0, swingSpeed * swingDirection * Time.deltaTime, 0);
            yield return null;
        }
        
    }
}
