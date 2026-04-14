using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class CaptureAT : ActionTask {
		public BBParameter<Transform> targetTransform;
		public LayerMask playerMask, hiderMask;
		public BBParameter<GameObject> defeatPanel; 

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			// If the target transform is on the player's layer, end the game and pause time
            if (targetTransform.value.gameObject.layer == 9)
			{
				defeatPanel.value.SetActive(true);
                Time.timeScale = 0;
                EndAction(true);
			}
			// If the target transform is on the hider's layer, set the hider to inactive
			if (targetTransform.value.gameObject.layer == 10)
			{
                targetTransform.value.gameObject.SetActive(false);
				EndAction(true);
            }
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}