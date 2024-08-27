#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveScript : MonoBehaviour
{
    public float rotateAmount = 45; // 회전 속도
    public float speed; // 총알 속도
    public float fireRate; // 발사 속도
    public GameObject muzzlePrefab; // 발사 이펙트 프리팹
    public GameObject hitPrefab; // 충돌 이펙트 프리팹
    public List<GameObject> trails; // 총알 궤적 리스트

    private bool collided; // 충돌 여부
    private Rigidbody rb; // Rigidbody 컴포넌트

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기

        // 발사 이펙트 생성
        if (muzzlePrefab != null)
        {
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                Destroy(muzzleVFX, ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
            }
        }
    }

    void FixedUpdate()
    {
        // FixedUpdate에서의 이동 로직은 삭제했습니다.
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && !collided)
        {
            collided = true; // 충돌 상태 설정

            // 충돌 지점 정보 전달
            ContactPoint contact = collision.contacts[0];
            DestroyBullet(gameObject, contact.point, contact.normal); // 충돌 지점과 표면의 법선을 전달
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bullet" && !collided)
        {
            collided = true; // 충돌 상태 설정

            // 트리거에서 충돌 지점 정보 전달
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - other.transform.position).normalized;

            DestroyBullet(gameObject, contactPoint, normal); // 충돌한 위치와 법선을 전달
        }
    }

    void DestroyBullet(GameObject target, Vector3 contactPoint, Vector3 normal)
    {
        // 궤적 효과 처리
        if (trails.Count > 0)
        {
            for (int i = 0; i < trails.Count; i++)
            {
                trails[i].transform.parent = null;
                var ps = trails[i].GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Stop();
                    Destroy(ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
                }
            }
        }

        // 속도 멈춤
        speed = 0;
        target.GetComponent<Rigidbody>().isKinematic = true;

        // 충돌 지점과 회전 정보 사용
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, normal);
        Vector3 pos = contactPoint;

        // 충돌 이펙트 생성
        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);

            var ps = hitVFX.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
            else
            {
                Destroy(hitVFX, ps.main.duration);
            }
        }

        StartCoroutine(DestroyParticle(0f)); // 총알 파괴
    }



    public IEnumerator DestroyParticle(float waitTime)
    {
        if (transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> tList = new List<Transform>();

            foreach (Transform t in transform.GetChild(0).transform)
            {
                tList.Add(t);
            }

            while (transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);
                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for (int i = 0; i < tList.Count; i++)
                {
                    tList[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
