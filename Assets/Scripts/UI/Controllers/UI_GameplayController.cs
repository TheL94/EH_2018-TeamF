using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameplayController : MonoBehaviour {

    public Text AmmoText;

	public void UpdateAmmo(int _ammoValue)
    {
        AmmoText.text = "Ammo: " + _ammoValue;
    }

}
