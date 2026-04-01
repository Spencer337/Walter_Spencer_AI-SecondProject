using JetBrains.Annotations;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class AvoidSteerAT : ActionTask {
		public BBParameter<Vector3> moveDirection;
		public LayerMask avoidMask;
		public float strength;

		public float detectionDistance;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Collider[] detectedColliders = Physics.OverlapSphere(agent.transform.position, detectionDistance, avoidMask);

			Vector3 totalDirection = Vector3.zero;
			foreach (Collider detectedCollider in detectedColliders)
			{
				Vector3 directionToHazard = detectedCollider.transform.position - agent.transform.position;
				totalDirection -= directionToHazard;
			}

			totalDirection = totalDirection.normalized;

			moveDirection.value += totalDirection * strength;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}