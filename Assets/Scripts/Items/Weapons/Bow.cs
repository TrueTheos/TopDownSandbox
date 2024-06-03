using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons
{
    public class Bow : Weapon
    {
        protected BowStatistics _bowStats => _weaponStats as BowStatistics;

        private float _dmg;
        private float _currentTension;

        public bool isPulling;

        public void Pull()
        {
            isPulling = true;

            _currentTension += Time.deltaTime;

            if(_currentTension > _bowStats.maxTensionTime)
            {
                _currentTension = _bowStats.maxTensionTime;
            }
        }

        public void Shoot()
        {
            isPulling = false;

            if(_currentTension < _bowStats.maxTensionTime * .1f)
            {
                _currentTension = 0;
                return;
            }

            _dmg = _bowStats.GetDamage() * (_bowStats.maxTensionDamageMultiplier * (_currentTension / _bowStats.maxTensionTime));

            WeaponProjectile projectile = Instantiate(_bowStats.projectile).GetComponent<WeaponProjectile>();
            _currentTension = 0;        
        }
    }
}
