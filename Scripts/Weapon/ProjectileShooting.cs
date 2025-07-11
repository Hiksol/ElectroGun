using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Camera cam;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void  Shoot()
    {
        var proj = Instantiate(projectilePrefab);
        proj.transform.position = cam.transform.position + cam.transform.forward * 3f;
        proj.transform.rotation = cam.transform.rotation;
    }
}
