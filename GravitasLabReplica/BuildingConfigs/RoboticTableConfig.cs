using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class RoboticTableConfig : IBuildingConfig
    {
        public const string Id = "SkyesRoboticTable";
        public const string DisplayName = "Robotics Research Table";
        public const string Description = "Gravitas Robotics Research Table Replica. \n\nThe work space of an extinct robotics technician who left behind some unfinished prototypes.\nNo one knows what those prototypes are supposed to be.";
        public static string Effect = $"Increases {UI.FormatAsLink("Decor", "DECOR")}, contributing to {UI.FormatAsLink("Morale", "MORALE")}.";
        public const string TechBranch = "RefractiveDecor";
        public const string Category = "Furniture";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //PropGravitasRobitcTable
                width: 3,
                height: 3,
                anim: "gravitas_robotic_table_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2, //30
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3, //30f
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER4,//new[] { 100f, 300f },
                construction_materials: MATERIALS.ALL_METALS,//new[] { MATERIALS.WOOD, MATERIALS.METAL }, 
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
