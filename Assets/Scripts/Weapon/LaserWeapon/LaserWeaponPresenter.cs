using System;
using Core;
using UnityEngine;

namespace Asteroids.LaserWeapon
{
    public class LaserWeaponPresenter : Presenter<LaserWeaponModel, LaserWeaponView>
    {
        public LaserWeaponPresenter(LaserWeaponModel model, LaserWeaponView view) : base(model, view)
        {
            view.OnUpdate += Update;
        }
 
        ~LaserWeaponPresenter()
        {
            view.OnUpdate -= Update;
        }
        
        private void Update(object sender, EventArgs args)
        {
            if (model.chargesLeft < model.ChargesMaxCapacity)
            {
                if (model.chargeCoolDownTimeLeft <= 0)
                {
                    if (model.chargeCoolDownTimeLeft <= 0)
                        model.chargesLeft++;

                    model.chargeCoolDownTimeLeft = model.ChargeCoolDownDuration;
                }
                else
                {
                    model.chargeCoolDownTimeLeft -= Time.deltaTime;
                }
            }

            if (!model.isBusy) return;

            if (model.activeTimeLeft <= 0)
            {
                Switch(false);
                model.isBusy = false;
                //TODO check if ready
                return;
            }
            
            if (model.FieldCalculationHelper.GetForwardIntersectionPosition(out var endPosition,
                view.transform.position, view.transform.up))
            {
                var startPosition = view.transform.position;
                view.SetLaser(startPosition, endPosition);
            }

            model.activeTimeLeft -= Time.deltaTime;
        }

        public void TryShoot()
        {
            if (!model.IsReady) return;
            
            Switch(true);
            model.activeTimeLeft = model.ActiveTimeDuration;
            model.chargesLeft--;
        }

        public void Switch(bool state)
        {
            model.isBusy = true;
            view.Switch(state);
        }
    }
}
