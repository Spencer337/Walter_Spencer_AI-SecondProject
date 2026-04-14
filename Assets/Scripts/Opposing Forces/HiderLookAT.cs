using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;     


namespace NodeCanvas.Tasks.Actions {

	public class HiderLookAT : ActionTask {
		public BBParameter<int> lookRepeats;
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
            // Set the hider's colour to blue
            agent.GetComponent<Renderer>().material.color = Color.darkGreen;
            // Set the value of rotation start
            rotationStart = agent.transform.rotation.y;
            currentRotation = rotationStart;

            // Reset the rotatingBackToStart and rotationTime variables
            rotatingBackToStart = false;
            rotationTime = 0;

            // Randomize the number of look repeats between 1 and 5
            lookRepeats.value = Random.Range(1, 5);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            // Increase rotation time 
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
            // If rotating away from the default position
            if (rotatingBackToStart == false)
            {
                // Lerp current rotation between the start value and end value
                currentRotation = Mathf.LerpAngle(rotationStart, rotationStart + rotationAmount, rotationTime / rotationDuration);
                
            }
            // If rotating back to the default position
            else
            {
                // Lerp current rotation between the end value and start value
                currentRotation = Mathf.LerpAngle(rotationStart + rotationAmount, rotationStart, rotationTime / rotationDuration);

            }
            // If rotation is done and the hider is rotating away from the default position
            if (rotationTime > rotationDuration && rotatingBackToStart == false)
            {
                // Rotate back to the default position, and reset rotation time
                rotatingBackToStart = true;
                rotationTime = 0;
            }
            // If rotating is done and the hider is rotating back to the default position
            else if (rotationTime > rotationDuration && rotatingBackToStart == true)
            {
                // Rotate the other way
                rotatingRight = !rotatingRight;
                rotatingLeft = !rotatingLeft;
                // End the action with a success
                EndAction(true);
            }

            // Set the hider's rotation
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