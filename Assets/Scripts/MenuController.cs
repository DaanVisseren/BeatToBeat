using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject songMenu;
    public KeyCode MenuButtonP;

    void Update()
    {
        if (Input.GetKeyDown(MenuButtonP))
        {
            songMenu.SetActive(!songMenu.activeSelf);
        }
    }
}