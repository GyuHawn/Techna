using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public bool isCursorVisible; // 마우스 커서 활성화 여부
    public Sprite customCursorSprite; // 커서 이미지로 사용할 스프라이트
    private Texture2D customCursorTexture; // Texture2D로 변환된 커서 이미지

    void Start()
    {
        // Sprite를 Texture2D로 변환
        if (customCursorSprite != null)
        {
            customCursorTexture = SpriteToTexture2D(customCursorSprite);
            Cursor.SetCursor(customCursorTexture, Vector2.zero, CursorMode.Auto); // 커서 이미지 설정
        }

        isCursorVisible = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        OffCursorVisibility(); // 마우스 커서 비/활성화
    }

    private void OffCursorVisibility() // 커서 비활성화
    {
        if (Input.GetButtonDown("CursorHide")) // 키 입력 감지
        {
            isCursorVisible = !isCursorVisible; // 마우스 포인터 활성화 여부
            Cursor.visible = isCursorVisible; // 마우스 포인터 활성화 상태 설정
            Cursor.lockState = isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked; // 마우스 포인터 잠금 상태 설정
        }
    }

    // Sprite를 Texture2D로 변환하는 함수
    private Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
