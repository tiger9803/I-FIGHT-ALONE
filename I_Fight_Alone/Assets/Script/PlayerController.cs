using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public GameObject[] weapons;

    public float speed;  // 플레이어 능력치
    public float maxHp;
    float maxExp;
    public float hp;
    public float exp;
    public int level;

    float hAxis;  // 이동관련 변수
    float vAxis; 
    int cnt_Dodge; // 구르기 횟수
    
    bool wDown;
    bool iDown;
    bool jDown;  // Dodge btn
    bool fDown;  // atk btn
    bool isFireReady = true;
    bool isDodge;
    bool isDamaged = false;  // 현재 피격 당하고 있는지 판별하는 플래그 변수  
    
    IEnumerator CoDamage;  // 피격 담당 코루틴
    
    bool isDie = false;  // 죽었는지 판별하는 플래그 변수

    Vector3 moveVec;

    Animator anim;

    public Weapon equipWeapon;
    float fireDelay; // atk delay

    // ------------------------------ events ---------------------------------
    void Awake() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {   
        // Weapon
        int weaponIndex = GameManager.instance.selectWeapon;

        equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
        equipWeapon.gameObject.SetActive(true);

        // Player setting
        if (gameObject.name == "Player") // hp
        {
            maxHp = 150.0f;
            speed = 5.0f;
        }
        else if (gameObject.name == "Ludo") // atk
        {
            maxHp = 100.0f;
            speed = 5.0f;
            equipWeapon.damage += 5;
        }
        else // Luna speed
        {
            maxHp = 100.0f;
            speed = 7.0f;
        }

        maxExp = 100;
        exp = 0;
        level = 1;
        cnt_Dodge = 3;
        hp = maxHp;
        GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeHp();
    }

    void Update()
    {   
        GetInput();
        move();
        Attack();
        Dodge();

        if(gameObject.transform.position.y < -4){Respwan();}

        if(exp >= maxExp){LevelUp();}
    }
    
    // ------------------------------ Update ---------------------------------
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        iDown = Input.GetButtonDown("Interation");
        
        fDown = Input.GetButtonDown("Fire1");
        jDown = Input.GetButtonDown("Jump"); // 이름만 Jump -> Dodge 키임
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

    void Respwan()
    {
        gameObject.transform.position = new Vector3(0, 1, 0);
        hp -= 50;
        GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeHp();
    }

    void Dodge()
    {
        if(jDown && isDodge != true && cnt_Dodge != 0)
        {   
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;
            cnt_Dodge--;

            Invoke("DodgeOut", 0.4f);
        }
    }
    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

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

    void LevelUp()
    {
        level++;
        exp = 0;
        cnt_Dodge = 3;
        Time.timeScale = 0;
        GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeExp();
        GameObject.Find("GameDirector").GetComponent<RunningTime>().selectUpgrade.SetActive(true);
    }

    
    /// <summary>
    /// 피격 당했을 때 호출되는 코루틴
    /// </summary>
    /// <param name="damage">해당 몬스터의 데미지</param>
    IEnumerator OnDamaged(int damage)
    {
        isDamaged = true;  // 플래그 변수를 true로 세팅
        hp -= damage;
        
        if(hp <= 0)   // 만약 hp가 0이하라면
        {
            hp = 0;
            GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeHp();
        }
        else  // hp가 남아있다면 피격 애니메이션 실행
        {
            GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeHp();
            anim.SetTrigger("doDamaged");  // 애니메이션이 실행 될 동안 코드 진행 유예
            yield return new WaitUntil(() => !anim.GetCurrentAnimatorStateInfo(0).IsName("Land"));   
            isDamaged = false;  // 애니메이션이 끝나면 플래그 변수를 false로 세팅
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.CompareTag("EnumyBullet") && !isDamaged)
        {
            // MobBullet의 데미지를 불러와서 hp를 깎는다.
            CoDamage = OnDamaged(collision.GetComponent<MobBullet>().damage);
            StartCoroutine(CoDamage);
        }

        if (collision.tag == "Item")
        {
            Items item = collision.GetComponent<Items>();

            switch (item.type)
            {
                case Items.Type.Exp:
                    exp += item.value;
                    GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeExp();
                    break;
                case Items.Type.Heart:
                    hp += item.value;
                    GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeHp();
                    break;
                case Items.Type.Grenade:

                    break;
                case Items.Type.Coin_B:
                    GameObject.Find("GameDirector").GetComponent<RunningTime>().AddScore(1);
                    break;
                case Items.Type.Coin_S:
                    GameObject.Find("GameDirector").GetComponent<RunningTime>().AddScore(3);
                    break;
                case Items.Type.Coin_G:
                    GameObject.Find("GameDirector").GetComponent<RunningTime>().AddScore(0);
                    break;
            }

            Destroy(collision.gameObject, 0.3f);
        }
    }

    

    
}
