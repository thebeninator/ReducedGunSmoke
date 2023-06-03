using GHPC.Player;
using GHPC.Weapons;
using MelonLoader;
using UnityEngine;

namespace ReducedGunSmoke
{
    public class ReducedGunSmokeMod : MelonMod
    {
        GameObject mission;
        GameObject currentDustEmitter;
        WeaponSystem currentPlayerWeapon;
        int currentPlayerWeaponId;

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            currentPlayerWeapon = null;
            currentDustEmitter = null;
            currentPlayerWeaponId = 0;
            mission = GameObject.Find("_APP_GHPC_");
        }

        public override void OnUpdate()
        {
            if (mission == null) 
                return;

            if (mission.GetComponent<PlayerInput>() == null)
                return; 

            if (mission.GetComponent<PlayerInput>().CurrentPlayerWeapon == null)
                return;
            
            PlayerInput player = mission.GetComponent<PlayerInput>();
            WeaponSystem weapon = player.CurrentPlayerWeapon.Weapon;
            int weaponId = weapon.GetInstanceID();

            if (currentPlayerWeaponId == 0)
            {
                currentPlayerWeapon = weapon;
                currentPlayerWeaponId = weaponId;
            }

            if (currentPlayerWeaponId != weaponId) {
                if (currentDustEmitter != null)
                    currentDustEmitter.SetActive(true);

                currentDustEmitter = null;
                currentPlayerWeapon = null;
                currentPlayerWeaponId = 0;
            }

            try
            {
                ParticleSystem[] particles = currentPlayerWeapon.MuzzleEffects;
                currentDustEmitter = particles[0].gameObject.transform.Find("Particle System").gameObject;
                currentDustEmitter.SetActive(false);
            }
            catch {

            }
        }
    }   
}

