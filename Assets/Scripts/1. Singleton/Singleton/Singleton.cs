using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Singleton
{
    //ù��° �ܰ迡���� Singleton Ŭ������ �����Ѵ�.
    //�̱����� ���⼺�� �� �� ���� ������ �� �ֵ��� �ΰ��� ������ �κ����� ������.
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        //Get �����ڷ� public static �Ӽ��� �����ߴ�.
        //�� �����ڿ��� ���ο� �ν��Ͻ��� �ʱ�ȭ�ϱ� ���� �ٸ� �ν��Ͻ��� ������ Ȯ���Ѵ�.
        public static T Instace
        {
            get
            {
                if (_instance == null)
                {
                    //FindObjectOfType<T>()�� ������ Ÿ���� ù��°�� �ε�� ������Ʈ�� �˻�
                    _instance = FindObjectOfType<T>();
                    //ã�� �� ���ٸ� ���ο� GameObject�� �����ϰ� �̸��� �ٲ� �� �������� ���� ������ ������Ʈ�� �߰��Ѵ�.
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

        //Singleton Ŭ������ ������ �κ��� �����غ���.
        //�Ļ� Ŭ�������� �ٽ� ������ �� �ִٴ� �ǹ��� virtual�� ������ Awake()�޼���,
        //Awake() �Լ��� ȣ������ �� �̱��� ������Ʈ�� �޷θ��� �ʱ�ȭ�� �ڽ��� �ν��Ͻ��� �̹� �ִ��� Ȯ��
        //���� �ν��Ͻ��� ���ٸ� �̱��� ������Ʈ �ڽ��� ���� �ν��Ͻ��� �ȴ�.
        //�̹� �ִٸ� ������ ���� ���� �����θ� �����Ѵ�.
        //���� ������ �ѹ��� �ϳ��� Ư���� �̱��� �ν��Ͻ��� �ִ�.(�ΰ��� �߰��Ϸ��� �ϸ� �ϳ��� �ڵ����� ����)
        public virtual void Awake()
        {
            if(_instance == null)
            {
                _instance = this as T;

                //DontDestroyOnLoad() ����Ƽ API�� ���Ե� public static �޼���
                //��� ������Ʈ�� ���ŵǴ� ���� ���� ������Ʈ�� ���� �ν��Ͻ��� �� ���̸� ��ȯ�� ���� �����ǵ��� �Ѵ�.
                //�� API ����� ���ø����̼�(����)�� �����ϴ� ���� ������Ʈ�� ����� �� �ִٰ� ����
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
