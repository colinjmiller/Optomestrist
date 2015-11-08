﻿using UnityEngine;
using System.Collections;

public class TargetInSight : MonoBehaviour {

	public float fieldOfView = 110.0f;
	public GameObject target;
	public bool targetInSight;
	public Vector3 lastSighting;

	private SphereCollider areaOfAwareness;
	private Vector3 previousSighting;

	void Awake() {
		areaOfAwareness = GetComponent<SphereCollider> ();
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject == target) {
			targetInSight = false;

			// Draw a vector between the object and its target
			Vector3 vectorToTarget = other.transform.position - transform.position;
			float angleToTarget = Vector3.Angle (vectorToTarget, transform.forward);

			// Is the angle drawn within this field of view?
			if (angleToTarget < fieldOfView * 0.5f) {
				// Cast a ray to see if there are walls in the way of the player.
				RaycastHit hit;
				if (Physics.Raycast(transform.position, vectorToTarget.normalized, out hit, areaOfAwareness.radius)) {
					if (hit.collider.gameObject == target) {
						Debug.Log ("I see you");
					}
				}
			}
		}
	}

	void Update() {

	}
}
