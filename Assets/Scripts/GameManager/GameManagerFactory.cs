using Core;

namespace Asteroids.GameManager
{
    public class GameManagerFactory : AbstractObjectFactory<GameManagerFacade>
    {
        public override GameManagerFacade Create(params object[] args)
        {
            //TODO Don't forget about game settings in args
            var model = new GameManagerModel();
            var view = (GameManagerView)args[0];
            var presenter = new GameManagerPresenter(model, view);
            return new GameManagerFacade(presenter);
        }
    }
}
