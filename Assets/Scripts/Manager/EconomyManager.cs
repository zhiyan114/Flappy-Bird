using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager
{
    private static SaveData Data = SaveManager.Data;
    public static int getBalance
    {
        get => Data.Balance;
    }
    public static bool setBalance(int val)
    {
        int newVal = getBalance + val;
        if (newVal > 2000000000 || newVal < 0) return false;
        SaveManager.Data.Balance = newVal;
        UIServiceHandler.setCoinCountUI(newVal);
        return true;
    }
}
