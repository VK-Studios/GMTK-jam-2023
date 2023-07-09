using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;

public class respawnw : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> players = new List<GameObject>();
    public GameObject Player;
    public int playerNum;
    public int totalSoulOrbs;
    public GameObject soulOrb;
    public Shop shop;
    public GameObject MainMenuShop;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerNum = 1;
        totalSoulOrbs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
    
    public void addOrb()
    {
        totalSoulOrbs++;    
    }

    public void death()
    {
        Destroy(GameObject.FindGameObjectWithTag("orb"));
        Vector3 pos = Player.transform.position;
        Quaternion rot = Player.transform.rotation;
        GameObject newPlayer = Instantiate(players[playerNum]);
        newPlayer.tag = Player.tag;
        Instantiate(soulOrb, pos, rot);
        Destroy(Player);
        Player = newPlayer;
        shop.newPlayer();
        MainMenuShop.SetActive(true);
        virtualCamera.Follow = Player.transform;

		playerNum++;
        if(playerNum >= players.Count)
        {
            playerNum = 0;

        }
    }
}
