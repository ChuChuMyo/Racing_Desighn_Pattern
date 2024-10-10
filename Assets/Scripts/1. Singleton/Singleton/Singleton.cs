using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Singleton
{
    //첫번째 단계에서는 Singleton 클래스를 구현한다.
    //싱글톤의 복잡성을 좀 더 쉽게 이해할 수 있도록 두개의 고유한 부분으로 나눈다.
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        //Get 접근자로 public static 속성을 구현했다.
        //이 접근자에서 새로운 인스턴스를 초기화하기 전에 다른 인스턴스가 없는지 확인한다.
        public static T Instace
        {
            get
            {
                if (_instance == null)
                {
                    //FindObjectOfType<T>()는 지정한 타입의 첫번째로 로드된 오브젝트를 검색
                    _instance = FindObjectOfType<T>();
                    //찾을 수 없다면 새로운 GameObject를 생성하고 이름을 바꾼 후 지정되지 않은 유형의 컴포넌트를 추가한다.
                    if(_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        //Singleton 클래스의 마지막 부분을 구현해보자.
        //파생 클래스에서 다시 정의할 수 있다는 의미인 virtual로 지정한 Awake()메서드,
        //Awake() 함수를 호출했을 때 싱글톤 컴포넌트는 메로리에 초기화된 자신의 인스턴스가 이미 있는지 확인
        //만약 인스턴스가 없다면 싱글톤 컴포넌트 자신이 현재 인스턴스가 된다.
        //이미 있다면 복제를 막기 위해 스스로를 제거한다.
        //따라서 씬에는 한번에 하나의 특정한 싱글턴 인스턴스가 있다.(두개를 추가하려고 하면 하나는 자동으로 제거)
        public virtual void Awake()
        {
            if(_instance == null)
            {
                _instance = this as T;

                //DontDestroyOnLoad() 유니티 API에 포함된 public static 메서드
                //대상 오브젝트가 제거되는 것을 막고 오브젝트의 현재 인스턴스가 씬 사이를 전환할 때도 유지되도록 한다.
                //이 API 기능은 애플리케이션(게임)이 존재하는 동안 오브젝트를 사용할 수 있다고 보장
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
