using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    public enum Type { Melee, Range }; // 근거리 : Melee, 원거리 : Range
    public Type type;
    public int damage;
    public float rate; // 공격속도
    public BoxCollider meleeArea; // 공격 범위
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if(type == Type.Range)
        {   
            Shot();
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f); // 0.1 sec wait
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f); // 0.3 sec wait
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f); // 0.3 sec wait
        trailEffect.enabled = false;
    }

    void Shot()
    {
        // 총알 발사
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;
    }
}
