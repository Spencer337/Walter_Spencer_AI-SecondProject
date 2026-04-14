using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveToHideSpotAT : ActionTask {
		private NavMeshAgent navAgent;
        public BBParameter<Transform> hideTransform;
		public BBParameter<Transform> targetTransform; 
		public float distanceToTarget, checkDistance;
		public Vector3 hidePosition;
		public BBParameter<float> normalSpeed; 

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			// Get the hider's nav agent
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			// Get the direction of the hider towards the hideable object's position
			Vector3 direction = (hideTransform.value.position - agent.transform.position).normalized;
			direction.y = 0;
            // Add the direction to the target position to offset the target position 
            hidePosition = hideTransform.value.position + direction;
            // Set's the hider's destination to the target position
            navAgent.SetDestination(hidePosition);
            // Set the hider's speed to the normal speed
            navAgent.speed = normalSpeed.value;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Get the distance from the hider to the target transform
            distanceToTarget = Vector3.Distance(agent.transform.position, targetTransform.value.position);
			// If the nav agent's path is done, and the distance to the target is less than the check distance, end the action with a failure
            if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1 && distanceToTarget <= checkDistance)
            {
                EndAction(false);
            }
            // If the nav agent's path is done, and the distance to the target is greater than the check distance, end the action with a failure
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