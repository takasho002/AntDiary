using UnityEngine;

namespace AntDiary
{

    public class AntDeath:MonoBehaviour
    {
        //アリprefabのantクラスをセット
        [SerializeField] private Ant ant = default;
        [SerializeField] private Animator animator = default;
        private NestSystem nestsystem => NestSystem.Instance;

        void Start()
        {
            ant = GetComponent<Ant>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            //AntDataからHPを読んで0以下か判定
            if (ant.Data.Health <=0 && ant.Data.IsAlive)
            {
                //HPが0以下ならステータスを死亡にしてセーブデータから削除し、アニメーション再生
                ant.Data.IsAlive = false;
                animator.SetTrigger("Death");
                if(ant.GetType()!=typeof(QueenAnt))nestsystem.RemoveAnt(ant);
            }
        }

        /// <summary>
        /// NestDataから自身のAntDataを削除&ゲームオブジェクト削除
        /// AnimationEventで死亡アニメ終了時に呼び出してほしい
        /// </summary>
        public void Despawn()
        {
            if (!ant.Data.IsAlive)
            {
                Destroy(this.gameObject);
            }
        }
    }
}