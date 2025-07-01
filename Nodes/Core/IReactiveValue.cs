using System;

namespace BuisSystem.Nodes.Core {
	public interface IReactiveValue {
		public bool Value { get; }
			
		public event EventHandler Discarded;
		public event EventHandler ValueUpdated;
	}
}
