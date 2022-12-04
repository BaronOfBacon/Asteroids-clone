using System;

namespace Asteroids.Movable
{
    //This class was made instead of delegates because of delegates limitations.
    public class MovableEventsHolder
    {
        public event EventHandler<MovableFacade> MovableCreated;
        public event EventHandler<MovableFacade> MovableDestroyed;

        public void AddMovable(MovableFacade facade)
        {
            MovableCreated?.Invoke(null, facade);
        }

        public void DeleteMovable(MovableFacade facade)
        {
            MovableDestroyed?.Invoke(null, facade);
        }
    }
}
