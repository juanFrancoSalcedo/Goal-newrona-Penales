using System;

namespace Services
{
    [Serializable]
    public class PlayerData : IComparable<PlayerData>
    {
        public string uid;
        public string nombre;
        public string correo;
        public string telefono;
        public int score;
        public float tiempo;

        public PlayerData(string uid, string nombre, string correo, string telefono, int score = 0, float tiempo = 0)
        {
            this.uid = uid;
            this.nombre = nombre;
            this.correo = correo;
            this.telefono = telefono;
            this.score = score;
            this.tiempo = tiempo;
        }

        public int CompareTo(PlayerData other)
        {
            return other.score.CompareTo(this.score);
        }
    }
}