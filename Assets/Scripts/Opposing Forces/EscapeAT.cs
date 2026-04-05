using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class EscapeAT : ActionTask {
		public BBParameter<Transform> targetTransform;
		private NavMeshAgent navAgent;
		public Vector3 escapeDirection, escapePoint;
		public float escapeDistance, checkDistance, distanceToTarget;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            navAgent = agent.GetComponent<NavMeshAgent>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            // Get the direction toward the player
            escapeDirection = targetTransform.value.position - agent.transform.position;
			// Normalize the direction
			escapeDirection = escapeDirection.normalized;
            // Inverse the direction
            escapeDirection = escapeDirection * -1;
			// Multiply the direction by the escape distance;
            escapePoint = escapeDirection * escapeDistance;
			escapePoint += agent.transform.position;
			navAgent.SetDestination(escapePoint);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            distanceToTarget = Vector3.Distance(agent.transform.position, targetTransform.value.position);
			if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1 && distanceToTarget <= checkDistance)
			{
				EndAction(false);
			}
            else if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1 && distanceToTarget >= checkDistance)
            {
                EndAction(true);
            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}