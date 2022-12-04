using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public enum Type { Exp, Heart, Grenade, Coin_B, Coin_S, Coin_G };
    public Type type;
    public int value;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
}
