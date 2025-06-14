using Godot;

namespace BuisUISystem.Nodes.Core {
	public interface IBuisComponent {
		public WeakRef ParentComponent { get; }
		
		public void Update();
	}
}
