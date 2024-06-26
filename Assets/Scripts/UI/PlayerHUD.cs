using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Theos.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [HideInInspector] public static PlayerHUD instance;

    private Player _player;

    [SerializeField] private GameObject staminaParent;
    [SerializeField] private Image staminaBar;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private float itemNameHideAfter = 2f;
    private Coroutine hideTextCoroutine;
    [SerializeField] private TextMeshProUGUI pickupHint;
    [SerializeField] private Image manaImage;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        itemName.color = new Color(itemName.color.r, itemName.color.g, itemName.color.b, 0f);
    }

    public void Init(Player player)
    {
        _player = player;
        StartCoroutine(RechargeMana());
    }

    private IEnumerator RechargeMana()
    {
        while (true)
        {
            _player.stats.CurrentMana += (_player.stats.MaxMana * (_player.stats.ManaRegeneration / 100f)) * Time.deltaTime;
            
            if (_player.stats.CurrentMana > _player.stats.MaxMana) _player.stats.CurrentMana = _player.stats.MaxMana;
            else if (_player.stats.CurrentMana < 0) _player.stats.CurrentMana = 0;
            yield return null;
        }
    }

    private void Update()
    {
        if (_player == null) return;
        UpdateManaImage(_player.stats.CurrentMana, _player.stats.MaxMana);
    }


    public void UpdateStaminaBar(float stamina, float maxStamina)
    {
        staminaBar.fillAmount = stamina / maxStamina;
    }

    private void UpdateManaImage(float mana, float maxMana)
    {
        manaImage.fillAmount = mana / maxMana;
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

    public void TogglePickupHint(bool toggle) 
    {
        pickupHint.enabled = toggle;
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
