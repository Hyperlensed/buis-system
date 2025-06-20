#define HYPERLENSED_BUIS_UI_SYSTEM_PLUGIN

using Godot;

#if TOOLS
using BuisUISystem.Preview;
#endif

namespace BuisUISystem {
	[Tool]
    public partial class BuisUISystemPlugin : EditorPlugin {
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
			// Script buisLogicListScript = GD.Load<Script>(
			// 	((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/BuisLogicList.cs"
			// );
			// Texture2D buisLogicListIcon = GD.Load<Texture2D>(
			// 	((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/BuisLogicList.svg"
			// );
			// AddCustomType("BuisLogicList", "Control", buisLogicListScript, buisLogicListIcon);

			// Script buisListInsertionCarretScript = GD.Load<Script>(
			// 	((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/BuisLogicListInsertionCarret.cs"
			// );
			// AddCustomType("BuisLogicListInsertionCarret", "Node", buisListInsertionCarretScript, null);

			Script buisValueBinderScript = GD.Load<Script>(
				((Resource)GetScript()).ResourcePath.GetBaseDir() + "/Nodes/Logic/Components/ValueBinder/BuisValueBinder.cs"
			);
			AddCustomType("BuisValueBinder", "Node", buisValueBinderScript, null);
        }
        private void UnregisterCustomTypes() {
			// RemoveCustomType("BuisLogicList");
			// RemoveCustomType("BuisLogicListInsertionCarret");
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
