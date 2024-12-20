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
    public GameObject ui;
    public List<GameObject> texts;
    public List<GameObject> choices;

    public List<Tuple<int, string>> list;

    private List<GameObject> _weapons;
    private List<GameObject> _addons;
    private List<string> _upgrades = new List<string>();

    public bool isWeapon;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _weapons = gameObject.GetComponent<WeaponManager>().basesWeapons;
        _addons = gameObject.GetComponent<WeaponManager>().upgradesWeapons;
        
        _upgrades.Add("Damage Up");
        _upgrades.Add("Health Up");
        _upgrades.Add("Speed Up");
        _upgrades.Add("Range Up");
        _upgrades.Add("Additional Projectiles");
        _upgrades.Add("Slicing Drone");
        _upgrades.Add("Aura of Hurting");
        _upgrades.Add("Slow Obliterator");
        _upgrades.Add("Robondiball");
    }

    public void initializeLvlUp(int lvl)
    {
        
        
        list = new List<Tuple<int, string>>();
        ui.GetComponent<uiController>().UpdateLevelUI();

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
                    player.GetComponent<CharacterControllerScript>().attackDamageModifier += 0.5f;
                    break;
                case 1 :
                    player.GetComponent<Health>().maxHealth = Mathf.FloorToInt(player.GetComponent<Health>().maxHealth * 0.5f);
                    break;
                case 2 :
                    player.GetComponent<CharacterControllerScript>().speedModifier += 0.2f;
                    break;
                case 3 :
                    player.GetComponent<CharacterControllerScript>().rangeModifer += 0.3f;
                    break;
                case 4 :
                    player.GetComponent<CharacterControllerScript>().additionalProjection += 1;
                    break;
                case 5 :
                    Instantiate(_addons[2]);
                    _takenUpgrades.Add(5);
                    break;
                case 6 :
                    Instantiate(_addons[1]);
                    _takenUpgrades.Add(6);
                    break;
                case 7 :
                    player.GetComponent<CharacterControllerScript>().weapons.Add(_addons[0]);
                    _takenUpgrades.Add(7);
                    break;
                case 8 :
                    player.GetComponent<CharacterControllerScript>().weapons.Add(_addons[3]);
                    _takenUpgrades.Add(8);
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
                texts[0].GetComponent<TextMeshProUGUI>().text= this.list[0].Item2;
                texts[1].GetComponent<TextMeshProUGUI>().text = this.list[1].Item2;
                texts[2].GetComponent<TextMeshProUGUI>().text = this.list[2].Item2;

                choices[0].GetComponent<Button>().onClick.AddListener(delegate {ApplyLvl(this.list[0].Item1);});
                choices[1].GetComponent<Button>().onClick.AddListener(delegate {ApplyLvl(this.list[1].Item1);});
                choices[2].GetComponent<Button>().onClick.AddListener(delegate {ApplyLvl(this.list[2].Item1);});

                
            } else
            {
                choices[0].GetComponent<Button>().onClick.RemoveAllListeners();
                choices[1].GetComponent<Button>().onClick.RemoveAllListeners();
                choices[2].GetComponent<Button>().onClick.RemoveAllListeners();
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
