using Core;

namespace Asteroids.Movable
{
    public class MovableFacade : Facade<MovableModel, MovableView>
    {
        public MovableFacade(Presenter<MovableModel, MovableView> presenter) : base(presenter)
        {
        }
    }
}
