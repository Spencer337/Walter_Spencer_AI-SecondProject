using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;


namespace NodeCanvas.Tasks.Actions {

	public class HiderWanderAT : ActionTask {
        private NavMeshAgent navAgent;
        public BBParameter<float> normalSpeed;
		public float randomX, randomZ, wanderDistance;
		public Vector3 wanderPoint;

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
            // Set the hider's speed to the normal speed
            navAgent.speed = normalSpeed.value;
            
			// Get a random number between -1 and 1 for the x and z
			randomX = Random.Range(-1f, 1f);
			randomZ = Random.Range(-1f, 1f);

			// Set the value of the wander point
			wanderPoint = new Vector3(randomX, 0f, randomZ);
			wanderPoint = wanderPoint * wanderDistance;
			wanderPoint += agent.transform.position;

			// Set the hider's destination to the wander point
			navAgent.SetDestination(wanderPoint);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

            // If the nav agent's path is done, end the action with a success

            if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1)
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