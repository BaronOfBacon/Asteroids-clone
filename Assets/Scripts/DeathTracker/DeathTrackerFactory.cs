using System;
using Core;
using UnityEngine;

namespace Asteroids.DeathTracker
{
    public class DeathTrackerFactory : AbstractObjectFactory<DeathTrackerFacade>
    {
        /// <summary>
        /// Creates DeathTracker entity.
        /// </summary>
        /// <param name="args">[0] GameObject (Game object/prefab), [1] bool (Is prefab?)</param>
        /// <returns></returns>
        public override DeathTrackerFacade Create(params object[] args)
        {
            var gameObject = (GameObject)args[0];
            var isPrefab = (bool)args[1];

            if (isPrefab) gameObject = GameObject.Instantiate(gameObject);

            var view = gameObject.GetComponentInChildren<DeathTrackerView>();
            var presenter = new DeathTrackerPresenter(null, view);

            return new DeathTrackerFacade(presenter);
        }
    }
}
