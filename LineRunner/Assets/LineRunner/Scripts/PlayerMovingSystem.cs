using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
namespace LineRunner
{
    public class PlayerMovingSystem : ComponentSystem
    {

        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();

            if (config.Collide || !config.Start)
                return;

            

            Entities.ForEach((DynamicBuffer<StopPositions> stoppositions) =>
            {
                Entities.ForEach((Entity entity, ref Player player, ref Translation translation) =>
                {
                var position = translation.Value;





                position.x += World.TinyEnvironment().frameDeltaTime * player.Speed;
                translation.Value.x = position.x;
                    for (int i = 0; i < stoppositions.Length; i++)
                    {
                        if (translation.Value.x <= stoppositions[i].position.x + 0.1 && translation.Value.x >= stoppositions[i].position.x - 0.1)
                        {
                            player.Speed = 0;
                            translation.Value.x = stoppositions[i].position.x;
                            player.Move = false;


                        }
                    }




                if (translation.Value.x >= 3f)
                {
                    translation.Value.x = -3f;
                }

                });


            });
        }
    }
}

