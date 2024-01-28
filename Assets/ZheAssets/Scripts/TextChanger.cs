using UnityEngine;
using UnityEngine.UI; // Include the UI namespace
using UnityEngine.EventSystems;

public class TextChanger : MonoBehaviour
{
    // Reference to the TextMesh Pro component
    public TMPro.TextMeshProUGUI displayText;

    private int clickCount = 0; // To keep track of clicks

    // Function to change text
    public void ChangeText()
    {
        SoundMgr.Instance.PlaySound("ClickSound", false);
        clickCount++;
        switch (clickCount)
        {
            case 1:
                displayText.text = "People live with them, play with them, and work with them.";
                break;
            case 2:
                displayText.text = "Most importantly, we fight with them.";
                break;
            case 3:
                displayText.text = "The Pal Fight Tournament is the most popular sport in the world.";
                break;
            case 4:
                displayText.text = "With our pals by our side, we can do anything, we have nothing to fear.";
                break;
            case 5:
                displayText.text = "Let's fight to be the champion of the Pal Fight Tournament!";
                break;
            case 6:
                displayText.text = "Let's go!";
                break;
            case 7:
                UIManager.Instance.HidePanel("SelectPanel");
                UIManager.Instance.ShowPanel<Player1SelectPanel>("Player1SelectPanel");
                break;
                

            // Add more cases as needed

        }
    }
}
