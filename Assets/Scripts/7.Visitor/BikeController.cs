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

        //오토바이가 레이스 트랙의 파워업 아이템과 충돌했을 때 자동으로 호출되게
        //파워업 엔티티는 이 메서드를 통해 방문자 객체를 BikeController로 전달할 수 있다.
        public void Accept(IVisitor visitor)
        {
            foreach(IBikeElement elemet in _bikeElements)
            {
                elemet.Accept(visitor);
            }
        }
    }

}
