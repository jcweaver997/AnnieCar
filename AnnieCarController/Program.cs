using System;
using System.Threading;
using QuadController;

namespace AnnieCarController
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Program p = new Program();
            p.Start();
            while (true)
            {
                p.Loop();
                Thread.Sleep(50);
            }
        }

        private Joystick js;
        private JcRobotNetworking jcnet;

        public void Start()
        {
            js = new LogitechController("/dev/input/js0");
            jcnet = new JcRobotNetworking(JcRobotNetworking.ConnectionType.Controller);
        }

        public void Loop()
        {
            jcnet.SendCommand(new JcRobotNetworking.Command(1,BitConverter.GetBytes(-js.GetThumbstickLeft().Y)));
            jcnet.SendCommand(new JcRobotNetworking.Command(2,BitConverter.GetBytes(js.GetThumbstickRight().X)));
        }
        
    }
}