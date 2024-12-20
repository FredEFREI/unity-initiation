using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelUP : MonoBehaviour
{
    private static List<int> _takenUpgrades = new List<int>(); 
    public GameObject player;
    public GameObject lvlMenu;
    public List<GameObject> texts;
    public List<GameObject> choices;

    public List<Tuple<int, string>> list;

    private List<GameObject> _weapons;
    private List<string> _upgrades = new List<string>();

    public bool isWeapon;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _weapons = gameObject.GetComponent<WeaponManager>().basesWeapons;
        
        _upgrades.Add("Damage Up");
        _upgrades.Add("Defense Up");
        _upgrades.Add("Health Up");
        _upgrades.Add("Speed Up");
        _upgrades.Add("Additional Projectiles");
        _upgrades.Add("Slicing Drone");
        _upgrades.Add("Aura of Hurting");
        _upgrades.Add("Slow Obliterator");
    }

    public void initializeLvlUp(int lvl)
    {
        list = new List<Tuple<int, string>>();

        // From the lvl 3 and after, all level up propose upgrade or additional non-base weapons
        if (lvl >= 3)
        {
            isWeapon = false;
            int choice = Random.Range(0, _upgrades.Count);
            list.Add(new Tuple<int, string>(choice, _upgrades[choice]));
            choice = Random.Range(0, _upgrades.Count);
            for (int i = 0; i <= 2; i++)
            {
                while (list.Contains(new Tuple<int, string>(choice, _upgrades[choice])) || _takenUpgrades.Contains(choice))
                {
                    choice = Random.Range(0, _upgrades.Count);
                }

                list.Add(new Tuple<int, string>(choice, _upgrades[choice]));
            }
            
            ToggleLvlMenu();
        }
        // Else, for level 2, we propose a choice of new base weapon.
        else
        {
            isWeapon = true;
            int nbrChoice = _weapons.Count;
            
            int choice = Random.Range(0, nbrChoice);
            list.Add(new Tuple<int, string>(choice, _weapons[choice].name));
            choice = Random.Range(0, nbrChoice);
            for (int i = 0; i <= 2; i++)
            {
                while (list.Contains(new Tuple<int, string>(choice, _weapons[choice].name)))
                {
                    choice = Random.Range(0, nbrChoice);
                }

                list.Add(new Tuple<int, string>(choice, _weapons[choice].name));
            }

            ToggleLvlMenu();
        }
    }
    
    public void ApplyLvl(int choice)
    {
        if (isWeapon)
        {
            player.GetComponent<CharacterControllerScript>().weapons[0] = _weapons[choice];
        }
        else
        {
            switch (choice)
            {
                case 0 :
                    print(_upgrades[0]);
                    break;
                case 1 :
                    print(_upgrades[1]);
                    break;
                case 2 :
                    print(_upgrades[2]);
                    break;
                case 3 :
                    print(_upgrades[3]);
                    break;
                case 4 :
                    print(_upgrades[4]);
                    break;
                case 5 :
                    print(_upgrades[5]);
                    break;
                case 6 :
                    print(_upgrades[6]);
                    break;
                case 7 :
                    print(_upgrades[7]);
                    break;
                case 8 :
                    print(_upgrades[8]);
                    break;
                case 9 :
                    print(_upgrades[9]);
                    break;
                default:
                    print("Oups, did not implement all LevelUp");
                    break;
            }
        }
        ToggleLvlMenu();
    }

    void ToggleLvlMenu()
    {
        if (lvlMenu)
        {
            if (!lvlMenu.activeSelf)
            {
                lvlMenu.SetActive(true);
                Time.timeScale = 0;
                print(this.list[0].Item2);
                texts[0].GetComponent<TextMeshProUGUI>().text= this.list[0].Item2;
                texts[1].GetComponent<TextMeshProUGUI>().text = this.list[1].Item2;
                texts[2].GetComponent<TextMeshProUGUI>().text = this.list[2].Item2;

                choices[0].GetComponent<Button>().onClick.AddListener(delegate {ApplyLvl(this.list[0].Item1);});
                choices[1].GetComponent<Button>().onClick.AddListener(delegate {ApplyLvl(this.list[1].Item1);});
                choices[2].GetComponent<Button>().onClick.AddListener(delegate {ApplyLvl(this.list[2].Item1);});

                
            } else
            {
                lvlMenu.SetActive(false);
                Time.timeScale = 1;
            }  
        }
        else
        {
            Debug.LogError("Canvas is not assigned in the inspector.");
        }
    }
    
}
