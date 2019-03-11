﻿using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoeHUD.Framework;
using PoeHUD.Hud.Settings;
using TreeRoutine.Menu;

namespace TreeRoutine.Routine.BuildYourOwnRoutine.Extension.Default.Conditions
{
    internal class IsKeyPressedCondition : ExtensionCondition
    {

        private int Key { get; set; }
        private const String keyString = "key";

        public IsKeyPressedCondition(string owner, string name) : base(owner, name)
        {

        }

        public override void Initialise(Dictionary<String, Object> Parameters)
        {
            base.Initialise(Parameters);

            Key = ExtensionComponent.InitialiseParameterInt32(keyString, Key, ref Parameters);
        }

        public override bool CreateConfigurationMenu(ExtensionParameter extensionParameter, ref Dictionary<String, Object> Parameters)
        {



            ImGui.TextDisabled("Condition Info");
            ImGuiExtension.ToolTip("Hotkey to check for pressed down state");

            base.CreateConfigurationMenu(extensionParameter, ref Parameters);

            Key = (int)ImGuiExtension.HotkeySelector("Hotkey", (Keys)Key);
            ImGuiExtension.ToolTip("Hotkey to press for this action.");
            Parameters[keyString] = Key.ToString();

            return true;
        }

        public override Func<bool> GetCondition(ExtensionParameter extensionParameter)
        {
            return () =>
            {
                bool retVal = WinApi.IsKeyDown((Keys) Key);
                ///extensionParameter.Plugin.Log($"Evaluated condition: {retVal}", 3);
                return retVal;
            };
        }

        public override string GetDisplayName(bool isAddingNew)
        {
            string displayName = "Can Use Flask";

            if (!isAddingNew)
            {
                displayName += " [";
                displayName += ("Key=" + Key);
                displayName += "]";

            }

            return displayName;
        }
    }
}
