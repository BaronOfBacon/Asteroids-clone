using Asteroids.MovableSystem;
using Core;
using UnityEngine;

namespace Asteroids.GameManager
{
    public class GameManagerFacade : Facade<GameManagerModel, GameManagerView>
    {
        private GameManagerPresenter _presenter;
        public GameManagerFacade(Presenter<GameManagerModel, GameManagerView> presenter) : base(presenter)
        {
            _presenter = (GameManagerPresenter) presenter;
        }

        public void StartGame(MovableSystemView movableSystemView, GameObject prefab)
        {
            _presenter.StartGame(movableSystemView, prefab);
        }
    }
}