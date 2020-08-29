using System.Collections;
using System.Collections.Generic;
using AntDiary;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NestSystemTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NestSystemTestSimplePasses()
        {
            // Use the Assert class to test conditions
            var mainSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AntDiary/Prefabs/System/MainSystem.prefab");
            Assert.NotNull(mainSystemPrefab);
            var mainSystemGameObject = Object.Instantiate(mainSystemPrefab);
            var nestSystem = mainSystemGameObject.GetComponentInChildren<NestSystem>();
            Assert.NotNull(nestSystem);

        }
    }
}
