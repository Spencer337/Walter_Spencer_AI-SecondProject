using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class EscapeAT : ActionTask {
		public BBParameter<Transform> playerTransform;
		private NavMeshAgent navAgent;
		public Vector3 escapeDirection, escapePoint;
		public float escapeDistance;
		public float distanceToPlayer;
		public float checkDistance;

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
            escapeDirection = agent.transform.position - playerTransform.value.position;
			// Normalize the direction
			escapeDirection = Vector3.Normalize(escapeDirection);
            // Inverse the direction
            escapeDirection = escapeDirection * -1;
			// Multiply the direction by the escape distance;
            escapePoint = escapeDirection * escapeDistance;
			navAgent.SetDestination(escapePoint);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			distanceToPlayer = Vector3.Distance(agent.transform.position, playerTransform.value.position);
			if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1 && distanceToPlayer <= checkDistance)
			{
				EndAction(false);
			}
            else if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1 && distanceToPlayer >= checkDistance)
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