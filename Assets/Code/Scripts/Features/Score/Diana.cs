using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Features.Score
{

    public class Diana : MonoBehaviour, IScoreReceptor
    {
        [SerializeField] private ScoreRange[] scoreRanges;
        [SerializeField] private UnityEvent<int> OnScoreApplied;
        [Header("-- GoalKeeper --")]
        [SerializeField] private Animator animatorgoalKeeper;
        [SerializeField] private string goalKeeperAnimation;
        [Header("-- Behaviors --")]
        [SerializeField] private float pathDuration = 2f;
        [SerializeField] private float scaleStepDuration = 0.5f;
        [SerializeField] bool isLeft = false;
        private int _lastScore;
        static int dianaLevel;
        static bool fourActive;
        public int Score => _lastScore *multiplier;

        Vector3 posInit;
        Vector3 scaleInit;

        static int multiplier =1;

        private void Start()
        {
            posInit = transform.position;
            scaleInit = transform.localScale;
            multiplier = 1;
            if(isLeft)
                fourActive = Random.value <0.5f;
        }

        private void OnEnable()
        {
            EndGameManager.Instance.OnGameAttempsChanged += Instance_OnGameAttempsChanged;
            GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, ()=>dianaLevel =0);
        }

        private void OnDisable()
        {
            if(EndGameManager.Instance != null)
                EndGameManager.Instance.OnGameAttempsChanged -= Instance_OnGameAttempsChanged;
            GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, () => dianaLevel = 0);
        }

        Sequence finalSequence;
        Sequence midSequence ;
        Sequence sequence;
        private void Instance_OnGameAttempsChanged(int obj)
        {
            switch (dianaLevel)
            {
                case 1:
                    transform.DOScale(scaleInit*0.7f, pathDuration).SetEase(Ease.InOutSine);
                    for(int i=0;i<scoreRanges.Length; i++)
                    {
                        scoreRanges[i].minDistance *= 0.7f;
                        scoreRanges[i].maxDistance *= 0.7f;
                    }

                    break;
                case 2:
                    transform.DOMove(posInit + Vector3.up, pathDuration).SetEase(Ease.InOutSine);
                    multiplier++;
                break;

                case 3:
                    transform.DOKill();
                    SequenceTwo();
                    multiplier++;
                    break;
                case 4:
                    transform.DOKill();
                    midSequence = DOTween.Sequence();
                    if (isLeft && fourActive)
                        midSequence.Append(transform.DOScale(scaleInit * 0, 0.2f).SetEase(Ease.Linear));
                    else if(!isLeft && !fourActive)
                        midSequence.Append(transform.DOScale(scaleInit * 0, 0.2f).SetEase(Ease.Linear));

                    midSequence.Append(transform.DOMove(posInit + Vector3.up * 2, pathDuration).SetEase(Ease.InOutSine));
                    finalSequence.Kill();
                    break;

                case 5:
                    midSequence.Kill();
                    //transform.DOKill();
                    //sequence = DOTween.Sequence();
                    //multiplier = 3;
                    //sequence.Append(transform.DOMove(posInit + Vector3.right*2, pathDuration).SetEase(Ease.InOutSine));
                    //sequence.Append(transform.DOMove(posInit, pathDuration).SetEase(Ease.InOutSine));
                    //sequence.SetLoops(-1, LoopType.Restart);
                    break;

                default:
                    break;
            }
        }

        private void SequenceTwo() 
        {
            if (finalSequence == null)
            { 
                finalSequence = DOTween.Sequence();
                finalSequence.Append(transform.DOMove(posInit + Vector3.down, pathDuration).SetEase(Ease.Linear));
                finalSequence.Append(transform.DOMove(posInit + Vector3.up * 2, pathDuration).SetEase(Ease.Linear));
                finalSequence.Append(transform.DOMove(transform.position, pathDuration/3).SetEase(Ease.Linear));
                finalSequence.SetLoops(-1, LoopType.Restart);
            }
            //sequence.OnComplete(() =>{
            //        SequenceTwo();
            //    //if (dianaLevel == 3)
            //    //{ 
            //    //}
            //});
            //Invoke(nameof(SequenceTwo), pathDuration * 3);
        }



        public void ApplyScore(Vector3 hitPoint, TypeShot typeShot)
        {
            float distanceToCenter = Vector2.Distance(
                new Vector2(hitPoint.x, hitPoint.y),
                new Vector2(transform.position.x, transform.position.y));
            _lastScore = 0;
            dianaLevel++;
            foreach (ScoreRange range in scoreRanges)
            {
                if (distanceToCenter >= range.minDistance && distanceToCenter <= range.maxDistance)
                {
                    _lastScore = range.score;
                    break;
                }
            }
            animatorgoalKeeper.SetTrigger(goalKeeperAnimation);
            OnScoreApplied?.Invoke(_lastScore);
            ScoreMediator.Publish(ScoreEventType.ScoreApplied, _lastScore);
            ShotMediator.Publish(ShotEventType.ShotApplied, typeShot);
            print($"Diana hit! Distance: {distanceToCenter:F2} | Score: {_lastScore} | TypeShot: {typeShot}");
        }
    }
}