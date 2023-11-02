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
