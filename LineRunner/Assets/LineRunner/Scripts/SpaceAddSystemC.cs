using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Scenes;
using Unity.Mathematics;

namespace LineRunner
{
    public class SpaceAddSystemC : ComponentSystem
    {
        int currentSegs;
        int maxSegs = 8;
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();
            bool LoadScene = false;

            if (config.AddC)
            {
                config.AddC = false;
                LoadScene = true;
                tinyEnv.SetConfigData(config);
            }
            if (LoadScene)
            {

                LoadScene = false;
                if (config.AddNum < config.Score)
                {
                    if (currentSegs < maxSegs)
                    {
                        SceneService.LoadSceneAsync(config.BlackSceneC);
                        config.AddNum = config.Score;
                        tinyEnv.SetConfigData(config);
                    }
                }

            }

            Entities.ForEach((Entity entity, ref blackC black, ref Translation translation) =>
            {
                if (black.IsCreated)
                    return;
                black.IsCreated = true;
                translation.Value.y = 12f;

                Entities.ForEach((DynamicBuffer<blackSegmentsC> segments) =>
                {
                    segments.Add(new blackSegmentsC
                    {
                        blacks = entity

                    });

                    currentSegs = segments.Length;

                });


            });


        }
    }
}


