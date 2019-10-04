using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Scenes;
using Unity.Tiny.UIControls;

namespace LineRunner
{
    public class RetryButtonSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();
            var retryButton = false;

            Entities.WithAll<RetryButton>().ForEach((Entity entity, ref PointerInteraction pointerInteraction, ref Sprite2DRenderer sprite2D) =>
            {
                retryButton = pointerInteraction.clicked;

                if (!config.GameOver)
                {
                    sprite2D.color.a = 0;
                }

                if (config.GameOver)
                {
                    sprite2D.color.a = 1;
                }

                Entities.ForEach((Entity _entity, ref GameOver gameover, ref Sprite2DRenderer _sprite2D) =>
                {

                    if (!config.GameOver)
                    {
                        _sprite2D.color.a = 0;
                    }

                    if (config.GameOver)
                    {
                        _sprite2D.color.a = 1;
                    }


                });

            });



            if (retryButton)
            {

                config.Retry = true;
                config.GameOver = false;

                SceneService.UnloadAllSceneInstances(config.BlackScene);
                SceneService.UnloadAllSceneInstances(config.BlackSceneB);
                SceneService.UnloadAllSceneInstances(config.BlackSceneC);

                Entities.ForEach((DynamicBuffer<blackSegments> blacksegments) =>
                {
                    var blacks = blacksegments.Reinterpret<Entity>().ToNativeArray((Unity.Collections.Allocator.Temp));
                    for (int i = 0; i < blacksegments.Length; i++)
                    {
                        blacksegments.Clear();
                        blacks.Dispose();
                    }

                });

                Entities.ForEach((DynamicBuffer<blackSegmentsB> blacksegments) =>
                {
                    var blacks = blacksegments.Reinterpret<Entity>().ToNativeArray((Unity.Collections.Allocator.Temp));
                    for (int i = 0; i < blacksegments.Length; i++)
                    {
                        blacksegments.Clear();
                        blacks.Dispose();
                    }

                });


                Entities.ForEach((DynamicBuffer<blackSegmentsC> blacksegments) =>
                {
                    var blacks = blacksegments.Reinterpret<Entity>().ToNativeArray((Unity.Collections.Allocator.Temp));
                    for (int i = 0; i < blacksegments.Length; i++)
                    {
                        blacksegments.Clear();
                        blacks.Dispose();
                    }

                });



                tinyEnv.SetConfigData(config);
            }

        }
    }

}


