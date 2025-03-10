using System.Collections.Generic;
using UnityEngine;

public class CanvaManager : MonoBehaviour
{
    public List<GameObject> canvasList; // List of canvases to manage

    void Start()
    {
        // Ensure only the first canvas is active at the start
        if (canvasList.Count > 0)
        {
            SelectCanvas(canvasList[1]);
        }
    }

    public void SelectCanvas(GameObject selectedCanvas)
    {
        foreach (GameObject canvas in canvasList)
        {
            if (canvas != null)
            {
                canvas.SetActive(canvas == selectedCanvas); // Activate only the selected canvas
            }
        }
    }

    // New method to switch to the next canvas
    public void SwitchToCanvas(int canvasIndex)
    {
        if (canvasIndex >= 0 && canvasIndex < canvasList.Count)
        {
            SelectCanvas(canvasList[canvasIndex]);
        }
    }
}
