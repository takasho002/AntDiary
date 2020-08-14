using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// AntDataから実際のGameObjectを作成するクラス。
    /// </summary>
    public abstract class AntFactory : MonoBehaviour
    {
        public abstract Type DataType { get; }
        
        /// <summary>
        /// AntDataをもとに、シーン上にAntのGameObjectを生成します。
        /// </summary>
        /// <param name="antData"></param>
        /// <returns></returns>
        public abstract Ant InstantiateAnt(AntData antData);
    }
    
    public abstract class AntFactory<T> : AntFactory where T : AntData
    {
        public sealed override Type DataType => typeof(T);

        public sealed override Ant InstantiateAnt(AntData antData)
        {
            if(antData is T antDataCast)
            {
                return InstantiateAnt(antDataCast);
            }
            else
            {
                throw new ArgumentException($"Type mismatch. Expected type is {typeof(T).Name} but {antData.GetType().Name} is specified.");
            }
        }

        /// <summary>
        /// AntDataをもとに、シーン上にAntのGameObjectを生成します。
        /// </summary>
        /// <param name="antData"></param>
        /// <returns>生成されたGameObjectのもつAntコンポーネント。</returns>
        public abstract Ant InstantiateAnt(T antData);
    }
}