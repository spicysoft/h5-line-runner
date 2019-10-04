using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;

namespace LineRunner
{

    public class BlackMovingSystemC : ComponentSystem
    {
        private Random _randomB;
        float3 prePosition;
        float3 lastPosition;

        float distance = 0.31f;

        protected override void OnCreate()
        {
            _randomB = new Random();
            _randomB.InitState();
        }

        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();

            Entities.ForEach((DynamicBuffer<blackSegmentsC> blacksegments) =>
            {
                for (int i = 0; i < blacksegments.Length; i++)
                {
                    var translation = EntityManager.GetComponentData<Translation>(blacksegments[i].blacks);
                    var position = translation.Value;

                    if (config.Collide || !config.Start)
                        return;

                    if (i == 0)
                    {
                        position.y += -1 * World.TinyEnvironment().frameDeltaTime * config.Speed;
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

                    if (lastPosition.y < -8)
                    {
                        translation.Value.y = 4;
                        translation.Value.x = setting();

                        config.AddC = true;
                        tinyEnv.SetConfigData(config);

                    }

                    EntityManager.SetComponentData(blacksegments[i].blacks, translation);

                    tinyEnv.SetConfigData(config);


                }

            });



            int setting()
            {


                int num = _randomB.NextInt(0, 3);

                switch (num)
                {
                    case 0:
                        num = 0;
                        break;

                    case 1:
                        num = 2;
                        break;

                    case 2:
                        num = -2;
                        break;
                }

                return num;

            }

        }
    }
}


