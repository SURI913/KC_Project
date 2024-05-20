using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCurrency : MonoBehaviour
{
    public CurrencyItemData item;

    private void Start()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = item.amount.ToString();
    }
}
