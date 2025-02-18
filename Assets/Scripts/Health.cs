using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    public int curHealth = 0;
    public int maxHealth = 100;
    public HealthBar healthBar;
    void Start()
    {
        curHealth = maxHealth;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            DamagePlayer(10);
        }
    }
    public void DamagePlayer( int damage )
    {
        curHealth -= damage;
        healthBar.SetHealth( curHealth );
    }

    public void SetHealth( int health )
    {
        curHealth += health;
        healthBar.SetHealth( curHealth );
    }
}