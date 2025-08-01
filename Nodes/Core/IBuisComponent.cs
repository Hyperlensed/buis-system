using BuisSystem.Core;

namespace BuisSystem.Nodes.Core {
	public interface IBuisComponent {
		public ReadonlyGodotNodeWeakRefWrapper<IBuisComponent> ParentComponent { get; }
	}
}
