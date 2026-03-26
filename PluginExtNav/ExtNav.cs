using GMap.NET;
using MissionPlanner;
using MissionPlanner.Plugin;
using System;
using System.Windows.Forms;
using static MAVLink;

namespace PluginExtNav
{
    public class ExtNav : Plugin
    {
        ToolStripMenuItem menuItem;

        private PointLatLng lastClickPoint;

        public override string Name => "ExtNav Plugin";
        public override string Version => "1.0";
        public override string Author => "failway";

        public override bool Init() => true;

        public override bool Loaded()
        {
            var map = MainV2.instance.FlightData.gMapControl1;

            menuItem = new ToolStripMenuItem("ExtNav");
            menuItem.Click += MenuItem_Click;
            map.ContextMenuStrip.Items.Add(menuItem);

            map.MouseDown += Map_MouseDown;

            return true;
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var map = sender as GMap.NET.WindowsForms.GMapControl;

                PointLatLng point = map.FromLocalToLatLng(e.X, e.Y);
                lastClickPoint = point;

                Console.WriteLine($"ExtNav clicked (PC): {point.Lat}, {point.Lng}");
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            if (lastClickPoint == null)
            {
                MessageBox.Show("Please first point to map.");
                return;
            }

            Console.WriteLine($"ExtNav menu clicked: {lastClickPoint.Lat}, {lastClickPoint.Lng}");
            SendExtNav(lastClickPoint.Lat, lastClickPoint.Lng);
        }

        private void SendExtNav(double lat, double lon)
        {
            int lat_int = (int)(lat * 1e7);
            int lon_int = (int)(lon * 1e7);

            var msg = new mavlink_command_int_t(
                param1: (float)(Environment.TickCount / 1000.0), // время (сек)
                param2: 0.05f,   // latency (50ms)
                param3: 2.0f,    // accuracy (метры)
                param4: 0f,
                x: lat_int,
                y: lon_int,
                z: float.NaN,    // ОБЯЗАТЕЛЬНО NaN
                command: 43003,
                target_system: (byte)MainV2.comPort.sysidcurrent,
                target_component: (byte)MainV2.comPort.compidcurrent,
                frame: (byte)MAV_FRAME.GLOBAL_INT,
                current: 0,
                autocontinue: 0
            );

            MainV2.comPort.sendPacket(msg, msg.target_system, msg.target_component);

            Console.WriteLine($"ExtNav SENT: {lat_int}, {lon_int}");
        }

        public override bool Loop() => true;
        public override bool Exit() => true;
    }
}