using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class LabWallConfig : IBuildingConfig
    {
        public const string Id = "SkyesLabWall";
        public const string DisplayName = "Lab Wall";
        public const string Description = "Gravistas Lab Wall Replica.\n\nA regular wall similar to the ones that once existed in a working lab.";
        public static string Effect = $"Increases {UI.FormatAsLink("Decor", "DECOR")}, contributing to {UI.FormatAsLink("Morale", "MORALE")}.";
        public const string TechBranch = "";
        public const string Category = "Utilities";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //PropGravitasLabWall
                width: 2,
                height: 3,
                anim: "gravitas_lab_wall_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2, //30
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2, //30f
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER0,
                construction_materials: MATERIALS.RAW_MINERALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1, //1600f
                build_location_rule: BuildLocationRule.NotInTiles,
                decor: DECOR.BONUS.TIER0,
                noise: NOISE_POLLUTION.NONE);

            buildingDef.Floodable = false;
            buildingDef.Overheatable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.ViewMode = OverlayModes.Decor.ID;
            buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
            buildingDef.DefaultAnimState = "on";
            buildingDef.PermittedRotations = PermittedRotations.R90;
            buildingDef.Entombable = false;
            buildingDef.ObjectLayer = ObjectLayer.Backwall;

            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration);//GameTags.Gravitas

            go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
            go.AddComponent<ZoneTile>();
            go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass);
            go.GetComponent<PrimaryElement>().Temperature = 273f;
            
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}
