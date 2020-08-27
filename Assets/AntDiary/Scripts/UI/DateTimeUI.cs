using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AntDiary
{
    public class DateTimeUI : MonoBehaviour
    {
        private TimeSystem TimeSystem => TimeSystem.Instance;

        [SerializeField] private Text monthAndSeasonData = default;
        [SerializeField] private Text timeData = default;
        [SerializeField] private Sprite springFrame = default;
        [SerializeField] private Sprite summerFrame = default;
        [SerializeField] private Sprite autumnFrame = default;
        [SerializeField] private Sprite winterFrame = default;

        private Image image;

        void Start()
        {
            image = gameObject.GetComponent<Image>();

            GetSeasonSprite(TimeSystem.CurrentSeason);
            monthAndSeasonData.text = GetCurrentMonthAndSeasonData(TimeSystem.CurrentMonth, TimeSystem.CurrentSeason);
            timeData.text = GetCurrentTimeData(TimeSystem.CurrentTime);
        }

        void Update()
        {
            GetSeasonSprite(TimeSystem.CurrentSeason);
            monthAndSeasonData.text = GetCurrentMonthAndSeasonData(TimeSystem.CurrentMonth, TimeSystem.CurrentSeason);
            timeData.text = GetCurrentTimeData(TimeSystem.CurrentTime);
        }

        private void GetSeasonSprite(int seasonId)
        {
            switch(seasonId)
            {
                case 0:
                    image.sprite = springFrame;
                    break;
                case 1:
                    image.sprite = summerFrame;
                    break;
                case 2:
                    image.sprite = autumnFrame;
                    break;
                case 3:
                    image.sprite = winterFrame;
                    break;
                default:
                    break;
            }
        }

        private string GetCurrentMonthAndSeasonData(int currentMonth, int seasonId)
        {
            string text = currentMonth.ToString("00") + "月 / ";

            switch (seasonId)
            {
                case 0:
                    text += "春";
                    break;
                case 1:
                    text += "夏";
                    break;
                case 2:
                    text += "秋";
                    break;
                case 3:
                    text += "冬";
                    break;
                default:
                    text += "No Data";
                    break;
            }

            return text;
        }

        private string GetCurrentTimeData(float currentTime)
        {
            int hour = (int)currentTime / 3600;
            int minute = (int)currentTime / 60;
            int second = (int)currentTime % 60;
            string text = hour.ToString("00") + "時";
            text += minute.ToString("00") + "分";
            text += second.ToString("00") + "秒";

            return text;
        }
    }
}
