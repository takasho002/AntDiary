using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// セーブデータのロードとか、初期化処理のエントリポイントとなったりするクラス
    /// </summary>
    public class MainSystem : MonoBehaviour
    {
        [SerializeField] private bool showDebugMenu;
        [SerializeField] private MonoBehaviour[] debugMenuPages;
        private List<IDebugMenu> DebugMenuPages = new List<IDebugMenu>();
        
        private SaveSystem saveSystem = new SaveSystem();


        private void Start()
        {
            DebugMenuPages.AddRange(debugMenuPages.OfType<IDebugMenu>());
            DebugMenuPages.Add(saveSystem);
            
            saveSystem.LoadDefaultSaveUnitToCurrent();
        }

        private void Update()
        {
            UpdateFrameRateMeasurement();
        }

        #region Debug

        private int frameRateFrameCount;
        private float frameRatePrevTime;
        private int measuredFrameRate;

        private void UpdateFrameRateMeasurement()
        {
            frameRateFrameCount++;
            float time = Time.realtimeSinceStartup - frameRatePrevTime;

            if (time >= 0.5f)
            {
                measuredFrameRate = Mathf.RoundToInt(frameRateFrameCount / time);

                frameRateFrameCount = 0;
                frameRatePrevTime = Time.realtimeSinceStartup;
            }
        }


        private void OnGUI()
        {
            if (!showDebugMenu) return;
            debugMenuWindowRect.height = 20;
            debugMenuWindowRect = GUILayout.Window(100, debugMenuWindowRect, OnGUIDebugWindow, "Debug Menu");
        }

        private bool isDebugMenuCollapsed = false;
        private string debugMenuPage = "";
        private Rect debugMenuWindowRect = new Rect(20, 20, 400, 20);
        private Vector2 debugMenuDragStarted;
        private bool debugMenuIsDragging = false;

        private void OnGUIDebugWindow(int id)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 20));
            if (isDebugMenuCollapsed)
            {
                if (GUILayout.Button("Show"))
                {
                    isDebugMenuCollapsed = false;
                }

                return;
            }

            GUILayout.Label($"AntDiary (ver \"{Application.version}\", Unity {Application.unityVersion})");
            GUILayout.BeginHorizontal();
            GUILayout.Label(
                $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");
            if (GUILayout.Button("Hide ")) isDebugMenuCollapsed = true;
            GUILayout.EndHorizontal();
            GUILayout.Label(measuredFrameRate + " FPS");

            if (string.IsNullOrEmpty(debugMenuPage))
            {
                int pageCount = DebugMenuPages.Count();
                int horizontalGrid = 2;
                GUILayout.BeginVertical();

                int i = 0;
                bool opened = false;
                foreach (var page in DebugMenuPages)
                {
                    if (i % horizontalGrid == 0)
                    {
                        GUILayout.BeginHorizontal();
                        opened = true;
                    }
                    
                    if (GUILayout.Button(page.pageTitle))
                    {
                        debugMenuPage = page.pageTitle;
                    }

                    if ((i+1) % horizontalGrid == 0)
                    {
                        GUILayout.EndHorizontal();
                        opened = false;
                    }
                    i++;
                }
                if(opened) GUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            else
            {
                if (GUILayout.Button("Back")) debugMenuPage = "";
                DebugMenuPages.FirstOrDefault(p => p.pageTitle == debugMenuPage)?.OnGUIPage();
            }


        }

        #endregion
    }
}