using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chapter.Command
{
    public class Invoker : MonoBehaviour
    {
        private bool _isRecording;
        private bool _isReplaying;
        private float _replayTime;
        private float _recordingTime;
        //SortedList는 key의 오름차순으로 키-값 쌍을 저장
        private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>();

        public void ExecuteCommand(Command command)
        {
            command.Execute();

            if (_isRecording)
                _recordedCommands.Add(_recordingTime, command);

            Debug.Log("Recorded Time: " + _recordingTime);
            Debug.Log("Recorded Command: " + command);
        }

        public void Record()
        {
            _recordingTime = 0.0f;
            _isRecording = true;
        }

        public void Replay()
        {
            _replayTime = 0.0f;
            _isReplaying = true;

            if (_recordedCommands.Count <= 0)
                Debug.LogError("No commands to replay!");

            //데이터 정렬 : Reverse() 컬렉션에서 요소의 순서를 반대로 바꾼다.
            _recordedCommands.Reverse();
        }

        private void FixedUpdate()
        {
            if (_isRecording)
                _recordingTime += Time.fixedDeltaTime; //FixedUpdate라서 fixedDeltaTime
            if(_isReplaying)
            {
                //수량자 작업 : 시퀀스에서 조건을 충족하는 요소가 일부인지 전체인지를 나타내는 것을 참/거짓으로 반환합니다.
                _replayTime += Time.fixedDeltaTime;

                //수량자 작업 Any메서드 : 시퀀스의 임의의 요소가 조건을 만족하는 지를 확인
                if(_recordedCommands.Any())
                {
                    if(Mathf.Approximately(_replayTime, _recordedCommands.Keys[0])) 
                    {
                        Debug.Log("Replay Time: " + _replayTime);
                        Debug.Log("Replay Command: " + _recordedCommands.Values[0]);

                        _recordedCommands.Values[0].Execute();
                        _recordedCommands.RemoveAt(0);
                    }
                }
                else
                {
                    _isReplaying = false;
                }
            }
        }
    }
}
