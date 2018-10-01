using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int swingDirection = 1;
    [SerializeField] float swingSpeed = 1;
    [SerializeField] int stepLimit = 0;
    public bool[] conditions;
    private bool canOpen = false;
    public void Interact()
    {
        canOpen = true;
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i])
            {
                canOpen = false;
                break;
            }
        }
        if (canOpen)
        {
            StartCoroutine(SwingOpen());
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
