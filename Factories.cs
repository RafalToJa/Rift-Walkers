using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factories : MonoBehaviour
{
    public int team;
    public int Turn;
    public bool selected;

    GameMaster gm;

    public GameObject FactoryUI;
    public static bool FactoryUIOpen = false;

    public GameObject miecznikPrefab;
    public Button Miecznik;
    public int miecznikCost;

    public GameObject tarczownikPrefab;
    public Button Tarczownik;
    public int tarczownikCost;

    public GameObject kuszniczkaPrefab;
    public Button Kuszniczka;
    public int kuszniczkaCost;

    private int money1;
    private int money2;

    private void Start()
    {
        Turn = 1;
        gm = FindObjectOfType<GameMaster>();

        Button miecz = Miecznik.GetComponent<Button>();
        miecz.onClick.AddListener(miecznikCreation);

        Button tarcz = Tarczownik.GetComponent<Button>();
        tarcz.onClick.AddListener(tarczownikCreation);

        Button kusz = Kuszniczka.GetComponent<Button>();
        kusz.onClick.AddListener(kuszniczkaCreation);
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Turn += 1;
        }

        //money1 = GetComponent<Mines>().player1Money;
        //money2 = GetComponent<Mines>().player2Money;
        //EndTurn();
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (FactoryUIOpen)
            {
                Close();
            }
        }*/
    }
    /*private void EndTurn()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Zmienic na przycisk
        {
            Turn += 1;
        }
    }*/
    void Close()
    {
        FactoryUI.SetActive(false);
        FactoryUIOpen = false;
    }

    void Open()
    {
        FactoryUI.SetActive(true);
        FactoryUIOpen = true;
    }
    
    
    
    private void OnMouseDown()
    {
        if (Turn % 2 == 1 && team == 1)
        {


            if (selected == true)
            {
                selected = false;
                gm.selectedFactory = null;
            }
            else
            {


                if (gm.selectedFactory != null)
                {
                    gm.selectedFactory.selected = false;
                }

                selected = true;
                gm.selectedFactory = this;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (FactoryUIOpen)
                    {
                        Close();
                    }
                    else
                    {
                        Open();
                    }
                }

            }
        }
        if (Turn % 2 == 0 && team == 2)
        {


            if (selected == true)
            {
                selected = false;
                gm.selectedFactory = null;
            }
            else
            {


                if (gm.selectedFactory != null)
                {
                    gm.selectedFactory.selected = false;
                }

                selected = true;
                gm.selectedFactory = this;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    if (FactoryUIOpen)
                    {
                        Close();
                    }
                    else
                    {
                        Open();
                    }
                }

            }
        }
    }

    private void miecznikCreation()
    {
        if(team == 1)
        {
            money1 = GetComponent<Mines>().player1Money;
            miecznikPrefab.GetComponent<Units>().Player = 1;
            Instantiate(miecznikPrefab);
            money1 -= miecznikCost;
            
        }
        if (team == 2)
        {
            money2 = GetComponent<Mines>().player2Money;
            miecznikPrefab.GetComponent<Units>().Player = 2;
            Instantiate(miecznikPrefab);
            money2 -= miecznikCost;            
        }
    }
    private void tarczownikCreation()
    {
        if (team == 1)
        {
            money1 = GetComponent<Mines>().player1Money;
            tarczownikPrefab.GetComponent<Units>().Player = 1;
            Instantiate(tarczownikPrefab);
            money1 -= tarczownikCost;
        }
        if (team == 2)
        {
            money2 = GetComponent<Mines>().player2Money;
            tarczownikPrefab.GetComponent<Units>().Player = 2;
            Instantiate(tarczownikPrefab);
            money2 -= tarczownikCost;
        }
    }
    private void kuszniczkaCreation()
    {
        if (team == 1)
        {
            money1 = GetComponent<Mines>().player1Money;
            kuszniczkaPrefab.GetComponent<Units>().Player = 1;
            Instantiate(kuszniczkaPrefab);
            money1 -= kuszniczkaCost;
        }
        if (team == 2)
        {
            money2 = GetComponent<Mines>().player2Money;
            kuszniczkaPrefab.GetComponent<Units>().Player = 2;
            Instantiate(kuszniczkaPrefab);
            money2 -= kuszniczkaCost;
        }
    }

}
