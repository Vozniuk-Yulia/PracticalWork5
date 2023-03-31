using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Traffic
    {
        private enum Light { Green, Yellow, Red };
        private Light[] lights = new Light[4];
        private object[] trafficLightLocks = new object[4];
        private Semaphore semaphore = new Semaphore(2, 2); 
        public Traffic()
        {
            for (int i = 0; i < 4; i++)
            {
                lights[i] = Light.Red;
                trafficLightLocks[i] = new object();
            }
        }
        public void Run()
        {
            Console.WriteLine("Traffic intersection simulation started.");
            while (true)
            {
                int trafficLightIndex = new Random().Next(4);
                Monitor.Enter(trafficLightLocks[trafficLightIndex]);
                try
                {
                    Light currentLightState = lights[trafficLightIndex];
                    switch (currentLightState)
                    {
                        case Light.Green:
                            lights[trafficLightIndex] = Light.Yellow;
                            Console.WriteLine($"Traffic light {trafficLightIndex} changed from Green to Yellow.");
                            Thread.Sleep(2000);
                            lights[trafficLightIndex] = Light.Red;
                            Console.WriteLine($"Traffic light {trafficLightIndex} changed from Yellow to Red.");
                            break;
                        case Light.Yellow:
                            lights[trafficLightIndex] = Light.Red;
                            Console.WriteLine($"Traffic light {trafficLightIndex} changed from Yellow to Red.");
                            break;
                        case Light.Red:
                            lights[trafficLightIndex] = Light.Green;
                            Console.WriteLine($"Traffic light {trafficLightIndex} changed from Red to Green.");
                            break;
                    }
                }
                finally
                {
                    Monitor.Exit(trafficLightLocks[trafficLightIndex]);
                }

                semaphore.WaitOne();
                Console.WriteLine("Car is passing through intersection.");
                Thread.Sleep(2000); 
                semaphore.Release();
            }
        }
    }
}
