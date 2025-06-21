using Godot;

using BuisSystem.Nodes.Core;

namespace BuisSystem.Nodes.Logic.ValueBinder {
	[Tool]
	public partial class BuisValueBinder : Node, IBuisComponent {
		public enum UpdateModes {
			Ok,
			NotOk
		}

		private void OnValueNodePropertyNameUpdated() {

		}
		private string _valueNodePropertyName = "";
		[Export]
		public string ValueNodePropertyName {
			get {
				return _valueNodePropertyName;
			}
			set {
				if (value == _valueNodePropertyName) {
					return;
				}

				_valueNodePropertyName = value;
				OnValueNodePropertyNameUpdated();
			}
		}

		[Export]
		public UpdateModes UpdateMode;

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
