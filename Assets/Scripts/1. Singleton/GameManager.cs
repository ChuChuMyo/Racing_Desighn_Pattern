using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton
{

    //단 한줄의 코드로 간단히 Singleton으로 변경
    public class GameManager : Singleton<GameManager>
    {
        private DateTime _sessionStartTime;
        private DateTime _sessionEndTime;

        private void Start()
        {
            _sessionStartTime = DateTime.Now;
            Debug.Log("Game session start @ : " + DateTime.Now);
        }

        private void OnApplicationQuit()
        {
            _sessionEndTime = DateTime.Now;
            //TimeSpan : 특정 시간을 나타내는 것이 아니고 '몇일, 몇시간, 몇분, 몇초'와 같은 시간의 크기를 나타내며
            //TimeSpan의 기본값은 00:00:00 이다.
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
            //Subtract(TimeSpan) 이 인스턴스의 값에서 지정된 기간을 뺀 새 DateTime을 반환합니다.
            //Subtract(DateTime) 이 신스턴스의 값에서 지정된 날짜와 시간을 뺀 새 TimeSpan을 반환합니다.

            Debug.Log("Game session ended @: " + DateTime.Now);
            Debug.Log("Game session lasted: " + timeDifference);
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Next Scene"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}

