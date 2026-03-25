using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class CrouchAT : ActionTask {
        public float crouchDuration, timeCrouching;
		public Vector3 startHeight, endHeight;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			startHeight = agent.transform.localScale;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            timeCrouching += Time.deltaTime;
            agent.transform.localScale = Vector3.Lerp(startHeight, endHeight, timeCrouching / crouchDuration);
            if (timeCrouching > crouchDuration)
            {
                EndAction(true);
            }
   //         YScale -= Time.deltaTime * crouchSpeed;
   //         agent.transform.localScale = new Vector3(agent.transform.localScale.x, agent.transform.localScale.y + YScale, agent.transform.localScale.z);
			//if (YScale < crouchAmount)
			//{
			//	EndAction(true);
			//}
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}