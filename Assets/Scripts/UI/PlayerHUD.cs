using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [HideInInspector] public static PlayerHUD instance;

    [SerializeField] private GameObject staminaParent;
    [SerializeField] private Image staminaBar;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private float itemNameHideAfter = 2f;
    private Coroutine hideTextCoroutine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemName.color = new Color(itemName.color.r, itemName.color.g, itemName.color.b, 0f);
    }

    public void UpdateStaminaBar(float stamina, float maxStamina)
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    public void ToggleStaminaBar(bool toggle) 
    {
        staminaParent.SetActive(toggle);
    }

    public void ChangeItemName(Item item)
    {
        itemName.text = item.name;
        itemName.color = new Color(itemName.color.r, itemName.color.g, itemName.color.b, 1f);
        if(hideTextCoroutine != null)
        {
            StopCoroutine(hideTextCoroutine);
            hideTextCoroutine = null;
        }

        hideTextCoroutine = StartCoroutine(HideItemName());
    }

    private IEnumerator HideItemName()
    {
        yield return new WaitForSeconds(itemNameHideAfter);

        float elapsedTime = 0f;
        float fadeDuration = 1f;
        Color originalColor = itemName.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            itemName.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        itemName.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        hideTextCoroutine = null;
    }
}
