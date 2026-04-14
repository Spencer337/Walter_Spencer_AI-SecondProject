using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class ChaseAT : ActionTask {
        private NavMeshAgent navAgent;
		public float searchInterval, searchTime;
		public float chaseTime, maxChaseTime;
		public BBParameter<Transform> targetTransform; 
        public BBParameter<Light> flashlight;
        public float distanceToTarget, captureDistance;

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
			// Set the parent's flashlight to be red
            flashlight.value.color = Color.red;
			chaseTime = 0;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Increase search time and chase time by time.deltaTime
            searchTime += Time.deltaTime;
			chaseTime += Time.deltaTime;

			// If the search time is greater than the search interval, set the parent's destination to the target
            if (searchTime >= searchInterval)
            {
				searchTime = 0;
                navAgent.SetDestination(targetTransform.value.position);
            }

			// If the chase time is greater than the max chase time, end the action with a success
			if (chaseTime >= maxChaseTime)
			{
				EndAction(true);
			}

			// Get the distance between the parent and target
            distanceToTarget = Vector3.Distance(agent.transform.position, targetTransform.value.position);

			// If the distance is less than the capture distance, end the action with a failure
			if (distanceToTarget <= captureDistance)
			{
				EndAction(false);
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