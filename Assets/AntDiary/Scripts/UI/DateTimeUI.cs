using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AntDiary
{
    public class DateTimeUI : MonoBehaviour
    {
        private TimeSystem TimeSystem => TimeSystem.Instance;

        [SerializeField] private Text monthAndSeason = default;
        [SerializeField] private Text timeData = default;
        [SerializeField] private Sprite springFrame = default;
        [SerializeField] private Sprite summerFrame = default;
        [SerializeField] private Sprite autumnFrame = default;
        [SerializeField] private Sprite winterFrame = default;

        private Image image;

        void Start()
        {
            image = gameObject.GetComponent<Image>();

            getSeasonSprite(TimeSystem.CurrentSeason);
            timeData.text = getCurrentTimeData(TimeSystem.CurrentTime);
        }

        void Update()
        {
            getSeasonSprite(TimeSystem.CurrentSeason);
            timeData.text = getCurrentTimeData(TimeSystem.CurrentTime);
        }

        private void getSeasonSprite(int seasonId)
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

        private string getCurrentTimeData(float currentTime)
        {
            int hour = (int)currentTime / 3600;
            int minute = (int)currentTime / 60;
            int second = (int)currentTime % 60;
            string time = hour.ToString("00") + ":";
            time += minute.ToString("00") + ":";
            time += second.ToString("00");

            return time;
        }
    }
}
