using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace LineRunner
{
    public struct GameConfig : IComponentData
    {
        public int Score;
        public int BestScore;
        public float Speed;

        public bool Collide;
        public bool Start;
        public bool GameOver;
        public bool Retry;

    

    }
}

