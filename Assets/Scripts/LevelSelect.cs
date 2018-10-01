using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {


    public void Working() {
        SceneManager.LoadScene(1);
    }
    public void Pretty() {
        SceneManager.LoadScene(2);
    }
    public void Test() {
        SceneManager.LoadScene(3);
    }
	
}
