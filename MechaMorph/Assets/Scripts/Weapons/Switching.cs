using UnityEngine;

namespace TrippleTrinity.MechaMorph.Weapons
{
    public class Switching : MonoBehaviour
    {
        [SerializeField] private int selectedWeapon;
        void Start()
        {
            SelecteWeapon();
        }

        void Update()
        {
            int previousSelectedWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                {
                    selectedWeapon = 0;
                }
                else
                {
                    selectedWeapon++;
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                {
                    selectedWeapon = transform.childCount - 1;
                }
                else
                {
                    selectedWeapon--;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                selectedWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            {
                selectedWeapon = 2;
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelecteWeapon();
            }
        }

        void SelecteWeapon()
        {
            int i = 0;
            foreach (Transform weapon in transform)
            {
                weapon.gameObject.SetActive(i == selectedWeapon);
                i++;
            }
        }
    }
}
