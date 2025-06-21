using Godot;

namespace BuisSystem.Nodes.Logic.List.InsertionCarret {
	[Tool]
	public partial class BuisListInsertionCarret : Node, IBuisListInsertionCarret {
		[Export]
		public bool ShouldInsertBelow { get; set; } = false;
	}
}
