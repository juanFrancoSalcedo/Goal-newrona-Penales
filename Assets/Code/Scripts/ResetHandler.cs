using B_Extensions.SceneLoader;
using Services;
using System.Collections;
using UnityEngine;

public class ResetHandler : MonoBehaviour
{
    [SerializeField] CallerSceneLoader callerScene;
    [SerializeField] private float inactivityTime = 50f;
    Coroutine resetCoroutine;
    void Start()
    {
        resetCoroutine = StartCoroutine(WaitInactivity());
    }

    IEnumerator WaitInactivity()
    {
        yield return new WaitForSeconds(inactivityTime);
        ResetGame();
    }

    public void ResetGame() 
    {
        callerScene.LoadScene();
        GameStateContext.ChangeState(GameEventType.Tutorial);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))   
        {
            ResetGame();
        }

        if(Input.anyKey || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if(resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(WaitInactivity());
        }
    }
}
