using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject globalLight, clockObject, player, maze, carpet;
    private PlayerController playerController;
    private TextMeshProUGUI clockText;
    private int seconds, minutes;
    void Start()
    {
        StartCoroutine(startGame());
        clockText = clockObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(countDown());
        playerController = player.GetComponent<PlayerController>();
        maze.transform.localScale = new Vector3(0.75f,0.75f,1);
        maze.transform.position = new Vector3(-10f,0,0);
    }
    IEnumerator countDown(){
        for(int i=10; i>-3; i--){
            if(i>0){
                clockText.text = i+"";
            }
            else{
                switch(i){
                    case 0:
                        clockText.text = "Ready";
                        break;
                    case -1:
                        clockText.text = "Set";
                        break;
                    case -2:
                        clockText.text = "Go";
                        break;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator startGame(){
        yield return new WaitForSeconds(13);
        carpet.SetActive(true);
        globalLight.SetActive(false);
        clockObject.SetActive(true);
        playerController.startGame();
        maze.transform.localScale = new Vector3(1.5f,1.5f,1);
        maze.transform.position = new Vector3(-5f,0,0);
        StartCoroutine(clock());
    }
    IEnumerator clock(){
        while(true){
            if(seconds <59){
                seconds++;
            }
            else{
                seconds = 0;
                minutes++;
            }
            string time = "";
            if(minutes < 10){
                time += "0"+minutes+":";
            }
            else{
                time += minutes+":";
            }
            if(seconds < 10){
                time += "0"+seconds;
            }
            else{
                time += seconds;
            }
            clockText.text = time;
            yield return new WaitForSeconds(1);
        }
    }
}
