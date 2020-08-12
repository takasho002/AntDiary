using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AntDiary.Editor
{
    [CustomEditor(typeof(GeneTree))]
    public class GeneTreeEditor : UnityEditor.Editor
    {
        private GeneTree Target => target as GeneTree;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("編集"))
            {
                GeneTreeEditorWindow.Open(Target);
            }
            base.OnInspectorGUI();
        }
    }
}