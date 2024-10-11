using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Chapter.ObjectPool
{
    public class DroneObjectPool : MonoBehaviour
    {
        public int maxPoolSize = 10; //Ǯ�� ������ ��� �ν��Ͻ��� �ִ� ����
        //�⺻ ���� ũ��
        //��� �ν��Ͻ��� �����ϴµ� ����� ���� ������ ����ü�� ���õ� �Ӽ�
        public int statkDefaultCapacity = 10;

        //������Ʈ Ǯ �ʱ�ȭ
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

        //ObjectPool<T> �����ڿ��� ������ �ݹ��� ����
        private Drone CreatedPooledItem() //��� �ν��Ͻ��� �ʱ�ȭ �Ѵ�.
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube); //��ũ��Ʈ�� �⺻ ������Ʈ �����

            Drone drone = go.AddComponent<Drone>();

            go.name = "Drone";
            drone.Pool = Pool;

            return drone;
        }

        private void OnReturnedToPool(Drone drone)
        {
            drone.gameObject.SetActive(false);
        }

        //Ŭ���̾�Ʈ�� ��� �ν��Ͻ��� ��û�� �� ȣ��ȴ�. �ν��Ͻ��� ������ ��ȯ�Ǵ� ���� �ƴ϶� ���� ������Ʈ�� Ȱ��ȭ�Ǵ� ��
        private void OnTakeFromPool(Drone drone)
        {
            drone.gameObject.SetActive(true);
        }

        //�ݵ�� �����ؾ� �ϴ� �߿��� �޼���
        //Ǯ�� �� �̻� ������ ���� �� ȣ�� �ȴ�
        //��ȯ�� �ν��Ͻ��� �޸𸮸� Ȯ���ϱ� ���� �ı��ȴ�.
        private void OnDestroyPoolObejct(Drone drone)
        {
            Destroy(drone.gameObject);
        }

        public void Spawn()
        {
            var amount = Random.Range(1, 10);

            for (int i = 0; i < amount; i++)
            {
                //Ǯ���� �ν��Ͻ��� �����´�.
                //Ǯ�� ��������� �� �ν��Ͻ��� �����ȴ�.
                var drone = Pool.Get();

                drone.transform.position = Random.insideUnitSphere * 10;
            }
        }
    }

}
