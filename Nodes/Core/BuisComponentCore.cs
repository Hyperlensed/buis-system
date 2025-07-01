using Godot;

using System;
using System.Collections.Generic;

using BuisSystem.Core;

namespace BuisSystem.Nodes.Core {
	public partial class BuisComponentCore {
		public readonly GodotNodeWeakRefWrapper<IBuisComponent> ParentComponent = new GodotNodeWeakRefWrapper<IBuisComponent>();
		
		private readonly List<GodotNodeWeakRefWrapper<IBuisComponent>> _childrenComponents = new List<GodotNodeWeakRefWrapper<IBuisComponent>>();
		public IEnumerable<GodotNodeWeakRefWrapper<IBuisComponent>> ChildrenComponents => _childrenComponents;

		private int _lastParentOrSceneTreeRelatedNotification = -1;
		public void OnNotification(Node self, IBuisComponent selfAsComponent, int what) {
			if (self == null || !GodotObject.IsInstanceValid(self)) {
				GD.PrintErr("BuisComponentCore: The provided node is Null or Invalid.");
				return;
			}

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

					Update(self);

					break;
				}
			}
		}

		private void Update(Node self) {
			UpdateParent(self);
			UpdatePropertyReceptorsProperties();
		}

		private void UpdateParent(Node self) {
			Node parentNode = self.GetParentOrNull<Node>();
			while (!ParentComponent.TrySetValueAsNode(parentNode, out var result, true)) {
				if (result == GodotNodeWeakRefWrapper.TrySetValueAsNodeResult.InvalidNode) {
					return;
				}

				parentNode = parentNode.GetParentOrNull<Node>();
			}
		}

		private void UpdatePropertyReceptorsProperties() {
			
		}
	}
}
