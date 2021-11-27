using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Settings;

    public void Back()
    {
        Menu.SetActive(true);
        Settings.SetActive(false);
    }
}
