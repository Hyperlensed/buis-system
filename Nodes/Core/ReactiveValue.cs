using System;
using System.Collections.Generic;

namespace BuisSystem.Nodes.Core {
	public partial class BuisComponentCore {
		public class ReactiveValue : IReactiveValue {
			public bool Value;

			private readonly List<WeakReference<EventHandler>> _discardedEventHandlers = [];
			public event EventHandler ReactiveValueChanged {
				add {
					_discardedEventHandlers.Add(new WeakReference<EventHandler>(
						value
					));
				}
				remove {
					_discardedEventHandlers.RemoveAll(
						(WeakReference<EventHandler> discardedEventHandlerReference) => {
							if (discardedEventHandlerReference.TryGetTarget(out EventHandler discardedEventHandler)) {
								return value == discardedEventHandler;
							} else {
								return true;
							}
						}
					);
				}
			}
		}
	}
}