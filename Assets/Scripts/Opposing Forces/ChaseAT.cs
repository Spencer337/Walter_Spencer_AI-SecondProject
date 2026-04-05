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
            flashlight.value.color = Color.red;
			chaseTime = 0;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            searchTime += Time.deltaTime;
			chaseTime += Time.deltaTime;
            if (searchTime >= searchInterval)
            {
				searchTime = 0;
                navAgent.SetDestination(targetTransform.value.position);
            }
			if (chaseTime >= maxChaseTime)
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