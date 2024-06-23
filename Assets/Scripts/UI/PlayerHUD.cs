using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [HideInInspector] public static PlayerHUD instance;

    [SerializeField] private GameObject staminaParent;
    [SerializeField] private Image staminaBar;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateStaminaBar(float stamina, float maxStamina)
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    public void ToggleStaminaBar(bool toggle) 
    {
        staminaParent.SetActive(toggle);
    }
}
