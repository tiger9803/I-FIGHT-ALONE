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
        //���� �ʱ�ȭ
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }

    IEnumerator GainPowerTimer()
    {
        //������ �⸦ ������ �ð� 
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        //���� �⸦ ������ �ð��� ���� ���� ũ�� ����
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
