using Core;

namespace Asteroids.Movable
{
    public class MovablePresenter : Presenter<MovableModel, MovableView>
    {
        public MovablePresenter(MovableModel model, MovableView view) : base(model, view)
        {
            
        }
    }
}
