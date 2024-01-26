using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCurrency : MonoBehaviour
{
    public CurrencyItemData item;

    private void Start()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.amount.ToString();
    }
}
