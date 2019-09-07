using Unity.Entities;

namespace LineRunner
{
    public struct Player : IComponentData
    {
        public bool Move;

        public float Speed;

    }
}

