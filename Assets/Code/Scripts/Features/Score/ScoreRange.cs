using System;

namespace Features.Score
{
    [Serializable]
    public struct ScoreRange
    {
        public float minDistance;
        public float maxDistance;
        public int score;
    }
}