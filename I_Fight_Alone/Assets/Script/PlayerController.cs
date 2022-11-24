using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public GameObject[] weapons;
    public bool[] hasWeapons;

    float speed;  // 플레이어 능력치
    // float hp;
    // float vit;

    float hAxis;  // 이동관련 변수
    float vAxis; 
    
    bool wDown;   
    bool iDown;
    bool sDown1; // slot 1,2,3
    bool sDown2;
    bool sDown3;
    bool fDown;  // atk but
    bool isFireReady = true;

    Vector3 moveVec;

    Animator anim;

    GameObject nearItem;
    Weapon equipWeapon;
    float fireDelay; // atk delay

    // ------------------------------ events ---------------------------------
    void Awake() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        speed = 5.0f;
        // hp = 100.0f;
        // vit = 0.0f;
    }

    void Update()
    {   
        GetInput();
        move();
        Swap();
        Attack();
    }
    
    // ------------------------------ Update ---------------------------------
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButtonDown("Interation");
        
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");

        fDown = Input.GetButtonDown("Fire1");
    }

    void move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if(!isFireReady && equipWeapon.type == Weapon.Type.Melee)
        {
            moveVec = Vector3.zero;
        }

        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        transform.LookAt(transform.position + moveVec);
    }

    // ------------------------------ stats ---------------------------------
    void Attack()
    {
        if(equipWeapon == null){return;}
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;

        if(fDown && isFireReady)
        {
            equipWeapon.Use();
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
        
    }
    void SetHp(float newHp){}
    void SetVit(float newVit){}
    void SetSpeed(float newSpeed){}

    void Swap()
    {   
        int weaponIndex = -1;
        if(sDown1) weaponIndex = 0;
        if(sDown2) weaponIndex = 1;
        if(sDown3) weaponIndex = 2;

        if(sDown1 || sDown2 || sDown3)
        {   
            if(equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
        }
    }    

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Item")
        {
            nearItem = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Item")
        {
            nearItem = null;
        }
    }
}
