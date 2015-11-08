using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

	public Transform[] waypoints;

	private NavMeshAgent agent;
	private int currentWaypoint = 0;

	void Start() {
		agent = GetComponent<NavMeshAgent> ();
		currentWaypoint = 0;
		agent.destination = waypoints[currentWaypoint].position;
	}

	void Update() {
		if (agent.remainingDistance < 0.5f) {
			currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
			agent.destination = waypoints[currentWaypoint].position;
		}
	}
}
