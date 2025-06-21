using Godot;

using System;
using System.Collections.Generic;

namespace BuisSystem.Nodes.Core {
	public class BuisComponentCore {
		private WeakRef _parentComponentAsWeakRef = null;
		public IBuisComponent ParentComponent {
			get {
				try {
					if (_parentComponentAsWeakRef == null) {
						return null;
					}

					Variant parentComponentRef = _parentComponentAsWeakRef
						.GetRef();

					if (parentComponentRef.VariantType == Variant.Type.Nil) {
						_parentComponentAsWeakRef = null;
						return null;
					}

					Node parentComponent = parentComponentRef.As<Node>();
					if (parentComponent == null || !GodotObject.IsInstanceValid(parentComponent)) {
						_parentComponentAsWeakRef = null;
						return null;
					}
					
					if (!typeof(IBuisComponent).IsAssignableFrom(parentComponent.GetType())) {
						GD.PushError("Stored ParentComponentAsWeakRef is not assignable to an IBuisComponent.");

						_parentComponentAsWeakRef = null;
						return null;
					}

					return parentComponent as IBuisComponent;
				} catch(Exception e) {
					GD.PushError("Unknown ParentComponent Getter Error ", e.ToString());
					return null;
				}
			}
		}

		private List<WeakRef> ChildrenComponentReference = new List<WeakRef>();

		private int _lastParentOrSceneTreeRelatedNotification = -1;
		public void OnNotification(Node self, int what) {
			switch ((long)what) {
				case Node.NotificationProcess: {
					_lastParentOrSceneTreeRelatedNotification = -1;
					break;
				}

				case Node.NotificationParented:
				case Node.NotificationUnparented:
				case Node.NotificationEnterTree:
				case Node.NotificationExitTree: {
					int lastRelatedNotification = _lastParentOrSceneTreeRelatedNotification;
					_lastParentOrSceneTreeRelatedNotification = what;

					if (
						what == Node.NotificationUnparented
						&& lastRelatedNotification == Node.NotificationExitTree
					) {
						return;
					}

					if (
						what == Node.NotificationEnterTree
						&& lastRelatedNotification == Node.NotificationParented
					) {
						return;
					}

					UpdateParentComponent(self);

					break;
				}
			}
		}

		private void UpdateParentComponent(Node self) {
			// Update parent reference
			if (self == null || !GodotObject.IsInstanceValid(self)) {
				return;
			}

			bool parentComponentReferenceUpdated = false;
			Node parentNode = self.GetParentOrNull<Node>();
			while (parentNode != null && GodotObject.IsInstanceValid(parentNode)) {
				if (typeof(IBuisComponent).IsAssignableFrom(parentNode.GetType())) {
					_parentComponentAsWeakRef = GodotObject.WeakRef(parentNode);
					parentComponentReferenceUpdated = true;

					break;
				}

				parentNode = parentNode.GetParentOrNull<Node>();
			}

			if (!parentComponentReferenceUpdated) {
				_parentComponentAsWeakRef = null;
			}

			if (typeof(IBuisComponent).IsAssignableFrom(self.GetType())) {
				IBuisComponent selfAsIBuisComponent = self as IBuisComponent;
				//selfAsIBuisComponent.Update();
			}
		}
	}
}
