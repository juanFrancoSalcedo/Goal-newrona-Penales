using UnityEngine;

namespace Features.Score
{
    public interface IScoreReceptor
    {
        int Score { get; }
        void ApplyScore(Vector3 hitPoint, TypeShot typeShot);
    }
}