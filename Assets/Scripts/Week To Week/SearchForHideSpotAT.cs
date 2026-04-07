using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class SearchForHideSpotAT : ActionTask {
        public Color scanColour;
        public int numberOfScanCirclePoints;
        public BBParameter<float> scanRadius;
        public LayerMask targetMask;
        public BBParameter<Transform> hideTransform;
        public BBParameter<Transform> targetTransform;  
        
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            // Draw the scanning circle which comes out from the hider's position to visualize the scanning
            DrawCircle(agent.transform.position, scanRadius.value, scanColour, numberOfScanCirclePoints);

            // Get a list of objects in range of a scanning sphere centered on the hider, that are on the hideables layer
            Collider[] objectsInRange = Physics.OverlapSphere(agent.transform.position, scanRadius.value, targetMask);

            // Reset the best hiding spot
            GameObject bestHidingSpot = null;
            // Set best distance to the distance from the hider to the target
            // Don't move to a hiding spot that is closer to the target than the current spot
            float bestDistance = Vector3.Distance(agent.transform.position, targetTransform.value.position);
            foreach (Collider collider in objectsInRange)
            {
                // Get the distance from the hider to the current hide spot
                float distanceToTarget = Vector3.Distance(collider.gameObject.transform.position, targetTransform.value.position);
                // If the distance is more than the best distance
                if (distanceToTarget > bestDistance)
                {
                    // Set the best hide spot to the current hide spot, and the best distance to the current distance
                    bestHidingSpot = collider.gameObject;
                    bestDistance = distanceToTarget;
                }
            }
            // If there is no best heiding spot, or the best hiding spot is the current hiding spot, end the action with a failure
            if (bestHidingSpot == null || hideTransform.value == bestHidingSpot.transform)
            {
                EndAction(false);
            }

            // Otherwise, end the action with a success
            else
            {
                hideTransform.value = bestHidingSpot.transform;
                EndAction(true);
            }
            

        }

        private void DrawCircle(Vector3 center, float radius, Color colour, int numberOfPoints)
        {
            Vector3 startPoint, endPoint;
            int anglePerPoint = 360 / numberOfPoints;
            for (int i = 1; i <= numberOfPoints; i++)
            {
                startPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * (i - 1)), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * (i - 1)));
                startPoint = center + startPoint * radius;
                endPoint = new Vector3(Mathf.Cos(Mathf.Deg2Rad * anglePerPoint * i), 0, Mathf.Sin(Mathf.Deg2Rad * anglePerPoint * i));
                endPoint = center + endPoint * radius;
                Debug.DrawLine(startPoint, endPoint, colour);
            }

        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
    }
}