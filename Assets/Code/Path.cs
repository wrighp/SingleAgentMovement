using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public List<Transform> points = new List<Transform>();

	void Start () {
		
	}
	
	void Update () {
		
	}

    void LateUpdate() {
        if (points.Count >= 2) {
            for (int i = 1; i < points.Count; i++) {
				PlayerDebug.DrawLine(points[i-1].transform.position, points[i].transform.position, Color.blue);
            }
			PlayerDebug.DrawLine(points[0].transform.position, points[points.Count-1].transform.position, Color.cyan);
        }
    }
}
