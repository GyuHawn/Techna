using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public ActivateGem gem;

    public float rotateDuration; // 회전 시간
    public float rotateNum; // 회전 각도
    public bool x; // x축 회전 여부
    public bool y; // y축 회전 여부
    public bool z; // z축 회전 여부

    public float waitTime; // 회전 후 대기 시간

    private bool rotating = false;

    void Update()
    {
        if (gem != null && gem.activate && !rotating)
        {
            rotating = true;
            StartCoroutine(RotateAndWait());
        }
    }

    IEnumerator RotateAndWait()
    {
        float elapsedTime = 0f;

        while (elapsedTime < rotateDuration)
        {
            if (x) // x방향으로 회전
            {
                transform.Rotate(Vector3.right * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (y) // y방향으로 회전
            {
                transform.Rotate(Vector3.up * rotateNum * Time.deltaTime / rotateDuration);
            }
            if (z) // z방향으로 회전
            {
                transform.Rotate(Vector3.forward * rotateNum * Time.deltaTime / rotateDuration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 회전 후 대기 시간
        yield return new WaitForSeconds(waitTime);

        rotating = false;
    }
}
