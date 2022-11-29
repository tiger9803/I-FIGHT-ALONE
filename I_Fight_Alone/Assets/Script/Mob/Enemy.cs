using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{   
    GameObject Target;
    NavMeshAgent nav;

    // Mob stats
    float hp;
    public float speed;
    float power;

    // ------------------------------ events ---------------------------------
    void Awake()
    {
        this.nav = GetComponent<NavMeshAgent>();
    }

    void Start() 
    {
        this.Target = GameObject.Find("Player");
        
        // speed = 3.0f;
        // power = 5;
    }

    void OnEnable()
    {
        hp = 100.0f;
    }

    void Update()
    {
        nav.SetDestination(Target.transform.position);
    }
    

    // ------------------------------ damage ---------------------------------

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Range")
        {
            hp -= GameObject.Find("Weapon HandGun").GetComponent<Weapon>().damage;
            Destroy(other.gameObject);
            gameObject.transform.Translate(Vector3.back * 1);
            Debug.Log(gameObject.name + "Hp: " + hp.ToString());
        }
        else if(other.tag == "Melee")
        {
            hp -= other.GetComponent<Weapon>().damage;
            gameObject.transform.Translate(Vector3.back * 1);
            Debug.Log(gameObject.name + "Hp: " + hp.ToString());
        }

        if(hp <= 0){gameObject.SetActive(false);}
    }

    // ------------------------------ stats ---------------------------------
    void SetSpeed(float newSpeed)
    {
        nav.speed = newSpeed;
    }

    void SetHp(float newHp){}
    void SetPower(float newPower){}
}
