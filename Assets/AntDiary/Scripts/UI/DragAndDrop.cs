using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject root;
    private GameObject rootObject;

    private Vector3 position;
    private Vector3 screenToWorldPointPosition;

    public void PushDown()
    {
        position = Input.mousePosition;
        position.z = 10f;
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        rootObject = Instantiate(root, screenToWorldPointPosition, Quaternion.identity);
    }

    public void PushDrag()
    {
        position = Input.mousePosition;
        position.z = 10f;
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        rootObject.transform.position = screenToWorldPointPosition;
    }
}
