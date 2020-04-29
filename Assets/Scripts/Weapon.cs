using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//from https://www.c7pixel.com/post/first_person_shooting_in_unity_using_raycast

public class Weapon : MonoBehaviour
{
    public float damage;
    // This variable determines 
    // how fast the weapon can shoot
    public float fireRate;
    // This variable determines 
    // how far the bullet fly when fired
    public float fireLength;
    // This variable determines 
    // how fast the weapon reload
    public float reloadTime;
    // A reference to the weapon's 
    // gun barrel game object
    public Transform Gun_Barrel;
    // The number of bullets 
    // this weapon has
    public int currentBullets;
    // The number of bullets 
    // this weapon CAN have
    public int maxBullets = 30;

    void Start()
    {
        // We set the weapon to 
        // start with a full magazine
        currentBullets = maxBullets;
    }


    // Update is called once per frame
    void Update()
    {

    }
}