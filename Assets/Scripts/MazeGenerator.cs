using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;
    // Start is called before the first frame update
    private void Awake() {
        GenerateMaze(mazeSize);
    }

    public void GenerateMaze(Vector2Int size){
        List<MazeNode> nodes = new List<MazeNode>();

        for(int x=0; x<size.x; x++){
            for(int y=0; y<size.y; y++){
                Vector3 nodePos = new Vector3(x-(size.x /2f),y-(size.y /2f),0);
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);
            }
        }
        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        //Choose the starting node
        currentPath.Add(nodes[Random.Range(0,nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        //Run until all nodes have been completed
        while(completedNodes.Count < nodes.Count){
            //Check Nodes next to current node
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count -1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if(currentNodeX < size.x-1){
                //Check node to the right of the current node 
                if(!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y])){
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }
            if(currentNodeX > 0){
                //Check node to the left of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y])){
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if(currentNodeY < size.y-1){
                //Check node above of the current node 
                if(!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1])){
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }
            if(currentNodeY > 0){
                //Check node to the left of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1])){
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            //Choose next node
            if(possibleDirections.Count > 0){
                int chosenDirection = Random.Range(0,possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch(possibleDirections[chosenDirection]){
                    //To the Right, remove left wall of chosen node and right wall of current node
                    case 1:
                        chosenNode.removeWall(1);
                        currentPath[currentPath.Count -1].removeWall(0);
                        break;
                    //To the Left, remove right wall of chosen node and left wall of current node
                    case 2:
                        chosenNode.removeWall(0);
                        currentPath[currentPath.Count -1].removeWall(1);
                        break;
                    //Up, remove bottom wall of chosen node and top wall of current node
                    case 3:
                        chosenNode.removeWall(3);
                        currentPath[currentPath.Count -1].removeWall(2);
                        break;
                    //Down, remove bottom wall of chosen node and top wall of current node
                    case 4:
                        chosenNode.removeWall(2);
                        currentPath[currentPath.Count -1].removeWall(3);
                        break;
                }
                currentPath.Add(chosenNode);
                chosenNode.SetState(NodeState.Current);
            }
            else{
                completedNodes.Add(currentPath[currentPath.Count-1]);   
                currentPath[currentPath.Count-1].SetState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count-1);
            }
        }
        nodes[5].removeWall(1);
        nodes[65].removeWall(2);
        nodes[60].setCenter();
        nodes[60].SetState(NodeState.Current);
        nodes[55].removeWall(3);
    }
}
