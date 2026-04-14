using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ClickToCatch : MonoBehaviour
{
    public LayerMask hiderMask;
    private Ray mouseClickRay;
    private RaycastHit mouseClickHit;
    public GameObject target;
    public float catchDistance, hidersCaught;
    public TMP_Text caughtText, scoreText;

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
            // If the ray hits a hider spot, delete it and increase hiders caught by 1
            if (Physics.Raycast(mouseClickRay, out mouseClickHit, catchDistance, hiderMask))
            {
                hidersCaught += 1;
                target = mouseClickHit.transform.gameObject;
                Destroy(target);
                target = null;

                // Set the caught text and score text to the number of hiders caught
                caughtText.text = hidersCaught.ToString();
                scoreText.text = hidersCaught.ToString();
            }
        }
    }
}
