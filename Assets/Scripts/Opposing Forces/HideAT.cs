using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class HideAT : ActionTask {
        public BBParameter<AudioClip> gaspSound;
		public float growSpeed = 10;
		public float YScale;
		public bool growing;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            AudioSource.PlayClipAtPoint(gaspSound.value, agent.transform.position);
			growing = true;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			YScale = agent.transform.localScale.y;
			
			// Increase the height of the object until it equals 2
            if (growing == true)
			{
				YScale += Time.deltaTime * growSpeed;
				agent.transform.localScale = new Vector3(agent.transform.localScale.x, YScale, agent.transform.localScale.z);
				if (YScale >= 2)
				{
					growing = false;
				}
            }
            else
            {
                YScale -= Time.deltaTime * growSpeed;
                agent.transform.localScale = new Vector3(agent.transform.localScale.x, YScale, agent.transform.localScale.z);
				if (YScale <= 1.5)
				{
                    agent.transform.localScale = new Vector3(agent.transform.localScale.x, 1.5f, agent.transform.localScale.z);
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