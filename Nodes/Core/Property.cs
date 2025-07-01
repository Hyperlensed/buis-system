using System;
using System.Collections.Generic;

namespace BuisSystem.Nodes.Core {
	public partial class BuisComponentCore {
		private class Property : IProperty {
			private readonly IPropertyTransmitter _propertyTransmitter;
			private readonly IPropertyReceptor _propertyReceptor;

			public Type PropertyType => typeof(int);
			public string Name {
				get {
					return _propertyTransmitter.Name;
				}
				set {
					_propertyTransmitter.Name = value;
					_propertyReceptor.Name = value;
				}
			}

			public Property(
				IPropertyTransmitter propertyTransmitter,
				IPropertyReceptor propertyReceptor
			) {
				_propertyTransmitter = propertyTransmitter;
				_propertyReceptor = propertyReceptor;
			}
		}

		private readonly List<WeakReference<Property>> _properties = [];
		private void ForEachProperties(Action<Property> action) {
			_properties.RemoveAll((propertyReference) => {
				if (propertyReference.TryGetTarget(out Property property)) {
					action.Invoke(property);
					return false;
				}

				return true;
			});
		}

		public IProperty CreateProperty(string startName) {
			IPropertyTransmitter propertyTransmitter = CreatePropertyTransmitter(startName);
			IPropertyReceptor propertyReceptor = CreatePropertyReceptor(startName);

			Property property = new Property(propertyTransmitter, propertyReceptor);

			_properties.Add(new WeakReference<Property>(
				property
			));

			return property;
		}
	}
}
