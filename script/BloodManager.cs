using UnityEngine;
using UnityEngine.UI;

public class BloodManager : MonoBehaviour
{
    public Image blood; // Reference to the Image component representing the blood fill.
    private float maxHealth = 100f; // Maximum health value
    private float currentHealth = 100f; // Current health value
    public HPManager hpManager;
    public int x = 0;
    // Inside a non-static method in BloodManager
    public void SomeMethod()
    {
        // Access hpManager, assuming it's a non-static field or property
        int currentHP = hpManager.GetCurrentHP();
        if (currentHP == 7) {
             x = 7;
        }
        else if (currentHP == 6)
        {
            x = 6;
        }
        else if (currentHP ==5)
        {
            x = 5;
        }
        else if (currentHP ==4)
        {
            x = 4;
        }
        else if(currentHP ==3)
        {
            x= 3;
        }
        else if(currentHP==2)
        {
            x = 2;
        }
        else if (currentHP ==1)
        {
            x = 1;
        }
        else if (currentHP == 0)
        {
            x = 0;
        }


        // Now you can use currentHP
        // ...
    }


    void Start()
    {
        // Initialize the health bar
        UpdateHealthBar();
    }

    void Update()
    {
        SomeMethod();
        // Simulate changes to health (for example, when the player takes damage)
        if ((x!=0) && Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(10f); // Decrease health by 10
            UpdateHealthBar();
        }
        if ((x == 3 || x == 4 || x == 5 || x == 6 || x == 7) && Input.GetKeyDown(KeyCode.C))
        {
            TakeDamage(30f); // Decrease health by 10
        }
        if ((x == 4 || x == 5 || x == 6 || x == 7) && Input.GetKeyDown(KeyCode.V))
        {
            TakeDamage(50f); // Decrease health by 10
        }
        if ((x == 5 || x == 6 || x == 7) && Input.GetKeyDown(KeyCode.B))
        {
            TakeDamage(90f); // Decrease health by 10 
        }

    }

    public void TakeDamage(float damageAmount)
    {

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health stays within valid range
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        // Update the blood fill amount based on current health
        float fillAmount = currentHealth / maxHealth;
        blood.fillAmount = fillAmount;
    }
}
