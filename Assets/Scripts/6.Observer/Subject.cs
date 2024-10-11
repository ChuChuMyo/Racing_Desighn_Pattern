using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Observer
{
    public abstract class Subject : MonoBehaviour
    {
        private readonly ArrayList _observers = new ArrayList();

        protected void Attach(Observer obesrver)
        {
            _observers.Add(obesrver);
        }

        protected void Detach(Observer observer)
        {
            _observers.Add(observer);
        }

        protected void NotifyObservers()
        {
            foreach (Observer observer in _observers)
            {
                observer.Notify(this);
            }
        }
    }
}
