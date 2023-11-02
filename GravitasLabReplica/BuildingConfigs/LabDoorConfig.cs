using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace GravitasLabReplica.BuildingConfigs
{
    public class LabDoorConfig : IBuildingConfig
    {
        //public static LocString NAME = (LocString)UI.FormatAsLink("Mechanized Airlock", nameof(PRESSUREDOOR));
        //public static LocString DESC = (LocString)"Mechanized airlocks open and close more quickly than other types of door.";
        //public static LocString EFFECT = (LocString)("Blocks " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " and " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " flow, maintaining pressure between areas.\n\nFunctions as a " + UI.FormatAsLink("Manual Airlock", "MANUALPRESSUREDOOR") + " when no " + UI.FormatAsLink("Power", "POWER") + " is available.\n\nWild " + UI.FormatAsLink("Critters", "CRITTERS") + " cannot pass through doors.");
        public const string Id = "SkyesLabDoor";
        public const string DisplayName = "Lab Door";
        public const string Description = "Gravitas Lab Door Replica.\n\nAn office door to an office that no longer exists.\nExcept now it does again. \nImproved with sealing.";
        public static string Effect = $"Increases {UI.FormatAsLink("Decor", "DECOR")}, contributing to {UI.FormatAsLink("Morale", "MORALE")}.";
        public const string TechBranch = "PressureManagement";
        public const string Category = "Base";
        public override BuildingDef CreateBuildingDef()
        {
            var buildingDef = BuildingTemplates.CreateBuildingDef(
                id: Id, //GravitasDoor
                width: 1,
                height: 3,
                anim: "gravitas_door_internal_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2, //30
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3, //30f
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER4,
                construction_materials: MATERIALS.ALL_METALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1, //1600f
                build_location_rule: BuildLocationRule.Tile,
                decor: DECOR.BONUS.TIER0,
                noise: NOISE_POLLUTION.NONE);

            buildingDef.Overheatable = false;
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 120f;
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.IsFoundation = true;
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.TileLayer = ObjectLayer.FoundationTile;
            buildingDef.AudioCategory = "Metal";
            
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.PermittedRotations = PermittedRotations.R90;
            buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
            buildingDef.ForegroundLayer = Grid.SceneLayer.InteriorWall;
            buildingDef.LogicInputPorts = DoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));
            SoundEventVolumeCache.instance.AddVolume("gravitas_door_internal_kanim", "GravitasDoorInternal_open", NOISE_POLLUTION.NOISY.TIER2);
            SoundEventVolumeCache.instance.AddVolume("gravitas_door_internal_kanim", "GravitasDoorInternal_close", NOISE_POLLUTION.NOISY.TIER2);
            //buildingDef.AudioSize = "small";
            //buildingDef.DefaultAnimState = "on";
            //buildingDef.ObjectLayer = ObjectLayer.Building;
            //buildingDef.LogicInputPorts = GravitasDoorConfig.CreateSingleInputPortList(new CellOffset(0, 0));

            return buildingDef;
        }

        //public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        //{
        //    go.GetComponent<KPrefabID>().AddTag(GameTags.Door);//GameTags.Gravitas

        //    Door door = go.AddOrGet<Door>();
        //    door.unpoweredAnimSpeed = 1f;
        //    door.doorType = Door.DoorType.Sealed;
        //    door.doorOpeningSoundEventName = "GravitasDoorInternal_open";
        //    door.doorClosingSoundEventName = "GravitasDoorInternal_close";
        //    go.AddOrGet<ZoneTile>();
        //    go.AddOrGet<AccessControl>().controlEnabled = true;
        //    go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
        //    go.AddOrGet<Workable>().workTime = 3f;
        //    go.AddOrGet<KBoxCollider2D>();
        //    Prioritizable.AddRef(go);
        //    Object.DestroyImmediate((Object)go.GetComponent<BuildingEnabledButton>());
        //}

        public override void DoPostConfigureComplete(GameObject go)
        {
            Door door = go.AddOrGet<Door>();
            //door.doorType = Door.DoorType.Sealed;
            door.hasComplexUserControls = true;
            door.unpoweredAnimSpeed = 0.65f;
            door.poweredAnimSpeed = 5f;
            door.doorClosingSoundEventName = "GravitasDoorInternal_close";
            door.doorOpeningSoundEventName = "GravitasDoorInternal_open";
            go.AddOrGet<ZoneTile>();
            go.AddOrGet<AccessControl>();
            go.AddOrGet<KBoxCollider2D>();
            Prioritizable.AddRef(go);
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Door;
            go.AddOrGet<Workable>().workTime = 5f;
            Object.DestroyImmediate((Object)go.GetComponent<BuildingEnabledButton>());
            go.GetComponent<AccessControl>().controlEnabled = true;
            go.GetComponent<KBatchedAnimController>().initialAnim = "closed";
        }
    }
}
