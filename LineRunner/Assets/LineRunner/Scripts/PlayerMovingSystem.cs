using Unity.Entities;
using Unity.Tiny.Core;
using Unity.Tiny.Core2D;
namespace LineRunner
{
    public class PlayerMovingSystem : ComponentSystem
    {

        protected override void OnUpdate()
        {


            Entities.ForEach((Entity entity, ref Player player, ref Translation translation) =>
            {
                var position = translation.Value;





                position.x += World.TinyEnvironment().frameDeltaTime * player.Speed;
                translation.Value.x = position.x;


                if (translation.Value.x <= 0.1 && translation.Value.x >= -0.1)
                {
                    player.Speed = 0;
                    translation.Value.x = 0;
                    player.Move = false;


                }


                else if (translation.Value.x >= 3.5f)
                {
                    translation.Value.x = -3.5f;
                }


            });
        }
    }
}

