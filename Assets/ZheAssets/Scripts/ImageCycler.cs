using UnityEngine;
using UnityEngine.UI;

public class ImageCycler : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = 0;

    void Start()
    {
        ShowOnlyCurrentImage();
    }

    public void OnButtonClick()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        ShowOnlyCurrentImage();
    }

    private void ShowOnlyCurrentImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex);
        }
    }
}
