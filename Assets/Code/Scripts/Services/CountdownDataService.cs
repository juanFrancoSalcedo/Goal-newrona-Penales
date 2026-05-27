using UnityEngine;

namespace Services
{
    public static class CountdownDataService
    {
        private const float DefaultDuration = 5f;

        public static void SaveDuration(float duration)
        {
            PlayerPrefs.SetFloat(KeyStorage.CountdownDuration, duration);
            PlayerPrefs.Save();
        }

        public static float LoadDuration()
        {
            return PlayerPrefs.GetFloat(KeyStorage.CountdownDuration, DefaultDuration);
        }

        public static bool HasDuration() => PlayerPrefs.HasKey(KeyStorage.CountdownDuration);
    }
}