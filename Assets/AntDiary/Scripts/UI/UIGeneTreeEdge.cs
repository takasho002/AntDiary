using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    public class UIGeneTreeEdge : MonoBehaviour
    {

        private RectTransform rectTransform;

        public RectTransform RectTransform
        {
            get
            {
                if (!rectTransform) rectTransform = GetComponent<RectTransform>();
                return rectTransform;
            }
        }
        
        public Vector2 Position
        {
            get => RectTransform.anchoredPosition;
            set => RectTransform.anchoredPosition = value;
        }

        public float Length
        {
            get => RectTransform.sizeDelta.x;
            set
            {
                var s = RectTransform.sizeDelta;
                s.x = value;
                RectTransform.sizeDelta = s;
            }
        }
        

        public float Rotation
        {
            get => RectTransform.localRotation.eulerAngles.z;
            set => RectTransform.localRotation = Quaternion.Euler(0, 0, value);
        }

        public void SetTarget(UIGeneTreeNode a, UIGeneTreeNode b)
        {
            Position = Vector2.Lerp(a.Position, b.Position, 0.5f);
            Vector2 diff = b.Position - a.Position;
            
            Length = diff.magnitude;
            Rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}