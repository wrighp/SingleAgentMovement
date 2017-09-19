using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDrawer : MonoBehaviour {

	void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
