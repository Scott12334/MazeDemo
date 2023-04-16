using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 5f;
    private bool gameStarted;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted){
            // get input from WASD keys
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // calculate movement direction
            Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
            // move the player
            transform.position += movement * moveSpeed * Time.deltaTime;
        }
    }
    public bool isGameStarted(){return gameStarted;}
    public void startGame(){
        this.GetComponent<SpriteRenderer>().enabled = true;
        gameStarted = true;
    }
}
