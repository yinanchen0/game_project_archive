using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UIElements;

public class HPManager : MonoBehaviour
{
    public TMP_Text hpText; // Reference to the TextMesh Pro text component
    public Image hpFillImage; // Reference to the Image component for the HP fill bar
    public Image blood; // Reference to the Image component representing the blood fill.e
    public Image normalCharacter;
    public Image characterChange;
    public Image[] action;
   
    public int maxHP = 8; // Maximum HP value
    public float hpIncreaseInterval = 3.0f; // Interval for increasing HP (every 3 seconds)
    public int currentHP = 3; // Current HP value
    private float timeSinceLastHPIncrease = 0.0f;
    private float hpDecreaseDuration = 0.0f;
    private float specialIncreaseDuration = 0f; // Duration of special increase
    private bool isDecreasingHP = false;
   
    private float maxHealth = 100f; // Maximum health value
    private float currentHealth = 100f; // Current health value
    private bool canTakeDamage = true;
    private bool isShieldActive = false;
    private SelectionPanelController selectionPanelController;

    public List<int> allCards; // List of all available cards
    public List<int> displayedCards; // List of currently displayed cards
    public int numberOfCardsToDisplay = 3; // Number of cards to display

    public List<int> selectCardList;
    private DataManager dataManager;
    public CardItem cardItem;
    private int card0UsageCount = 0;//for wolf-counter
    public int cardused=-1;
    public GameObject[] objectToMove;
    public float displayTime = 3f; // Adjust this to set the time the effect will be displayed
    public float moveSpeed = 5f;
    public Bullet bullet;

    public GameObject yourImageGameObject;
    private void Awake()
    {
        selectionPanelController = GameObject.FindObjectOfType<SelectionPanelController>();
    }


    private void Start()
    {
        normalCharacter.gameObject.SetActive(true);
        characterChange.gameObject.SetActive(false);

        for (int i = 0; i < action.Length; i++)
        {
            action[i].gameObject.SetActive(false);
        }


        UpdatePH();
        UpdateHealthBar();
        dataManager = DataManager.instance;
        if (DataManager.instance != null)
        {
            selectCardList = DataManager.instance.GetSelectCardList();
        }
        if (selectionPanelController != null)
        {
            // Access the list from ListHolder
            //foreach (int item in selectionPanelController.selectedCards)
            //{

            //}
        }
        else
        {
            Debug.LogWarning("ListHolder script not found!");
        }
        List<int> cardsShownInPanel = dataManager.GetCardsShownInPanel();
        if (selectionPanelController != null)
        {
            // Access the list from ListHolder
            //foreach (int item in selectionPanelController.selectedCards)
            //{
               // Debug.Log("Selected Card: " + item);
            //}
        }
        else
        {
            //Debug.LogWarning("ListHolder script not found!");
        }
        cardItem = FindObjectOfType<CardItem>();

    }

    private void Update()
    {


        List<int> selectCardList = dataManager.GetCardsShownInPanel();
        // Increase HP over time
        timeSinceLastHPIncrease += Time.deltaTime;
        // Check if HP decrease is in progress
        if (isDecreasingHP)
        {
            hpDecreaseDuration -= Time.deltaTime;

            if (hpDecreaseDuration <= 0)
            {
                isDecreasingHP = false;
                hpDecreaseDuration = 0.0f;
            }
        }
        if (!isDecreasingHP)
        {
            if (specialIncreaseDuration > 0)
            {
                if (timeSinceLastHPIncrease >= 1.0f)
                {
                    currentHP++;
                    specialIncreaseDuration -= 1.0f;
                    timeSinceLastHPIncrease = 0.0f;
                    if (currentHP >= maxHP)
                    {
                        currentHP = maxHP;
                    }
                }
            }
            else
            {
                if (timeSinceLastHPIncrease >= hpIncreaseInterval && currentHP < maxHP)
                {
                    currentHP++;
                    timeSinceLastHPIncrease = 0.0f;
                }
            }
        }
        UpdatePH();


        if (Input.GetKeyDown(KeyCode.Q) && selectCardList.Contains(0) && currentHP >= 4)
        {
            // Use card 0
            ActivateCard_Q();
            
            dataManager.Cardused(0);
            if (dataManager.GetCardUsed() != null)
            {
                List<int> cardsShown = dataManager.GetCardsShownInPanel();
                int cardUsed = dataManager.GetCardUsed();
                for (int i = 0; i < cardsShown.Count; i++)
                {
                    if (cardUsed == cardsShown[i])
                    {
                        
                        cardItem.ChangeCard(i);
                        dataManager.Cardused(-1);
                        break;
                    }
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.W) && currentHP >= 2 && selectCardList.Contains(1) )//FireGun
        {
            ActivateCard_W();
            dataManager.Cardused(1);
            if (dataManager.GetCardUsed() != null)
            {
                List<int> cardsShown = dataManager.GetCardsShownInPanel();
                int cardUsed = dataManager.GetCardUsed();
                for (int i = 0; i < cardsShown.Count; i++)
                {
                    if (cardUsed == cardsShown[i])
                    {
                       
                        cardItem.ChangeCard(i);
                        dataManager.Cardused(-1);
                        break;
                    }
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.E) && currentHP >= 4 && selectCardList.Contains(2) )//RockNRoll
        {
            ActivateCard_E();
            dataManager.Cardused(2);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                    if (cardItem != null)
                    {
                        cardItem.ChangeCard(i);
                        dataManager.Cardused(-1);
                    }
                    else
                    {
                        
                    }
                }
            }


        }
        if (Input.GetKeyDown(KeyCode.R) && currentHP >= 4 && selectCardList.Contains(3))//Protection
        {
            ActivateCard_R();
            dataManager.Cardused(3);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                    if (cardItem != null)
                    {
                        cardItem.ChangeCard(i);
                        dataManager.Cardused(-1);
                    }
                    else
                    {
                      
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.T) && currentHP >= 4 && selectCardList.Contains(4))//Heal
        {
            ActivateCard_T();
            dataManager.Cardused(4);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                    if (cardItem != null)
                    {
                        cardItem.ChangeCard(i);
                        dataManager.Cardused(-1);
                    }
                    else
                    {
                       
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Y) && currentHP >= 4 && selectCardList.Contains(5))//Thum
        {
            ActivateCard_Y();
            dataManager.Cardused(5);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                   
                    cardItem.ChangeCard(i);
                    dataManager.Cardused(-1);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.U) && currentHP >= 4 && selectCardList.Contains(6) )//Der:decrease oponents hp
        {
            ActivateCard_U();
            dataManager.Cardused(6);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                   
                    cardItem.ChangeCard(i);
                    dataManager.Cardused(-1);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I) && currentHP >= 1 && selectCardList.Contains(7) )//Gun
        {
            ActivateCard_I();
            dataManager.Cardused(7);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                   
                    cardItem.ChangeCard(i);
                    dataManager.Cardused(-1);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.O) && currentHP >= 2 && selectCardList.Contains(8))
        {
            ActivateCard_O();
            dataManager.Cardused(8);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();

            // Assuming yourImageGameObject is correctly assigned
            Bullet bulletScript = yourImageGameObject.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                Debug.Log("InitData calling");

                // Start the coroutine
                StartCoroutine(MoveBulletCoroutine(bulletScript));
            }
            else
            {
                Debug.LogError("Bullet script not found on the GameObject.");
            }

            // Continue with the rest of your logic
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                    cardItem.ChangeCard(i);
                    dataManager.Cardused(-1);
                    break;
                }
            }
        }

        




        if (Input.GetKeyDown(KeyCode.P) && currentHP >= 4 && selectCardList.Contains(9) )//RockNRoll
        {
            ActivateCard_P();
            dataManager.Cardused(9);
            List<int> cardsShown = dataManager.GetCardsShownInPanel();
            int cardUsed = dataManager.GetCardUsed();
            for (int i = 0; i < cardsShown.Count; i++)
            {
                if (cardUsed == cardsShown[i])
                {
                   
                    cardItem.ChangeCard(i);
                    dataManager.Cardused(-1);
                    break;
                }
            }
        }

    }

    private void UpdatePH()
    {
       
        // Update the TextMesh Pro text
        hpText.text = "HP: " + currentHP + " / " + maxHP;

        // Update the Image fill amount (calculate the fill amount)
        float fillAmount = (float)currentHP / maxHP;
        hpFillImage.fillAmount = fillAmount;
    }
    void UpdateHealthBar()
    {
        // Update the blood fill amount based on current health
        float fillAmount = currentHealth / maxHealth;
        blood.fillAmount = fillAmount;
    }
    public void DecreaseHP(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Max(0, currentHP);
        isDecreasingHP = true;
        hpDecreaseDuration = 0f; // Special decrease duration
    }
    private void IncreaseHP()
    {
       
        timeSinceLastHPIncrease = 6.0f;
        specialIncreaseDuration = 6.0f;
    }
    public int GetCurrentHP()
    {
        return currentHP;
    }
    public void TakeDamage(float damageAmount)
    {

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health stays within valid range
        UpdateHealthBar();
    }
    public void Heal(float healAmount)
    {

        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Ensure health stays within valid range
        UpdateHealthBar();
    }
    private IEnumerator DelayedTakeDamage(int amount, float damageAmount)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(amount);
        TakeDamage(damageAmount);
        if (canTakeDamage) {
            TakeDamage(damageAmount); // Apply damage after the delay
            canTakeDamage = true;
        }
        
    }
    private void ActivateShield()
    {
        isShieldActive = true;
        StartCoroutine(DeactivateShieldAfterDelay(7.0f)); // Deactivate shield after 5 seconds
    }

    private IEnumerator DeactivateShieldAfterDelay(float delay)//自己用不上
    {
        yield return new WaitForSeconds(delay);
        isShieldActive = false;
    }
    private IEnumerator HideImageAfterDelay(float delay)//人物控制
    {
        yield return new WaitForSeconds(delay);

        // After the delay, hide the image.
        characterChange.gameObject.SetActive(false);
    }
    private void ActivateCard_Q()
    {
        DecreaseHP(6);
        characterChange.gameObject.SetActive(true);
        if (card0UsageCount == 1)
        { 
            StartCoroutine(DelayedTakeDamage(2, 10.0f*card0UsageCount)); //延迟攻击
        }
        
        else if (card0UsageCount>=2&& card0UsageCount<=5) {
            StartCoroutine(DelayedTakeDamage(2, 10.0f * card0UsageCount)); //延迟攻击
        }
        else
        {
            StartCoroutine(DelayedTakeDamage(2, 50.0f));
        }
        StartCoroutine(HideImageAfterDelay(10f));
        
    }
    private void ActivateCard_W()
    {
        DecreaseHP(5); // Decrease by 5 HP
        characterChange.gameObject.SetActive(true);
        StartCoroutine(HideImageAfterDelay(10f));


    }
    private void ActivateCard_E()
    {
        DecreaseHP(3); // Decrease by 2 HP
    }
    private void ActivateCard_R()
    {
        DecreaseHP(4);
        ActivateShield();
        characterChange.gameObject.SetActive(true);
        StartCoroutine(HideImageAfterDelay(10f));
    }
    private void ActivateCard_T()
    {
        DecreaseHP(4);
        Heal(40f);
        characterChange.gameObject.SetActive(true);
        StartCoroutine(HideImageAfterDelay(10f));
    }
    private void ActivateCard_Y()
    {
        DecreaseHP(4);
        TakeDamage(50f);
        characterChange.gameObject.SetActive(true);
        StartCoroutine(HideImageAfterDelay(10f));
    }
    private void ActivateCard_U()
    {
        DecreaseHP(4);
        characterChange.gameObject.SetActive(true);
        StartCoroutine(HideImageAfterDelay(10f));
    }
    private void ActivateCard_I()
    {
        DecreaseHP(1);

        if (!isShieldActive)
        { TakeDamage(20f); 
          
        }
    }
    private void ActivateCard_O()
    {
        DecreaseHP(2); // Decrease by 3 HP

        if (!isShieldActive)
        { TakeDamage(40f); }
    }
    private void ActivateCard_P()
    {
        DecreaseHP(4); // Decrease by 3 HP
        IncreaseHP();
        characterChange.gameObject.SetActive(true);
        StartCoroutine(HideImageAfterDelay(2f));
    }

    // Coroutine definition outside the if statement
    IEnumerator MoveBulletCoroutine(Bullet bulletScript)
    {
        // Call InitData function
        bulletScript.InitData();

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // After 5 seconds, call StopMoving function
        bulletScript.StopMoving();
    }








}
