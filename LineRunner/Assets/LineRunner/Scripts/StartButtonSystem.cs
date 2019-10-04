using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
using Unity.Tiny.Scenes;
using Unity.Tiny.UIControls;


namespace LineRunner
{
    public class StartButtonSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var tinyEnv = World.TinyEnvironment();
            var config = World.TinyEnvironment().GetConfigData<GameConfig>();
            var startButton = false;
            
            Entities.WithAll<StartButton>().ForEach((Entity entity, ref PointerInteraction pointerInteraction , ref Sprite2DRenderer sprite2D) =>
            {
                startButton = pointerInteraction.clicked;

                if (config.Start)
                {
                    sprite2D.color.a = 0;
                }
                if (config.Retry)
                {
                    sprite2D.color.a = 1;
                }

                Entities.ForEach((Entity _entity, ref Title title, ref Sprite2DRenderer _sprite2D) =>
                {

                    if (config.Start)
                    {
                        _sprite2D.color.a = 0;
                    }
                    if (config.Retry)
                    {
                        _sprite2D.color.a = 1;
                    }

                });

            });



            if (startButton)
            {
                config.Start = true;
                config.Retry = false;
                config.Collide = false;
                for (int n = 0; n < 5; n++)
                {
                    SceneService.LoadSceneAsync(config.BlackScene);
                    SceneService.LoadSceneAsync(config.BlackSceneB);
                    SceneService.LoadSceneAsync(config.BlackSceneC);
                }


                tinyEnv.SetConfigData(config);
            }

        }
    }
    
}

