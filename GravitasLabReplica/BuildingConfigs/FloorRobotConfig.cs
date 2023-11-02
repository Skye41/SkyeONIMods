using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class FloorRobotConfig : IBuildingConfig
    {
        public const string Id = "SkyesFloorRobot";
        public const string DisplayName = "Robotic Arm";
        public const string Description = "Gravitas Robotic Arm Replica.\n\nThe grasping robotic claw designed to assist technicians in a lab.\nOr maybe chicken-plucking, who knows?";
        public static string Effect = $"Increases {UI.FormatAsLink("Decor", "DECOR")}, contributing to {UI.FormatAsLink("Morale", "MORALE")}.";
        public const string TechBranch = "RefractiveDecor";
        public const string Category = "Furniture";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //PropGravitasFloorRobot
                width: 2,
                height: 3,
                anim: "gravitas_floor_robot_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2, //30
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2, //30f
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER1,
                construction_materials: MATERIALS.ALL_METALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1, //1600f
                build_location_rule: BuildLocationRule.OnFloor,
                decor: DECOR.BONUS.TIER0,
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
