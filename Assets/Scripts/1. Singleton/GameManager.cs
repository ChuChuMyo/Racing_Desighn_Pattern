using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chapter.Singleton
{

    //�� ������ �ڵ�� ������ Singleton���� ����
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
            //TimeSpan : Ư�� �ð��� ��Ÿ���� ���� �ƴϰ� '����, ��ð�, ���, ����'�� ���� �ð��� ũ�⸦ ��Ÿ����
            //TimeSpan�� �⺻���� 00:00:00 �̴�.
            TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
            //Subtract(TimeSpan) �� �ν��Ͻ��� ������ ������ �Ⱓ�� �� �� DateTime�� ��ȯ�մϴ�.
            //Subtract(DateTime) �� �Ž��Ͻ��� ������ ������ ��¥�� �ð��� �� �� TimeSpan�� ��ȯ�մϴ�.

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

