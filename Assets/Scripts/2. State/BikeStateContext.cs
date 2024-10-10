using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public interface IBikeState
    {
        void Handle(BikeController controller);
    }

    public enum Direction
    {
        Left = -1,
        Right = 1,
    }

    public class BikeStateContext
    {
        public IBikeState CurrentState { get; set; }

        private readonly BikeController _bikeController;

        public BikeStateContext(BikeController bikeController)
        {
            _bikeController = bikeController;
        }

        public void Transition()
        {
            CurrentState.Handle(_bikeController);
        }

        public void Transition(IBikeState state)
        {
            CurrentState = state;
            CurrentState.Handle(_bikeController);
        }
    }
}


