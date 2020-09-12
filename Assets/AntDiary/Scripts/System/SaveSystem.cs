using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;

namespace AntDiary
{
    public static class SaveSystem
    {
        public static void SaveCurrentSaveUnit(int number)
        {
            SaveSaveFile(number, SaveUnit.Current);
        }

        public static void LoadSaveUnitToCurrent(int number)
        {
            LoadSaveFile(number).SetAsCurrent();
        }

        public static void LoadDefaultSaveUnitToCurrent(DefaultEnvironmentData defaultEnvironment = null)
        {
            var su = SaveUnit.GetDefaultSaveUnit();

            //初期環境のセットアップ
            if (defaultEnvironment != null)
                su.s_GameContext.s_NestData.Structure.NestElements.AddRange(defaultEnvironment.GeneralPathRoads);

            su.SetAsCurrent();
        }

        private static SaveUnit SaveSaveFile(int number, SaveUnit su)
        {
            string fileName = GetSaveFileName(number);
            byte[] raw = MessagePackSerializer.Serialize(su);
            System.IO.Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            System.IO.File.WriteAllBytes(fileName, raw);
            return su;
        }

        private static SaveUnit LoadSaveFile(int number)
        {
            string fileName = GetSaveFileName(number);
            byte[] raw = System.IO.File.ReadAllBytes(fileName);
            var su = MessagePackSerializer.Deserialize<SaveUnit>(raw);
            return su;
        }

        private static string GetSaveFileName(int number)
        {
            return $"{Application.persistentDataPath.TrimEnd('/', '\\')}/Save/{number}/saveunit.bin";
        }
    }

    public class SaveSystemDebugMenu : IDebugMenu
    {
        #region Debug

        public string pageTitle { get; } = "セーブ/ロード";

        public void OnGUIPage()
        {
            if (SaveUnit.Current == null)
            {
                GUILayout.Label("セーブデータは未ロードです");
            }

            if (GUILayout.Button($"空のデータをロード"))
            {
                SaveSystem.LoadDefaultSaveUnitToCurrent();
            }

            for (int i = 0; i < 4; i++)
            {
                GUILayout.BeginHorizontal();

                if (GUILayout.Button($"Load from {i}"))
                {
                    SaveSystem.LoadSaveUnitToCurrent(i);
                }

                GUI.enabled = SaveUnit.Current != null;
                if (GUILayout.Button($"Save to {i}"))
                {
                    SaveSystem.SaveCurrentSaveUnit(i);
                }

                GUI.enabled = true;
                GUILayout.EndHorizontal();
            }
        }

        #endregion
    }
}