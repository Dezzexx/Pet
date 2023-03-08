using Client;
using Leopotam.EcsLite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldownMB : MonoBehaviour
{
    public int amount, maxCooldown;
    private float currentCooldown;
    public bool reverseFill;
    [SerializeField] private Image fillImage;
    [SerializeField] private Text amountText;
    private Button button;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.interactable = amount > 0;
        amountText.text = amount.ToString();
        fillImage.fillAmount = currentCooldown;
    }

    public void StartCooldown()
    {
        button.interactable = false;
        amount--;
        amountText.text = amount.ToString();
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        while (currentCooldown <= maxCooldown)
        {
            currentCooldown += Time.deltaTime;
            fillImage.fillAmount = reverseFill? 1 - (currentCooldown / maxCooldown) : currentCooldown / maxCooldown;
            yield return new WaitForEndOfFrame();
        }
        EndCooldown();
        yield return null;
    }

    private void EndCooldown()
    {
        button.interactable = amount > 0;
        currentCooldown = 0;
        fillImage.fillAmount = 0;
    }
}
