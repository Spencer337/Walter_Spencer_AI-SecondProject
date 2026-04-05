using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class HiderFoundTargetCT : ConditionTask {
        public BBParameter<Transform> targetTransform;
        public int numberOfScanCirclePoints;
        public BBParameter<float> scanRadius;
        public LayerMask parentMask, playerMask;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            // Get a list of parents in range of a scanning sphere centered on the parent
            Collider[] parentsInRange = Physics.OverlapSphere(agent.transform.position, scanRadius.value, parentMask);

            foreach (Collider parentCollider in parentsInRange)
            {
                targetTransform.value = parentCollider.gameObject.transform;
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