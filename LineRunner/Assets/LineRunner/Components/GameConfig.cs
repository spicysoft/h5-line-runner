using Unity.Entities;
using Unity.Tiny.Scenes;

namespace LineRunner
{
    public struct GameConfig : IComponentData
    {
        
        public int Score;
        public int BestScore;
        public int AddNum;
        public float Speed;

        public bool Collide;
        public bool Start;
        public bool GameOver;
        public bool Retry;

        public bool Add;
        public bool AddB;
        public bool AddC;

        public SceneReference BlackScene;
        public SceneReference BlackSceneB;
        public SceneReference BlackSceneC;
    

    }
}

