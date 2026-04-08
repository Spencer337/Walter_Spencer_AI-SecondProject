using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;


namespace NodeCanvas.Tasks.Actions {

	public class AlertAT : ActionTask {
		public BBParameter<AudioClip> alertSound;
		public BBParameter<Light> flashlight;
        public BBParameter<Transform> targetTransform; 
        public float distance, checkDistance; 
        private NavMeshAgent navAgent;
        public BBParameter<float> speed, baseSpeed; 

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
            // Reset the speed of the parent to the base speed
            speed.value = baseSpeed.value;
            navAgent.speed = speed.value;

            // Play an alert noise and change the flashlight color to red
            AudioSource.PlayClipAtPoint(alertSound.value, agent.transform.position);
			flashlight.value.color = Color.yellow;
            // Set the parent's destination to the target transform
            navAgent.SetDestination(targetTransform.value.position);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            // If the parent has reached the end of it's path
            if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.2)
            {
                // Get the distance between the parent and it's target
                distance = Vector3.Distance(agent.transform.position, targetTransform.value.position);
                // If the distance is less than the check distance, end the action with a success
                if (distance < checkDistance)
                {
                    EndAction(true);
                }
                // If not, end the action with a failure
                else
                {
                    EndAction(false);
                }
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