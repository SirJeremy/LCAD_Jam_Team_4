using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int swingDirection = 1;
    [SerializeField] float swingSpeed = 1;
    [SerializeField] int stepLimit = 0;
    public void Interact()
    {
        // Open
        StartCoroutine(SwingOpen());
    }


    void Start()
    {
        StartCoroutine(SwingOpen());
    }

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
