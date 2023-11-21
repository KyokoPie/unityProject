using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manger : Singleton<UI_Manger>
{
    public GameObject pasueMenu;
    public GameObject deadMenu;
    public GameObject pormptMenu;

    GameObject player;

    public bool pasueOpen;
    public bool deadOpen;
    public bool pormptOpen;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player");
        print(player);

    }

    void Update()
    {
        PasueGame();               
        DeadMenu();
        PormptMenu();
    }

    public void PasueGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pasueOpen && !deadOpen && !pormptOpen)
        {
            if(player != null)
            {
                player.GetComponent<Player_Control>().isEventActive = true;
            }

            Cursor.visible = true;            
            pasueMenu.SetActive(true);
            pasueOpen = true;            
            Time.timeScale = 0f;
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pasueOpen)
        {
            if (player != null)
            {
                player.GetComponent<Player_Control>().isEventActive = false;
            }

            Cursor.visible = false;
            pasueMenu.SetActive(false);
            pasueOpen = false;            
            Time.timeScale = 1f;           
        }
    }

    public void PormptMenu()
    {
        if (Input.GetKeyDown(KeyCode.M) && !pasueOpen && !deadOpen && !pormptOpen)
        {
            if (player != null)
            {
                player.GetComponent<Player_Control>().isEventActive = true;
            }

            Cursor.visible = true;
            pormptMenu.SetActive(true);
            pormptOpen = true;
            Time.timeScale = 0f;

        }
        else if (Input.GetKeyDown(KeyCode.M) && pormptOpen)
        {
            if (player != null)
            {
                player.GetComponent<Player_Control>().isEventActive = false;
            }

            Cursor.visible = false;
            pormptMenu.SetActive(false);
            pormptOpen = false;
            Time.timeScale = 1f;
        }
    }

    public void Continue()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        if(player != null)
        {
            player.GetComponent<Player_Control>().isEventActive = false;
        }
        

        Cursor.visible = false;
        pasueMenu.SetActive(false);
        pasueOpen = false;
        Time.timeScale = 1f;        
    }
        

    public void DeadMenu()
    {
        GameObject playerStats = GameObject.FindGameObjectWithTag("Player");

        if(playerStats != null)
        {
            if (playerStats.GetComponent<Player_Control>().isDead == true)
            {
                deadOpen = true;
                deadMenu.SetActive(true);
                pasueMenu.SetActive(false);
                pormptMenu.SetActive(false);
                Cursor.visible = true;
            }
            else if (playerStats.GetComponent<Player_Control>().isDead == false)
            {
                deadOpen = false;
                deadMenu.SetActive(false);
            }
        }
    }



    
}
