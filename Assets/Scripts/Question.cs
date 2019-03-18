using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newQuestion", menuName = "Question", order = 1)]
public class Question : ScriptableObject
{
    public string questionText;

    public string response1 = "";
    public float interestMod1 = 0;
    public float happyMod1 = 0;

    public string response2 = "";
    public float interestMod2 = 0;
    public float happyMod2 = 0;

    public string response3 = "";
    public float interestMod3 = 0;
    public float happyMod3 = 0;

    public string response4 = "";
    public float interestMod4 = 0;
    public float happyMod4 = 0;

    public string response5 = "";
    public float interestMod5 = 0;
    public float happyMod5 = 0;

    public string GetResponseIndex(int index)
    {
        switch (index)
        {
            case 0:
                return response1;
            case 1:
                return response2;
            case 2:
                return response3;
            case 3:
                return response4;
            case 4:
                return response5;
        }

        return "";
    }

    public float GetInterestMod(int index)
    {
        switch (index)
        {
            case 0:
                return interestMod1;
            case 1:
                return interestMod2;
            case 2:
                return interestMod3;
            case 3:
                return interestMod4;
            case 4:
                return interestMod5;
        }

        return 0;
    }

    public float GetHappyMod(int index)
    {
        switch (index)
        {
            case 0:
                return happyMod1;
            case 1:
                return happyMod2;
            case 2:
                return happyMod3;
            case 3:
                return happyMod4;
            case 4:
                return happyMod5;
        }

        return 0;
    }
}
