using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AntDiary;
using MessagePack;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class DataSerializationTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AntDataSerializationTestSimplePasses()
        {
            //AntData継承クラスのうち、非abstractなものを抽出
            var root = typeof(AntData);
            var unions = root.GetCustomAttributes(false).OfType<UnionAttribute>();
            var antDataTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes)
                .Where(t => t.IsSubclassOf(root) && !t.IsAbstract);
            foreach (var t in antDataTypes)
            {
                if(!t.GetCustomAttributes(false).Any(a => a is MessagePackObjectAttribute)) Assert.Fail($"MessagePackObjectAttributeを付与していないAntDataがあります: {t.Name}");
                if (!unions.Any(u => u.SubType == t)) Assert.Fail($"Unionに登録されていないAntDataがあります: {t.Name}");
            }
            
            //シリアライズしてみる
            
            foreach (var t in antDataTypes)
            {
                var constructor = t.GetConstructor(new Type[0]);
                Assert.NotNull(constructor, $"引数を持たないコンストラクタが実装されていません: {t.Name}");
                var data = constructor.Invoke(new object[0]);
                byte[] bytes = MessagePackSerializer.Serialize(t, data);
                var deserialized = MessagePackSerializer.Deserialize(t, new ReadOnlyMemory<byte>(bytes));
                Assert.NotNull(deserialized);
            }
            
        }
        
        [Test]
        public void NestElementDataSerializationTestSimplePasses()
        {
            //AntData継承クラスのうち、非abstractなものを抽出
            var root = typeof(NestElementData);
            var unions = root.GetCustomAttributes(false).OfType<UnionAttribute>();
            var nestElementDataTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.DefinedTypes)
                .Where(t => t.IsSubclassOf(root) && !t.IsAbstract);
            foreach (var t in nestElementDataTypes)
            {
                if(!t.GetCustomAttributes(false).Any(a => a is MessagePackObjectAttribute)) Assert.Fail($"MessagePackObjectAttributeを付与していないNestElementDataがあります: {t.Name}");
                if (!unions.Any(u => u.SubType == t)) Assert.Fail($"Unionに登録されていないNestElementDataがあります: {t.Name}");
            }
            
            //シリアライズしてみる
            
            foreach (var t in nestElementDataTypes)
            {
                var constructor = t.GetConstructor(new Type[0]);
                Assert.NotNull(constructor, $"引数を持たないコンストラクタが実装されていません: {t.Name}");
                var data = constructor.Invoke(new object[0]);
                byte[] bytes = MessagePackSerializer.Serialize(t, data);
                var deserialized = MessagePackSerializer.Deserialize(t, new ReadOnlyMemory<byte>(bytes));
                Assert.NotNull(deserialized);
            }
        }
        
    }
}