using UnityEngine;
using UnityEngine.UI;

public class ImageCyclerP2 : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = 0;
    private string selectedPokemon1;

    void Start()
    {
        ShowOnlyCurrentImage();
    }

    public void OnNextButtonClick()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        ShowOnlyCurrentImage();
    }

    public void OnSelectButtonClick()
    {
        selectedPokemon1 = images[currentIndex].gameObject.name; // get the name of the selected Pokémon
        Debug.Log("Selected Pokémon: " + selectedPokemon1);
        SelectedPokemon.Instance.playerBPokemon = selectedPokemon1; // set the selected Pokémon to the singleton
        //UIManager.Instance.HidePanel("Player1SelectPanel");
        //UIManager.Instance.ShowPanel<Player2SelectPanel>("Player2SelectPanel");
    }

    private void ShowOnlyCurrentImage()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex);
        }
    }

    // get the selected Pokémon
    public string GetSelectedPokemon1()
    {
        return selectedPokemon1;
    }
}
