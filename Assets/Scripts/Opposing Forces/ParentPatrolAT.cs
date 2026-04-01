using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class ParentPatrolAT : ActionTask {
		public BBParameter<Transform> targetTransform;
		private NavMeshAgent navAgent;
		public BBParameter<float> baseSpeed, speed, maxSpeed, speedIncrease;
		public BBParameter<int> currentIndex; 

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            navAgent = agent.GetComponent<NavMeshAgent>();
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			// If the current index is greater than the list's length, set the current index to the start of the list
			//if (currentIndex.value >= 4)
			//{
			//	currentIndex.value = 0;
			//}
			// Set the parent's destination to the patrol point
            navAgent.SetDestination(targetTransform.value.position);
			// Increase speed by the speed increase
			speed.value += speedIncrease.value;
			// If speed is greater than max speed, set speed to max speed
			if (speed.value > maxSpeed.value)
			{
				speed.value = maxSpeed.value;
			}
			navAgent.speed = speed.value;
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            if (navAgent.pathPending == false && navAgent.remainingDistance <= 0.1)
            {
				currentIndex.value += 1;
                EndAction(true);
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