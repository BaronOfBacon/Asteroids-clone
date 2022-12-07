using System;
using Core;

namespace Asteroids.DeathTracker
{
    public class DeathTrackerFacade : Facade<DeathTrackerModel, DeathTrackerView>
    {
        public EventHandler Death;
        
        private DeathTrackerPresenter _presenter;
        
        public DeathTrackerFacade(Presenter<DeathTrackerModel, DeathTrackerView> presenter) : base(presenter)
        {
            _presenter = (DeathTrackerPresenter)presenter;
            _presenter.Death += HandleDeath;
        }

        ~DeathTrackerFacade()
        {
            _presenter.Death -= HandleDeath;
        }

        private void HandleDeath(object sender, EventArgs args)
        {
            Death?.Invoke(this, null);
        }
    }
}