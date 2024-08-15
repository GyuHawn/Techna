using UnityEngine;

// 이전 노드에서 보내는 시작/정지 신호에 따라 빛을 방출하는 LED 노드 클래스.
public class LEDNode : MonoBehaviour
{
    // 이전에 연결된 LED 노드.
    public LEDNode prevNode = null;
    // 다음에 연결된 LED 노드에 대한 시작/정지 신호.
    public bool nextStart { get; private set; } = false;

    // 실행 여부
    public bool activate = false;

    // 체인의 첫 번째 노드인 경우 GUI에 표시.
    public bool isFirstNode = false;
    // 포인트 라이트(점광원) 활성화 제어.
    public bool isPointLightEn = false;
    // 느린 빛 진행 표시기.
    public bool slowProgress = false;

    // 연결된 컴포넌트들.
    private Light pointLight = null;
    private Renderer rend = null;

    // 방출된 빛의 강도 매개변수.
    private float intensity = 0;
    private float minIntensity = 0;
    private float maxIntensity = 1.0f;
    private float onTime = 0;
    private float maxOnTime = 2.0f;
    private float onTimerSpeed = 2.0f;
    private float offTime = 0;
    private float maxOffTime = 20.0f;
    private float offTimerSpeed = 20.0f;
    // 빛의 증가/감소 제어.
    private enum LightState { INCR, DECR, IDLE }
    private LightState lightState = LightState.IDLE;

    void Start()
    {
        // 연결된 컴포넌트 초기화.
        pointLight = this.GetComponent<Light>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (prevNode == null)
        {
            if (activate)
            {
                ResolveNodeState();
                UpdateColor();
            }
        }
        else
        {
            if (prevNode.activate)
            {
                activate = true;
                ResolveNodeState();
                UpdateColor();
            }
            else
            {
                activate = false;
            }
        }
    }

    // 입력 매개변수에 따라 밝기를 증가/감소 시킬지 상태를 결정.
    private void ResolveNodeState()
    {
        switch (lightState)
        {
            case LightState.INCR:
                IntensityIncrease();
                break;
            case LightState.DECR:
                IntensityDecrease();
                break;
            case LightState.IDLE:
                LightIdle();
                break;
            default:
                break;
        }
    }

    // 밝기 수준을 GUI에서 정의된 최대 수준까지 점진적으로 증가시킴.
    private void IntensityIncrease()
    {
        if (!slowProgress)
        {
            if (nextStart == false)
                nextStart = true;
        }

        intensity = maxIntensity;

        // 포인트 라이트가 활성화된 경우, 켜짐.
        if (isPointLightEn)
            pointLight.enabled = true;

        // ON 타이머가 아직 도달하지 않은 경우
        if (onTime < maxOnTime)
            onTime += onTimerSpeed * Time.deltaTime;
        // ON 타이머가 도달한 경우.
        else
        {
            onTime = 0;
            // 밝기 감소 시작.
            lightState = LightState.DECR;
        }
    }

    // 밝기 수준을 0으로 점진적으로 감소시킴.
    private void IntensityDecrease()
    {
        intensity = minIntensity;

        // 포인트 라이트 컴포넌트가 활성화된 경우, 포인트 라이트를 끔.
        if (isPointLightEn)
            pointLight.enabled = false;

        if (slowProgress)
        {
            if (nextStart == false)
                nextStart = true;
        }
        else
        {
            // 다음 노드로의 시작 신호를 중지.
            if (nextStart == true)
                nextStart = false;
        }

        // 유휴(빛 없음) 상태로 전환.
        lightState = LightState.IDLE;
    }

    // 신호를 받을 때까지 빛이 유휴 상태를 유지.
    private void LightIdle()
    {
        if (slowProgress)
        {
            if (nextStart == true)
                nextStart = false;
        }

        // 이전에 연결된 노드가 있는 경우
        if (prevNode != null)
        {
            if (prevNode.nextStart)
            {
                // 빛의 강도 증가 시작.
                lightState = LightState.INCR;
            }
        }
        // 체인의 첫 번째 노드인 경우
        else if (isFirstNode)
        {
            // 최대 유휴 시간이 아직 도달하지 않은 경우
            if (offTime < maxOffTime)
                offTime += offTimerSpeed * Time.deltaTime;
            else
            {
                offTime = 0;
                // 빛의 강도 증가 시작.
                lightState = LightState.INCR;
                // 다음 노드를 점등.
                if (nextStart == false)
                    nextStart = true;
            }
        }
    }

    // 계산된 LED 색상과 밝기 수준을 업데이트.
    private void UpdateColor()
    {
        Material mat = rend.material;
        Color baseColor = mat.color;

        // 밝기를 기반으로 최종 색상을 계산.
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(intensity);
        mat.SetColor("_EmissionColor", finalColor);
    }
}
