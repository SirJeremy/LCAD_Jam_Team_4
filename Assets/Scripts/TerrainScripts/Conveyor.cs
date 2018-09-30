using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 direction = Vector3.forward;
    [SerializeField]
    private float speedMultiplyer = 1;
    private Vector3 velocity = Vector3.forward;

    public float Speed {
        get { return speed; }
        set { speed = value; CalculateForce(); }
    }
    public Vector3 Direction {
        get { return direction; }
        set { direction = value; CalculateForce(); }
    }
    public Vector3 Velocity { get { return velocity; } }

    private void Start() {
        CalculateForce();
    }
    /*
    public float visualSpeedScalar;
    private float currentScroll;


    private void Update() {
        // Scroll texture to fake it moving
        currentScroll = currentScroll + Time.deltaTime * speed * visualSpeedScalar;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, currentScroll);
    }
    */


    private void OnCollisionStay(Collision other) {
        if(other.rigidbody != null) {
            other.rigidbody.MovePosition(other.transform.position + (velocity * speedMultiplyer * Time.deltaTime));
        }
            
    }
    private void CalculateForce() {
        velocity = direction.normalized * speed;
    }
}
