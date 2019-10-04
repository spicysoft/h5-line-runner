using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Mathematics;

namespace LineRunner
{
    public class CollideSystem : ComponentSystem
    {
        float collide = 0.125f;
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();
            Entities.ForEach((Entity moveEntity, ref Player player, ref Translation playerTransform) =>
            {
                float3 playerTrans = playerTransform.Value;
                float3 blackTrans = new float3();
                Entities.ForEach((Entity wallEntity, ref black _black, ref Translation blackTransform) =>
                {
                    blackTrans = blackTransform.Value;

                    if (math.distance(playerTrans, blackTrans) < collide)
                    {
                        config.Collide = true;
                        tinyEnv.SetConfigData(config);
                    }


                });


            });


            Entities.ForEach((Entity moveEntity, ref Player player, ref Translation playerTransform) =>
            {
                float3 playerTrans = playerTransform.Value;
                float3 blackTrans = new float3();
                Entities.ForEach((Entity wallEntity, ref blackB _black, ref Translation blackTransform) =>
                {
                    blackTrans = blackTransform.Value;

                    if (math.distance(playerTrans, blackTrans) < collide)
                    {
                        config.Collide = true;
                        tinyEnv.SetConfigData(config);
                    }


                });


            });

            Entities.ForEach((Entity moveEntity, ref Player player, ref Translation playerTransform) =>
            {
                float3 playerTrans = playerTransform.Value;
                float3 blackTrans = new float3();
                Entities.ForEach((Entity wallEntity, ref blackC _black, ref Translation blackTransform) =>
                {
                    blackTrans = blackTransform.Value;

                    if (math.distance(playerTrans, blackTrans) < collide)
                    {
                        config.Collide = true;
                        tinyEnv.SetConfigData(config);
                    }


                });


            });



        }
    }
}

