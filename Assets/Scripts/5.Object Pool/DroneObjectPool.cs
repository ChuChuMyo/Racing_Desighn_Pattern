using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Chapter.ObjectPool
{
    public class DroneObjectPool : MonoBehaviour
    {
        public int maxPoolSize = 10; //풀에 보관할 드론 인스턴스의 최대 개수
        //기본 스택 크기
        //드론 인스턴스를 저장하는데 사용할 스택 데이터 구조체와 관련된 속성
        public int statkDefaultCapacity = 10;

        //오브젝트 풀 초기화
        public IObjectPool<Drone> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<Drone>(CreatedPooledItem, 
                        OnTakeFromPool, 
                        OnReturnedToPool, 
                        OnDestroyPoolObejct, 
                        true, 
                        statkDefaultCapacity, 
                        maxPoolSize);
                return _pool;
            }
        }

        private IObjectPool<Drone> _pool;

        //ObjectPool<T> 생성자에서 선언한 콜백을 구현
        private Drone CreatedPooledItem() //드론 인스턴스를 초기화 한다.
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube); //스크립트로 기본 오브젝트 만들기

            Drone drone = go.AddComponent<Drone>();

            go.name = "Drone";
            drone.Pool = Pool;

            return drone;
        }

        private void OnReturnedToPool(Drone drone)
        {
            drone.gameObject.SetActive(false);
        }

        //클라이언트가 드론 인스턴스를 요청할 때 호출된다. 인스턴스가 실제로 반환되는 것이 아니라 게임 오브젝트가 활성화되는 것
        private void OnTakeFromPool(Drone drone)
        {
            drone.gameObject.SetActive(true);
        }

        //반드시 이해해야 하는 중요한 메서드
        //풀에 더 이상 공간이 없을 때 호출 된다
        //반환된 인스턴스는 메모리를 확보하기 위해 파괴된다.
        private void OnDestroyPoolObejct(Drone drone)
        {
            Destroy(drone.gameObject);
        }

        public void Spawn()
        {
            var amount = Random.Range(1, 10);

            for (int i = 0; i < amount; i++)
            {
                //풀에서 인스턴스를 가져온다.
                //풀이 비어있으면 새 인스턴스가 생성된다.
                var drone = Pool.Get();

                drone.transform.position = Random.insideUnitSphere * 10;
            }
        }
    }

}
