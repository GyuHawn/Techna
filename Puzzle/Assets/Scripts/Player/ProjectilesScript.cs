using System.Collections.Generic;
using UnityEngine;

public class ProjectilesScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GemCombination gemCombination;

    public GameObject player;
    public Camera camera;
    public GameObject firePoint; // 발사 위치

    public float gemIndex;
    public bool changeBullet;
    public GameObject[] B_Bullets; // 총알 투사체 배열
    public GameObject[] B_A_Bullets; // 총알 + 속성 투사체 배열
    public GameObject[] B_F_Bullets; // 총알 + 기능 투사체 배열
    public GameObject[] B_A_F_Bullets; // 총알 + 속성 + 기능 투사체 배열
    public bool b_Bullet;
    public bool b_A_Bullet;
    public bool b_F_Bullet;
    public bool b_A_F_Bullet;

    private float timeToFire = 0f; // 발사 시간 추적
    public GameObject effectToSpawn; // 생성할 투사체

    private void Awake()
    {
        if (!playerMovement)
            playerMovement = player.GetComponent<PlayerMovement>();
        if (!gemCombination)
            gemCombination = GameObject.Find("GemManager").GetComponent<GemCombination>();
    }

    void Start()
    {
        if (B_Bullets.Length > 0) // 총알 프리팹 확인
            effectToSpawn = B_Bullets[0]; // 첫 번째 효과 선택
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

        if(gemIndex != gemCombination.currentGemNum && !changeBullet)
        {
            changeBullet = true;
            SelectProjectiles();
        }
    }

    void SelectProjectiles() // 투사체 선택
    {
        float gemIndex;
        gemIndex = gemCombination.currentGemNum;
        changeBullet = false;

        // 정수부와 소수부 분리
        int intPart = Mathf.FloorToInt(gemIndex);
        float decimalPart = gemIndex - intPart;

        // 배열 선택
        GameObject[] bulletArray = null;
        int index = -1; // 초기값을 -1로 설정하여 잘못된 경우를 확인할 수 있도록 함

        if (b_Bullet)
        {
            bulletArray = B_Bullets;
            index = intPart - 1;
        }
        else if (b_A_Bullet)
        {
            bulletArray = B_A_Bullets;
            index = (intPart - 1) * 10 + Mathf.RoundToInt(decimalPart * 10) - 1;
        }
        else if (b_F_Bullet)
        {
            bulletArray = B_F_Bullets;
            if(decimalPart > 0.01)
            {
                index = (intPart - 1) * 100 + Mathf.RoundToInt(decimalPart * 100) - 1;
            }
            else
            {
                index = 0;
            }
        }
        else if (b_A_F_Bullet)
        {
            bulletArray = B_A_F_Bullets;
            index = (intPart - 1) * 100 + Mathf.RoundToInt(decimalPart * 100);
        }

        Debug.Log("Index : " + index);

        if (bulletArray != null && index >= 0 && index < bulletArray.Length)
        {
            effectToSpawn = bulletArray[index];
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
