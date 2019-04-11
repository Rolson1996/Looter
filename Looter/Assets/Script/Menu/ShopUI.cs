using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System;

public class ShopUI : MonoBehaviour
{
    public GameObject CoinsGainedText;

    public GameObject BackpackLevelLabel;
    public GameObject ShoesLevelLabel;

    private Text BackpackLevelText;
    private Text ShoesLevelText;

    public GameObject BackpackCurrentEffectLabel;
    public GameObject ShoesCurrentEffectLabel;

    private Text BackpackCurrentEffectText;
    private Text ShoesCurrentEffectText;

    public GameObject BackpackNextEffectLabel;
    public GameObject ShoesNextEffectLabel;

    private Text BackpackNextEffectText;
    private Text ShoesNextEffectText;

    public GameObject BackpackCostLabel;
    public GameObject ShoesCostLabel;

    private Text BackpackCostText;
    private Text ShoesCostText;

    private int[] BackpackUpgradeCosts;
    private int[] ShoesUpgradeCosts;

    private int[] BackpackUpgradeEffects;
    private float[] ShoesUpgradeEffects;

    // Use this for initialization
    void Start()
    {

        BackpackLevelText = BackpackLevelLabel.GetComponent<Text>();
        BackpackCurrentEffectText = BackpackCurrentEffectLabel.GetComponent<Text>();
        BackpackNextEffectText = BackpackNextEffectLabel.GetComponent<Text>();
        BackpackCostText = BackpackCostLabel.GetComponent<Text>();



        ReadUpgradeDataFromXML();

        BackpackLevelText.text = "Level " + DataAndAchievementManager.instance.upgrades.BackpackLevel.ToString();
        BackpackCurrentEffectText.text = "Current Effect   " + DataAndAchievementManager.instance.upgrades.CurrentBackpackEffect.ToString() + " Bag Spaces";

        if (DataAndAchievementManager.instance.upgrades.BackpackLevel < BackpackUpgradeCosts.Length)
        {
            BackpackNextEffectText.text = "Next Effect   " + BackpackUpgradeEffects[DataAndAchievementManager.instance.upgrades.BackpackLevel] + " Bag Spaces";
            BackpackCostText.text = BackpackUpgradeCosts[DataAndAchievementManager.instance.upgrades.BackpackLevel] + " Cash";
        }
        else
        {
            BackpackNextEffectText.text = "";
            BackpackCostText.text = "Max Level";
        }
    }

    public void UpdateCashNumber()
    {
        CoinsGainedText.GetComponent<Text>().text = DataAndAchievementManager.instance.GetCurrentCash().ToString();
    }


    public void LevelUpBackpack()
    {
        int upgradeCost = BackpackUpgradeCosts[DataAndAchievementManager.instance.upgrades.BackpackLevel];
        if(DataAndAchievementManager.instance.GetCurrentCash() >= upgradeCost)
        {
            DataAndAchievementManager.instance.upgrades.BackpackLevel++;
            DataAndAchievementManager.instance.upgrades.CurrentBackpackEffect = BackpackUpgradeEffects[DataAndAchievementManager.instance.upgrades.BackpackLevel - 1];
            DataAndAchievementManager.instance.SpendCash(upgradeCost);
            UpdateCashNumber();

            DataAndAchievementManager.instance.SaveDataToFile();

            BackpackLevelText.text = "Level " + DataAndAchievementManager.instance.upgrades.BackpackLevel.ToString();
            BackpackCurrentEffectText.text = "Current Effect   " + DataAndAchievementManager.instance.upgrades.CurrentBackpackEffect.ToString() + " Bag Spaces";

            if (DataAndAchievementManager.instance.upgrades.BackpackLevel < BackpackUpgradeCosts.Length)
            {
                BackpackNextEffectText.text = "Next Effect   " + BackpackUpgradeEffects[DataAndAchievementManager.instance.upgrades.BackpackLevel] + " Bag Spaces";
                BackpackCostText.text = BackpackUpgradeCosts[DataAndAchievementManager.instance.upgrades.BackpackLevel] + " Cash";
            }
            else
            {
                BackpackNextEffectText.text = "";
                BackpackCostText.text = "Max Level";
            }
        }

        //increase level of upgrade by 1
        //Save new effect
        //deduct amount of cash
        //change the text in the shop


        //else
        //display error message
    }

    public void ReadUpgradeDataFromXML()
    {
        XmlDocument xmlUpgrades = new XmlDocument();
        xmlUpgrades.Load(Application.streamingAssetsPath + "/UpgradeEffectsAndCosts.xml");
        foreach(XmlElement upgradeTypeNode in xmlUpgrades.DocumentElement)
        {
            int upgradeCount = Convert.ToInt32(upgradeTypeNode.GetAttribute("UpgradeCount"));

            if(upgradeTypeNode.Name == "Backpack")
            {
                BackpackUpgradeCosts = new int[upgradeCount];
                BackpackUpgradeEffects = new int[upgradeCount];

                foreach (XmlNode levelNode in upgradeTypeNode.ChildNodes)
                {
                    int upgradeLevel = Convert.ToInt32(levelNode.Attributes[0].Value);

                    foreach (XmlNode levelInfo in levelNode.ChildNodes)
                    {
                        if (levelInfo.Name == "Cost")
                        {
                            int levelCost = Convert.ToInt32(levelInfo.InnerText);
                            BackpackUpgradeCosts[upgradeLevel - 1] = levelCost;
                        }
                        else if (levelInfo.Name == "Effect")
                        {
                            int levelEffect = Convert.ToInt32(levelInfo.InnerText);
                            BackpackUpgradeEffects[upgradeLevel - 1] = levelEffect;
                        }
                    }
                }
            }
            else if(upgradeTypeNode.Name == "Shoes")
            {
                ShoesUpgradeCosts = new int[upgradeCount];
                ShoesUpgradeEffects = new float[upgradeCount];

                foreach (XmlNode levelNode in upgradeTypeNode.ChildNodes)
                {
                    int upgradeLevel = Convert.ToInt32(levelNode.Attributes[0].Value);

                    foreach (XmlNode levelInfo in levelNode.ChildNodes)
                    {
                        if (levelInfo.Name == "Cost")
                        {
                            int levelCost = Convert.ToInt32(levelInfo.InnerText);
                            ShoesUpgradeCosts[upgradeLevel - 1] = levelCost;
                        }
                        else if (levelInfo.Name == "Effect")
                        {
                            float levelEffect = (float)Convert.ToDouble(levelInfo.InnerText);
                            ShoesUpgradeEffects[upgradeLevel - 1] = levelEffect;
                        }
                    }
                }
            }

            
        }


        
    }


}
