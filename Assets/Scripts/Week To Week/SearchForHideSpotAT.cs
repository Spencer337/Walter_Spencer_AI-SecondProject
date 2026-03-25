using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class SearchForHideSpotAT : ActionTask {
        public Color scanColour;
        public int numberOfScanCirclePoints;
        public BBParameter<float> scanRadius;
        public BBParameter<float> initialScanRadius;
        public BBParameter<float> maxScanRadius;
        public LayerMask targetMask;
        public BBParameter<float> scanSpeed;
        public BBParameter<Vector3> targetPosition; 
        private NavMeshAgent navAgent;
        public BBParameter<Transform> playerTransform; 

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            // Set the scan radius to it's inital value
            //scanRadius.value = initialScanRadius.value;
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            // Draw the scanning circle which comes out from the pig's position to visualize the scanning
            DrawCircle(agent.transform.position, scanRadius.value, scanColour, numberOfScanCirclePoints);

            // Get a list of objects in range of a scanning sphere centered on the pig, that are on the grass layer mask
            Collider[] objectsInRange = Physics.OverlapSphere(agent.transform.position, scanRadius.value, targetMask);

            GameObject bestHidingSpot = null;
            float bestDistance = 1000;
            foreach (Collider collider in objectsInRange)
            {
                float distanceToPlayer = Vector3.Distance(collider.gameObject.transform.position, playerTransform.value.position);
                Debug.Log(distanceToPlayer);
                if (distanceToPlayer < bestDistance)
                {
                    bestHidingSpot = collider.gameObject;
                    bestDistance = distanceToPlayer;
                }
            }
            if (bestHidingSpot != null)
            {
                targetPosition = bestHidingSpot.transform.position;
            }
            EndAction(true);
            //// If there are any objects in the list
            //if (objectsInRange.Length != 0)
            //{
            //    // Set the target object to the first object in the list
            //    targetPosition.value = objectsInRange[0].gameObject.transform.position;


            //    // End the action with a success
            //    EndAction(true);
            //}

            // Increase the radius of the pig's scanning sphere
            //scanRadius.value += scanSpeed.value * Time.deltaTime;

            //// If the scan radius is greater than the max scan radius, end the action with a failure
            //if (scanRadius.value >= maxScanRadius.value)
            //{
            //    EndAction(false);
            //}
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