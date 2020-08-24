using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace AntDiary
{
    public class DragAndDrop_ExOfBuildingSystem : MonoBehaviour
    {
        public GameObject root;
        private GameObject rootObject;

        private Vector3 position;
        private Vector3 screenToWorldPointPosition;

        public BuildingSystem Instance = NestSystem.Instance.BuildingSystem;

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
}
