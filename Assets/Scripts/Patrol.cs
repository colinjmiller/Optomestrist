using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour {

	public Transform[] waypoints;
	public bool patrolActive;

	private NavMeshAgent agent;
	private int currentWaypoint = 0;
	private bool chasingTarget = false;

	void Start() {
		agent = GetComponent<NavMeshAgent> ();
		if (patrolActive) {
			agent.destination = waypoints[currentWaypoint].position;
		}
	}

	void Update() {
		if (!patrolActive || chasingTarget) {
			return;
		}
		if (agent.remainingDistance < 0.5f) {
			currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
			agent.destination = waypoints[currentWaypoint].position;
		}
	}

	public void InterjectTarget(Vector3 target) {
		chasingTarget = true;
		agent.speed = agent.speed * 2f;
		agent.destination = target;
	}

	public void UpdateTargetPosition(Vector3 target) {
		if (chasingTarget) {
			agent.destination = target;
		}
	}

	public void ReturnToPatrol() {
		chasingTarget = false;
		agent.speed = agent.speed * 0.5f;
		agent.destination = waypoints [currentWaypoint].position;
	}

	public void beginPatrolling() {
		patrolActive = true;
		agent.destination = waypoints[currentWaypoint].position;
	}

	public void stopPatrolling() {
		patrolActive = false;
		agent.enabled = false;
	}
}
