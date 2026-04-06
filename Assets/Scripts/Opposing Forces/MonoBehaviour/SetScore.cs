using NodeCanvas.Framework;
using TMPro;
using UnityEngine;

public class SetScore : MonoBehaviour
{
    public int numberOfScanCirclePoints;
    public float scanRadius;
    public LayerMask hiderMask;
    public int numOfHiders;
    public TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get a list of hiders in range of a scanning sphere centered on the parent
        Collider[] hidersInRange = Physics.OverlapSphere(Vector3.zero, scanRadius, hiderMask);
        numOfHiders = hidersInRange.Length;
        scoreText.text = numOfHiders.ToString();
        if (numOfHiders <= 0)
        {
            Debug.Log("Victory");
        }
    }
}
