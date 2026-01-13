using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;
    private List<int> selectCardList;
    private List<int> cardsShownInPanel;
    private int cardused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Duplicate instance of DataManager. Destroying the extra one.");
            Destroy(this);
        }

        selectCardList = new List<int>();
        cardsShownInPanel = new List<int>();
    }

    void Start()
    {
        instance = this;
    }


    // Add a method to add a selected card index
    public void AddSelectIndex(int index)
    {
        if (!selectCardList.Contains(index))
            selectCardList.Add(index);
    }

    // Add a method to get the selected card list
    public List<int> GetSelectCardList()
    {
        return selectCardList;
    }

    // Add a method to record a card shown in the panel
    public void AddCardToPanel(int cardIndex)
    {
        
            cardsShownInPanel.Add(cardIndex);
    }
   

    // Add a method to get the list of cards shown in the panel
    public List<int> GetCardsShownInPanel()
    {
        return cardsShownInPanel;
    }
    public void Cardused(int cardnumber)//ÓÃÁËÊ²Ã´¿¨
    {
        cardused = cardnumber;

    }
    public int GetCardUsed()
    {
        return cardused;
    }
    public void ReplaceCardInPanel(int position, int newCardIndex)
    {
        if (position >= 0 && position < cardsShownInPanel.Count)
        {
            cardsShownInPanel[position] = newCardIndex;
        }
        else
        {
            Debug.LogWarning("Invalid position to replace card: " + position);
        }
    }

}
