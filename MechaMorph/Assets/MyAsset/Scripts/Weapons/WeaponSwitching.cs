using TrippleTrinity.MechaMorph.MyAsset.Scripts.Ui;
using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class WeaponSwitching : MonoBehaviour
    {
        public GameObject[] weapons; 
        private int _currentWeaponIndex;
        private GunUIManager _gunUIManager;

        void Start()
        {
            _gunUIManager = GunUIManager.Instance;
            SwitchWeapon(_currentWeaponIndex);
        }

        void Update()
        {
            HandleWeaponSwitchInput();
        }

        void HandleWeaponSwitchInput()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                _currentWeaponIndex = (_currentWeaponIndex + 1) % weapons.Length;
                SwitchWeapon(_currentWeaponIndex);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                _currentWeaponIndex = (_currentWeaponIndex - 1 + weapons.Length) % weapons.Length;
                SwitchWeapon(_currentWeaponIndex);
            }

            for (int i = 0; i < weapons.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    _currentWeaponIndex = i;
                    SwitchWeapon(_currentWeaponIndex);
                }
            }
        }

        void SwitchWeapon(int newWeaponIndex)
        {
            foreach (var i in weapons)
            {
                i.SetActive(false);
            }

            weapons[newWeaponIndex].SetActive(true);

            _gunUIManager?.UpdateGunName(weapons[newWeaponIndex].name);
        }
    }
}