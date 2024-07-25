using System.Collections.Generic;
using UnityEngine;

public class ProjectilesScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject player;
    public Camera camera; // 카메라를 Camera 타입으로 변경
    public GameObject firePoint; // 발사 위치
    public List<GameObject> bulletPrifabs = new List<GameObject>(); // 총알 프리팹 리스트

    private float timeToFire = 0f; // 발사 시간 추적 변수
    private GameObject effectToSpawn; // 생성할 효과를 나타낼 변수

    private void Awake()
    {
        if (!playerMovement)
            playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Start()
    {
        if (bulletPrifabs.Count > 0) // 총알 프리팹 확인
            effectToSpawn = bulletPrifabs[0]; // 첫 번째 효과 선택
    }

    void FindPlayer()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void Update()
    {
        FindPlayer();
        if (Input.GetMouseButton(0) && Time.time >= timeToFire && !playerMovement.isCursorVisible) // 마우스 클릭 & 발사시간o & 커서 비활성화 중
        {
            timeToFire = Time.time + 1f / effectToSpawn.GetComponent<ProjectileMoveScript>().fireRate; // 발사 시간 업데이트
            ShotBullet(); // 효과 생성
        }
    }

    public void ShotBullet() // 효과 생성
    {
        if (firePoint != null && camera != null) // 발사 위치와 카메라 확인
        {
            // 카메라 중앙에서 레이 발사
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point; // 레이가 맞은 지점
            }
            else
            {
                targetPoint = ray.GetPoint(1000); // 아무것도 맞지 않으면 먼 거리의 점을 목표로 설정
            }

            // 레이 시각적으로 표시
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 2.0f);

            Vector3 direction = (targetPoint - firePoint.transform.position).normalized;

            // firePoint의 위치에서 카메라 중앙 방향으로 총알 생성
            GameObject shotBullet = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.LookRotation(direction));

            // 총알에 Rigidbody가 있을 경우 초기 속도 설정
            Rigidbody rb = shotBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * shotBullet.GetComponent<ProjectileMoveScript>().speed; // 총알 속도 설정
            }
        }
    }
}
