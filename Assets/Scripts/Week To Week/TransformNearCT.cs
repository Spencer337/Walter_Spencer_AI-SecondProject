using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class TransformNearCT : ConditionTask {
        public BBParameter<Transform> checkTransform;
        public float distanceToTarget;
        public float checkDistance;

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
            distanceToTarget = Vector3.Distance(agent.transform.position, checkTransform.value.position);
            if (distanceToTarget < checkDistance)
            {
                return true;
            }
			else
			{
				return false;
			}
        }
	}
}