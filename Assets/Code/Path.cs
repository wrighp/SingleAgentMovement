using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public List<Transform> points = new List<Transform>();

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnDrawGizmos() {
        if (points.Count >= 2) {
            for (int i = 1; i < points.Count; i++) {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(points[i-1].transform.position, points[i].transform.position);
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(points[0].transform.position, points[points.Count-1].transform.position);
        }
    }
}
