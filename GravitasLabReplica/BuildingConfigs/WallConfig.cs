using STRINGS;
using System.Collections.Generic;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class WallConfig : IBuildingConfig
    {
        public const string Id = "SkyesLabWallTiny";
        public const string DisplayName = "Gravitas Wall";
        public const string Description = "Gravitas Lab Wall Replica. \n\nThe wall similar to the ones of a once-great scientific facility.";
        public static string Effect = $"Increases {UI.FormatAsLink("Decor", "DECOR")}, contributing to {UI.FormatAsLink("Morale", "MORALE")}.";
        public const string TechBranch = "";
        public const string Category = "Utilities";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //PropGravitasWall
                width: 1,
                height: 1,
                anim: "gravitas_walls_kanim",
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
            buildingDef.DefaultAnimState = "off";
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.Entombable = false;
            buildingDef.ObjectLayer = ObjectLayer.Backwall;
            buildingDef.ReplacementLayer = ObjectLayer.ReplacementBackwall;
            buildingDef.ReplacementCandidateLayers = new List<ObjectLayer>()
            {
                ObjectLayer.FoundationTile,
                ObjectLayer.Backwall
            };
            buildingDef.ReplacementTags = new List<Tag>()
            {
              GameTags.FloorTiles,
              GameTags.Backwall
            };
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
            go.AddComponent<ZoneTile>();
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
            
            //go.GetComponent<KPrefabID>().AddTag(GameTags.Backwall);//GameTags.Gravitas

            //go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
            //go.AddComponent<ZoneTile>();
            //go.GetComponent<PrimaryElement>().SetElement(SimHashes.Granite);
            //go.GetComponent<PrimaryElement>().Temperature = 273f;

            //BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Backwall);
            GeneratedBuildings.RemoveLoopingSounds(go);
        }
    }
}
