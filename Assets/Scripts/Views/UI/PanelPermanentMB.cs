using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPermanentMB : MonoBehaviour
{
    [SerializeField] private MainCanvasMB mainCanvasMB;
    public Text MoneyAmount;

    public void UpdateMoneyPanel()
    {
        MoneyAmount.text = mainCanvasMB.State.PlayerResourceValue.ToString();
    }
}
