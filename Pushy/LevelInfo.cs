﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Pushy
{
    public class LevelInfo
    {
        public LevelInfo()
        {
            Feld = new List<FeldTyp[]>();
            PushyLooking = Pushy.Move.Up;
            PushyPoint = new Point(2, 9);
        }

        public List<FeldTyp[]> Feld { get; set; }
            /*= new List<FeldTyp[]>
        {
            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Ziel, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Leer, FeldTyp.Mauer },

            new FeldTyp[20] { FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer, FeldTyp.Mauer },

        };

        */

        public Point PushyPoint { get; set; } 

        public Move PushyLooking { get; set; }


        public static LevelInfo LoadById(int Id)
        {
            LevelInfo LevelInfo = new LevelInfo();

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(LevelInfo));
                MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes($"Level/Level_{Id}.lvl"));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                LevelInfo = (LevelInfo)xs.Deserialize(memoryStream);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.Message);
                LevelInfo = null;
            }

            return LevelInfo;
        }

        public Spielfeld ZuSpielfeld()
        {
            Spielfeld Spielfeld = new Spielfeld();
            
            for(int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Spielfeld.Feld[i, j] = (int)Feld.ElementAt(i).ElementAt(j);
                }
            }

            Spielfeld.PushyLooking = PushyLooking;
            Spielfeld.PushyPoint = PushyPoint;

            return Spielfeld;
        }
    }
}
