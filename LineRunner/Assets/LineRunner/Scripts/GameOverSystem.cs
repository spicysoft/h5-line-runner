using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.UIControls;

namespace LineRunner
{
    public class GameOverSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();

            Entities.ForEach((Entity entity, ref RetryButton retryButton, ref Sprite2DRenderer sprite2D) =>
            {

            });

        }
    }
}

