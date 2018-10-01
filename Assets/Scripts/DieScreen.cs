using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScreen : MonoBehaviour {
    [SerializeField]
    private GameObject dieScreen;


	// Use this for initialization
	void Start () {
        dieScreen.SetActive(false);
        Character.OnDeath += OnDeath;
	}

    private void OnDeath() {
        dieScreen.SetActive(true);
    }
}
