using Godot;

using BuisSystem.Nodes.Core;

namespace BuisSystem.Nodes.Logic.List.InsertionMarker {
	public enum BuisListInsertionMarkerInsertionMode {
		/// <summary>
		/// Allows any item to be inserted without restrictions.
		/// </summary>
		Unrestricted,

		/// <summary>
		/// Allows only items that exist in a predefined allow list to be inserted.
		/// </summary>
		AllowList,
		
		/// <summary>
		/// Allows any item to be inserted, except for those that exist in a predefined deny list.
		/// </summary>
		DenyList
	}

	[Tool]
	public partial class BuisListInsertionMarker : Node, IBuisComponent {
		[Export]
		public bool InsertItemsBelow = false;

		private BuisListInsertionMarkerInsertionMode _insertionMode = BuisListInsertionMarkerInsertionMode.Unrestricted;
		[Export]
		public BuisListInsertionMarkerInsertionMode InsertionMode {
			get {
				return _insertionMode;
			}
			set {
				if (value == _insertionMode) {
					return;
				}

				_insertionMode = value;
				NotifyPropertyListChanged();
			}
		}
		[Export]
		public PackedScene[] ComponentsAllowList = [];
		[Export]
		public PackedScene[] ComponentsDenyList = [];

#region Buis Component Core Bridge

		protected BuisComponentCore _core = new BuisComponentCore();

		public IBuisComponent ParentComponent => _core.ParentComponent;

		public sealed override void _Notification(int what) {
			OnNotification(what);
			_core.OnNotification(this, what);
		}
		protected virtual void OnNotification(int what) {}

#endregion

		public override void _ValidateProperty(Godot.Collections.Dictionary property) {
			string propertyName = property["name"].AsStringName();

			if (propertyName == PropertyName.ComponentsAllowList && _insertionMode != BuisListInsertionMarkerInsertionMode.AllowList) {
				PropertyUsageFlags usage = property["usage"].As<PropertyUsageFlags>() & ~PropertyUsageFlags.Editor;
				property["usage"] = (int)usage;
			} else if (propertyName == PropertyName.ComponentsDenyList && _insertionMode != BuisListInsertionMarkerInsertionMode.DenyList) {
				PropertyUsageFlags usage = property["usage"].As<PropertyUsageFlags>() & ~PropertyUsageFlags.Editor;
				property["usage"] = (int)usage;
			}
		}

		public void InsertListItem() {

		}
	}
}
