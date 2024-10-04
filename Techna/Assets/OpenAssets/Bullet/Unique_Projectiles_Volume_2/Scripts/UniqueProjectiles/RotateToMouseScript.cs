using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouseScript : MonoBehaviour
{
    public float maximumLenght; // 회전할 최대 거리

    private Ray rayMouse; // 마우스의 레이
    private Vector3 direction; // 방향 변수
    private Quaternion rotation; // 회전 변수
    public Camera cam; // 카메라 변수
    private WaitForSeconds updateTime = new WaitForSeconds(0.01f); // 업데이트 시간 간격

    // 레이 업데이트 시작 함수
    public void StartUpdateRay()
    {
        StartCoroutine(UpdateRay());
    }

    // 레이 업데이트 코루틴
    IEnumerator UpdateRay()
    {
        if (cam != null)
        { // 카메라가 설정되어 있을 경우

            RaycastHit hit; // 레이캐스트 히트 변수
            var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0); // 화면의 중심 계산
            rayMouse = cam.ScreenPointToRay(screenCenter); // 화면의 중심으로부터 레이를 생성

            var pos = rayMouse.GetPoint(maximumLenght); // 최대 거리 지점을 계산
            RotateToMouse(gameObject, pos); // 최대 거리 지점으로 회전

            yield return updateTime; // 업데이트 시간 간격만큼 대기
            StartCoroutine(UpdateRay()); // 다시 코루틴 시작
        }
    }

    // 마우스 방향으로 오브젝트를 회전시키는 함수
    public void RotateToMouse(GameObject obj, Vector3 destination)
    {
        direction = destination - obj.transform.position; // 목적지와 현재 위치의 차이를 방향으로 계산
        rotation = Quaternion.LookRotation(direction); // 방향을 회전으로 변환
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1); // 오브젝트의 회전을 목적지 방향으로 부드럽게 변경
    }
}
