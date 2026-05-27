using B_Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace Services
{
    public class CsvPlayerSaver
    {
        private static string FileName => $"goal-{DateTime.Now:yyyy-MM-dd}.csv";
        private static string FilePath => Path.Combine(Application.persistentDataPath, FileName);

        private static bool fileInitialized;

        public static void Save(PlayerData playerData)
        {

            Debug.Log($"Player Data: {playerData == null}");
            Debug.Log($"Saving player data: {playerData.nombre}, Score: {playerData.score}, Time: {playerData.tiempo}");
            try
            {
                if (!fileInitialized)
                {
                    if (!File.Exists(FilePath))
                    {
                        using (StreamWriter writer = new StreamWriter(FilePath, false, Encoding.UTF8))
                        {
                            writer.WriteLine("uid,nombre,correo,telefono,score,tiempo");
                        }
                    }
                    fileInitialized = true;
                }

                using (StreamWriter writer = new StreamWriter(FilePath, true, Encoding.UTF8))
                {
                    string line = $"{EscapeField(playerData.uid)},{EscapeField(playerData.nombre)},{EscapeField(playerData.correo)},{EscapeField(playerData.telefono)},{playerData.score},{playerData.tiempo.ToString(CultureInfo.InvariantCulture)}";
                    writer.WriteLine(line);
                }

                Debug.Log($"Player data saved to {FilePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save player data: {e.Message}");
            }
        }

        public static List<PlayerData> GetSavedPlayers()
        {
            var players = new List<PlayerData>();

            try
            {
                if (!File.Exists(FilePath)) return players;

                string[] lines = File.ReadAllLines(FilePath, Encoding.UTF8);

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    string[] fields = ParseCsvLine(line);
                    if (fields.Length < 5) continue;

                    string uid = fields[0];
                    string nombre = fields[1];
                    string correo = fields[2];
                    string telefono = fields[3];
                    int score = int.TryParse(fields[4], NumberStyles.Integer, CultureInfo.InvariantCulture, out int s) ? s : 0;
                    float tiempo = fields.Length >= 6 && float.TryParse(fields[5], NumberStyles.Float, CultureInfo.InvariantCulture, out float t) ? t : 0f;

                    players.Add(new PlayerData(uid, nombre, correo, telefono, score, tiempo));
                }

                players.Sort();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load player data: {e.Message}");
            }

            return players;
        }

        private static string[] ParseCsvLine(string line)
        {
            var fields = new List<string>();
            bool inQuotes = false;
            var current = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (inQuotes)
                {
                    if (c == '"' && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else if (c == '"')
                    {
                        inQuotes = false;
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else if (c == ',')
                    {
                        fields.Add(current.ToString());
                        current.Clear();
                    }
                    else
                    {
                        current.Append(c);
                    }
                }
            }

            fields.Add(current.ToString());
            return fields.ToArray();
        }

        private static string EscapeField(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }
}
