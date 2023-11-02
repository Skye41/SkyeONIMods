using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class CryotankConfig : IBuildingConfig
    {
        public const string Id = "SkyesCryotank";
        public const string DisplayName = "Cryotank 3000";
        public const string Description = "Gravitas Cryotank 3000 Replica.\n\nThe tank appears impossibly old, but smells crisp and brand new.\n";
        public static string Effect = $"Increases {UI.FormatAsLink("Decor", "DECOR")}, contributing to {UI.FormatAsLink("Morale", "MORALE")}.";
        public const string TechBranch = "";
        public const string Category = "Furniture";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //CryoTank
                width: 2,
                height: 3,
                anim: "cryo_chamber_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2, //30
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3, //30f
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER4,
                construction_materials: MATERIALS.ALL_METALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1, //1600f
                build_location_rule: BuildLocationRule.OnFloor,
                decor: DECOR.BONUS.TIER1,
                noise: NOISE_POLLUTION.NOISY.TIER0);

            buildingDef.Floodable = false;
            buildingDef.Overheatable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.ViewMode = OverlayModes.Decor.ID;
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.DefaultAnimState = "on";
            buildingDef.PermittedRotations = PermittedRotations.FlipH;
            buildingDef.Entombable = false;
            buildingDef.ObjectLayer = ObjectLayer.Building;

            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration);//GameTags.Gravitas
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}
