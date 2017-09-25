using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicWander : DynamicPursue {

	public float circleDistance = 4f;
	public float circleRadius = 3f;
	public float maxDistance = .5f; //How close agent can get to target before making new target
	public float maxTime = 3f; //Max seconds agent can go towards a target before it must select new target (prevents getting stuck)

	float timer;
	Vector2 circlePosition;
	protected override void Awake(){
		base.Awake();
		target = new GameObject().transform;
		target.name = name + " Wander target";
		target.position = transform.position;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		timer -= Time.deltaTime;
		Vector3 pos = transform.position;
		//Make new wander point after getting close enough to previous point or running out of time
		if(Vector2.Distance(target.position,pos) < maxDistance || timer <= 0)
		{
			//Raycast forward so agent may eventually move from wall (wander target won't go past wall)
			RaycastHit2D raycast = Physics2D.Raycast(pos, transform.up, circleDistance);
			PlayerDebug.DrawRay(pos, transform.up * circleDistance,Color.blue,.5f);


			//Rigidbody will be null if nothing hit
			float dist = (raycast.rigidbody == null) ? circleDistance : raycast.distance;
			circlePosition = transform.position + transform.up * dist;

            
            bool position_within_bounds = false;
            while (!position_within_bounds) {
                target.position = (Vector3)(Random.insideUnitCircle * circleRadius + circlePosition);
                if (Physics2D.Raycast(pos, target.position - transform.position, Vector3.Distance(target.position,transform.position)).rigidbody == null) {
                    position_within_bounds = true;
                    print("found a position");
                }
            }
            
			timer = maxTime;
		}
	}
	void LateUpdate(){
		//Outer circle
		Color outerColor = new Color(1f, 0, 0, .25f * timer / maxTime);
		PlayerDebug.DrawCircle(circlePosition, circleRadius, outerColor);
		//Inner Circle
		PlayerDebug.DrawLine (transform.position, target.position, Color.red);
		PlayerDebug.DrawCircle(target.position, maxDistance/2f, Color.red);
	}
	protected override void OnDrawGizmos(){
		//base.OnDrawGizmos();

		if(!enabled || target == null){
			return;
		}
		//Outer circle
		Color outerColor = new Color(1f, 0, 0, .25f * timer / maxTime);
		Gizmos.color = outerColor;
		Gizmos.DrawSphere(circlePosition, circleRadius);
		PlayerDebug.DrawCircle(circlePosition, circleRadius, outerColor);

		//Inner circle
		Gizmos.color = Color.red;
		Gizmos.DrawSphere( target.position, maxDistance/2f);
	}

	void OnDestroy(){
		if(target == null){
			return;
		}
		Destroy(target.gameObject);
	}
}
