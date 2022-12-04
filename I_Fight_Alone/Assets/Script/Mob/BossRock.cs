using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : MobBullet
{
    Rigidbody rigid;
    float angularPower = 2;
    float scaleValue = 0.1f;
    bool isShoot;

    void Awake()
    {
        //변수 초기화
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        //보스가 기를 모으는 시간 
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        //위의 기를 모으는 시간에 따른 돌의 크기 결정
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.001f;
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration);
            yield return null;
        }
    }
}
