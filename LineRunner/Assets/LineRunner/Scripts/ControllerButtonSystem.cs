using Unity.Entities;
using Unity.Tiny.Core2D;
using Unity.Tiny.UIControls;

namespace LineRunner
{
    public class ControllerButtonSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var controlButton = false;
            Entities.ForEach((DynamicBuffer<StopPositions> stoppositions) =>
            {
            Entities.ForEach((Entity entity, ref Player player, ref Translation translation) =>
            {
                if (player.Move)
                    return;
                    Entities.WithAll<ControllerButton>().ForEach((Entity _entity, ref PointerInteraction pointerInteraction) =>
                {
                    controlButton = pointerInteraction.clicked;

                });


                var position = translation.Value;

                if (controlButton)
                {
                    player.Move = true;
                    player.Speed = 10f;
                    translation.Value.x = translation.Value.x + 0.11f;
                }

            });

            });
        }
    }
}

