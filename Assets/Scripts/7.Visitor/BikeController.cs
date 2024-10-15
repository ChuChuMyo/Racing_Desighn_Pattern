using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Visitor
{
    public class BikeController : MonoBehaviour, IBikeElement
    {
        private List<IBikeElement> _bikeElements = new List<IBikeElement>();

        void Start()
        {
            _bikeElements.Add(gameObject.AddComponent<BikeShield>());
            _bikeElements.Add(gameObject.AddComponent<BikeWeapon>());
            _bikeElements.Add(gameObject.AddComponent<BikeEngine>());

        }

        //������̰� ���̽� Ʈ���� �Ŀ��� �����۰� �浹���� �� �ڵ����� ȣ��ǰ�
        //�Ŀ��� ��ƼƼ�� �� �޼��带 ���� �湮�� ��ü�� BikeController�� ������ �� �ִ�.
        public void Accept(IVisitor visitor)
        {
            foreach(IBikeElement elemet in _bikeElements)
            {
                elemet.Accept(visitor);
            }
        }
    }

}
