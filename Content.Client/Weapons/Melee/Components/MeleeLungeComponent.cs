using Robust.Client.GameObjects;
using Robust.Shared.GameObjects;
using Robust.Shared.Maths;

namespace Content.Client.Weapons.Melee.Components
{
    [RegisterComponent]
    public sealed class MeleeLungeComponent : Component
    {
        public override string Name => "MeleeLunge";

        private const float ResetTime = 0.3f;
        private const float BaseOffset = 0.25f;

        private Angle _angle;
        private float _time;

        public void SetData(Angle angle)
        {
            _angle = angle;
            _time = 0;
        }

        public void Update(float frameTime)
        {
            _time += frameTime;

            var offset = Vector2.Zero;
            var deleteSelf = false;

            if (_time > ResetTime)
            {
                deleteSelf = true;
            }
            else
            {
                offset = _angle.RotateVec((0, -BaseOffset));
                offset *= (ResetTime - _time) / ResetTime;
            }

            if (Owner.TryGetComponent(out ISpriteComponent? spriteComponent))
            {
                spriteComponent.Offset = offset;
            }

            if (deleteSelf)
            {
                Owner.RemoveComponent<MeleeLungeComponent>();
            }
        }
    }
}
