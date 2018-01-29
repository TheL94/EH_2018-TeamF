using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCratesController : MonoBehaviour {

    AmmoCrate[] Crates;

	public void Init()
    {
        Crates = GetComponentsInChildren<AmmoCrate>();
    }

    public void ReinitAmmoCrates()
    {
        for (int i = 0; i < Crates.Length; i++)
        {
            Crates[i].gameObject.SetActive(true);
        }
    }
}
