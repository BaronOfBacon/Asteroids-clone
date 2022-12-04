using Asteroids.Movable;
using Core;

namespace Asteroids.MovableSystem
{
    public class MovableSystemFacade : Facade<MovableSystemModel, MovableSystemView>
    {
        private new MovableSystemPresenter _presenter;
        
        public MovableSystemFacade(Presenter<MovableSystemModel, MovableSystemView> presenter) : base(presenter)
        {
            _presenter = (MovableSystemPresenter)presenter;
        }

        public void AddMovable(object sender, MovableFacade movable)
        {
            _presenter.AddMovable(movable);
        }

        public void DeleteMovable(object sender, MovableFacade movable)
        {
            _presenter.DeleteMovable(movable);
        }
        
    }
}