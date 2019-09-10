using Unity.Entities;

namespace LineRunner
{
    public struct GameConfig : IComponentData
    {
        public int Score;
        public bool Collide;
    }
}

