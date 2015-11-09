using UnityEngine;
using System.Collections;

public class TargetInSight : MonoBehaviour {

	public float fieldOfView = 110.0f;
	public GameObject target;
	public bool targetInSight;
	public AudioClip spottedSound;
	public AudioClip atmosphere;
	public AudioClip pursuit;

	private SphereCollider areaOfAwareness;
	private Vector3 lastSighting;
	private Patrol patrolScript;
	private AudioSource doctorMusic;
	private bool pursuingTarget = false;

	void Awake() {
		areaOfAwareness = GetComponent<SphereCollider> ();
		patrolScript = GetComponent<Patrol> ();
		doctorMusic = GetComponent<AudioSource> ();
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
						BeginPursuit(other);
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject == target && pursuingTarget) {
			EndPursuit();
		}
	}

	void BeginPursuit(Collider other) {
		targetInSight = true;
		lastSighting = target.transform.position;
		if (!pursuingTarget) {
			patrolScript.InterjectTarget (lastSighting);
			doctorMusic.clip = pursuit;
			doctorMusic.Play ();
			if (spottedSound != null) {
				AudioSource.PlayClipAtPoint (spottedSound, other.transform.position);
			}
		} else {
			patrolScript.UpdateTargetPosition(other.transform.position);
		}
		pursuingTarget = true;
	}

	void EndPursuit() {
		targetInSight = false;
		doctorMusic.clip = atmosphere;
		doctorMusic.Play();
		pursuingTarget = false;
		patrolScript.ReturnToPatrol();
	}
}
