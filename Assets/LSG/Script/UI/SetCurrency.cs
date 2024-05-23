using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCurrency : MonoBehaviour
{
    public CurrencyItemData item;

    private void Update()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = item.amount.ToString();
    }
}
