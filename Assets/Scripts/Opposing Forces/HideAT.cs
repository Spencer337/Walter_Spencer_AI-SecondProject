using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class HideAT : ActionTask {
        public BBParameter<AudioClip> gaspSound;
		public float growSpeed = 10;
		public float YScale;
		public bool growing;
		public float maxHeight, normalHeight;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			// Play the gasp sound, and set growing to true
            AudioSource.PlayClipAtPoint(gaspSound.value, agent.transform.position);
			growing = true;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			YScale = agent.transform.localScale.y;
			
			// Increase the height of the hider until it equals max height
            if (growing == true)
			{
                // Increase the y scale by time multiplied by grow speed
                YScale += Time.deltaTime * growSpeed;
                // Set the height to the y scale
                agent.transform.localScale = new Vector3(agent.transform.localScale.x, maxHeight, agent.transform.localScale.z);

				// If the y scale is greater than the max height, set growing to false
				if (YScale >= maxHeight)
				{
					growing = false;
				}
            }
			// Then decrease the height of the hider until it is back to normal
            else
            {
				// Decrease the y scale by time multiplied by grow speed
                YScale -= Time.deltaTime * growSpeed;
				// Set the height to the y scale
                agent.transform.localScale = new Vector3(agent.transform.localScale.x, YScale, agent.transform.localScale.z);

				// If the y scale is less than the normal height, end the action and set the hider's height to it's normal height
				if (YScale <= normalHeight)
				{
                    agent.transform.localScale = new Vector3(agent.transform.localScale.x, normalHeight, agent.transform.localScale.z);
                    EndAction(true);
				}
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