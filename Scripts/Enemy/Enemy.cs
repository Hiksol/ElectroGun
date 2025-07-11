using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 Position;
    private int Health = 100;
    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHealth(int delta)
    {
        Debug.Log("hit");
        Health -= delta;
        if (Health <= 0)
        {
            Debug.Log("Enemy died");
            Destroy(gameObject);
        }
    }
}

