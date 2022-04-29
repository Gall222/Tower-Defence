using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{


    Waypoint waypoint;


    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapToGrid();
        UpdatesTheLabel();

    }

    private void SnapToGrid()
    {

        int gridSize = waypoint.GetGridSize();

        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize,
            0f,
            waypoint.GetGridPos().y * gridSize);
    }

    private void UpdatesTheLabel()
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
       
        string lableText = waypoint.GetGridPos().x  + ", " + waypoint.GetGridPos().y ;
        textMesh.text = lableText;
        gameObject.name = lableText;
    }
}
