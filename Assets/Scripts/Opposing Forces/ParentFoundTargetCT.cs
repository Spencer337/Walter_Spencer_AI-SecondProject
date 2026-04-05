using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class ParentFoundTargetCT : ConditionTask {
		public BBParameter<Transform> targetTransform; 
        public int numberOfScanCirclePoints;
        public BBParameter<float> scanRadius;
        public LayerMask hiderMask, playerMask;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			targetTransform.value = null;
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			// Get a list of hiders in range of a scanning sphere centered on the parent
			Collider[] hidersInRange = Physics.OverlapSphere(agent.transform.position, scanRadius.value, hiderMask);

			float closestDistance = 10000;
			foreach (Collider hiderCollider in hidersInRange)
			{
				float distanceToTarget = Vector3.Distance(hiderCollider.gameObject.transform.position, agent.transform.position);
				if (distanceToTarget < closestDistance)
				{
					targetTransform.value = hiderCollider.gameObject.transform;
					closestDistance = distanceToTarget;
				}
			}

			// Get a list of players in range of a scanning sphere centered on the parent
			Collider[] playersInRange = Physics.OverlapSphere(agent.transform.position, scanRadius.value, playerMask);
			foreach (Collider playerCollider in playersInRange)
			{
				targetTransform.value = playerCollider.gameObject.transform;
			}

			if (targetTransform.value == null)
			{
				return false;
			}
			else
			{
				return true;
			}
        }
	}
}