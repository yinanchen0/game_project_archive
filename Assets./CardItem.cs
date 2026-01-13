using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardItem : MonoBehaviour
{
    private DataManager dataManager;
    public Sprite[] cardSprites; // Store the sprites for each card
    public Image cardImage; // Image to display card sprite
    public Image cardImage1; // Image to display card sprite
    public Image cardImage2; // Image to display card sprite

    private void Start()
    {
        // Get the selected card list from the DataManager
        dataManager = DataManager.instance;
        dataManager.Cardused(100);
    }

    void Update()
    {
        if (dataManager.GetCardsShownInPanel().Count < 3)
        {
            ShowRandomCard();

        }
    }

    public void ShowRandomCard()
    {
        List<int> selectedCardList = dataManager.GetSelectCardList();
        int randomIndex = Random.Range(0, selectedCardList.Count);
        int randomIndex1 = Random.Range(0, selectedCardList.Count);
        int randomIndex2 = Random.Range(0, selectedCardList.Count);

        while (randomIndex == randomIndex1 || randomIndex == randomIndex2 || randomIndex1 == randomIndex2)
        {
            randomIndex1 = Random.Range(0, selectedCardList.Count);
            randomIndex2 = Random.Range(0, selectedCardList.Count);
        }

        int selectedCardIndex = selectedCardList[randomIndex];
        int selectedCardIndex1 = selectedCardList[randomIndex1];
        int selectedCardIndex2 = selectedCardList[randomIndex2];

        dataManager.AddCardToPanel(selectedCardIndex);
        dataManager.AddCardToPanel(selectedCardIndex1);
        dataManager.AddCardToPanel(selectedCardIndex2);

        Card card = InitializeCard(selectedCardIndex);
        Card card1 = InitializeCard(selectedCardIndex1);
        Card card2 = InitializeCard(selectedCardIndex2);

        DisplayCard(card, card1, card2);
    }

    public void ChangeCard(int positionToReplace)
    {
        DataManager dataManager = DataManager.instance;
        List<int> cardList = dataManager.GetCardsShownInPanel();

        if (cardList.Count >= 3)
        {
            List<int> selectedCardList = dataManager.GetSelectCardList();
            int randomIndex = GetRandomIndexNotInList(selectedCardList, cardList);

            int newCardIndex = selectedCardList[randomIndex];

            Card newCard = InitializeCard(newCardIndex);
            Card currentCard0 = InitializeCard(cardList[0]);
            Card currentCard1 = InitializeCard(cardList[1]);
            Card currentCard2 = InitializeCard(cardList[2]);

            if (positionToReplace == 0)
            {
                dataManager.ReplaceCardInPanel(0, newCardIndex);
                DisplayCard(newCard, currentCard1, currentCard2);
            }
            else if (positionToReplace == 1)
            {
                dataManager.ReplaceCardInPanel(1, newCardIndex);
                DisplayCard(currentCard0, newCard, currentCard2);
            }
            else if (positionToReplace == 2)
            {
                dataManager.ReplaceCardInPanel(2, newCardIndex);
                DisplayCard(currentCard0, currentCard1, newCard);
            }
        }
        else
        {
            Debug.LogWarning("Not enough cards in cardList to update.");
        }
    }

    void DisplayCard(Card card, Card card1, Card card2)
    {
        Debug.Log("Displaying card: " + card.Name);
        Debug.Log("Displaying card1: " + card1.Name);
        Debug.Log("Displaying card2: " + card2.Name);

        cardImage.sprite = card.Sprite;
        cardImage1.sprite = card1.Sprite;
        cardImage2.sprite = card2.Sprite;
    }

    int GetRandomIndexNotInList(List<int> availableIndices, List<int> excludedIndices)
    {
        int randomIndex = Random.Range(0, availableIndices.Count);

        while (excludedIndices.Contains(randomIndex))
        {
            randomIndex = Random.Range(0, availableIndices.Count);
        }

        return randomIndex;
    }

    Card InitializeCard(int cardNumber)
    {
        int Number = cardNumber;
        string name = "";
        int attack = 0;
        int duration = 0;
        Sprite sprite = null;
        string active = "";

        // Initialize the card attributes based on the card number
        // Modify this logic according to your card setup
        switch (cardNumber)
        {
            case 0:
                name = "Card 1";
                attack = 100;
                duration = 6;
                sprite = cardSprites[0];
               
                break;
            case 1:
                name = "Card 2";
                attack = 120;
                duration = 5;
                sprite = cardSprites[1];
              
                break;
            case 2:
                name = "Card 3";
                attack = 120;
                duration = 5;
                sprite = cardSprites[2];
               
                break;
            case 3:
                name = "Card 4";
                attack = 100;
                duration = 6;
                sprite = cardSprites[3];
     
                break;
            case 4:
                name = "Card 5";
                attack = 120;
                duration = 5;
                sprite = cardSprites[4];
             
                break;
            case 5:
                name = "Card 6";
                attack = 120;
                duration = 5;
                sprite = cardSprites[5];
              
                break;
            case 6:
                name = "Card 7";
                attack = 120;
                duration = 5;
                sprite = cardSprites[6];
               
                break;
            case 7:
                name = "Card 8";
                attack = 120;
                duration = 5;
                sprite = cardSprites[7];
          
                break;
            case 8:
                name = "Card 9";
                attack = 100;
                duration = 6;
                sprite = cardSprites[8];
                
                break;
            case 9:
                name = "Card 10";
                attack = 120;
                duration = 5;
                sprite = cardSprites[9];
              
                break;

            // Handle other card numbers

            default:
                Debug.Log("Selected card number: " + cardNumber);
                break;
        }

        return new Card(cardNumber, name, attack, duration, sprite, active);
    }
}
