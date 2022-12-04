using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;
    
    private void OnCollisionEnter(Collision collision)
    {
        //ź�ǰ� �ٴڿ� ������ 2���� �����
        if(!isRock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�Ѿ��� ���� ������ �����
        if (!isMelee && other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
