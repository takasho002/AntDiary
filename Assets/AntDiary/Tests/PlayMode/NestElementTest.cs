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
    public class NestElementTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NestElementTestSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NestElementTestWithEnumeratorPasses()
        {
            var mainSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/AntDiary/Prefabs/System/MainSystem.prefab");
            Assert.NotNull(mainSystemPrefab);
            var mainSystemGameObject = Object.Instantiate(mainSystemPrefab);
            yield return null;
            
            var nestSystem = mainSystemGameObject.GetComponentInChildren<NestSystem>();
            Assert.NotNull(nestSystem);

            var debugRoom = nestSystem.InstantiateNestElement(new DebugRoomData());
            Assert.NotNull(debugRoom);
            Assert.True(debugRoom is NestBuildableElement);
            var debugRoomCast = (NestBuildableElement) debugRoom;
            Assert.NotNull(debugRoomCast);
            
            
            var road = nestSystem.InstantiateNestElement(new IShapeRoadData(EnumRoadHVDirection.Horizontal));
            Assert.NotNull(road);
            Assert.True(road is NestBuildableElement);
            var roadCast = (NestBuildableElement) road;
            Assert.NotNull(roadCast);
            
        }
    }
}
