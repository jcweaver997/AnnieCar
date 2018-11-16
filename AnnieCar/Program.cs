
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace AnnieCar
{
    internal class Program
    {
        private JcRobotNetworking jcnet;
        
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


        private float speed;
        private float turn;

        public void OnCommandRecieve(JcRobotNetworking.Command c)
        {
            switch(c.commandID)
            {
                case 1:
                    speed = BitConverter.ToSingle(c.param,0);
                    break;
                case 2:
                    turn = BitConverter.ToSingle(c.param, 0);
                    break;
            }
                
        }

        public void Start()
        {
            jcnet = new JcRobotNetworking(JcRobotNetworking.ConnectionType.Robot, OnCommandRecieve);
            jcnet.Connect(1296);
            Process.Start("gpio","-g mode 18 pwm");
            Process.Start("gpio","-g mode 17 output");
            Process.Start("gpio","pwm-ms");
            Process.Start("gpio","pwmc 192");
            Process.Start("gpio","pwmr 2000");
        }

        public void Loop()
        {
            Process.Start("gpio","-g pwm 18 "+(150+turn*100));
            Process.Start("gpio","write 17 "+(int)Math.Round(speed));
        }
    }
}