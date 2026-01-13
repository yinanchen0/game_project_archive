using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    // Reference to the HPManager script
    public HPManager hpManager;

  
     
    private void Update()
    {
        if (hpManager != null)
        {
            // Check for user input to perform an action (e.g., press "x")
            if (Input.GetKeyDown(KeyCode.X))
            {
                // Decrease blood by 10
                hpManager.TakeDamage(10);

                // Decrease HP by 2
                hpManager.DecreaseHP(2);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                // Decrease blood by 30
                hpManager.TakeDamage(30);

                // Decrease HP by 3
                hpManager.DecreaseHP(3);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                hpManager.TakeDamage(50);
                hpManager.DecreaseHP(4);
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                hpManager.TakeDamage(90);
                hpManager.DecreaseHP(5);
            }
        }
    }
}
