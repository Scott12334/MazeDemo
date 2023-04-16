using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer circleRenderer;
    [SerializeField]
    private EdgeCollider2D edgeCollider;
    // Start is called before the first frame update
    void Start()
    {
        DrawCricle(100,5,circleRenderer);
        addCollider(circleRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void DrawCricle(int steps, float radius, LineRenderer currentCircle){
        currentCircle.positionCount = steps;

        for(int currentStep=0; currentStep<steps; currentStep++){
            if(currentStep == 5){

            }
            else{
                
            }
            float circumferenceProgress = (float)currentStep/(steps-1);

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x,y,0);

            currentCircle.SetPosition(currentStep,currentPosition);
        }
    }
    public void addCollider(LineRenderer lineRenderer){
        List<Vector2> edges = new List<Vector2>();

        for(int i=0; i<lineRenderer.positionCount; i++){
            Vector3 lineRendererPoint = lineRenderer.GetPosition(i);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        }

        edgeCollider.SetPoints(edges);
    }
}
