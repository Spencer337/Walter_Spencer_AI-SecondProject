using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToCatch : MonoBehaviour
{
    public LayerMask hiderMask;
    private Ray mouseClickRay;
    private RaycastHit mouseClickHit;
    public GameObject target;
    public float catchDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool leftMouseClicked = Mouse.current.leftButton.wasPressedThisFrame;
        if (leftMouseClicked)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();

            mouseClickRay = Camera.main.ScreenPointToRay(mousePosition);
            // If the ray hits a sand spot, delete it
            if (Physics.Raycast(mouseClickRay, out mouseClickHit, catchDistance, hiderMask))
            {
                target = mouseClickHit.transform.gameObject;
                Destroy(target);
                Debug.Log("Hider Caught");
                target = null;
            }
        }
    }
}
