using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class LastLoginInData
{
    public int day;
    public int month;
    public int year;


    public int streak = 0;


    public void SaveLoginDay(DateTime dt)
    {
        day = dt.Day;
        month = dt.Month;
        year = dt.Year;
    }
}

