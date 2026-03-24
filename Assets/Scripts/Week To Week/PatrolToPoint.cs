using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class PatrolToPoint : ActionTask {
		public BBParameter<NavMeshAgent> navAgent;
		public BBParameter<Transform> destinationTransform;
		public BBParameter<Transform> criminalTransform;
		public float distanceToTarget;
		//public float checkDistance;
		//public float farDistance, mediumDistance, closeDistance;
        public BBParameter<float> checkDistance, farDistance, mediumDistance, closeDistance;
        private MeshRenderer meshRenderer;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            meshRenderer = agent.GetComponent<MeshRenderer>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			navAgent.value.SetDestination(destinationTransform.value.position);
            
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            // If object is within range, EndActionFalse
            distanceToTarget = Vector3.Distance(agent.transform.position, criminalTransform.value.position);
            if (distanceToTarget < checkDistance.value)
            {
                EndAction(false);
                return;
            }

            // If there is no more distance on the path, end the action
            if (navAgent.value.pathPending == false && navAgent.value.remainingDistance <= 0.5)
            {
                EndAction(true);
            }

            else if (distanceToTarget < closeDistance.value)
            {
                meshRenderer.material.color = Color.orange;
            }
            else if (distanceToTarget < mediumDistance.value)
            {
                meshRenderer.material.color = Color.yellow;
            }
            else if (distanceToTarget < farDistance.value)
			{
                meshRenderer.material.color = Color.green;
            }
            else
            {
                meshRenderer.material.color = Color.blue;
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