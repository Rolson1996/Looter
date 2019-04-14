using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardUI : MonoBehaviour
{
    public GameObject DailyRewardPanel;
    public GameObject CashGainedLabel;

    //public GameObject TotalStreakLabel;
    //public GameObject StreakProgressLabel;

    private Text CashGainedText;

    //private Text TotalStreakText;
    //private Text StreakProgressText;

    private LastLoginInData lastLoginData;
    public Material dimOutMaterial;

    public GameObject[] DayImages;
    public void AppLoads()
    {
        CashGainedText = CashGainedLabel.GetComponent<Text>();
        //TotalStreakText = TotalStreakLabel.GetComponent<Text>();
        //StreakProgressText = StreakProgressText.GetComponent<Text>();
    
        DateTime thisLogin = DateTime.Now;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileLogin = null;

        try
        {
            fileLogin = File.Open(Application.persistentDataPath + "/lastLoginData.data", FileMode.Open);
        }
        catch
        {

        }

        if (fileLogin != null)
        {
            lastLoginData = (LastLoginInData)bf.Deserialize(fileLogin);
            fileLogin.Close();
        }
        else
        {
            lastLoginData = new LastLoginInData();
            lastLoginData.SaveLoginDay(DateTime.MinValue);
        }

        //var test1 = lastLogin.Date;
        //var test2 = lastLogin.Day;
        //var test3 = thisLogin.Date;
        //var test4 = thisLogin.Day;

        bool newDay = false;

        if(lastLoginData.year < thisLogin.Year)
        {
            newDay = true;
        }
        else if(lastLoginData.year == thisLogin.Year)
        {
            if (lastLoginData.month < thisLogin.Month)
            {
                newDay = true;
            }
            else if(lastLoginData.month == thisLogin.Month)
            {
                if (lastLoginData.day < thisLogin.Day)
                {
                    newDay = true;
                }
            }
        }

        if(newDay)
        {
            DailyRewardPanel.SetActive(true);
            DateTime yesterday = thisLogin.AddDays(-1);

            if(lastLoginData.day == yesterday.Day && lastLoginData.day == yesterday.Day && lastLoginData.day == yesterday.Day)
            {
                lastLoginData.streak++;
            }
            else
            {
                lastLoginData.streak = 1;
            }

            int currentStreak = lastLoginData.streak % 7;

            int arrayNumToRemove = 0;

            switch (currentStreak)
            {
                case 1:
                    DataAndAchievementManager.instance.EarnCash(10);
                    CashGainedText.text = "10 Cash";
                    arrayNumToRemove = 0;
                    break;
                case 2:
                    DataAndAchievementManager.instance.EarnCash(15);
                    CashGainedText.text = "15 Cash";
                    arrayNumToRemove = 1;
                    break;
                case 3:
                    DataAndAchievementManager.instance.EarnCash(20);
                    CashGainedText.text = "20 Cash";
                    arrayNumToRemove = 2;
                    break;
                case 4:
                    DataAndAchievementManager.instance.EarnCash(30);
                    CashGainedText.text = "30 Cash";
                    arrayNumToRemove = 3;
                    break;
                case 5:
                    DataAndAchievementManager.instance.EarnCash(40);
                    CashGainedText.text = "40 Cash";
                    arrayNumToRemove = 4;
                    break;
                case 6:
                    DataAndAchievementManager.instance.EarnCash(50);
                    CashGainedText.text = "50 Cash";
                    arrayNumToRemove = 5;
                    break;
                case 0:
                    DataAndAchievementManager.instance.EarnCash(75);
                    CashGainedText.text = "75 Cash";
                    arrayNumToRemove = 6;
                    break;
                default:
                    DataAndAchievementManager.instance.EarnCash(10);
                    CashGainedText.text = "10 Cash";
                    arrayNumToRemove = 0;
                    break;
            }

            foreach (Image i in DayImages[arrayNumToRemove].GetComponentsInChildren<Image>())
            {
                i.material = null;
            }

            lastLoginData.SaveLoginDay(thisLogin);

            FileStream fileSaveLogin = File.Create(Application.persistentDataPath + "/lastLoginData.data");
            bf.Serialize(fileSaveLogin, lastLoginData);
            fileSaveLogin.Close();
        }
    }
    
    public void CloseDailyRewardPanel()
    {
        DailyRewardPanel.SetActive(false);
    }
}
