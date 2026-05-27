using Features.Score;
using UnityEngine;

[RequireComponent(typeof(BaseDoAnimationController))]
public class CrowdHandler : MonoBehaviour
{
    [SerializeField] private ScoreEventType eventType;
    [SerializeField] private float _time;
    BaseDoAnimationController animaController;
    private void OnEnable() => ScoreMediator.Subscribe(eventType, OnTotalScoreChanged);
    private void OnDisable() => ScoreMediator.Unsubscribe(eventType, OnTotalScoreChanged);

    private void Awake()
    {
        animaController = GetComponent<BaseDoAnimationController>();
    }
    private void OnTotalScoreChanged(int totalScore)
    {
        animaController.SetInloop(true);
        animaController.ActiveAnimation(0);
        Invoke(nameof(StopAnimation),_time);
    }

    private void StopAnimation() 
    {
        animaController.SetInloop(false);
    }
}