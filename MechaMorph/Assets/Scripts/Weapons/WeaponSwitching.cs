using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public GameObject[] weapons; 
    private int _currentWeaponIndex; 

    void Start()
    {
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

        Debug.Log("Switched to weapon: " + weapons[newWeaponIndex].name);
    }
}