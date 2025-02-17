using BoneLib.BoneMenu;
using HarmonyLib;
using Il2CppSLZ.Marrow;
using MelonLoader;
using System.Reflection;

[assembly: MelonInfo(typeof(VirtualStockDelete.VirtualStockDelete), "VirtualStockDelete", "1.0.0", "Bhijn", null)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonAuthorColor(255, 198, 119, 230)]
[assembly: MelonAdditionalDependencies("BoneLib")]

[assembly: AssemblyDescription("Deletes Bonelab's virtual stocks from existence")]

namespace VirtualStockDelete;

public class VirtualStockDelete : MelonMod
{
    public static MelonPreferences_Category StockedCategory { get; private set; }
    public static MelonPreferences_Entry<bool> MelonVirtualStockDisabled { get; private set; }

    public static Page StockedPage { get; private set; }
    public static BoolElement BonedVirtualStockDisabled { get; private set; }

    public static bool VirtualStockDisabled { get; private set; }
    public static bool ProperlyInitialized { get; private set; }

    public override void OnInitializeMelon()
    {
        StockedCategory = MelonPreferences.CreateCategory("VirtualStockDelete");
        MelonVirtualStockDisabled = StockedCategory.CreateEntry("VirtualStockDisabled", true, "Virtual Stock Disabled");
        VirtualStockDisabled = MelonVirtualStockDisabled.Value;

        StockedPage = Page.Root.CreatePage("Virtual Stock Delete", UnityEngine.Color.white);
        BonedVirtualStockDisabled = StockedPage.CreateBool("Virtual Stock Disabled", UnityEngine.Color.white, VirtualStockDisabled, OnStockToggle);

        ProperlyInitialized = true;

        LoggerInstance.Msg("Initialized.");
    }

    public override void OnPreferencesLoaded()
    {
        if (!ProperlyInitialized)
            return;

        VirtualStockDisabled = MelonVirtualStockDisabled.Value;
        BonedVirtualStockDisabled.Value = VirtualStockDisabled;
    }

    public static void OnStockToggle(bool value)
    {
        VirtualStockDisabled = value;
        MelonVirtualStockDisabled.Value = value;
        StockedCategory.SaveToFile(false);
    }

    [HarmonyPatch(typeof(RifleVirtualController), "OnVirtualControllerSolve")]
    public static class RifleSolve
    {
        public static bool Prefix(VirtualControlerPayload payload)
        {
            if (VirtualStockDisabled)
            {
                return false;
            }
            return true;
        }
    }
}