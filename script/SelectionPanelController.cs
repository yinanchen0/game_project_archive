using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionPanelController : MonoBehaviour
{
    public GameObject objectToMove;//mouse
    public Image[] imagesToDisplay;//bigger picture
    public GameObject[] cardUIObjects; // selected yellow
    public Image done;
    public List<int> selectedCards = new List<int>();
    public DataManager dataManager;
    private int currentImageIndex = 0;
    private int moveCount = 0;
    private int maxMoveCount = 4; // Maximum number of moves to the right in the first row
    private int maxTotalMoves = 10; // Maximum total moves before resetting
    private int maxSelectedCards = 5; // Maximum number of selected cards
    private float newYPosition = 100f; // Initial Y position for the new line
    private float secondLineYPosition = 200f; // Y position for the second row
    private bool isFirstImageShown = false;
    private Vector3 originalPosition; // Store the original position of the object
    private Vector3 currentPosition; // Store the current position of the object
    public bool end1 = false;
    private bool end = false;

    private void Start()
    {
        Debug.Log("step3");
        // Store the original position
        originalPosition = objectToMove.transform.position;
        currentPosition = originalPosition;

        // Initially, hide all images except the first one
        foreach (var image in imagesToDisplay)
        {
            image.gameObject.SetActive(false);
        }

        // Show the first image
        imagesToDisplay[currentImageIndex].gameObject.SetActive(true);
        isFirstImageShown = true;

        // Hide all card UI objects initially
        foreach (var cardUIObject in cardUIObjects)
        {
            cardUIObject.SetActive(false);
        }
        done.gameObject.SetActive(false);


    }

    private void Update()
    {
        
        // Check for the "A" key press
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("step5");
            // Move the object to the next position
            MoveObjectToNextPosition();

            // Show the next image
            ShowNextImage();

        }

        // Check for the "S" key press to select a card
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("step5");
            SelectCard();
        }

        // Check for the "D" key press to deselect a card
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeselectCard();
        }

        // Check for the "W" key press to change to the next panel
        if (Input.GetKeyDown(KeyCode.W) && end)
        {

            
            end1 = true;
        }
    }

    private void SelectCard()
    {
        if (DataManager.instance != null)
        {
            if (selectedCards.Count < maxSelectedCards && !selectedCards.Contains(currentImageIndex))
            {
                selectedCards.Add(currentImageIndex);

                // Show the corresponding UI object
                cardUIObjects[currentImageIndex].SetActive(true);

                // Check if the maximum number of selected cards is reached
                if (selectedCards.Count == maxSelectedCards)
                {
                    done.gameObject.SetActive(true);
                    end = true;
                    foreach (int item in selectedCards)
                    {
                        DataManager.instance.AddSelectIndex(item);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("DataManager is not yet initialized.");
        }
    }


    private void DeselectCard()
    {
        done.gameObject.SetActive(false);
        if (selectedCards.Count > 0 && selectedCards.Contains(currentImageIndex))
        {

            selectedCards.Remove(currentImageIndex);

            // Hide the corresponding UI object
            cardUIObjects[currentImageIndex].SetActive(false);
            end1 = false;

        }
    }

    private void ShowNextImage()
    {
        // Hide the current image
        imagesToDisplay[currentImageIndex].gameObject.SetActive(false);

        // Calculate the next image index
        currentImageIndex = (currentImageIndex + 1) % imagesToDisplay.Length;

        // Show the next image
        imagesToDisplay[currentImageIndex].gameObject.SetActive(true);
    }

    private void MoveObjectToNextPosition()
    {
        // Move the object to the next position
        if (objectToMove != null && moveCount < maxTotalMoves)
        {
            // Continue moving to the right
            Vector3 nextPosition = currentPosition + new Vector3(128f, 0f, 0f);
            objectToMove.transform.position = nextPosition;
            currentPosition = nextPosition;

            moveCount++;

            // Check if it's time to move to the second row
            if (moveCount == maxMoveCount && moveCount > 0)
            {
                currentPosition = new Vector3(originalPosition.x - 127, secondLineYPosition, originalPosition.z);
            }

            // Check if it's the 10th press
            if (moveCount == maxTotalMoves)
            {
                // Move back to the original position and reset the move count
                objectToMove.transform.position = originalPosition;
                currentPosition = originalPosition;
                moveCount = 0;
            }
        }
    }


}
