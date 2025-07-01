using System;
using System.Collections.Generic;

namespace BuisSystem.Nodes.Core {
	public partial class BuisComponentCore {

#region Management

		private class PropertyReceptor : IPropertyReceptor {
			private readonly WeakReference<BuisComponentCore> _core;



			private string _name;
			public string Name {
				get {
					return _name;
				}
				set {
					if (value == _name) {
						return;
					}

					string oldName = _name;
					_name = value;

					OnNameChange(oldName, value);
				}
			}
			private void OnNameChange(string oldName, string newName) {
				if (_core.TryGetTarget(out BuisComponentCore core)) {
					core.DecrementPropertyUseCount(oldName);
					core.IncrementPropertyUseCount(newName);
				};
			}



			public PropertyReceptor(string startName, BuisComponentCore core) {
				_name = startName;
				_core = new WeakReference<BuisComponentCore>(
					core
				);

				core.IncrementPropertyUseCount(startName);
			}
			~PropertyReceptor() {
				if (_core.TryGetTarget(out BuisComponentCore core)) {
					core.DecrementPropertyUseCount(_name);
				}
			}
		}



		private readonly List<WeakReference<PropertyReceptor>> _propertyReceptorsReference = [];
		private void ForEachPropertyReceptors(Action<PropertyReceptor> action) {
			_propertyReceptorsReference.RemoveAll((propertyReceptorReference) => {
				if (propertyReceptorReference.TryGetTarget(out PropertyReceptor propertyReceptor)) {
					action.Invoke(propertyReceptor);
					return false;
				}

				return true;
			});
		}



		private readonly Dictionary<string, int> _propertiesUseCount = new Dictionary<string, int>();
		private void IncrementPropertyUseCount(string propertyName) {
			if (_propertiesUseCount.TryAdd(propertyName, 1)) {
				// Add
			} else {
				_propertiesUseCount[propertyName]++;
			}
		}
		private void DecrementPropertyUseCount(string propertyName) {
			if (_propertiesUseCount.ContainsKey(propertyName)) {
				_propertiesUseCount[propertyName]--;

				if (_propertiesUseCount[propertyName] <= 0) {
					// Remove
				}
			}
		}



		//private readonly Dictionary<string, 

#endregion

#region Creation

		public IPropertyReceptor CreatePropertyReceptor(string startName) {
			PropertyReceptor propertyReceptor = new PropertyReceptor(startName, this);

			_propertyReceptorsReference.Add(new WeakReference<PropertyReceptor>(
				propertyReceptor
			));

			return propertyReceptor;
		}

#endregion

	}
}
