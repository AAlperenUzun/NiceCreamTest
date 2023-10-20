using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StandUpgrade : MonoBehaviour
{
    [SerializeField]private TMP_Text levelText;
    [SerializeField]private TMP_Text priceText;
    [SerializeField]private TMP_Text cdText;
    [SerializeField]private TMP_Text upgradePriceText;
    [SerializeField]private TMP_Text nameText;
    [SerializeField] private Image fillbarUI;
    public List<int> goalLevels;
    
    
    public void SetStandUpgrade(int level, int price, float cd, int upgradePrice, string name)
    {
        levelText.text = "Level " + level;
        priceText.text =NumberFormatter.FormatNumber(price);
        cdText.text =cd.ToString("0.00")+ " s";
        upgradePriceText.text =NumberFormatter.FormatNumber(upgradePrice);
        nameText.text =""+ name;
        var x = 0;
        foreach (var goalLevel in goalLevels)
        {
            if (level>goalLevel)
            {
                x++;
            }
            else
            {
                break;
            }
        }
        fillbarUI.fillAmount = (level-(float)goalLevels[x-1]) / ((float)goalLevels[x]-(float)goalLevels[x-1]);
    }
}
