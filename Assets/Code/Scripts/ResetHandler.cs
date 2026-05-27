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
        callerScene.LoadScene();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))   
        {
            callerScene.LoadScene();
        }

        if(Input.anyKey)
        {
            if(resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
            }
            resetCoroutine = StartCoroutine(WaitInactivity());
        }
    }
}
