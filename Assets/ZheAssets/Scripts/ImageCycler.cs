using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
using System.Collections;
using System;

public class ImageCycler : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = 0;
    private string selectedPokemon1;

    public TextMeshProUGUI pokemonNameText;

    Animation anim;

    public GameObject pokeball;

    void Start()
    {
        ShowOnlyCurrentImage();
    }

    public void OnNextButtonClick()
    {
        currentIndex = (currentIndex + 1) % images.Length;
        ShowOnlyCurrentImage();
        pokemonNameText.text = images[currentIndex].gameObject.name;
    }

    public void OnSelectButtonClick()
    {
        selectedPokemon1 = images[currentIndex].gameObject.name; // get the name of the selected Pokémon
        Debug.Log("Selected Pokémon: " + selectedPokemon1);
        SelectedPokemon.Instance.playerAPokemon = selectedPokemon1; // set the selected Pokémon to the singleton

        // activate the pokeball game object
        pokeball.SetActive(true);

        //anim.enabled = true;
        StartCoroutine(WaitAndPrint(1));
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        // Before waiting
        Debug.Log("Start Wait");

        // Wait for the specified number of seconds
        yield return new WaitForSeconds(waitTime);
        UIManager.Instance.HidePanel("Player1SelectPanel");
        UIManager.Instance.ShowPanel<Player2SelectPanel>("Player2SelectPanel");
        // After waiting
        Debug.Log("End Wait");
        yield return null;
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
