using Legends.Core.Time;
using Legends.Network;
using Legends.World.Games.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.World.Games
{
    public class GameProvider
    {
        /// <summary>
        /// Theorically 30fps
        /// Aprox equal to (16.666 * 2)
        /// </summary>
        public const double REFRESH_RATE = (1000d / 30d);

        public static Game TestGame = new Game(1, "Partie de test", 11);

        static HighResolutionTimer m_timer;
        static Stopwatch m_stopwatch;
        public static void GameLoop()
        {
            m_timer = new HighResolutionTimer(1);
            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
            m_timer.Elapsed += OnTick;
            m_timer.Start();


        }

        private static void OnTick()
        {
            LoLServer.NetLoop();

            if (TestGame.Started)
            {
                if (m_stopwatch.Elapsed.TotalMilliseconds + 1.0 > REFRESH_RATE)
                {
                    float deltaTime = (float)m_stopwatch.Elapsed.TotalMilliseconds;

                    m_stopwatch.Restart();
                    TestGame.Update(deltaTime);

                }
            }
        }



    }
}
