using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class LungeWindupAT : ActionTask {
        private NavMeshAgent navAgent;
		public Vector3 startPosition, windupPosition;
		public float windupTime;
		public float windupDuration;

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
			// Reset windup time to 0
			windupTime = 0;
			// Stop updating the nav mesh
			navAgent.updatePosition = false;
			navAgent.updateRotation = false;

			// Get the start and end positions of the windup movement
			startPosition = agent.transform.position;
            windupPosition = startPosition - agent.transform.forward;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			// Increase windupTime by time
			windupTime += Time.deltaTime;
			// Lerp between the start and end positions
			agent.transform.position = Vector3.Lerp(startPosition, windupPosition, windupTime / windupDuration);
			// If the windup movement is finished, end the action
			if (windupTime > windupDuration) 
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