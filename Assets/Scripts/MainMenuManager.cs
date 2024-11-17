using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public RectTransform indicator; // Drag the indicator UI element here
    public TextMeshProUGUI[] menuButtons; // Drag the menu buttons here
    private int currentIndex = 0;

    public AudioSource audioSource;
    public AudioClip navigationSound;
    public AudioClip errorSound;

    // private const string PLAY_BUTTON_NAME = "Play";
    private const string SCENE_TO_LOAD = "Game";

    public GameObject optionsMenu;
    public GameObject mainMenu;

    private bool isNavigate = true;

    public enum MenuButton
    {
        Play,
        Settings,
        Quit
    }

    void Start()
    {
        // UpdateIndicator();
   /*      Vector3 indicatorPosition = indicator.position;
        indicatorPosition.y = menuButtons[0].transform.position.y;
 */
        // Debug.Log(menuButtons[0].transform.position.y);

    }

    void Update()
    {
        if (!isNavigate)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithButton();
            // menuButtons[currentIndex].onClick.Invoke();
        }
    }

    void ChangeSelection(int direction)
    {
        currentIndex = (currentIndex + direction + menuButtons.Length) % menuButtons.Length;
        UpdateIndicator();
        PlaySound(navigationSound);
    }

    void UpdateIndicator()
    {
        // Vector3 offsetX = Vector3.right * 5f;
        Vector3 indicatorPosition = indicator.position;
        indicatorPosition.y = menuButtons[currentIndex].transform.position.y;

        indicator.position = indicatorPosition;
    }

    void InteractWithButton()
    {
        MenuButton currentButton = (MenuButton)currentIndex;

        switch (currentButton)
        {
            case MenuButton.Play:
                // Debug.Log("Interacted with: Play");
                LoadScene(SCENE_TO_LOAD);
                break;

            case MenuButton.Settings:
                // Debug.Log("Interacted with: Settings");
                // Handle Settings button interaction
                OptionsMenu();

                break;

            case MenuButton.Quit:
                // Debug.Log("Interacted with: Quit");
                PlaySound(errorSound);
                // Application.Quit(); // Quit the application (for standalone builds)
                break;

            default:
                break;
        }


        /* 
                if (menuButtons[currentIndex].name == MenuButton.Play.ToString())
                {
                    LoadScene(SCENE_TO_LOAD);
                }
                else if (menuButtons[currentIndex].name == MenuButton.Settings.ToString())
                {
                    //
                } */


        // Debug.Log("Interacted with: " + menuButtons[currentIndex].name);
        // Optionally, you can still invoke the button's onClick event
        // menuButtons[currentIndex].onClick.Invoke();
    }

    private void PlaySound(AudioClip audioClip)
    {
        if (audioSource != null && navigationSound != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void OptionsMenu()
    {
        audioSource.Stop();
        isNavigate = false;

        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        isNavigate = true;
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        PlaySound(navigationSound);

    }

}
