using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject[] panels; // Assign your panels in the Inspector
    public SelectionPanelController selectionPanelController;
    private bool IsEnd1True
    {
        get { return selectionPanelController.end1; }
    }

    private void Start()
    {
        Debug.Log("start");
        // Disable all panels except the initial one
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);
        Debug.Log("step2");
    }

    // Update is called once per frame
    void Update()
    {
        // Check the value of the "end" variable
        if (IsEnd1True)
        {
        
            
            // Ensure the array index is valid (e.g., for switching from panel 0 to 1)
            if (panels.Length >= 2)
            {
                // Disable the first panel and enable the second panel
                panels[0].SetActive(false);
                panels[1].SetActive(true);

                // Optionally, reset the "end" variable to prevent continuous switching
                // You may want to remove this line because you probably don't want to reset it immediately.
                // isEnd1True = false;
            }
            else
            {
                Debug.LogError("Panel array should have at least 2 panels to switch between.");
            }
        }
    }
}
