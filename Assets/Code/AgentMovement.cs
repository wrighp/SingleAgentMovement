using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class AgentMovement : MonoBehaviour {

	public float maxAcceleration;
	public float maxSpeed;
	public float acceleration;

	public float maxAngularAcceleration;
	public float maxAngularSpeed;
	public float angularAcceleration;

	public Vector2 forceDirection = Vector2.zero;

	Rigidbody2D rb;

	void Awake(){
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){

		rb.AddForce(forceDirection * Mathf.Min(maxAcceleration, acceleration));
		forceDirection = Vector2.zero;
		//This clamping is not actually accurate as the addForce will increase the speed for a frame after
		//If you disable force when speed is over max speed that creates hysteresis
		//A correct force reduction is more complicated
		rb.velocity = Vector2.ClampMagnitude (rb.velocity, maxSpeed);

		//Clamps max acceleration and max speed
		rb.angularVelocity = rb.angularVelocity + Mathf.Clamp (angularAcceleration, -maxAngularAcceleration, maxAngularAcceleration);
		rb.angularVelocity = Mathf.Clamp (rb.angularVelocity, -maxAngularSpeed, maxAngularSpeed);
	
	
	}
}
