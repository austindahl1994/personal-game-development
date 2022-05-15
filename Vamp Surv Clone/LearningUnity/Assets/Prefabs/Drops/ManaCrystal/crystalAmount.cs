using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class crystalAmount : MonoBehaviour
{
    public TMP_Text text;
    public InventoryStats inventory;

    private void Start()
    {
        inventory.manaCrystal = 0; //remove, just for testing
    }

    private void Update()
    {
        text.text = inventory.manaCrystal.ToString();
    }

}
