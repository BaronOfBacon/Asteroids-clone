using System;
using Asteroids.Helpers;
using Core;

namespace Asteroids.LaserWeapon
{
    public class LaserWeaponFactory : AbstractObjectFactory<LaserWeaponFacade>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns>[0] LaserWeaponView, [1] FieldCalculationHelper, [2] float (Active time duration),
        /// [3] int (charges capacity), [4] float (Charge cool down)</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override LaserWeaponFacade Create(params object[] args)
        {
            var view = (LaserWeaponView)args[0];
            var model = new LaserWeaponModel((FieldCalculationHelper)args[1], (float)args[2], 
                (int)args[3], (float)args[4]);
            var presenter = new LaserWeaponPresenter(model, view);
            return new LaserWeaponFacade(presenter);
        }
    }
}
