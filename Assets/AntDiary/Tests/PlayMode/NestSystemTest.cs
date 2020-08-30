using System.Collections;
using System.Collections.Generic;
using AntDiary;
using AntDiary.Scripts.Roads;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NestSystemTest
    {
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerable NestSystemTestSimplePasses()
        {
            var mainSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AntDiary/Prefabs/System/MainSystem.prefab");
            Assert.NotNull(mainSystemPrefab);
            var mainSystemGameObject = Object.Instantiate(mainSystemPrefab);
            yield return null;
            
            var nestSystem = mainSystemGameObject.GetComponentInChildren<NestSystem>();
            Assert.NotNull(nestSystem);

        }
    }
}
