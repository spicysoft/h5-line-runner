using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Text;

namespace LineRunner
{
    public class ScoreSystem : ComponentSystem
    {
        float Time = 0;
        float ScoreSpan = 2;
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();

            if (config.Retry)
            {
                config.Score = 0;
                config.Speed = 2;
                Time = 0;
                tinyEnv.SetConfigData(config);
            }
            if (config.Collide || !config.Start)
                return;

            Time += 1 * World.TinyEnvironment().frameDeltaTime;
            if (Time > ScoreSpan)
            {
                config.Score += 10;
                Time = 0;
                tinyEnv.SetConfigData(config);
                if (config.Speed < 6f)
                {
                    config.Speed += 0.05f;
                    tinyEnv.SetConfigData(config);
                }
            }

            if (config.BestScore <= config.Score)
            {
                config.BestScore = config.Score;
                tinyEnv.SetConfigData(config);
            }

            Entities.WithAll<Score>().ForEach((Entity entity) =>
            {
                int score = config.Score;
                EntityManager.SetBufferFromString<TextString>(entity, "Score:" + score.ToString());


            });

            Entities.WithAll<BestScore>().ForEach((Entity entity) =>
            {
                int score = config.BestScore;
                EntityManager.SetBufferFromString<TextString>(entity, "Best score:" + score.ToString());


            });
        }

    }

}
