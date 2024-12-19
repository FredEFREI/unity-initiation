using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUP : MonoBehaviour
{
    private static List<int> takenUpgrades = new List<int>(); 
    private List<int> list = new List<int>();
    public int nbrUpgrade = 10;

    void Start()
    {
        int choice = Random.Range(0, nbrUpgrade);
        list.Add(choice);
        choice = Random.Range(0, nbrUpgrade);
        for (int i = 0; i < 2; i++)
        {
            while (list.Contains(choice) || takenUpgrades.Contains(choice))
            {
                choice = Random.Range(0, nbrUpgrade);
            }

            list.Add(choice);
        }
        
        print(list);
        ApplyLvl(list[0]);

    }

    void ApplyLvl(int lvl)
    {
        switch (lvl)
        {
            case 0 :
                print("Level upgraded 0");
                break;
            case 1 :
                print("Level upgraded 1");
                break;
            case 2 :
                print("Level upgraded 2");
                break;
            case 3 :
                print("Level upgraded 3");
                break;
            case 4 :
                print("Level upgraded 4");
                break;
            case 5 :
                print("Level upgraded 5");
                break;
            case 6 :
                print("Level upgraded 6");
                break;
            case 7 :
                print("Level upgraded 7");
                break;
            case 8 :
                print("Level upgraded 8");
                break;
            case 9 :
                print("Level upgraded 9");
                break;
        }
    }
    
}
