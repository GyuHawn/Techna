using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossStage : MonoBehaviour
{
    public Transform[] bossMapPositions; // 보스맵 포인트
    public GameObject[] SkillWalls; // 조롱 스킬 벽
    public Transform[] SkillPositions; // 단지기 큐브 생성 포인트w
    public GameObject clearWall; // 클리어 시 다음스테이지 이동 관련 벽
    public GameObject destroyFloor; // 삭제 바닥
    public GameObject gem; // 클리어 보석

    private void Update()
    {
        if(gem == null && destroyFloor != null)
        {
            StageClear();
        }
    }

    public void StageClear()
    {
        Debug.Log("a");
        Destroy(destroyFloor);
    }
}
