using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private int health;

    private void Start()
    {
        health = 5;
    }

    public void Hurt(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log($"Health: {health}");
        }
    }
}
