#define HYPERLENSED_BUIS_SYSTEM_PLUGIN

using Godot;

#if TOOLS
using BuisSystem.Preview;
#endif

namespace BuisSystem {
	[Tool]
    public partial class BuisSystemPlugin : EditorPlugin {
        public override void _EnterTree() {
            RegisterCustomTypes();
			RegisterCustomResources();

#if TOOLS
			PreviewData.Initialize(
				((Resource)GetScript()).ResourcePath.GetBaseDir()
			);
#endif
        }
        public override void _ExitTree() {
            UnregisterCustomTypes();
			UnregisterCustomResources();
        }

#region Custom Types
        private void RegisterCustomTypes() {
			// List
			Script buisListScript = GD.Load<Script>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/BuisList.cs"
			);
			Texture2D buisListIcon = GD.Load<Texture2D>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/BuisList.svg"
			);
			Texture2D buisListNode2DIcon = GD.Load<Texture2D>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/BuisListNode2D.svg"
			);
			Texture2D buisListNode3DIcon = GD.Load<Texture2D>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/BuisListNode3D.svg"
			);
			Texture2D buisListControlIcon = GD.Load<Texture2D>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/BuisListControl.svg"
			);
			AddCustomType("BuisList", "Node", buisListScript, buisListIcon);
			AddCustomType("BuisList (Node2D)", "Node2D", buisListScript, buisListNode2DIcon);
			AddCustomType("BuisList (Node3D)", "Node3D", buisListScript, buisListNode3DIcon);
			AddCustomType("BuisList (Control)", "Control", buisListScript, buisListControlIcon);

			// Buis List Insertion Marker
			Script buisListInsertionMarkerScript = GD.Load<Script>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/InsertionMarker/BuisListInsertionMarker.cs"
			);
			Texture2D buisListInsertionMarkerIcon = GD.Load<Texture2D>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/List/InsertionMarker/BuisListInsertionMarker.svg"
			);
			AddCustomType("BuisListInsertionMarker", "Node", buisListInsertionMarkerScript, buisListInsertionMarkerIcon);

			// Value Binder
			Script buisValueBinderScript = GD.Load<Script>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/ValueBinder/BuisValueBinder.cs"
			);
			Texture2D buisValueBinderIcon = GD.Load<Texture2D>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/ValueBinder/BuisValueBinder.svg"
			);
			AddCustomType("BuisValueBinder", "Node", buisValueBinderScript, buisValueBinderIcon);
        }
        private void UnregisterCustomTypes() {
			RemoveCustomType("BuisList");
			RemoveCustomType("BuisList (Node2D)");
			RemoveCustomType("BuisList (Node3D)");
			RemoveCustomType("BuisList (Control)");

			RemoveCustomType("BuisListInsertionMarker");

			RemoveCustomType("BuisValueBinder");
        }
#endregion

#region Custom Types
        private void RegisterCustomResources() {

        }
        private void UnregisterCustomResources() {

        }
#endregion

    }
}
