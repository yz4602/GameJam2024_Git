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
	
	// create image2 with a size of 1
	private Image[] image2 = new Image[1];

	// public text box for pokemon name
	public TextMeshProUGUI pokemonNameText;
	

	void Start()
	{
		// show green frog if first player selected Pikachu, otherwise show Pikachu, remove the unshown character
		if (SelectedPokemon.Instance.playerAPokemon == "Pikachu")
		{
			//images[0].gameObject.SetActive(false);
			//images[1].gameObject.SetActive(true);
			//currentIndex = 1;
			images[1].gameObject.SetActive(true);
			image2[0] = images[1];
			images = image2;
			//change the text of the pokemon name
			pokemonNameText.text = images[currentIndex].gameObject.name;
		}
		else
		{
			//images[0].gameObject.SetActive(true);
			//images[1].gameObject.SetActive(false);
			//currentIndex = 0;

			images[0].gameObject.SetActive(true);
			image2[0] = images[0];
			images = image2;

			//change the text of the pokemon name
			pokemonNameText.text = images[currentIndex].gameObject.name;

		}

		



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
