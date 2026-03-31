using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class LungeAT : ActionTask {
		private NavMeshAgent navAgent;
        public Vector3 startPosition, endPosition;
        public float lungeTime;
        public float lungeDuration;
		public float lungeDistance;

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
            // Reset lunge time to 0
            lungeTime = 0;

            // Get the start and end positions of the lunge movement
            startPosition = agent.transform.position;
            endPosition = startPosition + agent.transform.forward * lungeDistance;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            // Increase lungeTime by time
            lungeTime += Time.deltaTime;
            // Lerp between the start and end positions
            agent.transform.position = Vector3.Lerp(startPosition, endPosition, lungeTime / lungeDuration);
            // If the lunge movement is finished, begin updating the nav mesh again, and end the action
            if (lungeTime > lungeDuration)
            {
                navAgent.Warp(agent.transform.position);
                navAgent.updatePosition = true;
                navAgent.updateRotation = true;
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