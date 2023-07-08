using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy1 : MonoBehaviour, IDamageable
{
    public int maxHealth = 10;
    public int heatlh;
    public void dealDamage(int damage)
    {
        heatlh = heatlh - damage;
        if(heatlh <= 0 )
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        heatlh = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
