using Godot;

namespace BuisUISystem.Nodes.Core {
	public class BuisComponentCore {
		public WeakRef ParentComponentReference { get; private set; } = null;

		private int _lastParentOrSceneTreeRelatedNotification = -1;
		public void OnNotification(Node self, int what) {
			switch ((long)what) {
				case Node.NotificationProcess: {
					_lastParentOrSceneTreeRelatedNotification = -1;
					break;
				}
				case Node.NotificationParented: {
					UpdateParentComponent(self);

					_lastParentOrSceneTreeRelatedNotification = what;
					break;
				}
				case Node.NotificationUnparented: {
					if (_lastParentOrSceneTreeRelatedNotification != Node.NotificationExitTree) {
						UpdateParentComponent(self);
					}

					_lastParentOrSceneTreeRelatedNotification = what;
					break;
				}
				case Node.NotificationEnterTree: {
					if (_lastParentOrSceneTreeRelatedNotification != Node.NotificationParented) {
						UpdateParentComponent(self);
					}
					
					_lastParentOrSceneTreeRelatedNotification = what;
					break;
				}
				case Node.NotificationExitTree: {
					UpdateParentComponent(self);

					_lastParentOrSceneTreeRelatedNotification = what;
					break;
				}
			}
		}

		private void UpdateParentComponent(Node self) {
			if (self == null || !GodotObject.IsInstanceValid(self)) {
				return;
			}

			bool parentComponentReferenceUpdated = false;
			Node parentNode = self.GetParentOrNull<Node>();
			while (parentNode != null && GodotObject.IsInstanceValid(parentNode)) {
				if (typeof(IBuisComponent).IsAssignableFrom(parentNode.GetType())) {
					ParentComponentReference = GodotObject.WeakRef(parentNode);
					parentComponentReferenceUpdated = true;

					break;
				}

				parentNode = parentNode.GetParentOrNull<Node>();
			}

			if (!parentComponentReferenceUpdated) {
				ParentComponentReference = null;
			}

			UpdateComponent(self);
		}

		private void UpdateComponent(Node self) {
			if (typeof(IBuisComponent).IsAssignableFrom(self.GetType())) {
				IBuisComponent selfAsIBuisComponent = self as IBuisComponent;
				selfAsIBuisComponent.Update();
			}
		}
	}
}
