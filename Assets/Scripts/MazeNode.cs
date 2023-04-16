using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum NodeState{
    Available,
    Current,
    Completed
}
public class MazeNode : MonoBehaviour
{
    bool isCenter = false;
    [SerializeField] GameObject[] walls;
    [SerializeField] SpriteRenderer floor;
    public void SetState(NodeState state){
        switch(state){
            case NodeState.Available:
                floor.color = Color.black;
                break;
            case NodeState.Current:
                floor.color = Color.yellow;
                break;
            case NodeState.Completed:
                floor.color = Color.blue;
                break;
        }
    }

    public void removeWall(int removedWall){
        walls[removedWall].gameObject.SetActive(false);
    }
    public void setCenter(){isCenter = true;}
    private void OnTriggerEnter2D(Collider2D other) {
        if(this.isCenter && GameObject.Find("Character").GetComponent<PlayerController>().isGameStarted()){
            SceneManager.LoadScene(0);
        }
    }
}
