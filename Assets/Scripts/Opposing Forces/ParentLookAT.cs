using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class ParentLookAT : ActionTask {
        public float rotationMax, rotationStart, currentRotation, rotationTime, rotationDuration;
        public bool rotatingRight, rotatingLeft, rotatingBackToStart;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            // Set the value of rotation start
            rotationStart = agent.transform.rotation.y;
            currentRotation = rotationStart;

            rotatingBackToStart = false;
            rotationTime = 0;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            rotationTime += Time.deltaTime;

            // Rotating to the right
            if (rotatingRight == true)
            {
                rotateBackAndForth(rotationMax);
            }

            // Rotating to the left
            else if (rotatingLeft == true)
            {
                rotateBackAndForth(-rotationMax);
            }
        }

        private void rotateBackAndForth(float rotationAmount)
        {
            if (rotatingBackToStart == false)
            {
                currentRotation = Mathf.LerpAngle(rotationStart, rotationStart + rotationAmount, rotationTime / rotationDuration);

            }
            else
            {
                currentRotation = Mathf.LerpAngle(rotationStart + rotationAmount, rotationStart, rotationTime / rotationDuration);

            }
            if (rotationTime > rotationDuration && rotatingBackToStart == false)
            {
                rotatingBackToStart = true;
                rotationTime = 0;
            }
            else if (rotationTime > rotationDuration && rotatingBackToStart == true)
            {
                rotatingRight = !rotatingRight;
                rotatingLeft = !rotatingLeft;
                //EndAction(true);
            }

            agent.transform.eulerAngles = new Vector3(agent.transform.rotation.x, currentRotation, agent.transform.rotation.z);
        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}