namespace BuisSystem.Nodes.Core {
	public interface IProperty {
		public System.Type PropertyType { get; }
		public string Name { get; set; }
	}
}
