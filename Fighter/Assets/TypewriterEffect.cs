using System.Collections;
using TMPro; // Nếu dùng TextMeshPro
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textComponent; // Kéo TMP_Text vào đây
    public string fullText = "An era of darkness has descended, as traitors have stolen the kingdom's peace.As a legendary knight, your mission is to rise, defend your honor, and crush every foe on the blood-soaked battlefield.Only victory will etch your name into history!";
    public float totalTime = 3f; // Thời gian hoàn thành toàn bộ hiệu ứng
    private float typingSpeed;

    private void Start()
    {
        // Tính toán tốc độ hiển thị dựa trên tổng thời gian và độ dài text
        typingSpeed = totalTime / fullText.Length;
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed); // Chờ trước khi hiển thị ký tự tiếp theo
        }
    }
}