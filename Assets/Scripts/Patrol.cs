using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {
	
	public Transform[] waypoints;

	private NavMeshAgent agent;
	private int currentWaypoint;

	void Start() {
		agent = GetComponent<NavMeshAgent> ();
		currentWaypoint = 0;
	}

	void Update() {
		if (agent.remainingDistance < 0.5f) {
			currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
			agent.destination = waypoints[currentWaypoint].position;
		}
	}
}
