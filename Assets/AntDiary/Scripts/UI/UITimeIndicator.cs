using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace AntDiary
{
    public class UITimeIndicator : MonoBehaviour
    {
        private TimeSystem TimeSystem => TimeSystem.Instance;

        [SerializeField] private Text monthNameText = default;
        [SerializeField] private Text remainMinutesText = default;
        [SerializeField] private string remainMinutesTextFormat = "{0}: 残り{1}分";
        [SerializeField] private string[] seasonNames = new string[4];
        [SerializeField] private string[] monthNames = new string[12];


        // Start is called before the first frame update
        void Start()
        {
            TimeSystem.OnMonthUpdate.Subscribe(month => { monthNameText.text = monthNames[month]; });
            TimeSystem.OnSecondUpdate.Subscribe(second =>
            {
                float remainTime = (Mathf.FloorToInt(TimeSystem.CurrentTime / TimeSystem.SeasonInterval) + 1) *
                                   TimeSystem.SeasonInterval - TimeSystem.CurrentTime;
                int remainMinutes = Mathf.CeilToInt(remainTime / 60);
                remainMinutesText.text = string.Format(remainMinutesTextFormat, seasonNames[TimeSystem.CurrentSeason],
                    remainMinutes);
            });
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}