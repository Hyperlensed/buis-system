using System;
using System.Collections.Generic;

namespace BuisSystem.Nodes.Core {
	public partial class BuisComponentCore {
		private class PropertyTransmitter : IPropertyTransmitter {
			private string _name;
			public string Name {
				get {
					return _name;
				}
				set {
					if (value == _name) {
						return;
					}

					_name = value;
					NameChanged.Invoke(this, value);
				}
			}
			public event EventHandler<string> NameChanged;

			public PropertyTransmitter(string startName) {
				_name = startName;
			}
		}

		private readonly List<WeakReference<PropertyTransmitter>> _propertyTransmittersReference = [];
		private void ForEachPropertyTransmitters(Action<PropertyTransmitter> action) {
			_propertyTransmittersReference.RemoveAll((propertyTransmittersReference) => {
				if (propertyTransmittersReference.TryGetTarget(out PropertyTransmitter propertyTransmitter)) {
					action.Invoke(propertyTransmitter);
					return false;
				}

				return true;
			});
		}

		public IPropertyTransmitter CreatePropertyTransmitter(string startName) {
			PropertyTransmitter propertyTransmitter = new PropertyTransmitter(startName);

			_propertyTransmittersReference.Add(new WeakReference<PropertyTransmitter>(
				propertyTransmitter
			));

			return propertyTransmitter;
		}
	}
}