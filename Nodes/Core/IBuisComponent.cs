using Godot;

namespace BuisSystem.Nodes.Core {
	public interface IBuisComponent {
		public IBuisComponent ParentComponent { get; }
	}
}
