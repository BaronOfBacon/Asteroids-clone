using Core;

namespace Asteroids.Player
{
    public class PlayerFacade : Facade<PlayerModel, PlayerView>
    {
        public PlayerFacade(Presenter<PlayerModel, PlayerView> presenter) : base(presenter)
        {
        }
    }
}