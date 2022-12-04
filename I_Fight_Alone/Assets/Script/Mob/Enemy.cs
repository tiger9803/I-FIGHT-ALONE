using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D };
    public Type enemyType;

    public int maxHealth;
    public int curHealth;

    public GameObject[] player;
    public GameObject target;
    public BoxCollider meleeArea;
    public GameObject bullet;
    public Rigidbody rigid;
    public BoxCollider boxCollider;
    public MeshRenderer[] meshs;
    public NavMeshAgent nav;
    public Animator anim;

    public bool isChase;
    public bool isAttack;
    public bool isDead;

    // ------------------------------ events ---------------------------------
    void Awake()
    {
        //???? ????
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        //??? ???? 2???? ?��???? ?????
        if (enemyType != Type.D)
            Invoke("ChaseStart", 2);
    }

    void Start() 
    {   
        this.player = new GameObject[3];
        this.player[0] = GameObject.Find("Player");
        this.player[1] = GameObject.Find("Ludo");
        this.player[2] = GameObject.Find("Luna");

        this.target = player[GameManager.instance.selectPlayer];
    }

    void Update()
    {
        //????? ????? ?��????? ????
        if (nav.enabled && enemyType != Type.D)
        {
            nav.SetDestination(target.transform.position);
            nav.isStopped = !isChase;
        }
    }

    void FixedUpdate()
    {
        Targeting();
        FreezeVelocity();
    }

    void ChaseStart()
    {
        //????? ????
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Targeting()
    {
        //?????? ???? ???? ????
        if (!isDead && enemyType != Type.D)
        {
            float targetRadius = 0;
            float targetRange = 0;

            //????? ???? ????
            switch (enemyType)
            {
                case Type.A:
                    targetRadius = 1f;
                    targetRange = 1f;
                    break;
                case Type.B:
                    targetRadius = 1f;
                    targetRange = 6f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;
                    targetRange = 12f;
                    break;
            }

            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine("Attack");
            }
        }

    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        //????? ???? ???
        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);

                meleeArea.enabled = true;

                yield return new WaitForSeconds(1f);

                meleeArea.enabled = false;

                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 5, ForceMode.Impulse);
                meleeArea.enabled = true;

                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;

                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;

                yield return new WaitForSeconds(2f);
                break;
        }

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    void FreezeVelocity()
    {
        //????? ????? ?????? ?��?? ???? ???? ????
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    // ------------------------------ damage ---------------------------------

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Range" || other.tag == "Melee")
        {
            curHealth -= target.GetComponent<PlayerController>().equipWeapon.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec));
        }
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        //?????? ??? ???? ?? ???? ?? ????
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.red;
        }
        nav.enabled = false;

        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 2, ForceMode.Impulse);
        nav.enabled = true;

        yield return new WaitForSeconds(0.05f);

        if (curHealth > 0 && !isDead)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.white;
            }

        }
        else
        {
            isDead = true;
            foreach (MeshRenderer mesh in meshs)
            {
                mesh.material.color = Color.gray;
            }
            isChase = false;
            nav.enabled = false;
            anim.SetTrigger("doDie");

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec * 2, ForceMode.Impulse);

            Destroy(gameObject, 0.5f);
        }
    }

    // ------------------------------ stats ---------------------------------
    void SetSpeed(float newSpeed)
    {
        nav.speed = newSpeed;
    }

    void SetHp(float newHp){}
    void SetPower(float newPower){}

    void OnDestroy()
    {   
        int tmp;

        if(enemyType == Type.A)
        {
            tmp = 1;
            target.GetComponent<PlayerController>().exp += 10;
        }
        else if(enemyType == Type.B)
        {
            tmp = 2;
            target.GetComponent<PlayerController>().exp += 20;
        }
        else if(enemyType == Type.C)
        {
            tmp = 3;
            target.GetComponent<PlayerController>().exp += 30;
        }
        else
        {
            tmp = 4;
            target.GetComponent<PlayerController>().exp += 100;
        }
        GameObject.Find("GameDirector").GetComponent<RunningTime>().ChangeExp();
        GameObject.Find("GameDirector").GetComponent<RunningTime>().AddScore(tmp);
    }
}
