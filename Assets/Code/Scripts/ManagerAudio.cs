using B_Extensions;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerAudio : Singleton<ManagerAudio>
{
    public AudioSource[] kick;
    public AudioSource startClaps;
    public AudioSource[] whistels;
    public AudioSource whistelStart;
    public AudioSource whistelEnd;
    public AudioSource[] crowds;
    public AudioSource[] cheers;
    public AudioSource select;
    public void PlayKick() => kick[Random.Range(0,kick.Length)].Play();
    public void PlayStart() => startClaps.Play();
    public void PlayWhistelRandom() => whistels[Random.Range(0, whistels.Length)].Play();
    public void PlayWhistelEnd() => whistelEnd.Play();
    public void PlayWhistelStart() => whistelStart.Play();
    public void PlayCheers() => cheers[Random.Range(0, cheers.Length)].Play();

    public void PlaySelectUI()=> select.Play();

    Coroutine crowdsCor = null;

    public void PlayCrowds() 
    {
        if(crowdsCor == null)
            crowdsCor = StartCoroutine(DoPlayCrowds());
    }

    private IEnumerator DoPlayCrowds()
    {
        AudioSource buffer  = null;
        while (true) 
        {
            var referenceAudio = crowds[Random.Range(0, crowds.Length)];
            while (ReferenceEquals(buffer, referenceAudio))
            {
                referenceAudio = crowds[Random.Range(0, crowds.Length)];
            }
            referenceAudio.Play();
            buffer = referenceAudio;
            yield return new WaitForSeconds(referenceAudio.clip.length-2);
        }
    }

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(GameEventType.GameFinished,()=>PlayWhistelEnd());
        GameStateContext.GameStateMediator.Subscribe(GameEventType.IntroCountDown, () => PlayCrowds());
    }

    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameFinished, () => PlayWhistelEnd());
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.IntroCountDown, () => PlayCrowds());
    }
}


