﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;


namespace Pushy
{
    public enum FeldTyp
    {
        Leer =                      0b0000000000000001,
        Mauer =                     0b0000000000000010,
        Ziel =                      0b0000000000000100,
        Pushy =                     0b0000000000001000,
        Kiste =                     0b0000000000010000,
        Kugel =                     0b0000000000100000,
        Pfütze =                    0b0000000001000000,
        Rot =                       0b0000000010000000,
        Pfütze_Rot =                0b0000000011000000,
        Kugel_Rot =                 0b0000000010100000,
        Klecks_Rot =                0b0000010010000000,
        Blau =                      0b0000000100000000,
        Pfütze_Blau =               0b0000000101000000,
        Kugel_Blau =                0b0000000100100000,
        Klecks_Blau =               0b0000010100000000,
        Grün =                      0b0000001000000000,
        Pfütze_Grün =               0b0000001001000000,
        Kugel_Grün =                0b0000001000100000,
        Klecks_Grün =               0b0000011000000000,
        Klecks =                    0b0000010000000000,
        Teleporter =                0b0000100000000000,
        Knopf =                     0b0001000000000000,
        Knopf_Kugel_Rot =           0b0001000010100000,
        Knopf_Kugel_Blau =          0b0001000100100000,
        Knopf_Kugel_Grün =          0b0001001000100000,
        Knopf_Kiste =               0b0001000000010000,
        Knopf_Mauer =               0b0010000000000000,
        Knopf_Mauer_Kugel_Rot =     0b0010000010100000,
        Knopf_Mauer_Kugel_Blau =    0b0010000100100000,
        Knopf_Mauer_Kugel_Grün =    0b0010001000100000,
        Knopf_Mauer_Kiste =         0b0010000000010000,
        Taboo =                     0b0100000000000000,

    }

    public enum Move
    {
        Up = 0b1,
        Right = 0b10,
        Down = 0b100,
        Left = 0b1000
    }

    public class Spielfeld : FrameworkElement
    {
        private int _Level;


        public int[,] Feld;
       
        public System.Drawing.Point PushyPoint;

        public Move PushyLooking;

        public bool Knopf = false;

        public int Level
        {
            get { return _Level; }
            set
            {
                if (value > 0)
                {
                    LevelInfo LevelInfo = LevelInfo.LoadById(value);
                    if (LevelInfo is not null)
                    {
                        LoadLevelInfo(LevelInfo);
                        _Level = value;
                        InvalidateVisual();
                    }
                }
            }
        }

        public Spielfeld()
        {
            Feld = new int[12, 20];
            PushyPoint = new System.Drawing.Point(0,0); 
            PushyLooking = Pushy.Move.Up;
            Level = 1;
        }


        private BitmapSource GetImage(FeldTyp pFeldTyp, bool pImage = false)
        {
            Bitmap bmp;

            switch (pFeldTyp)
            {
                case FeldTyp.Leer:
                    bmp = Pushy.Properties.Resources.Leer;
                    break;
                case FeldTyp.Mauer:
                    bmp = Pushy.Properties.Resources.Mauer;
                    break;
                case FeldTyp.Ziel:
                    bmp = Pushy.Properties.Resources.Ziel;
                    break;
                case FeldTyp.Kiste:
                case FeldTyp.Knopf_Kiste:
                case FeldTyp.Knopf_Mauer_Kiste:
                    bmp = Pushy.Properties.Resources.Kiste;
                    break;
                case FeldTyp.Pfütze_Rot:
                    bmp = Pushy.Properties.Resources.Pfütze_Rot;
                    break;
                case FeldTyp.Pfütze_Blau:
                    bmp = Pushy.Properties.Resources.Pfütze_Blau;
                    break;
                case FeldTyp.Pfütze_Grün:
                    bmp = Pushy.Properties.Resources.Pfütze_Grün;
                    break;
                case FeldTyp.Kugel_Rot:
                    bmp = Pushy.Properties.Resources.Kugel_Rot;
                    break;
                case FeldTyp.Knopf_Kugel_Rot:
                    bmp = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf, Pushy.Properties.Resources.Kugel_Rot_Durchsichtig);
                    break;
                case FeldTyp.Knopf_Mauer_Kugel_Rot:
                    bmp = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf_Mauer_Aktiv, Pushy.Properties.Resources.Kugel_Rot_Durchsichtig);
                    break;
                case FeldTyp.Kugel_Blau:
                    bmp = Pushy.Properties.Resources.Kugel_Blau;
                    break;
                case FeldTyp.Knopf_Kugel_Blau:
                    bmp = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf, Pushy.Properties.Resources.Kugel_Blau_Durchsichtig);
                    break;
                case FeldTyp.Knopf_Mauer_Kugel_Blau:
                    bmp = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf_Mauer_Aktiv, Pushy.Properties.Resources.Kugel_Blau_Durchsichtig);
                    break;
                case FeldTyp.Kugel_Grün:
                    bmp = Pushy.Properties.Resources.Kugel_Grün;
                    break;
                case FeldTyp.Knopf_Kugel_Grün:
                    bmp = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf, Pushy.Properties.Resources.Kugel_Grün_Durchsichtig);
                    break;
                case FeldTyp.Knopf_Mauer_Kugel_Grün:
                    bmp = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf_Mauer_Aktiv, Pushy.Properties.Resources.Kugel_Grün_Durchsichtig);
                    break;
                case FeldTyp.Klecks_Rot:
                    bmp = Pushy.Properties.Resources.Klecks_Rot;
                    break;
                case FeldTyp.Klecks_Blau:
                    bmp = Pushy.Properties.Resources.Klecks_Blau;
                    break;
                case FeldTyp.Klecks_Grün:
                    bmp = Pushy.Properties.Resources.Klecks_Grün;
                    break;
                case FeldTyp.Teleporter:
                    bmp = Pushy.Properties.Resources.Teleporter;
                    break;
                case FeldTyp.Knopf:
                    bmp = Pushy.Properties.Resources.Knopf;
                    break;
                case FeldTyp.Knopf_Mauer:
                    bmp = Knopf ? Pushy.Properties.Resources.Knopf_Mauer_Aktiv : Pushy.Properties.Resources.Knopf_Mauer;
                    break;
                default:
                    bmp = Pushy.Properties.Resources.Missing;
                    break;
            }

            return bmp.ToBitmapSource();
        }

        public Bitmap ZeichneFeld()
        {
            Bitmap bmp = new Bitmap(640, 384);


            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        Image image;
                        switch (Feld[i, j])
                        {
                            case (int)FeldTyp.Leer:
                                image = Pushy.Properties.Resources.Leer;
                                break;
                            case (int)FeldTyp.Mauer:
                                image = Pushy.Properties.Resources.Mauer;
                                break;
                            case (int)FeldTyp.Ziel:
                                image = Pushy.Properties.Resources.Ziel;
                                break;
                            case (int)FeldTyp.Kiste:
                            case (int)FeldTyp.Knopf_Kiste:
                            case (int)FeldTyp.Knopf_Mauer_Kiste:
                                image = Pushy.Properties.Resources.Kiste;
                                break;
                            case (int)FeldTyp.Pfütze_Rot:
                                image = Pushy.Properties.Resources.Pfütze_Rot;
                                break;
                            case (int)FeldTyp.Pfütze_Blau:
                                image = Pushy.Properties.Resources.Pfütze_Blau;
                                break;
                            case (int)FeldTyp.Pfütze_Grün:
                                image = Pushy.Properties.Resources.Pfütze_Grün;
                                break;
                            case (int)FeldTyp.Kugel_Rot:
                            case (int)FeldTyp.Knopf_Kugel_Rot:
                                image = Pushy.Properties.Resources.Kugel_Rot;
                                break;
                            case (int)FeldTyp.Knopf_Mauer_Kugel_Rot:
                                image = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf, Pushy.Properties.Resources.Kugel_Rot);
                                break;
                            case (int)FeldTyp.Kugel_Blau:
                            case (int)FeldTyp.Knopf_Kugel_Blau:
                                image = Pushy.Properties.Resources.Kugel_Blau;
                                break;
                            case (int)FeldTyp.Knopf_Mauer_Kugel_Blau:
                                image = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf, Pushy.Properties.Resources.Kugel_Blau);
                                break;
                            case (int)FeldTyp.Kugel_Grün:
                            case (int)FeldTyp.Knopf_Kugel_Grün:
                                image = Pushy.Properties.Resources.Kugel_Grün;
                                break;
                            case (int)FeldTyp.Knopf_Mauer_Kugel_Grün:
                                image = Pushy.Extension.MergeImages(Pushy.Properties.Resources.Knopf, Pushy.Properties.Resources.Kugel_Grün);
                                break;
                            case (int)FeldTyp.Klecks_Rot:
                                image = Pushy.Properties.Resources.Klecks_Rot;
                                break;
                            case (int)FeldTyp.Klecks_Blau:
                                image = Pushy.Properties.Resources.Klecks_Blau;
                                break;
                            case (int)FeldTyp.Klecks_Grün:
                                image = Pushy.Properties.Resources.Klecks_Grün;
                                break;
                            case (int)FeldTyp.Teleporter:
                                image = Pushy.Properties.Resources.Teleporter;
                                break;
                            case (int)FeldTyp.Knopf:
                                image = Pushy.Properties.Resources.Knopf;
                                break;
                            case (int)FeldTyp.Knopf_Mauer:
                                image = Knopf ? Pushy.Properties.Resources.Knopf_Mauer_Aktiv : Pushy.Properties.Resources.Knopf_Mauer;
                                break;
                            default:
                                image = Pushy.Properties.Resources.Leer;
                                break;
                        }
                        g.DrawImage(image, j * 32, i * 32, 32, 32);
                    }
                }

                switch (PushyLooking)
                {
                    case Pushy.Move.Up:
                        g.DrawImage(Pushy.Properties.Resources.PushyUp, PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32);
                        break;
                    case Pushy.Move.Right:
                        g.DrawImage(Pushy.Properties.Resources.PushyRight, PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32);
                        break;
                    case Pushy.Move.Down:
                        g.DrawImage(Pushy.Properties.Resources.PushyDown, PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32);
                        break;
                    case Pushy.Move.Left:
                        g.DrawImage(Pushy.Properties.Resources.PushyLeft, PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32);
                        break;
                }


            }

            return bmp;

        }

        public void Move(Move lMove)
        {
            PushyLooking = lMove;
            switch (lMove)
            {
                case Pushy.Move.Up:
                    if(CanIGo(PushyPoint, new System.Drawing.Point(0, -1),lMove))
                        PushyPoint.Y -= 1;
                   // else
                    //    PushyPoint.Y -= 1;
                    break;
                case Pushy.Move.Right:
                    if (CanIGo(PushyPoint, new System.Drawing.Point(1, 0), lMove))
                        PushyPoint.X += 1;
                  //  else
                   //     PushyPoint.X += 1;
                    break;
                case Pushy.Move.Down:
                    if (CanIGo(PushyPoint, new System.Drawing.Point(0, 1), lMove))
                        PushyPoint.Y += 1;
                   // else
                   //     PushyPoint.Y += 1;
                    break;
                case Pushy.Move.Left:
                    if (CanIGo(PushyPoint, new System.Drawing.Point(-1, 0), lMove))
                        PushyPoint.X -= 1;
                   // else
                   //     PushyPoint.X -= 1;
                    break;
                default:
                    throw new Exception("Hölle was zur!");
            }


            if (Feld[PushyPoint.Y, PushyPoint.X] == (int)FeldTyp.Ziel)
            {
                if(LevelFinished())
                    Level++;
            }


            if (Feld[PushyPoint.Y, PushyPoint.X] == (int)FeldTyp.Teleporter)
            {
                Teleport();
            }


            InvalidateVisual();
        }


        private void Teleport()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if(Feld[i, j] == (int)FeldTyp.Teleporter && !(PushyPoint.X == j && PushyPoint.Y == i))
                    {
                        PushyPoint = new System.Drawing.Point(j, i);
                        return;
                    }
                }
            }
        }

        public bool LevelFinished()
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if ((Feld[i, j] & (int)FeldTyp.Kugel) > 0)
                        return false;

                    switch (Feld[i, j])
                    {
                        case (int)FeldTyp.Kugel_Blau:
                        case (int)FeldTyp.Kugel_Rot:
                        case (int)FeldTyp.Kugel_Grün:
                            return false;
                            break;
                    }
                }
            }
            return true;
        }

        public bool CanIGo(System.Drawing.Point pPoint, System.Drawing.Point pRichtung, Move pMove)
        {
            if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ( (int)FeldTyp.Leer | (int)FeldTyp.Ziel | (int)FeldTyp.Pfütze | (int)FeldTyp.Teleporter |  (int)FeldTyp.Klecks ) ) > 0)
            {
                return true;
            }
            if(MoveObsticle(pPoint, pRichtung, pMove))
            {
                return true;
            }


            return false;

        }

        public bool MoveObsticle(System.Drawing.Point pPoint, System.Drawing.Point pRichtung, Move pMove)
        {

            if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ((int)FeldTyp.Knopf)) > 0)
            {
                if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] ) == (int)FeldTyp.Knopf)
                    return true;
                else if((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] & ((int)FeldTyp.Leer)) > 0)
                {
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] = Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ~(int)FeldTyp.Knopf;
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Knopf;
                    Knopf = false;
                    return true;
                }
            }
            else if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ((int)FeldTyp.Knopf_Mauer)) > 0 )
            {
                if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X]) == (int)FeldTyp.Knopf_Mauer && Knopf)
                    return true;
                else if((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] & ((int)FeldTyp.Leer)) > 0)
                {
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] = Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ~(int)FeldTyp.Knopf_Mauer;
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Knopf_Mauer;
                    return true;
                }
            }
            else if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ((int)FeldTyp.Kiste)) == (int)FeldTyp.Kiste)
            {
                if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] & ((int)FeldTyp.Leer)) > 0)
                {
                    int tmp = Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X];
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X];
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] = tmp;
                    return true;
                }
                else if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X]) == (int)FeldTyp.Knopf)
                {
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Leer;
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] += (int)FeldTyp.Kiste;
                    Knopf = true;
                    return true;
                }
                else if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X]) == (int)FeldTyp.Knopf_Mauer && Knopf)
                {
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Leer;
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] += (int)FeldTyp.Kiste;
                    Knopf = true;
                    return true;
                }

            }
            else if ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & ((int)FeldTyp.Kugel)) > 0)
            {

                if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] & ((int)FeldTyp.Leer)) > 0)
                {
                    int tmp = Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X];
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X];
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] = tmp;
                    return true;
                }
                else if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] & ((int)FeldTyp.Pfütze)) > 0 &&
                    ((Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] & Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X]) > 0))
                {
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Leer;
                    return true;
                }
                else if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] & ((int)FeldTyp.Klecks)) > 0)
                {
                    int Klecks = Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X];
                    int Farbe = (Klecks & (int)FeldTyp.Rot) | (Klecks & (int)FeldTyp.Blau) | (Klecks & (int)FeldTyp.Grün);
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] = Farbe + (int)FeldTyp.Kugel;
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Leer;
                    return true;
                }
                else if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X]) == (int)FeldTyp.Knopf)
                {
                    int Klecks = Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X];
                    int Farbe = (Klecks & (int)FeldTyp.Rot) | (Klecks & (int)FeldTyp.Blau) | (Klecks & (int)FeldTyp.Grün);
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Leer;
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] += Farbe + (int)FeldTyp.Kugel;
                    Knopf = true;
                    return true;
                }
                else if ((Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] ) == (int)FeldTyp.Knopf_Mauer && Knopf)
                {
                    int Klecks = Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X];
                    int Farbe = (Klecks & (int)FeldTyp.Rot) | (Klecks & (int)FeldTyp.Blau) | (Klecks & (int)FeldTyp.Grün);
                    Feld[pPoint.Y + pRichtung.Y, pPoint.X + pRichtung.X] = (int)FeldTyp.Leer;
                    Feld[pPoint.Y + pRichtung.Y + pRichtung.Y, pPoint.X + pRichtung.X + pRichtung.X] += Farbe + (int)FeldTyp.Kugel;
                    Knopf = true;
                    return true;
                }

            }



            return false;
        }

        public void LoadLevelInfo(LevelInfo LevelInfo)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Feld[i, j] = (int)LevelInfo.Feld.ElementAt(i).ElementAt(j);
                }
            }

            PushyLooking = LevelInfo.PushyLooking;
            PushyPoint = LevelInfo.PushyPoint;
            Knopf = false;
        }


        public static Spielfeld Level1
        {
            get
            {
                LevelInfo LevelInfo = new LevelInfo();

                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(LevelInfo));
                    MemoryStream memoryStream = new MemoryStream(Pushy.Properties.Resources.Level_1);
                    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                    LevelInfo = (LevelInfo)xs.Deserialize(memoryStream);

                }
                catch
                {

                }


                return LevelInfo.ZuSpielfeld();
            }
        }

        #region Override

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);


            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    drawingContext.DrawImage(GetImage((FeldTyp)Feld[i, j]), new Rect(j * 32, i * 32, 32, 32));
                }
            }

            switch (PushyLooking)
            {
                case Pushy.Move.Up:
                    drawingContext.DrawImage(Pushy.Properties.Resources.PushyUp.ToBitmapSource(), new Rect(PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32));
                    break;
                case Pushy.Move.Right:
                    drawingContext.DrawImage(Pushy.Properties.Resources.PushyRight.ToBitmapSource(), new Rect(PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32));
                    break;
                case Pushy.Move.Down:
                    drawingContext.DrawImage(Pushy.Properties.Resources.PushyDown.ToBitmapSource(), new Rect(PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32));
                    break;
                case Pushy.Move.Left:
                    drawingContext.DrawImage(Pushy.Properties.Resources.PushyLeft.ToBitmapSource(), new Rect(PushyPoint.X * 32, PushyPoint.Y * 32, 32, 32));
                    break;
            }




        }

        #endregion

    }
}
