using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.ViewVariables;

namespace Content.Server.Construction.Components
{
    [RegisterComponent]
    public class ComputerBoardComponent : Component
    {
        public override string Name => "ComputerBoard";

        [ViewVariables]
        [DataField("prototype")]
        public string? Prototype { get; private set; }
    }
}
