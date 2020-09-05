using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AntDiary
{
    /// <summary>
    /// 建築をサポートするクラス。NestSystemのラッパーともいえる
    /// スナップ、自動接続、NestSystemへの登録など。
    /// BuildingSystem.Instanceでアクセス可能
    /// </summary>
    public class BuildingSystem
    {
        public static BuildingSystem Instance => NestSystem.Instance.BuildingSystem;

        private NestSystem Host { get; }

        public BuildingSystem(NestSystem host)
        {
            Host = host;
        }

        /// <summary>
        /// 指定したNestElementに関して、付近のノードにスナップした後の座標を取得する
        /// </summary>
        /// <param name="target">スナップさせるNestElement</param>
        /// <param name="thresholdDistance">スナップの基準となる</param>
        /// <returns></returns>
        public Vector2 GetSnappedPosition(NestElement target, float thresholdDistance = 0.2f)
        {
            GetSnappableNode(target, out NestPathNode originNode, out NestPathNode targetNode, out float distance);

            if (targetNode == null) return target.transform.position;
            if (distance > thresholdDistance) return target.transform.position;

            return (Vector2) target.transform.position +
                   (targetNode.WorldPosition - originNode.WorldPosition);
        }

        /// <summary>
        /// 自動スナップを行った際に吸引されるノードを取得する。 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="originNode"></param>
        /// <param name="targetNode"></param>
        /// <param name="distance"></param>
        public void GetSnappableNode(NestElement target, out NestPathNode originNode, out NestPathNode targetNode,
            out float distance)
        {
            var nodes = Host.NestElements.Where(e => e != target).SelectMany(e => e.GetNodes()).Where(n => n.IsExposed);

            NestPathNode argMinDistanceOrigin = null;
            NestPathNode argMinDistance = null;
            float minSqrDistance = float.MaxValue;

            foreach (var n in target.GetNodes().Where(n => n.IsExposed))
            {
                var connectables = nodes.Where(other =>
                    other.IsExposed && other.IsConnectable(n) && n.IsConnectable(other));
                foreach (var other in connectables)
                {
                    float sqrDistance = (other.WorldPosition - n.WorldPosition).sqrMagnitude;
                    if (sqrDistance < minSqrDistance)
                    {
                        argMinDistanceOrigin = n;
                        minSqrDistance = sqrDistance;
                        argMinDistance = other;
                    }
                }
            }

            originNode = argMinDistanceOrigin;
            targetNode = argMinDistance;
            distance = Mathf.Sqrt(minSqrDistance);
        }

        /// <summary>
        /// 指定したNestElementをNestSystemに登録し、付近のNestElementのノードと自動的に接続する。
        /// 自動接続は現在の建築システムの仕様に基づき、一か所だけで行われます。（変更される可能性あり）
        /// </summary>
        /// <param name="target"></param>
        /// <param name="needToBeConnected">スナップによってほかのNestElementに接続できない場合、設置不能として判定する。</param>
        /// <returns>配置が成功したかどうか。</returns>
        public bool PlaceElementWithAutoConnect(NestElement target, bool needToBeConnected = true,
            float autoConnectThresholdDistance = 0.01f)
        {
            GetSnappableNode(target, out NestPathNode originNode, out NestPathNode targetNode, out float distance);

            if (distance <= autoConnectThresholdDistance)
            {
                //自動接続を行う
                if (!IsPlaceable(target, targetNode.Host)) return false;
                Host.ConnectElements(originNode, targetNode);
            }
            else
            {
                if (needToBeConnected) return false;
                if (!IsPlaceable(target)) return false;
            }

            Host.RegisterNestElementToGameContext(target);

            return true;
        }

        private Collider2D[] overlapResult = new Collider2D[2];

        /// <summary>
        /// NestElementが現在の位置に設置できるかどうかを取得する。
        /// 【注意】NestElementの位置を更新した直後、同一フレーム内で呼び出すと、移動前の位置で重複判定が行われるようです。結果正しい結果が得られないことがあります。
        /// </summary>
        /// <param name="target">設置するNestElement</param>
        /// <param name="ignore">スナップ対象のNestElement。重複していても無視される。</param>
        /// <returns></returns>
        public bool IsPlaceable(NestElement target, NestElement ignore = null)
        {
            var cf = new ContactFilter2D()
            {
                useLayerMask = true,
                layerMask = LayerMask.GetMask("NestElement"),
            };
            int res = target.GetBlockingShape().OverlapCollider(cf, overlapResult);

            //スナップ対象のNestElementとの重複を無視します。グリッド上でしか接続を行わない現状ならこういう施策が可能ですが、そうでない場合は不正な形状の巣を形成する可能性があります
            if (ignore != null && res == 1 && overlapResult[0] == ignore.GetBlockingShape()) return true;

            return res == 0;
        }
    }
}