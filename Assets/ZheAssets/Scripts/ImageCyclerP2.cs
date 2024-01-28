using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ImageCyclerP2 : MonoBehaviour
{
	public Image[] images;
	private int currentIndex = 0;
	private string selectedPokemon1;
    public TextMeshProUGUI pokemonNameText2;
    public GameObject pokeball;
	public GameObject light;

	public GameObject character;

	private int count = 0;
	

	void Start()
	{
		ShowOnlyCurrentImage();
	}

	public void OnNextButtonClick()
	{
		SoundMgr.Instance.PlaySound("ClickSound", false);
		currentIndex = (currentIndex + 1) % images.Length;
		ShowOnlyCurrentImage();
        pokemonNameText2.text = images[currentIndex].gameObject.name;
	}

	public void OnPreviousButtonClick()
	{
		SoundMgr.Instance.PlaySound("ClickSound", false);
		currentIndex--;
		if (currentIndex < 0)
		{
			currentIndex = images.Length - 1;
		}
		ShowOnlyCurrentImage();
		pokemonNameText2.text = images[currentIndex].gameObject.name;
	}

	public void OnSelectButtonClick()
	{
		if(count == 0){
			SoundMgr.Instance.PlaySound("ClickSound", false);
			SoundMgr.Instance.PlaySound("PokeballCapture2", false);
			selectedPokemon1 = images[currentIndex].gameObject.name; // get the name of the selected Pokémon
			Debug.Log("Selected Pokémon: " + selectedPokemon1);
			SelectedPokemon.Instance.playerBPokemon = selectedPokemon1; // set the selected Pokémon to the singleton

			// activate the pokeball game object
			pokeball.SetActive(true);
			//anim.enabled = true;
			StartCoroutine(WaitAndPrint(1));
			count++;
		}
		//UIManager.Instance.HidePanel("Player1SelectPanel");
		//UIManager.Instance.ShowPanel<Player2SelectPanel>("Player2SelectPanel");
	}

    
    IEnumerator WaitAndPrint(float waitTime)
    {
        // Before waiting
        Debug.Log("Start Wait 2");

        // Wait for the specified number of seconds
        yield return new WaitForSeconds(waitTime);

		light.SetActive(true);
        //yield return new WaitForSeconds(1);
        // wait for 0.4 seconds
        yield return new WaitForSeconds(0.8f);
		// hide the character
		character.SetActive(false);
		SoundMgr.Instance.StopBKMusic();
		// wait for 0.4 seconds before loading the next scene
		yield return new WaitForSeconds(1.4f);

	

        UIManager.Instance.HidePanel("Player2SelectPanel");
        ScenesMgr.Instance.LoadScene("MotionScene");
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
