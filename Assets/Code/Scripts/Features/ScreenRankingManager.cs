using Features;
using UnityEngine;

public class ScreenRankingManager: MonoBehaviour
{
    [SerializeField] private GameObject canvasEnd;

    private void OnEnable()
    {
        EndGameManager.Instance.OnGameEnd += End;
    }

    private void OnDisable()
    {
        EndGameManager.Instance.OnGameEnd -= End;
    }
    private void End()
    {
        canvasEnd.SetActive(true);
    }
}