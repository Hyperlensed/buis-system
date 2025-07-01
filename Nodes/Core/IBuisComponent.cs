using Godot;

using BuisSystem.Core;

namespace BuisSystem.Nodes.Core {
	public interface IBuisComponent {
		public GodotNodeWeakRefWrapper<IBuisComponent> ParentComponent { get; }
	}
}
