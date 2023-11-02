using HarmonyLib;
using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GravitasLabReplica
{
    public class GravitasLabReplicaPatches
    {
        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public class Db_Initialize_Patch
        {
            public static readonly IEnumerable<Type> Buildings = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.Namespace == "GravitasLabReplica.BuildingConfigs");

            public static void Prefix()
            {
                Debug.Log("Adding Skye's Gravitas Buildings");

                foreach (var t in Buildings)
                {
                    var Id = t.GetField("Id")?.GetValue(null)?.ToString();
                    var DisplayName = t.GetField("DisplayName")?.GetValue(null)?.ToString();
                    var Description = t.GetField("Description")?.GetValue(null)?.ToString();
                    var Effect = t.GetField("Effect")?.GetValue(null)?.ToString();
                    var Category = t.GetField("Category")?.GetValue(null)?.ToString();

                    AddBuildingStrings(Id, DisplayName, Description, Effect);
                    ModUtil.AddBuildingToPlanScreen(Category, Id);
                }

                //AddBuildingStrings(LabWallConfig.Id, LabWallConfig.DisplayName, LabWallConfig.Description, LabWallConfig.Effect);
                //AddBuildingStrings(LabWindowVConfig.Id, LabWindowVConfig.DisplayName, LabWindowVConfig.Description, LabWindowVConfig.Effect);
                //AddBuildingStrings(LabWindowHConfig.Id, LabWindowHConfig.DisplayName, LabWindowHConfig.Description, LabWindowHConfig.Effect);
                //AddBuildingStrings(WallConfig.Id, WallConfig.DisplayName, WallConfig.Description, WallConfig.Effect);

                //AddBuildingStrings(CeilingRobotConfig.Id, CeilingRobotConfig.DisplayName, CeilingRobotConfig.Description, CeilingRobotConfig.Effect);
                //AddBuildingStrings(DeskPodiumConfig.Id, DeskPodiumConfig.DisplayName, DeskPodiumConfig.Description, DeskPodiumConfig.Effect);
                //AddBuildingStrings(EletronicDisplayConfig.Id, EletronicDisplayConfig.DisplayName, EletronicDisplayConfig.Description, EletronicDisplayConfig.Effect);
                //AddBuildingStrings(FirstAidKitConfig.Id, FirstAidKitConfig.DisplayName, FirstAidKitConfig.Description, FirstAidKitConfig.Effect);
                //AddBuildingStrings(FloorRobotConfig.Id, FloorRobotConfig.DisplayName, FloorRobotConfig.Description, FloorRobotConfig.Effect);

                //AddBuildingStrings(HandScannerConfig.Id, HandScannerConfig.DisplayName, HandScannerConfig.Description, HandScannerConfig.Effect);
                //AddBuildingStrings(LabShelfConfig.Id, LabShelfConfig.DisplayName, LabShelfConfig.Description, LabShelfConfig.Effect);
                //AddBuildingStrings(LabDeskConfig.Id, LabDeskConfig.DisplayName, LabDeskConfig.Description, LabDeskConfig.Effect);
                //AddBuildingStrings(PosterConfig.Id, PosterConfig.DisplayName, PosterConfig.Description, PosterConfig.Effect);
                //AddBuildingStrings(RoboticTableConfig.Id, RoboticTableConfig.DisplayName, RoboticTableConfig.Description, RoboticTableConfig.Effect);

                // GameStrings.PlanMenuCategory.Furniture
                //ModUtil.AddBuildingToPlanScreen("Utilities", LabWallConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Utilities", LabWindowVConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Utilities", LabWindowHConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Utilities", WallConfig.Id);

                //ModUtil.AddBuildingToPlanScreen("Furniture", CeilingRobotConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", DeskPodiumConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", EletronicDisplayConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", FirstAidKitConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", FloorRobotConfig.Id);

                //ModUtil.AddBuildingToPlanScreen("Furniture", HandScannerConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", LabShelfConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", LabDeskConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", PosterConfig.Id);
                //ModUtil.AddBuildingToPlanScreen("Furniture", RoboticTableConfig.Id); 
            }

            public static void Postfix()
            {
                foreach (var t in Buildings)
                {
                    var Id = t.GetField("Id")?.GetValue(null)?.ToString();
                    var TechBranch = t.GetField("TechBranch")?.GetValue(null)?.ToString();

                    if (!TechBranch.IsNullOrWhiteSpace())
                        Db.Get().Techs.Get(TechBranch).unlockedItemIDs.Add(Id);
                }
                Debug.Log(Buildings.Count() + " Skye's Gravitas Buildings added successfully");
            }

            private static void AddBuildingStrings(string buildingId, string name, string description, string effect)
            {
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{buildingId.ToUpperInvariant()}.NAME", UI.FormatAsLink(name, buildingId));
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{buildingId.ToUpperInvariant()}.DESC", description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{buildingId.ToUpperInvariant()}.EFFECT", effect);
            }
        }
    }
}
