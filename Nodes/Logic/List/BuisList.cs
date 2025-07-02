using Godot;

using BuisSystem.Core;
using BuisSystem.Nodes.Core;

namespace BuisSystem.Nodes.Logic.List {
	[Tool]
	public partial class BuisList : Node, IBuisComponent {

#region Properties

		protected static string StartListPropertyName => "List";
		protected static string StartListIndexPropertyName => $"{StartListPropertyName}.Index";
		protected static string StartListItemPropertyName => $"{StartListPropertyName}.Item";
		


		[ExportGroup("Receptors")]

		[Export]
		public string ListPropertyName {
			get {
				return _listPropertyReceptor.Name;
			}
			set {
				_listPropertyReceptor.Name = value;
			}
		}



		[ExportGroup("Transmitters")]

		[Export]
		public string ListItemPropertyName {
			get {
				return _listItemPropertyTransmitter.Name;
			}
			set {
				_listItemPropertyTransmitter.Name = value;
			}
		}

		[Export]
		public bool ListIndexPropertyEnabled {
			get {
				return _listIndexPropertyTransmitter != null;
			}
			set {
				if (value == ListIndexPropertyEnabled) {
					return;
				}

				_listIndexPropertyTransmitter = value
					? _core.CreatePropertyTransmitter(
						_listIndexPropertyName
					)
					: null;
			}
		}
		
		private string _listIndexPropertyName = StartListIndexPropertyName;
		[Export]
		public string ListIndexPropertyName {
			get {
				return _listIndexPropertyName;
			}
			set {
				_listIndexPropertyName = value;

				if (_listIndexPropertyTransmitter != null) {
					_listIndexPropertyTransmitter.Name = value;
				}
			}
		}

#endregion

#region Property Receptors

		private readonly IPropertyReceptor _listPropertyReceptor;
		
#endregion

#region Property Transmitters

		private readonly IPropertyTransmitter _listItemPropertyTransmitter;
		private IPropertyTransmitter _listIndexPropertyTransmitter;

#endregion

#region Buis Core Bridge

		protected readonly BuisComponentCore _core = new BuisComponentCore();

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

			// Property Transmitters
			_listItemPropertyTransmitter = _core.CreatePropertyTransmitter(
				StartListItemPropertyName
			);
			_listIndexPropertyTransmitter = null;
		}
	}
}
