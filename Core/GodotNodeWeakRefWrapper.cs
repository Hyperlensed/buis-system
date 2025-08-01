using Godot;

using System;
using System.Diagnostics.CodeAnalysis;

namespace BuisSystem.Core {

	public static class GodotNodeWeakRefWrapper {
		public enum TrySetValueResult {
			Success,
			InvalidNode,
			NotAssignableToNodeType,
			UnknownError
		}
		public enum TrySetValueAsNodeResult {
			Success,
			InvalidNode,
			NotAssignableToProvidedType,
			UnknownError
		}
	}

	public sealed class GodotNodeWeakRefWrapper<T>
		where T : class
	{
		private WeakRef _valueWeakRef = null;

#region Set

		public bool TrySetValue(T newValue, bool defaultToNull = false) {
			return TrySetValue(newValue, out _, defaultToNull);
		}
		public bool TrySetValue(T newValue, out GodotNodeWeakRefWrapper.TrySetValueResult result, bool defaultToNull = false) {
			try {
				if (newValue == null) {
					_valueWeakRef = null;

					result = GodotNodeWeakRefWrapper.TrySetValueResult.Success;
					return true;
				}

				if (!typeof(Node).IsAssignableFrom(newValue.GetType())) {
					if (defaultToNull) {
						_valueWeakRef = null;
					}

					result = GodotNodeWeakRefWrapper.TrySetValueResult.NotAssignableToNodeType;
					return false;
				}

				Node newValueAsNode = newValue as Node;

				if (!GodotObject.IsInstanceValid(newValueAsNode)) {
					if (defaultToNull) {
						_valueWeakRef = null;
					}

					result = GodotNodeWeakRefWrapper.TrySetValueResult.InvalidNode;
					return false;
				}

				_valueWeakRef = GodotObject.WeakRef(newValueAsNode);

				result = GodotNodeWeakRefWrapper.TrySetValueResult.Success;
				return true;
			} catch (Exception exception) {
				if (defaultToNull) {
					_valueWeakRef = null;
				}

				GD.PrintErr("GodotWeakRefWrapper: Unknown Exception\n", exception.ToString());

				result = GodotNodeWeakRefWrapper.TrySetValueResult.UnknownError;
				return false;
			}
		}

		public bool TrySetValueAsNode(Node newValue, bool defaultToNull = false) {
			return TrySetValueAsNode(newValue, out _, defaultToNull);
		}
		public bool TrySetValueAsNode(Node newValue, out GodotNodeWeakRefWrapper.TrySetValueAsNodeResult result, bool defaultToNull = false) {
			try {
				if (newValue == null) {
					_valueWeakRef = null;

					result = GodotNodeWeakRefWrapper.TrySetValueAsNodeResult.Success;
					return true;
				}

				if (!typeof(T).IsAssignableFrom(newValue.GetType())) {
					if (defaultToNull) {
						_valueWeakRef = null;
					}

					result = GodotNodeWeakRefWrapper.TrySetValueAsNodeResult.NotAssignableToProvidedType;
					return false;
				}

				if (!GodotObject.IsInstanceValid(newValue)) {
					if (defaultToNull) {
						_valueWeakRef = null;
					}

					result = GodotNodeWeakRefWrapper.TrySetValueAsNodeResult.InvalidNode;
					return false;
				}

				_valueWeakRef = GodotObject.WeakRef(newValue);

				result = GodotNodeWeakRefWrapper.TrySetValueAsNodeResult.Success;
				return true;
			} catch (Exception exception) {
				if (defaultToNull) {
					_valueWeakRef = null;
				}

				GD.PrintErr("GodotWeakRefWrapper: Unknown Exception\n", exception.ToString());

				result = GodotNodeWeakRefWrapper.TrySetValueAsNodeResult.UnknownError;
				return false;
			}
		}

#endregion

#region Get

		public bool TryGetValue([MaybeNullWhen(false)][NotNullWhen(true)] out T value) {
			try {
				if (TryGetValueAsNode(out Node valueAsNode)) {
					if (!typeof(T).IsAssignableFrom(valueAsNode.GetType())) {
						GD.PushError("GodotWeakRefWrapper: The stored value can't be converted to it's associated type.");

						_valueWeakRef = null;

						value = null;
						return false;
					}

					value = valueAsNode as T;
					return true;
				} else {
					value = null;
					return false;
				}
			} catch (Exception exception) {
				GD.PrintErr("GodotWeakRefWrapper: Unknown Exception\n", exception.ToString());

				value = null;
				return false;
			}
		}
		public bool TryGetValueAsNode([MaybeNullWhen(false)][NotNullWhen(true)] out Node value) {
			try {
				if (_valueWeakRef == null) {
					value = null;
					return true;
				}

				Variant valueRef = _valueWeakRef
					.GetRef();

				if (valueRef.VariantType == Variant.Type.Nil) {
					_valueWeakRef = null;
					
					value = null;
					return false;
				}

				Node valueAsNode = valueRef.As<Node>();
				if (valueAsNode == null || !GodotObject.IsInstanceValid(valueAsNode)) {
					_valueWeakRef = null;

					value = null;
					return false;
				}

				value = valueAsNode;
				return true;
			} catch (Exception exception) {
				GD.PrintErr("GodotWeakRefWrapper: Unknown Exception\n", exception.ToString());

				value = null;
				return false;
			}
		}

#endregion

		public ReadonlyGodotNodeWeakRefWrapper<T> GetAsReadonly() {
			return new ReadonlyGodotNodeWeakRefWrapper<T>(this);
		}
	}

	public sealed class ReadonlyGodotNodeWeakRefWrapper<T>
		where T : class
	{
		private readonly GodotNodeWeakRefWrapper<T> _sourceGodotNodeWeakRefWrapper;

		public ReadonlyGodotNodeWeakRefWrapper(GodotNodeWeakRefWrapper<T> sourceGodotNodeWeakRefWrapper) {
			_sourceGodotNodeWeakRefWrapper = sourceGodotNodeWeakRefWrapper;
		}

		public bool TryGetValue([MaybeNullWhen(false)][NotNullWhen(true)] out T value) {
			return _sourceGodotNodeWeakRefWrapper.TryGetValue(out value);
		}
		public bool TryGetValueAsNode([MaybeNullWhen(false)][NotNullWhen(true)] out Node value) {
			return _sourceGodotNodeWeakRefWrapper.TryGetValueAsNode(out value);
		}
	}
}