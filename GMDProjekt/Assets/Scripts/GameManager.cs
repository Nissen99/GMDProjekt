
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    //TODO: This is a huge hack. I could not find a way to rebake bake the navmesh when the door was destroyed. 
    // So to make the player not be able to move there i removed the "GROUND" tag from the floor in the room,
    // and when the player is allowed to go there i have to set the tag og the ground again.
    // This is ofcource a horriable way of doing it, and it has created som bugs, but it will do for now :P 
    public GameObject GroundToSetTagOn;
    public GameObject DoorToOpen;
    public string SoundClipWhenWon;
    public string SoundClipWhenLost;
    public int NumberKilledElitesToEnter;
    public Image blackImage;
    public float fadeDuration = 3f;
    private int _currentNumberOfElitesKilled;
    private UIQuestTextManager _questTextManager;

    private const string _messageToTellPlayerWhenDoorBroken = "Door has been destroyed go face the boss";

    private void Start()
    {
        _questTextManager = FindObjectOfType<UIQuestTextManager>();
        updateQuestNoFinished();
    }

    public void EliteKilled()
    {
        _currentNumberOfElitesKilled++;
        if (_currentNumberOfElitesKilled >= NumberKilledElitesToEnter)
        {
            GroundToSetTagOn.tag = TAGS.GROUND_TAG;
            Destroy(DoorToOpen.gameObject);
            changeQuestText(_messageToTellPlayerWhenDoorBroken);
        }
        else
        {
            updateQuestNoFinished();
        }
    }

    public void BossKilled()
    {
        FindObjectOfType<AudioManager>().Play(SoundClipWhenWon);

        StartCoroutine(FadeOut(true));
    }

    public void GameLost()
    {
        FindObjectOfType<AudioManager>().Play(SoundClipWhenLost);
        StartCoroutine(FadeOut(false));
    }
    
    private void changeQuestText(string newText)
    {
        _questTextManager.SetQuestText(newText);
    }

    private void updateQuestNoFinished()
    {
        changeQuestText($"To open the door and face LUCIFER, you need to slay his most Loyal minions \n" +
                        $"Remaining: {NumberKilledElitesToEnter - _currentNumberOfElitesKilled}");
    }
    
    IEnumerator FadeOut(bool  won)
    {
        float elapsedTime = 0f;
        Color color = blackImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            blackImage.color = color;
            yield return null;
        }

        if (won)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
