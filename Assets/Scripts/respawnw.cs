using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnw : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> players = new List<GameObject>();
    public GameObject Player;
    public int playerNum;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }

    public void death()
    {
        GameObject newPlayer = Instantiate(players[playerNum]);
        newPlayer.tag = Player.tag;
        Destroy(Player);
        Player = newPlayer;
        playerNum++;
        if(playerNum >= players.Count)
        {
            playerNum = 0;
        }
    }
}
