using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
namespace LineRunner
{

    public class PlayerFallSystem : ComponentSystem
    {
        float speed = 0.5f;
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();

            if (!config.Collide || !config.Start)
                return;

            Entities.ForEach((Entity entity, ref Player player, ref NonUniformScale scale) =>
            {
                if (config.Retry)
                {
                    scale.Value = 0.25f;
                }
                if (scale.Value.x >= 0)
                {
                    scale.Value -= speed * World.TinyEnvironment().frameDeltaTime;
                }
                if (scale.Value.x <= 0)
                {
                    config.GameOver = true;
                    tinyEnv.SetConfigData(config);
                }


            });
        }
    }
}
