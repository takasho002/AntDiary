using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace AntDiary
{
    public class FoodGameOverSystem : MonoBehaviour
    {
        private float timeleft;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0f)
            {
                if (NestSystem.Instance.Data.StoredFood < -50)
                {
                    SceneManager.LoadScene("Sayoshi_GameOver");
                }
                timeleft = 10.0f;
            }
        }
    }
}