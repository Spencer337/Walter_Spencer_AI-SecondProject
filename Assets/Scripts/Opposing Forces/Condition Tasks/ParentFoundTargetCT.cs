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

			// Set the closest distance to a very large number
			float closestDistance = 10000;
			// For each hider detected by the parent
			foreach (Collider hiderCollider in hidersInRange)
			{
				// Get the distance from the parent to the hider
				float distanceToTarget = Vector3.Distance(hiderCollider.gameObject.transform.position, agent.transform.position);
				// If the distance is less than the closest distance, set the target transform to the hider's transform and set the closest distance to the distance to the hider
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
				// If there is a player detected in the parent's range, set the target transform to the player
				targetTransform.value = playerCollider.gameObject.transform;
			}

			// If theres no target transform, return false
			if (targetTransform.value == null)
			{
				return false;
			}
			// Otherwise, return true
			else
			{
				return true;
			}
        }
	}
}