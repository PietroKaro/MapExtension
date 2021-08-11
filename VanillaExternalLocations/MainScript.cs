using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTA;
using GTA.UI;
using GTA.Native;
using GTA.Math;
using System.Windows.Forms;

namespace VanillaExternalLocations
{
    public class MainScript : Script
    {
        readonly Vector3 CAYO_PERICO_POS = new Vector3(4840.571f, -5174.425f, 2.0f);
        readonly Vector3 AIRCRAFT_CARRIER_POS = new Vector3(3084.73f, -4770.709f, 15.26167f);
        readonly Vector3 YACHT_VESPUCCI_POS = new Vector3(-2027.946f, -1036.695f, 6.707587f);
        readonly Vector3 YACHT_PALETO_POS = new Vector3(-1413.325f, 6750.301f, 11.90974f);

        readonly string[] _CayoPericoIPL = { "island_lodlights", "h4_islandairstrip", "h4_islandairstrip_props", "h4_islandx_mansion", "h4_islandx_mansion_props", "h4_islandx_props", "h4_islandxdock", "h4_islandxdock_props", "h4_islandxdock_props_2", "h4_islandxtower", "h4_islandx_maindock", "h4_islandx_maindock_props", "h4_islandx_maindock_props_2", "h4_IslandX_Mansion_Vault", "h4_islandairstrip_propsb", "h4_beach", "h4_beach_props", "h4_beach_bar_props", "h4_islandx_barrack_props", "h4_islandx_checkpoint",
            "h4_islandx_checkpoint_props", "h4_islandx_Mansion_Office", "h4_islandx_Mansion_LockUp_01", "h4_islandx_Mansion_LockUp_02", "h4_islandx_Mansion_LockUp_03", "h4_islandairstrip_hangar_props", "h4_IslandX_Mansion_B", "h4_islandairstrip_doorsclosed", "h4_Underwater_Gate_Closed", "h4_mansion_gate_closed", "h4_aa_guns", "h4_IslandX_Mansion_GuardFence", "h4_IslandX_Mansion_Entrance_Fence", "h4_IslandX_Mansion_B_Side_Fence", "h4_IslandX_Mansion_Lights", "h4_islandxcanal_props", "h4_beach_props_party",
            "h4_islandX_Terrain_props_06_a", "h4_islandX_Terrain_props_06_b", "h4_islandX_Terrain_props_06_c", "h4_islandX_Terrain_props_05_a", "h4_islandX_Terrain_props_05_b", "h4_islandX_Terrain_props_05_c", "h4_islandX_Terrain_props_05_d", "h4_islandX_Terrain_props_05_e", "h4_islandX_Terrain_props_05_f", "H4_islandx_terrain_01", "H4_islandx_terrain_02", "H4_islandx_terrain_03", "H4_islandx_terrain_04", "H4_islandx_terrain_05", "H4_islandx_terrain_06", "h4_ne_ipl_00", "h4_ne_ipl_01", "h4_ne_ipl_02",
            "h4_ne_ipl_03", "h4_ne_ipl_04", "h4_ne_ipl_05", "h4_ne_ipl_06", "h4_ne_ipl_07", "h4_ne_ipl_08", "h4_ne_ipl_09", "h4_nw_ipl_00", "h4_nw_ipl_01", "h4_nw_ipl_02", "h4_nw_ipl_03", "h4_nw_ipl_04", "h4_nw_ipl_05", "h4_nw_ipl_06", "h4_nw_ipl_07", "h4_nw_ipl_08", "h4_nw_ipl_09", "h4_se_ipl_00", "h4_se_ipl_01", "h4_se_ipl_02", "h4_se_ipl_03", "h4_se_ipl_04", "h4_se_ipl_05", "h4_se_ipl_06", "h4_se_ipl_07", "h4_se_ipl_08", "h4_se_ipl_09", "h4_sw_ipl_00", "h4_sw_ipl_01", "h4_sw_ipl_02", "h4_sw_ipl_03",
            "h4_sw_ipl_04", "h4_sw_ipl_05", "h4_sw_ipl_06", "h4_sw_ipl_07", "h4_sw_ipl_08", "h4_sw_ipl_09", "h4_islandx_mansion", "h4_islandxtower_veg", "h4_islandx_sea_mines", "h4_islandx", "h4_islandx_barrack_hatch", "h4_islandxdock_water_hatch", "h4_beach_party", "h4_mph4_terrain_01_grass_0", "h4_mph4_terrain_01_grass_1", "h4_mph4_terrain_02_grass_0", "h4_mph4_terrain_02_grass_1", "h4_mph4_terrain_02_grass_2", "h4_mph4_terrain_02_grass_3", "h4_mph4_terrain_04_grass_0", "h4_mph4_terrain_04_grass_1",
            "h4_mph4_terrain_05_grass_0", "h4_mph4_terrain_06_grass_0", "h4_mph4_airstrip_interior_0_airstrip_hanger" };

        readonly string[] _AircraftCarrierIPL = { "hei_carrier", "hei_carrier_DistantLights", "hei_Carrier_int1", "hei_Carrier_int2", "hei_Carrier_int3", "hei_Carrier_int4", "hei_Carrier_int5", "hei_Carrier_int6", "hei_carrier_LODLights" };
        readonly string[] _YachtVespucciIPL = { "hei_yacht_heist", "hei_yacht_heist_enginrm", "hei_yacht_heist_Lounge", "hei_yacht_heist_Bridge", "hei_yacht_heist_Bar", "hei_yacht_heist_Bedrm", "hei_yacht_heist_DistantLights", "hei_yacht_heist_LODLights" };
        readonly string[] _YachtPaletoIPL = { "gr_heist_yacht2", "gr_heist_yacht2_bar", "gr_heist_yacht2_bedrm", "gr_heist_yacht2_bridge", "gr_heist_yacht2_enginrm", "gr_heist_yacht2_lounge" };

        private byte GlobalWaterType // Get/Set if the ocean has to be modified for Cayo Perico island (to avoid high waves).
        {
            get
            {
                return Function.Call<byte>((Hash)0xF741BD853611592D);
            }
            set
            {
                Function.Call((Hash)0x7E3F55ED251B76D3, value);
            }
        }

        Blip _BCayoPerico, _BAircraftCarrier, _BVespucciY, _BPaleyoY;
        bool _AreLocationsLoaded, _CayoPericoCheck, _YVespucciCheck, _YPaletoCheck, _CarrierCheck, ShipOcean;
        
        public MainScript()
        {
            Tick += OnTick;
            KeyUp += OnKeyUp;
            KeyDown += OnKeyDown;
            Aborted += OnAborted;
            ConfigLoader loader = ConfigLoader.GetLoader();
            (_CayoPericoCheck, _CarrierCheck, _YVespucciCheck, _YPaletoCheck) = loader.GetActiveLocations(BaseDirectory + @"\VELConfig.xml");
        }

        void OnTick(object sender, EventArgs args)
        {
            if (!_AreLocationsLoaded)
            {
                InitializeLocations();
                _AreLocationsLoaded = true;
            }
            //OceanControl();
        }

        void OnKeyUp(object sender, KeyEventArgs args)
        {

        }

        void OnKeyDown(object sender, KeyEventArgs args)
        {
            // debug
            switch (args.KeyCode)
            {
                case Keys.F6:
                    GlobalWaterType = 1;
                    break;
                case Keys.F7:
                    GlobalWaterType = 0;
                    break;
                case Keys.F8:
                    World.Weather = Weather.ThunderStorm;
                    break;
                case Keys.F9:
                    ToggleShipWater(true);
                    GTA.UI.Screen.ShowSubtitle("ship water true");
                    break;
                case Keys.F10:
                    ToggleShipWater(false);
                    GTA.UI.Screen.ShowSubtitle("ship water false");
                    break;
                case Keys.F11:
                    Notification.Show(Game.Player.Character.Position.ToString());
                    Notification.Show(Vector3.Distance2D(Game.Player.Character.Position, AIRCRAFT_CARRIER_POS).ToString());
                    Notification.Show(_CayoPericoCheck.ToString() + " " + _CarrierCheck.ToString() + " " + _YVespucciCheck.ToString() + " " + _YPaletoCheck.ToString());
                    break;
                case Keys.F12:
                    World.CurrentTimeOfDay = new TimeSpan(12, 00, 00);
                    World.Weather = Weather.ExtraSunny;
                    break;
                case Keys.D0:
                    foreach(Vehicle v in World.GetAllVehicles())
                    {
                        v.Delete();
                    }
                    foreach (Ped p in World.GetAllPeds())
                    {
                        p.Delete();
                    }
                    break;
            }
        }

        void OnAborted(object sender, EventArgs args)
        {
            _BCayoPerico.Delete();
            _BAircraftCarrier.Delete();
            _BVespucciY.Delete();
            _BPaleyoY.Delete();
            GlobalWaterType = 0;
            ToggleShipWater(false);
        }
        
        void InitializeLocations() // Set locations
        {
            Function.Call(Hash._LOAD_MP_DLC_MAPS);
            Yield();
            Notification.Show("Mappa online caricata"); // debug
            if (_CayoPericoCheck)
            {
                while (!IsLocationPresent(_CayoPericoIPL))
                {
                    ToggleLocation(_CayoPericoIPL, true);
                    Yield();
                }
                _BCayoPerico = CreateIPLBlip(CAYO_PERICO_POS, BlipSprite.PointOfInterest, 1.0f, "Cayo Perico");
            }
            if (_CarrierCheck)
            {
                while (!IsLocationPresent(_AircraftCarrierIPL))
                {
                    ToggleLocation(_AircraftCarrierIPL, true);
                    Yield();
                }
                _BAircraftCarrier = CreateIPLBlip(AIRCRAFT_CARRIER_POS, BlipSprite.Airport, 1.0f, "Aircraft Carrier");
            }
            if (_YVespucciCheck)
            {
                while (!IsLocationPresent(_YachtVespucciIPL))
                {
                    ToggleLocation(_YachtVespucciIPL, true);
                    Yield();
                }
                _BVespucciY = CreateIPLBlip(YACHT_VESPUCCI_POS, BlipSprite.Yacht, 1.0f, "Vespucci Yacht");
            }
            if (_YPaletoCheck)
            {
                while (!IsLocationPresent(_YachtPaletoIPL))
                {
                    ToggleLocation(_YachtPaletoIPL, true);
                    Yield();
                }
                _BPaleyoY = CreateIPLBlip(YACHT_PALETO_POS, BlipSprite.Yacht, 1.0f, "Paleto Yacht");
            }
            Notification.Show("mappe caricate"); // debug
        }

        void ToggleLocation(string[] ipls, bool toggle)
        {
            foreach (string ipl in ipls)
            {
                if (toggle)
                {
                    Function.Call(Hash.REQUEST_IPL, ipl);
                }
                else
                {
                    Function.Call(Hash.REMOVE_IPL, ipl);
                }
            }
        }

        bool IsLocationPresent(string[] ipls)
        {
            foreach (string ipl in ipls)
            {
                if (!Function.Call<bool>(Hash.IS_IPL_ACTIVE, ipl))
                {
                    return false;
                }
            }
            return true;
        }

        Blip CreateIPLBlip(Vector3 position, BlipSprite sprite, float scale, string name)
        {
            Blip createdBlip = World.CreateBlip(position);
            createdBlip.Sprite = sprite;
            createdBlip.Scale = scale;
            createdBlip.Name = name;
            return createdBlip;
        }

        void ToggleShipWater(bool toggle) // The ocean will be calm around the player.
        {
            if (toggle)
            {
                Function.Call(Hash.SET_DEEP_OCEAN_SCALER, 0.0f);
            }
            else
            {
                Function.Call(Hash.RESET_DEEP_OCEAN_SCALER);
            }
        }

        void OceanControl()
        {
            if (_CayoPericoCheck)
            {
                float distance = Vector3.Distance2D(Game.Player.Character.Position, CAYO_PERICO_POS);
                if (GlobalWaterType != 1 && distance < 2500.0f)
                {
                    GlobalWaterType = 1;
                    GTA.UI.Screen.ShowSubtitle("Oceano CP");
                }
                else if (distance > 2500.0f && GlobalWaterType == 1)
                {
                    GlobalWaterType = 0;
                    GTA.UI.Screen.ShowSubtitle("Oceano LS");
                }
            }
            bool nearCarrier = IsPlayerNearShip(AIRCRAFT_CARRIER_POS);
            bool nearVY = IsPlayerNearShip(YACHT_VESPUCCI_POS);
            bool nearPY = IsPlayerNearShip(YACHT_PALETO_POS);
            if (!ShipOcean && ((_CarrierCheck && nearCarrier) || (_YVespucciCheck && nearVY) || (_YPaletoCheck && nearPY)))
            {
                ToggleShipWater(true);
                ShipOcean = true;
                GTA.UI.Screen.ShowSubtitle("Oceano ship");
            }
            else if (ShipOcean && !nearCarrier && !nearVY && !nearPY)
            {
                ToggleShipWater(false);
                ShipOcean = false;
                GTA.UI.Screen.ShowSubtitle("Oceano normale");
            }
        }

        bool IsPlayerNearShip(Vector3 shipPos)
        {
            float distance = Vector3.Distance2D(Game.Player.Character.Position, shipPos);
            if (distance < 373.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
