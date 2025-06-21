using Godot;
using BuisSystem.Nodes.Core;

namespace BuisSystem.Nodes.Logic.List {
	[Tool]
	public partial class BuisList : Node, IBuisComponent {
		
#region Buis Component Core Bridge

		protected BuisComponentCore _core = new BuisComponentCore();

		public IBuisComponent ParentComponent => _core.ParentComponent;

		public sealed override void _Notification(int what) {
			OnNotification(what);
			_core.OnNotification(this, what);
		}
		protected virtual void OnNotification(int what) {}

#endregion

	}
}
