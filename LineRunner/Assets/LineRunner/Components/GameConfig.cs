using Unity.Entities;

namespace LineRunner
{
    public struct GameConfig : IComponentData
    {
        public int Score;
        public float Speed;

        public bool Collide;
        public bool Start;
        public bool GameOver;
        public bool Retry;

    

    }
}

