using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectilesScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject firePoint; // 발사 위치
    public List<GameObject> bulletPrifabs = new List<GameObject>(); // 총알 프리팹 리스트

    private float timeToFire = 0f; // 발사 시간 추적 변수
    private GameObject effectToSpawn; // 생성할 효과를 나타낼 변수

    private void Awake()
    {
        if (!playerMovement)
            playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (bulletPrifabs.Count > 0) // 총알 프리팹 확인
            effectToSpawn = bulletPrifabs[0]; // 첫 번째 효과 선택

    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= timeToFire && !playerMovement.isCursorVisible) // 마우스 클릭 & 발사시간o & 커서 비활성화 중
        {
            timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate; // 발사 시간 업데이트
            SpawnVFX(); // 효과 생성
        }
    }

    public void SpawnVFX() // 효과 생성
    {
        GameObject shotBullet;
        if (firePoint != null) // 발사 위치 확인
        { 
            shotBullet = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity); // 발사 위치에서 효과 생성          
        }
    }
}
