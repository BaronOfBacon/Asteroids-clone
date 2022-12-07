using System;
using Core;

namespace Asteroids.DeathTracker
{
    public class DeathTrackerPresenter : Presenter<DeathTrackerModel, DeathTrackerView>
    {
        public EventHandler Death;
        
        public DeathTrackerPresenter(DeathTrackerModel model, DeathTrackerView view) : base(model, view)
        {
            view.Death += HandleDeath;
        }

        ~DeathTrackerPresenter()
        {
            view.Death -= HandleDeath;
        }

        private void HandleDeath(object sender, EventArgs args)
        {
            Death?.Invoke(this, null);
        }
    }
}
