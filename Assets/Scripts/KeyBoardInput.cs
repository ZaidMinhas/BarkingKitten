using UnityEngine;

public class KeyBoardInput : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character movement

    private Vector2 movement;

    void Update()
    {
        // Get input from arrow keys or WASD keys
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        movement.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        // Move the character based on the input
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}