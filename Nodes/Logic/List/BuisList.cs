using Godot;

using BuisSystem.Core;
using BuisSystem.Nodes.Core;

namespace BuisSystem.Nodes.Logic.List {
	[Tool]
	public partial class BuisList : Node, IBuisComponent {

#region Properties

		protected static string StartListPropertyName => "List";
		[Export]
		public string ListPropertyName {
			get {
				return _listPropertyReceptor.Name;
			}
			set {
				_listPropertyReceptor.Name = value;
			}
		}

#endregion

#region Property Receptors

		protected readonly IPropertyReceptor _listPropertyReceptor;
		
#endregion

#region Buis Core Bridge

		protected BuisComponentCore _core = new BuisComponentCore();

		public GodotNodeWeakRefWrapper<IBuisComponent> ParentComponent => _core.ParentComponent;

		public sealed override void _Notification(int what) {
			OnNotification(what);
			_core.OnNotification(this, this, what);
		}
		protected virtual void OnNotification(int what) {}

#endregion

		public BuisList() {
			// Property Receptors
			_listPropertyReceptor = _core.CreatePropertyReceptor(
				StartListPropertyName
			);
		}

	}
}
