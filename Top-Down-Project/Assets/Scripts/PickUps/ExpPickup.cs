using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : Pickup
{
    [SerializeField] int amount;
    [SerializeField] TextMesh amountText;

    private void Start()
    {
        if (amount == 0)
        {
            amount = 25;
        }

        if (amountText == null)
        {
            amountText = GetComponent<TextMesh>();
        }
        amountText.text = "+" + amount.ToString();
    }

    protected override void OnPickUp(Player player)
    {
        GameManager.Instance.UpdateScore(amount);
        base.OnPickUp(player);
    }
}
