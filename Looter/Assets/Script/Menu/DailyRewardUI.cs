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
    //public GameObject CollectRewardPanel;
    //public GameObject WelcomeBackPanel;

    private int treak;

    private LastLoginInData lastLoginData;
    public Material dimOutMaterial;

    public GameObject[] DayImages;
    public void AppLoads()
    {
        
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
                    arrayNumToRemove = 0;
                    break;
                case 2:
                    DataAndAchievementManager.instance.EarnCash(15);
                    arrayNumToRemove = 1;
                    break;
                case 3:
                    DataAndAchievementManager.instance.EarnCash(20);
                    arrayNumToRemove = 2;
                    break;
                case 4:
                    DataAndAchievementManager.instance.EarnCash(30);
                    arrayNumToRemove = 3;
                    break;
                case 5:
                    DataAndAchievementManager.instance.EarnCash(40);
                    arrayNumToRemove = 4;
                    break;
                case 6:
                    DataAndAchievementManager.instance.EarnCash(50);
                    arrayNumToRemove = 5;
                    break;
                case 0:
                    DataAndAchievementManager.instance.EarnCash(75);
                    arrayNumToRemove = 6;
                    break;
                default:
                    DataAndAchievementManager.instance.EarnCash(10);
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
