using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Scenes;

namespace LineRunner
{
    public class BlackResetSystem : ComponentSystem
    {
        private Random _random;
        float3 prePosition;
        float3 lastPosition;

        float3 prePositionB;
        float3 lastPositionB;

        float3 prePositionC;
        float3 lastPositionC;

        float distance = 0.31f;

        protected override void OnCreate()
        {
            _random = new Random();
            _random.InitState();
        }

        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();

            Entities.ForEach((DynamicBuffer<blackSegments> blacksegments) =>
            {
                for (int i = 0; i < blacksegments.Length; i++)
                {
                    var translation = EntityManager.GetComponentData<Translation>(blacksegments[i].blacks);
                    var position = translation.Value;

                    if (!config.Retry)
                        return;

                    config.Speed = 2;

                    if (i == 0)
                    {
                        position.y = 4;
                        translation.Value = position;
                        prePosition = position;

                    }
                    else if (i > 0)
                    {
                        prePosition.y += distance;
                        translation.Value = prePosition;


                        if (i == blacksegments.Length - 1)
                        {
                            lastPosition = position;
                        }
                    }
                    
                    EntityManager.SetComponentData(blacksegments[i].blacks, translation);

                    tinyEnv.SetConfigData(config);


                }

            });

      
        }
    }
}

