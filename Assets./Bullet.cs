using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 50f;
    private bool isMoving = false;
    private float elapsedTime = 0f;
    private float duration = 5f;

    void Update()
    {
        // Check if the bullet should move continuously
        if (isMoving)
        {
            MoveBullet();
        }
    }

    void MoveBullet()
    {
        Vector3 movement = new Vector3(50, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        elapsedTime += Time.deltaTime;

        // Check if the bullet has reached a certain position to stop or if the duration has passed
        if (transform.position.x >= 300f || elapsedTime >= duration)
        {
            StopMoving();
        }
    }

    public void InitData()
    {
        // Initialization code goes here, if needed

        // Set the flag to start moving
        isMoving = true;
        elapsedTime = 0f;
    }

    public void StopMoving()
    {
        Debug.Log("Stopping bullet");
        isMoving = false;

        // Reset the bullet's position or perform other actions here
        transform.position = Vector3.zero;
    }
}
