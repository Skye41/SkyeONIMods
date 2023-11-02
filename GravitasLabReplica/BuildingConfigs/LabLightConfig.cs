using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class LabLightConfig : IBuildingConfig
    {
        public const string Id = "SkyesLabLight";
        public const string DisplayName = "LED Light";
        public const string Description = "Gravitas LED Light Replica.\n\nAn overhead light therapy lamp designed to soothe the minds.";
        public static string Effect = "An overhead light therapy lamp designed to soothe the minds.";
        public const string TechBranch = "";
        public const string Category = "Furniture";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //GravitasLabLight
                width: 1,
                height: 1,
                anim: "gravitas_lab_light_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2, //30
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3, //30f
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER1,
                construction_materials: MATERIALS.ALL_METALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1, //1600f
                build_location_rule: BuildLocationRule.OnCeiling,
                decor: DECOR.NONE,
                noise: NOISE_POLLUTION.NONE);

            buildingDef.Floodable = false;
            buildingDef.Overheatable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.ViewMode = OverlayModes.Light.ID;
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.DefaultAnimState = "on";
            buildingDef.PermittedRotations = PermittedRotations.FlipH;
            buildingDef.Entombable = false;
            buildingDef.ObjectLayer = ObjectLayer.Building;

            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 10f;
            buildingDef.SelfHeatKilowattsWhenActive = 0.5f;

            return buildingDef;
        }


        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource);//GameTags.Gravitas
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            LightShapePreview lightShapePreview = go.AddComponent<LightShapePreview>();
            lightShapePreview.lux = 1800;
            lightShapePreview.radius = 10f;
            lightShapePreview.shape = LightShape.Cone;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LoopingSounds>();
            Light2D light2D = go.AddOrGet<Light2D>();
            light2D.overlayColour = LIGHT2D.CEILINGLIGHT_OVERLAYCOLOR;
            light2D.Color = LIGHT2D.CEILINGLIGHT_COLOR;
            light2D.Range = 10f;
            light2D.Angle = 2.6f;
            light2D.Direction = LIGHT2D.CEILINGLIGHT_DIRECTION;
            light2D.Offset = LIGHT2D.CEILINGLIGHT_OFFSET;
            light2D.shape = LightShape.Cone;
            light2D.drawOverlay = true;
            light2D.Lux = 1800;
            go.AddOrGetDef<LightController.Def>();
        }
    }
}
