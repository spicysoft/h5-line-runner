using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Scenes;
using Unity.Mathematics;

namespace LineRunner
{
    public class SpaceAddSystem : ComponentSystem
    {
        int currentSegs;
        int maxSegs = 8;
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();
            bool LoadScene = false;

            if (config.Add)
            {
                config.Add = false;
                LoadScene = true;
                tinyEnv.SetConfigData(config);
            }
            if (LoadScene)
            {

                LoadScene = false;
                if(config.AddNum < config.Score)
                {
                    if(currentSegs < maxSegs)
                    {
                        SceneService.LoadSceneAsync(config.BlackScene);
                        config.AddNum = config.Score;
                        tinyEnv.SetConfigData(config);
                    }
                }

            }

            Entities.ForEach((Entity entity, ref black black, ref Translation translation) =>
            {
                if (black.IsCreated)
                    return;
                black.IsCreated = true;
                translation.Value.y = 4f;

                Entities.ForEach((DynamicBuffer<blackSegments> segments) =>
                {
                        segments.Add(new blackSegments
                        {
                            blacks = entity

                        });

                        currentSegs = segments.Length;

                });


            });


        }
    }
}


