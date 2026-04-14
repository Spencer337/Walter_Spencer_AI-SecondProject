using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class EscapeAT : ActionTask {
		public BBParameter<Transform> targetTransform;
		private NavMeshAgent navAgent;
		public Vector3 escapeDirection, escapePoint;
		public float escapeDistance, checkDistance, distanceToTarget, runVolume;
		public BBParameter<float> runSpeed;
        public BBParameter<AudioClip> hiderRunSound; 

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
            // Set the hider's colour to purple
            agent.GetComponent<Renderer>().material.color = Color.purple;

            // Get the direction toward the player
            escapeDirection = targetTransform.value.position - agent.transform.position;

			// Normalize the direction
			escapeDirection = escapeDirection.normalized;

            // Inverse the direction
            escapeDirection = escapeDirection * -1;

			// Multiply the direction by the escape distance;
            escapePoint = escapeDirection * escapeDistance;

			// Add the hider's position as an offset
			escapePoint += agent.transform.position;

			// Set the hider's destionation to the escape point 
			navAgent.SetDestination(escapePoint);

			// Set the hider's speed to the run speed
			navAgent.speed = runSpeed.value;

			// Play the hider's run sound
			AudioSource.PlayClipAtPoint(hiderRunSound.value, agent.transform.position, runVolume);
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