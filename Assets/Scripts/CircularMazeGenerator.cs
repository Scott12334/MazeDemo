using UnityEngine;

public class CircularMazeGenerator : MonoBehaviour
{
    public int numRings = 5;
    public int numSectors = 8;
    public float radius = 5f;
    public float diameter = 2;
    public float wallThickness = 0.1f;
    public Color wallColor = Color.white;
    public float lineThickness = 0.05f;
    public Color lineColor = Color.gray;

    private int[,] maze;
    private Vector2[,] positions;

    private void Start()
    {
        maze = new int[numRings, numSectors];
        positions = new Vector2[numRings, numSectors];
        diameter = radius*2;
        GenerateMaze();

        DrawMaze();
    }

    private void GenerateMaze()
    {
        // Initialize maze with all walls
        for (int i = 0; i < numRings; i++)
        {
            for (int j = 0; j < numSectors; j++)
            {
                maze[i, j] = 15;
            }
        }

        // Pick random starting position
        int startRing = Random.Range(0, numRings);
        int startSector = Random.Range(0, numSectors);

        // Run depth-first search algorithm to generate maze
        DFS(startRing, startSector);

        // Compute positions of walls and circles
        for (int i = 0; i < numRings; i++)
        {
            for (int j = 0; j < numSectors; j++)
            {
                float angle = j * Mathf.PI * 2f / numSectors;
                float x = radius * (i + 0.5f) / numRings * Mathf.Cos(angle);
                float y = radius * (i + 0.5f) / numRings * Mathf.Sin(angle);
                positions[i, j] = new Vector2(x, y);
            }
        }
    }

    private void DFS(int ring, int sector)
    {
        // Mark current cell as visited
        maze[ring, sector] &= ~8;

        // Randomly shuffle directions
        int[] directions = { 0, 1, 2, 3 };
        for (int i = 0; i < 4; i++)
        {
            int tmp = directions[i];
            int r = Random.Range(i, 4);
            directions[i] = directions[r];
            directions[r] = tmp;
        }

        // Visit neighboring cells in random order
        for (int i = 0; i < 4; i++)
        {
            int dir = directions[i];
            int neighborRing = ring + ((dir == 0) ? 1 : ((dir == 2) ? -1 : 0));
            int neighborSector = (sector + dir) % numSectors;
            if (neighborRing >= 0 && neighborRing < numRings &&
                (maze[neighborRing, neighborSector] & 8) != 0)
            {
                // Knock down wall between current cell and neighbor
                maze[ring, sector] &= ~(1 << dir);
                maze[neighborRing, neighborSector] &= ~(1 << ((dir + 2) % 4));
                DFS(neighborRing, neighborSector);
            }
        }
    }

    private void DrawMaze()
    {
        // Create empty game object to hold line renderer
        GameObject obj = new GameObject("Maze");
        obj.transform.parent = transform;

        // Add line renderer component
        LineRenderer line = obj.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startWidth = lineThickness;
        line.endWidth = lineThickness;
        line.startColor = lineColor;
        line.endColor = lineColor;
        for (int i = 0; i < numRings; i++)
        {
            for (int j = 0; j < numSectors; j++)
            {
                Vector3 pos = new Vector3((i + 0.5f) / numRings * diameter - radius, (j + 0.5f) / numSectors * diameter - radius, 0f);

                if ((maze[i, j] & 1) == 0)
                {
                    float r1 = i / (float)numRings * radius;
                    float r2 = (i + 1f) / numRings * radius;
                    float angle = j / (float)numSectors * 2f * Mathf.PI;

                    line.positionCount = 2;
                    line.SetPosition(0, new Vector3(pos.x + r1 * Mathf.Cos(angle), pos.y + r1 * Mathf.Sin(angle), 0f));
                    line.SetPosition(1, new Vector3(pos.x + r2 * Mathf.Cos(angle), pos.y + r2 * Mathf.Sin(angle), 0f));
                    line.startColor = lineColor;
                    line.endColor = lineColor;
                    line.startWidth = lineThickness;
                    line.endWidth = lineThickness;
                }

                if ((maze[i, j] & 4) != 0)
                {
                    float r3 = (i + 1f) / numRings * radius;
                    float angle1 = j / (float)numSectors * 2f * Mathf.PI;
                    float angle2 = (j + 1f) / numSectors * 2f * Mathf.PI;

                    line.positionCount = 2;
                    line.SetPosition(0, new Vector3(pos.x + r3 * Mathf.Cos(angle1), pos.y + r3 * Mathf.Sin(angle1), 0f));
                    line.SetPosition(1, new Vector3(pos.x + r3 * Mathf.Cos(angle2), pos.y + r3 * Mathf.Sin(angle2), 0f));
                    line.startColor = Color.black;
                    line.endColor = Color.black;
                    line.startWidth = 2f;
                    line.endWidth = 2f;
                }
            }
        }
    

    }
}


