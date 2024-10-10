using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Chapter.EventBus
{
    public class RaceEventBus
    {
        private static readonly IDictionary<RaceEventType, UnityEvent> Events = new Dictionary<RaceEventType, UnityEvent>();

        //Ŭ���̾�Ʈ�� Ư�� �̺�Ʈ Ÿ���� �����ڷ� �ڽ��� �߰��Ϸ��� public static �޼����� Sbusscribe()�� ȣ���ؾ��Ѵ�.
        //Subscribe() �޼���� �� ���� �Ķ���� (�̺�Ʈ����, �ݹ� �Լ�)
        //UnityAction�� ��������Ʈ Ÿ���̹Ƿ� ���ڷ� �޼��带 �����Ѵ�.
        //Ư���� ���̽� �̺�Ʈ�� �����ڴ� Ŭ���̾�Ʈ ������Ʈ�� Publish()�޼��带 ȣ���� ��
        //����� �ݹ� �޼��嵵 ���ÿ� ȣ��� ���̴�.
        public static void Subscribe(RaceEventType eventType, UnityAction listener)
        {
            UnityEvent thisEvent;
            //TryGetValue �Լ��� �̿��� ù ��° ���ڷ� ���� eventType ��ȿ�� Ű ������ Ȯ��
            //out Ű���带 �̿��� thisEvent UnityEvent�� ��ȯ
            if(Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(eventType, thisEvent);
            }
        }

        //Unsubscribe() �޼���� Ư�� �̺�Ʈ�� �����ϴ� ������Ʈ�� �����Ѵ�.
        //������Ʈ�� �̺�Ʈ�� �Խ��� �� �̺�Ʈ ������ ������ �ݹ� �޼���� ȣ������ �ʴ´�.
        public static void Unsubscribe(RaceEventType type, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(type, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        //Ư���� ���̽� �̺�Ʈ�� �����ڴ� Ŭ���̾�Ʈ ������Ʈ�� Publis() �޼��带 ȣ���� ��
        //����� �ݹ� �޼��嵵 ���ÿ� ȣ���� �� �ִ�.
        public static void Publish(RaceEventType type)
        {
            UnityEvent thisEvent;

            if(Events.TryGetValue(type,out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}
