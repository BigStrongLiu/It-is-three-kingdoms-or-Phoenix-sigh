using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour{
	void OnDrawGizmos () {
        Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,0.2f);
	}
}

