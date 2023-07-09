using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public PlayerMovement Player;
    public pointAtk point;
    public TMP_Text souls;
    // Start is called before the first frame update
    void Start()
    {
        newPlayer();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void newPlayer()
    {
        point = GameObject.FindFirstObjectByType<pointAtk>();
        Player = GameObject.FindFirstObjectByType<PlayerMovement>();
        souls.SetText("current orbs: " + Player.spendableSoul);

    }

    public void healthUp()
    {
        if (Player.spendableSoul > 2)
        {
            Player.spendableSoul -= 2;
            Player.addHealth(5);

        }
    }
    public void speedUp()
    {
        if (Player.spendableSoul > 2)
        {
            Player.spendableSoul -= 2;
            Player.activeMoveSpeed += 0.3f;

        }
    }
    public void jumpUp()
    {
        if (Player.spendableSoul > 2)
        {
            Player.spendableSoul -= 2;
            Player.m_JumpForce += 40;

        }
    }
      public void fireballUp()
    {
        if (Player.spendableSoul > 2)
        {
            Player.spendableSoul -= 2;
            point.damageOfFire += 3;
        }
    }  
    public void swordUp()
    {
        if (Player.spendableSoul > 2)
        {
            Player.spendableSoul -= 2;
            point.swordDamage += 2; 
        }
    }

}
