using static System.Math;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Globalization;
using System;
using static NC_Tool.Form1;
using System.Drawing;

namespace NC_Tool
{
    static class Zyklen
    {
        #region Variablen
        public static bool ErrM = false;
        public static double SafetyHeight;
        public static double DT;
        public static int Spin;
        public static double StartHeight;
        public static double DepthZ;
        public static double StepZ;
        public static int FFinish;
        public static double StartPosX;
        public static double StartPosY;
        public static double PocketSizeX;
        public static double PocketSizeY;
        public static int FCut;
        public static double Radius;
        public static double StepXY;
        public static bool RemoveOFin;
        public static double OFin;
        public static double StepZFin;
        public static int FPlunge;
        public static double StartPosZ;
        public static double OFinX;
        public static double OFinY;
        public static double[,] BP = new double[61, 3];
        public static double DepthBs;
        public static double DepthRs;
        public static double DepthAst;
        public static int Winkel;
        public static double Drehwinkel;
        public static double TKD;
        public static double SW;
        public static double OW;
        public static double AbstX;
        public static double AbstY;
        public static int BoT;
        #endregion

        #region Zyklus Planfräsen
        public static void Planfraesen()
        {
            // interne Variablen definieren
            int Z1;
            double X;
            double Y;
            double B;
            var BR = default(double);
            int N;
            var O = default(int);
            int M;
            var X1 = default(double);
            var Y1 = default(double);
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX <= 0d | PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String251"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY <= 0d | PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String252"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            M = 0;
            // interne Berechnungen
            DT /= 2d;
            X = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in X
            Y = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in Y
            Z1 = (int)Round(DepthZ / StepZ);                                                                    // Anzahl der Zustellungen
            B = Conversions.ToDouble(Strings.Format(Z1 * StepZ, "###0.000"));
            if (B < DepthZ)                                                  // Restmaterial in Z ermitteln
            {
                BR = Conversions.ToDouble(Strings.Format(DepthZ - B, "###0.000"));
            }
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zyklusbeginn -----------------------------------------------------------------------
            // Planfläche mit Anzahl der Zustellungen fräsen
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                   // Zyklusposition in Z
            var loopTo = Z1;
            for (N = 1; N <= loopTo; N++)
            {
                if (N > 1)
                {
                    X = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in X
                    Y = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in Y
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                StartPosZ -= StepZ;
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + StartPosZ.ToString("#0.0##") + " )" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // einmal komplett um die Kontur
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " X0.000 Y0.000" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + PocketSizeY.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + PocketSizeX.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y0.000" + Constants.vbCrLf);
                O = (int)Round(PocketSizeY / DT - 1d);
                X = 0d;
                Y = 0d;
                X1 = PocketSizeX;
                Y1 = PocketSizeY;
                if (RemoveOFin == false)
                {
                    // danach die Planfläche abzeilen
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X0.000" + Constants.vbCrLf);
                    var loopTo1 = O;
                    for (M = 1; M <= loopTo1; M++)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        Y += Conversions.ToDouble(Strings.Format(DT, "###0.000"));
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + PocketSizeX.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " X0.000" + Constants.vbCrLf);
                    }
                }
                else
                {
                    // danach die Planfläche fräsen
                    O = (int)Round(PocketSizeX / 2d / DT + 1d);
                    var loopTo2 = O;
                    for (M = 1; M <= loopTo2; M++)
                    {
                        X += DT;
                        if (X > PocketSizeX / 2d)
                            break;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + X.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        Y1 -= DT;
                        if (Y1 < PocketSizeY / 2d)
                            break;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + Y1.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        X1 -= DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + X1.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        Y += DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + Y.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            // Restmaterial bis zur finalen Tiefe entfernen
            X = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in X
            Y = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in Y
            if (BR > 0d)
            {
                if (N > 1)
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                StartPosZ -= BR;
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + StartPosZ.ToString("#0.0##") + "           )" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // einmal komplett um die Kontur
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " X0.000 Y0.000" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + PocketSizeY.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + PocketSizeX.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y0.000" + Constants.vbCrLf);
                Y = 0d;
                if (RemoveOFin == false)
                {
                    // danach die Planfläche abzeilen
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X0.000" + Constants.vbCrLf);
                    var loopTo3 = O;
                    for (M = 1; M <= loopTo3; M++)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        Y += Conversions.ToDouble(Strings.Format(DT, "###0.000"));
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + PocketSizeX.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " X0.000" + Constants.vbCrLf);
                    }
                }
                else
                {
                    // danach die Planfläche fräsen
                    O = (int)Round(PocketSizeX / 2d / DT + 1d);
                    var loopTo4 = O;
                    for (M = 1; M <= loopTo4; M++)
                    {
                        X += DT;
                        if (X > PocketSizeX / 2d)
                            break;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + X.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        Y1 -= DT;
                        if (Y1 < PocketSizeY / 2d)
                            break;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + Y1.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        X1 -= DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + X1.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        Y += DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + Y.ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            X = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in X
            Y = Conversions.ToDouble(Strings.Format(0d - DT, "###0.000"));                                      // Startpunkt in Y
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
        }
        #endregion

        #region Zyklus Rechtecktasche
        public static void Rechtecktasche()
        {
            // interne Variablen definieren
            double ItX;
            double ItY;
            var Iteration = default(int);
            int IterationTemp;
            var StepX = default(double);
            var StepY = default(double);
            double StepZ_Temp;
            double PocketX;
            double PocketY;
            double PocketsizeXH;
            double PocketsizeYH;
            double DepthZ_ORG;
            double Z;
            double XX;
            double YY;
            int ZLoop;
            int N;
            int Turn;
            int X;
            double XTemp;
            double YTemp;
            int Radius_Loop;
            var Radius_Temp = default(double);
            double XTemp_R;
            double YTemp_R;
            double ItX1;
            double ItY1;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX <= DT | PocketSizeY <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String256"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text) | PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String256"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Radius < DT / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String257"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Radius > PocketSizeX / 2d | Radius > PocketSizeY / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String258"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (RemoveOFin == true & OFin < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String259"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));                             // Zustellung in X/Y
            StepZFin = Conversions.ToDouble(Strings.Format(1, "###0.000"));                                 // Zustellung pro Umlauf beim Schlichten
            FPlunge = 800;                                                                                  // Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                // Zyklusposition in Z
            // interne Variablen
            StepZ_Temp = StepZ / 2d;
            PocketsizeXH = PocketSizeX / 2d;
            PocketsizeYH = PocketSizeY / 2d;
            PocketX = (PocketSizeX - DT) / 2d;
            PocketY = (PocketSizeY - DT) / 2d;
            Turn = 1;
            Radius_Loop = 1;
            N = 0;
            if (RemoveOFin == false)
            {
                // Tasche nur schruppen (Schlichtaufmaß ist 0.0)
                // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Zyklusbeginn -----------------------------------------------------------------------
                // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                ItX = PocketSizeX / 2d - OFin;
                ItY = PocketSizeY / 2d - OFin;
                // Sicherheitsüberprüfungen
                if (ItX < 0d)
                    ItX = 0d;
                if (ItY < 0d)
                    ItY = 0d;
                if (ItX <= ItY)
                    Iteration = (int)Round(ItY / StepXY);
                if (ItY <= ItX)
                    Iteration = (int)Round(ItX / StepXY);
                Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                IterationTemp = Iteration - 1;
                // Programmabbruch, wenn Iteration = 0 ist
                if (Iteration == 0)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String260"), (MsgBoxStyle)16, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                    return;
                }
                // Zustellung in X und Y ermitteln - Resultate = "StepX"  und "StepY"
                if (ItY > ItX)
                {
                    StepY = StepXY;
                    StepX = PocketX / IterationTemp;
                }
                if (ItX > ItY)
                {
                    StepX = StepXY;
                    StepY = PocketY / IterationTemp;
                }
                if (ItX == ItY) // Sonderfall X=Y
                {
                    StepY = StepXY;
                    StepX = StepXY;
                    IterationTemp = (int)Round(PocketsizeXH / StepXY);
                    IterationTemp = Conversion.Int(IterationTemp);
                    IterationTemp--;
                    if (IterationTemp * StepXY >= PocketX + StepXY) // vermeidet Leerfahrt auf letzter Bahn einer Ebene
                    {
                        IterationTemp--;
                    }
                }
                if (StartPosZ >= 0d)
                    StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
                if (StartPosZ < 0d)
                    StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                DepthZ_ORG = DepthZ / StepZ;
                ZLoop = (int)Round(DepthZ / StepZ);
                if (DepthZ_ORG > ZLoop)
                {
                    ZLoop++;
                }
                // Äußere Schleife in Z
                var loopTo = ZLoop;
                for (N = 1; N <= loopTo; N++)
                {
                    Z = -(N * StepZ);
                    if (Z < -DepthZ)
                        Z = -DepthZ;
                    // Erstes in der Mitte hin und her pendeln
                    // Automatische Abfrage nach der längsten Seite
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                    // Wenn X die längste Seite ist, oder X=Y ist
                    if (PocketX >= PocketY)
                    {
                        XX = (PocketSizeX / 2d - DT) / 2d - OFin;
                        if (Turn == 1)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z + 2d * (StepZ / 3d)).ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);

                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XX).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // Wenn Y die längste Seite ist
                    if (PocketY > PocketX)
                    {
                        YY = (PocketSizeY / 2d - DT) / 2d - OFin;
                        if (Turn == 1)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // Innere Schleife "Umrandung"
                    var loopTo1 = IterationTemp;
                    for (X = 0; X <= loopTo1; X++)
                    {
                        XTemp = X * StepX;
                        YTemp = X * StepY;
                        if (XTemp > PocketX - OFin)
                            XTemp = PocketX - OFin;
                        if (YTemp > PocketY - OFin)
                            YTemp = PocketY - OFin;
                        if (Radius <= DT / 2d)
                        {
                            // Rechteckige Verfahrbewegung
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Lange Seite X oder X=Y
                        if (Radius > DT / 2d & XTemp >= YTemp)
                        {
                            if (Radius_Loop == 1)
                            {
                                Radius_Temp = Radius - DT / 2d;
                                Radius_Loop = 0;
                            }
                            XTemp_R = XTemp - Radius_Temp;
                            YTemp_R = YTemp - Radius_Temp;
                            // Ecke rechts-oben
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke links oben
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke links-unten
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke rechts unten
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                        // Lange Seite  Y
                        if (Radius > DT / 2d & YTemp > XTemp)
                        {
                            if (Radius_Loop == 1)
                            {
                                Radius_Temp = Radius - DT / 2d;
                                Radius_Loop = 0;
                            }
                            XTemp_R = XTemp - Radius_Temp;
                            YTemp_R = YTemp - Radius_Temp;
                            // Ecke links oben
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke links-unten
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke rechts unten
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke rechts-oben
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    if (Z > -DepthZ)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
                if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
                }
                My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String363");
            }
            else
            {
                // Tasche nur schlichten, Schlichtaufmaß X/Y wird entfernt
                // Zyklusbeginn -----------------------------------------------------------------------
                // Zahl der Umlaeufe ermitteln; Resultat= "Iteration"
                ItX = PocketSizeX / 2d;
                ItY = PocketSizeY / 2d;
                ItX1 = PocketSizeX / 2d - OFin;
                ItY1 = PocketSizeY / 2d - OFin;
                // Sicherheitsüberprüfungen
                if (ItX < 0d)
                    ItX = 0d;
                if (ItY < 0d)
                    ItY = 0d;
                if (ItX < ItY)
                {
                    Iteration = (int)Round((ItY - ItY1) / StepXY);
                    StartPosX = 0d;
                    StartPosY = ItY1 - DT - DT / 2d;
                }
                if (ItY <= ItX)
                {
                    Iteration = (int)Round((ItX - ItX1) / StepXY);
                    StartPosX = ItX1 - DT - DT / 2d;
                    StartPosY = 0d;
                }
                // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                IterationTemp = Iteration - 1;
                // Zustellung in X und Y ermitteln - Resultate = "StepX"  und "StepY"
                StepX = OFin / Iteration;
                StepY = OFin / Iteration;
                // nur eine Zustellung, wenn Iteration = 0 ist
                if (Iteration == 0)
                {
                    StepX = OFin;
                    StepY = OFin;
                    IterationTemp = 0;
                }
                if (StartPosZ >= 0d)
                    StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
                if (StartPosZ < 0d)
                    StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
                DepthZ_ORG = DepthZ;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                StartPosZ -= StepZ;
                DepthZ_ORG = DepthZ / StepZ;
                ZLoop = (int)Round(DepthZ / StepZ);
                if (DepthZ_ORG > ZLoop)
                {
                    ZLoop++;
                }
                // Äußere Schleife in Z
                var loopTo2 = ZLoop;
                for (N = 1; N <= loopTo2; N++)
                {
                    Z = -(N * StepZ);
                    if (Z < -DepthZ)
                        Z = -DepthZ;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Innere Schleife "Umrandung"
                    var loopTo3 = IterationTemp;
                    for (X = 0; X <= loopTo3; X++)
                    {
                        XTemp = ItX1 - DT / 2d + StepX * (X + 1);
                        YTemp = ItY1 - DT / 2d + StepY * (X + 1);
                        if (Radius <= DT / 2d)
                        {
                            // Rechteckige Verfahrbewegung
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);

                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Lange Seite X oder X=Y
                        if (Radius > DT / 2d & XTemp >= YTemp)
                        {
                            if (Radius_Loop == 1)
                            {
                                Radius_Temp = Radius - DT / 2d;
                                Radius_Loop = 0;
                            }
                            XTemp_R = XTemp - Radius_Temp;
                            YTemp_R = YTemp - Radius_Temp;
                            // Ecke rechts-oben
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke links oben
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke links-unten
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke rechts unten
                            if (Radius <= YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            if (Radius > YTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                        // Lange Seite  Y
                        if (Radius > DT / 2d & YTemp > XTemp)
                        {
                            if (Radius_Loop == 1)
                            {
                                Radius_Temp = Radius - DT / 2d;
                                Radius_Loop = 0;
                            }
                            XTemp_R = XTemp - Radius_Temp;
                            YTemp_R = YTemp - Radius_Temp;
                            // Ecke links oben
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke links-unten
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke rechts unten
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // Ecke rechts-oben
                            if (Radius <= XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);


                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            if (Radius > XTemp)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    if (Z > -DepthZ)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
                if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
                }
                My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String364");
            }
            // Zyklusende ------------------------------------------------------------------------
        }
        #endregion

        #region Zyklus Rechteckzapfen
        public static void Rechteckzapfen()
        {
            // interne Variablen definieren
            double X1;
            double Y1;
            double ItX;
            double ItY;
            var Iteration = default(int);
            int IterationTemp;
            var StepX = default(double);
            var StepY = default(double);
            int N;
            double XTemp;
            double YTemp;
            int ZLoop;
            double Radius_Temp;
            double XTemp_R;
            double YTemp_R;
            double Z;
            double DepthZ_ORG;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX <= 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String265"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String266"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY <= 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String267"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String268"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinX < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String269"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String270"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinY < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String271"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String272"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));                             // Zustellung in X/Y
            StepZFin = Conversions.ToDouble(Strings.Format(1, "###0.000"));                                 // Zustellung pro Umlauf beim Schlichten
            FPlunge = 800;                                                                                  // Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                // Zyklusposition in Z
            // interne Variablen
            X1 = PocketSizeX / 2d + OFinX + 0.5d;
            Y1 = PocketSizeY / 2d + OFinY + 0.5d;
            N = 0;
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
            ItX = OFinX / StepXY;
            ItY = OFinY / StepXY;
            // Sicherheitsüberprüfungen
            if (ItX < 0d)
                ItX = 0d;
            if (ItY < 0d)
                ItY = 0d;
            if (ItX <= ItY)
                Iteration = (int)Round(ItY);
            if (ItY <= ItX)
                Iteration = (int)Round(ItX);
            Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
            IterationTemp = Iteration - 1;
            // nur eine Schleife, wenn Iteration = 0 ist
            if (Iteration == 0)
            {
                Iteration = 1;
            }
            // Zustellung in X und Y ermitteln - Resultate = "StepX"  und "StepY"
            if (ItY > ItX)
            {
                StepY = StepXY;
                StepX = OFinX / IterationTemp;
            }
            if (ItX > ItY)
            {
                StepX = StepXY;
                StepY = OFinY / IterationTemp;
            }
            if (ItX == ItY) // Sonderfall X=Y
            {
                StepY = StepXY;
                StepX = StepXY;
                IterationTemp = (int)Round(OFinX / StepXY);
                IterationTemp = Conversion.Int(IterationTemp);
                IterationTemp--;
                if (IterationTemp * StepXY >= OFinX + StepXY) // vermeidet Leerfahrt auf letzter Bahn einer Ebene
                {
                    IterationTemp--;
                }
            }
            // Startposition in Z
            if (StartPosZ >= 0d)
                StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
            if (StartPosZ < 0d)
                StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            StartPosZ -= StepZ;
            DepthZ_ORG = DepthZ / StepZ;
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // Äußere Schleife in Z
            var loopTo = ZLoop;
            for (N = 1; N <= loopTo; N++)
            {
                Z = -(N * StepZ);
                if (Z < -DepthZ)
                    Z = -DepthZ;
                // wenn Aufmaß in X oder in Y größer Werkzeugradius ist
                if (OFinX > StepXY | OFinY > StepXY)
                {
                    // Zapfen vorschruppen, Restmaterial entfernen
                    // wenn X die längste Seite ist
                    if (X1 > Y1)
                    {
                        // Startposition in X und Y
                        StartPosX = 0d;
                        StartPosY = Y1 + StepXY;
                        if (N < 2)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StartPosY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Innere Schleife "Umrandung"
                        for (int X = 0, loopTo1 = IterationTemp; X <= loopTo1; X++)
                        {
                            XTemp = X1 - X * StepX;
                            if (XTemp <= PocketSizeX / 2d + StepXY + 0.5d)
                            {
                                XTemp = PocketSizeX / 2d + StepXY + 0.5d;
                            }
                            YTemp = Y1 - X * StepY;
                            if (YTemp <= PocketSizeY / 2d + StepXY + 0.5d)
                            {
                                YTemp = PocketSizeY / 2d + StepXY + 0.5d;
                            }
                            // wenn Radius = 0 oder Radius > 0 und schruppen
                            if (Radius == 0d | Radius > 0d & X < IterationTemp)
                            {
                                // Rechteckige Verfahrbewegung
                                if (X < 1)
                                {
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                }
                                else
                                {
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                }
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // wenn Radius > 0 und letzter Umlauf vor dem schlichten
                            if (Radius > 0d & X == IterationTemp)
                            {
                                Radius_Temp = Radius / 2d;
                                XTemp_R = XTemp - Radius_Temp;
                                YTemp_R = YTemp - Radius_Temp;
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                    // wenn Y die längste Seite ist, oder X=Y ist
                    if (Y1 >= X1)
                    {
                        // Startposition in X und Y
                        StartPosX = X1 + StepXY;
                        StartPosY = 0d;
                        if (N < 2)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Innere Schleife "Umrandung"
                        for (int X = 0, loopTo2 = IterationTemp; X <= loopTo2; X++)
                        {
                            XTemp = X1 - X * StepX;
                            if (XTemp <= PocketSizeX / 2d + StepXY + 0.5d)
                            {
                                XTemp = PocketSizeX / 2d + StepXY + 0.5d;
                            }
                            YTemp = Y1 - X * StepY;
                            if (YTemp <= PocketSizeY / 2d + StepXY + 0.5d)
                            {
                                YTemp = PocketSizeY / 2d + StepXY + 0.5d;
                            }
                            // wenn Radius = 0 oder Radius > 0 und schruppen
                            if (Radius == 0d | Radius > 0d & X < IterationTemp)
                            {
                                // Rechteckige Verfahrbewegung
                                if (X < 1)
                                {
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                }
                                else
                                {
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                }
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            // wenn Radius > 0 und letzter Umlauf vor dem schlichten
                            if (Radius > 0d & X == IterationTemp)
                            {
                                Radius_Temp = Radius / 2d;
                                XTemp_R = XTemp - Radius_Temp;
                                YTemp_R = YTemp - Radius_Temp;
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                }
                // Zapfen auf Fertigmaß schlichten
                XTemp = PocketSizeX / 2d + StepXY;
                YTemp = PocketSizeY / 2d + StepXY;
                // Wenn Aufmaß kleiner oder gleich Werkzeugradius ist
                if (OFinX <= StepXY | OFinY <= StepXY)
                {
                    if (N < 2)
                    {
                        // Wenn X die längste Seite ist
                        if (X1 > Y1)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-(YTemp + 1d)).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Wenn Y die längste Seite ist, oder X=Y ist
                        if (Y1 >= X1)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + (XTemp + 1d).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                // Wenn X die längste Seite ist
                if (X1 > Y1)
                {
                    // wenn Radius = 0
                    if (Radius == 0d)
                    {
                        // Rechteckige Verfahrbewegung
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-(YTemp + 1d)).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // wenn Radius > 0
                    if (Radius > 0d)
                    {
                        Radius_Temp = Radius + StepXY;
                        XTemp_R = XTemp - Radius_Temp;
                        YTemp_R = YTemp - Radius_Temp;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-(YTemp + 1d)).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                // Wenn Y die längste Seite ist, oder X=Y ist
                if (Y1 >= X1)
                {
                    // wenn Radius = 0
                    if (Radius == 0d)
                    {
                        // Rechteckige Verfahrbewegung
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (XTemp + 1d).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // wenn Radius > 0
                    if (Radius > 0d)
                    {
                        Radius_Temp = Radius + StepXY;
                        XTemp_R = XTemp - Radius_Temp;
                        YTemp_R = YTemp - Radius_Temp;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (XTemp + 1d).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String365");
        }
        #endregion

        #region Zyklus Kreistasche
        public static void Kreistasche()
        {
            // interne Variablen definieren
            int N;
            int Iteration;
            int IterationTemp;
            var StepXY1 = default(double);
            double StepXY2;
            double DepthZ_ORG;
            int ZLoop;
            double Z;
            double Z1;
            var Step_Temp = default(double);
            double Z_Temp;
            double SZ;
            double X1;
            var Y1 = default(double);
            double Z2;
            var Temp_XY = default(double);
            double StepXY3;
            SZ = PocketSizeX - OFinX;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (OFinX > PocketSizeX)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String273"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (SZ > DT / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String274"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String275"));
                RemoveOFin = false;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));                               // Zustellung in X/Y
            FPlunge = 800;                                                       // Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                   // Zyklusposition in Z
            Z = 0d;
            // interne Variablen
            N = 0;
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Tasche schruppen wenn schlichten deaktiviert und Fertigteildurchmesser >= 1,2x Werkzeugdurchmesser ist
            if (RemoveOFin == false & PocketSizeX >= DT * 1.2d)
            {
                // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                Iteration = (int)Round(PocketSizeX / 2d / StepXY);
                Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                IterationTemp = Iteration - 1;
                // nur eine Schleife, wenn Iteration = 0 ist
                if (Iteration == 0)
                {
                    Iteration = 1;
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String276") + Constants.vbCrLf);
                if (StartPosZ >= 0d)
                    StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
                if (StartPosZ < 0d)
                    StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
                StartPosZ -= StepZ;
                DepthZ_ORG = DepthZ / StepZ;
                ZLoop = (int)Round(DepthZ / StepZ);
                if (DepthZ_ORG > ZLoop)
                {
                    ZLoop++;
                }
                // Äußere Schleife in Z
                var loopTo = ZLoop;
                for (N = 1; N <= loopTo; N++)
                {
                    Z = -(N * StepZ);
                    Z1 = Z + StepZ;
                    // wenn Z > Endtiefe dann Zustellung vierteln
                    if (Z > -DepthZ)
                    {
                        Z_Temp = -(StepZ / 4d);
                    }
                    else
                    {
                        Z_Temp = (-DepthZ - Z1) / 4d;
                    }
                    if (Z < -DepthZ)
                        Z = -DepthZ;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                    // Innere Schleife - Tasche ausräumen im Helix
                    for (int X = 1, loopTo1 = IterationTemp; X <= loopTo1; X++)
                    {
                        // Zustellung in X und Y ermitteln
                        StepXY1 = X * StepXY;
                        // wird für Helixfahrt in XY benötigt
                        StepXY2 = StepXY1 / 2d;
                        // erste Runde Z in Helix zustellen
                        if (X == 1)
                        {
                            // Bogen vom Startpunkt zu Y+
                            X1 = StartPosX;
                            Y1 = StartPosY + StepXY2;
                            Z2 = Z1 + 1d * Z_Temp;
                            Step_Temp = StepXY2 / 2d;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Step_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z2.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von Y+ nach X-
                            X1 = StartPosX - StepXY2;
                            Y1 = StartPosY;
                            Z2 = Z1 + 2d * Z_Temp;
                            Step_Temp = StepXY2;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY2).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z2.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von X- nach Y-
                            X1 = StartPosX;
                            Y1 = StartPosY - StepXY2;
                            Z2 = Z1 + 3d * Z_Temp;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY2.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z2.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von Y- nach X+
                            X1 = StartPosX + StepXY2;
                            Y1 = StartPosY;
                            Z2 = Z1 + 4d * Z_Temp;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY2.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z2.ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von X+ nach Y+
                            X1 = StartPosX;
                            Y1 = StartPosY + StepXY2;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY2).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von Y+ nach Y-
                            X1 = StartPosX;
                            Y1 = StartPosY - StepXY2;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY2).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von Y- nach Y+
                            X1 = StartPosX;
                            Y1 = StartPosY + StepXY2;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY2.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            Temp_XY = StepXY;
                        }
                        else
                        {
                            // Bogen von Y+ nach Y-
                            StepXY3 = Y1;
                            X1 = StartPosX;
                            Y1 = StartPosY - ((X - 1) * StepXY + Step_Temp);
                            StepXY3 += Y1;
                            StepXY2 = Temp_XY;
                            Temp_XY += Step_Temp;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY2).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Bogen von Y- nach Y+
                            StepXY3 = Y1;
                            X1 = StartPosX;
                            Y1 = -Y1;
                            StepXY3 += Y1;
                            StepXY2 = Temp_XY;
                            Temp_XY += Step_Temp;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY2.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        if (X == IterationTemp)
                        {
                            // Bogen von Y+ nach Y-
                            StepXY3 = Y1;
                            X1 = StartPosX;
                            Y1 = StartPosY - X * StepXY;
                            StepXY3 = (StepXY3 + -Y1) / 2d;
                            StepXY2 = Temp_XY;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + X1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + Y1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY3).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // letzte seitliche Zustellung (Kreisförmig)
                    // wenn Rest zum schlichten bleibt, dann die Zustellung anpassen
                    if (StepXY1 < PocketSizeX / 2d)
                    {
                        StepXY1 = PocketSizeX / 2d - DT / 2d;
                    }
                    // Bogen nach X+
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Bogen nach Y+
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Bogen nach X-
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Bogen nach Y-
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Bogen nach X+
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    if (Z > -DepthZ)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            else
            {
                // Tasche nur schlichten
                if (OFinX / 2d > DT)
                {
                    StartPosX = OFinX / 2d - DT - 0.5d;
                }
                else
                {
                    StartPosX = 0d;
                }
                StartPosY = 0d;
                // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                Iteration = (int)Round((PocketSizeX / 2d - OFinX / 2d) / StepXY);
                Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                // nur eine Schleife, wenn Iteration = 0 ist
                if (Iteration == 0)
                {
                    Iteration = 1;
                }
                StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                   // Zyklusposition in Z
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                StartPosZ -= StepZ;
                DepthZ_ORG = DepthZ / StepZ;
                ZLoop = (int)Round(DepthZ / StepZ);
                if (DepthZ_ORG > ZLoop)
                {
                    ZLoop++;
                }
                // Äußere Schleife in Z
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String277") + Constants.vbCrLf);
                var loopTo2 = ZLoop;
                for (N = 1; N <= loopTo2; N++)
                {
                    Z = -(N * StepZ);
                    if (Z < -DepthZ)
                        Z = -DepthZ;
                    // Innere Schleife
                    for (int X = 1, loopTo3 = Iteration; X <= loopTo3; X++)
                    {
                        // Zustellung in X und Y ermitteln
                        StepXY1 = X * StepXY + (OFinX / 2d - StepXY);
                        if (StepXY1 > PocketSizeX / 2d - StepXY)
                            StepXY1 = PocketSizeX / 2d - StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Bogen nach Y+
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Bogen nach X-
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Bogen nach Y-
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Bogen nach X+
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            StartPosX = 0d;
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String366");
        }
        #endregion

        #region Zyklus Kreiszapfen
        public static void Kreiszapfen()
        {
            // interne Variablen definieren
            double X1;
            double Y1;
            double ItX;
            double ItY;
            var Iteration = default(int);
            int IterationTemp;
            var StepX = default(double);
            var StepY = default(double);
            int N;
            double XTemp;
            double YTemp;
            int ZLoop;
            double Z;
            double Z1;
            double DepthZ_ORG;
            double R1;
            double R2;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeY > PocketSizeX)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String273"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String280"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY <= 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String281"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String282"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinX < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String269"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String270"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinY < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String271"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OFinY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String272"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));                               // Zustellung in X/Y
            StepZFin = Conversions.ToDouble(Strings.Format(1, "###0.000"));                                   // Zustellung pro Umlauf beim Schlichten
            FPlunge = 800;                                                                                    // Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                  // Zyklusposition in Z
                                                                                                              // interne Variablen
            N = 0;
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zyklusbeginn -----------------------------------------------------------------------
            // Startposition in Z
            if (StartPosZ >= 0d)
                StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
            if (StartPosZ < 0d)
                StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Anzahl der Zustellungen in Z
            DepthZ_ORG = DepthZ / StepZ;
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // wenn Aufmaß in X oder in Y größer 0 ist (Restmaterial entfernen)
            if (OFinX > 0d | OFinY > 0d)
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String276") + Constants.vbCrLf);
                // auf Rechteckzapfen bis auf Frästiefe vorschruppen, Restmaterial entfernen
                X1 = PocketSizeX / 2d + OFinX;
                Y1 = PocketSizeX / 2d + OFinY;
                // Äußere Schleife in Z
                var loopTo = ZLoop;
                for (N = 1; N <= loopTo; N++)
                {
                    Z = -(N * StepZ);
                    Z1 = Z + StepZ;
                    // restliche Zustellung bis Frästiefe
                    if (Z < -DepthZ)
                        Z = -DepthZ;
                    // wenn X die längste Seite ist
                    if (X1 > Y1)
                    {
                        // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                        ItX = OFinX / StepXY;
                        ItY = OFinY / StepXY;
                        // Sicherheitsüberprüfungen
                        if (ItX < 0d)
                            ItX = 0d;
                        if (ItY < 0d)
                            ItY = 0d;
                        if (ItX <= ItY)
                            Iteration = (int)Round(ItY);
                        if (ItY <= ItX)
                            Iteration = (int)Round(ItX);
                        Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                        IterationTemp = Iteration - 1;
                        // nur eine Schleife, wenn Iteration = 0 ist
                        if (Iteration == 0)
                        {
                            Iteration = 1;
                        }
                        // Zustellung in X und Y ermitteln - Resultate = "StepX"  und "StepY"
                        if (ItY > ItX)
                        {
                            StepY = StepXY;
                            StepX = OFinX / IterationTemp;
                        }
                        if (ItX > ItY)
                        {
                            StepX = StepXY;
                            StepY = OFinY / IterationTemp;
                        }
                        if (ItX == ItY) // Sonderfall X=Y
                        {
                            StepY = StepXY;
                            StepX = StepXY;
                            IterationTemp = (int)Round(OFinX / StepXY);
                            IterationTemp = Conversion.Int(IterationTemp);
                            IterationTemp--;
                            if (IterationTemp * StepXY >= OFinX + StepXY) // vermeidet Leerfahrt auf letzter Bahn einer Ebene
                            {
                                IterationTemp--;
                            }
                        }
                        // Startposition in X und Y
                        StartPosX = 0d;
                        StartPosY = Y1 + StepXY;
                        if (N < 2)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StartPosY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z1.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Innere Schleife "Umrandung"
                        for (int X = 0, loopTo1 = IterationTemp; X <= loopTo1; X++)
                        {
                            // Vorschruppen bis an den Rohteildurchmesser
                            XTemp = X1 - X * StepX;
                            if (XTemp <= PocketSizeX / 2d + StepXY)
                            {
                                XTemp = PocketSizeX / 2d + StepXY;
                            }
                            YTemp = Y1 - X * StepY;
                            if (YTemp <= PocketSizeX / 2d + StepXY)
                            {
                                YTemp = PocketSizeX / 2d + StepXY;
                            }
                            // Rechteckige Verfahrbewegung
                            if (X < 1)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            else
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Kreiszapfen auf Rohteildurchmesser ausbilden
                        // Radien ermitteln 
                        R1 = PocketSizeX / 2d * 1.4142d;
                        R2 = PocketSizeX / 2d;
                        // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                        ItX = (R1 - R2) / StepXY;
                        Iteration = (int)Round(ItX);
                        Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                        IterationTemp = Iteration - 1;
                        // nur eine Schleife, wenn Iteration = 0 ist
                        if (Iteration == 0)
                        {
                            Iteration = 1;
                        }
                        // Zustellung in X und Y ermitteln - Resultat = "StepX"
                        StepX = StepXY;
                        IterationTemp = (int)Round((R1 - R2) / StepXY);
                        IterationTemp = Conversion.Int(IterationTemp);
                        IterationTemp--;
                        if (IterationTemp * StepXY >= R1 - R2 + StepXY) // vermeidet Leerfahrt auf letzter Bahn einer Ebene
                        {
                            IterationTemp--;
                        }
                        // Innere Schleife "Umrandung"
                        for (int M = Iteration; M >= 0; M -= 1)
                        {
                            XTemp = R1 - M * StepXY;
                            if (XTemp < R2 + StepXY)
                            {
                                XTemp = R2 + StepXY;
                            }
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StartPosY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // wenn Y die längste Seite ist, oder X=Y ist
                    if (Y1 >= X1)
                    {
                        // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                        ItX = OFinX / StepXY;
                        ItY = OFinY / StepXY;
                        // Sicherheitsüberprüfungen
                        if (ItX < 0d)
                            ItX = 0d;
                        if (ItY < 0d)
                            ItY = 0d;
                        if (ItX <= ItY)
                            Iteration = (int)Round(ItY);
                        if (ItY <= ItX)
                            Iteration = (int)Round(ItX);
                        Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                        IterationTemp = Iteration - 1;
                        // nur eine Schleife, wenn Iteration = 0 ist
                        if (Iteration == 0)
                        {
                            Iteration = 1;
                        }
                        // Zustellung in X und Y ermitteln - Resultate = "StepX"  und "StepY"
                        if (ItY > ItX)
                        {
                            StepY = StepXY;
                            StepX = OFinX / IterationTemp;
                        }
                        if (ItX > ItY)
                        {
                            StepX = StepXY;
                            StepY = OFinY / IterationTemp;
                        }
                        if (ItX == ItY) // Sonderfall X=Y
                        {
                            StepY = StepXY;
                            StepX = StepXY;
                            IterationTemp = (int)Round(OFinX / StepXY);
                            IterationTemp = Conversion.Int(IterationTemp);
                            IterationTemp--;
                            if (IterationTemp * StepXY >= OFinX + StepXY) // vermeidet Leerfahrt auf letzter Bahn einer Ebene
                            {
                                IterationTemp--;
                            }
                        }
                        // Startposition in X und Y
                        StartPosX = X1 + StepXY;
                        StartPosY = 0d;
                        if (N < 2)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z1.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Innere Schleife "Umrandung"
                        for (int X = 0, loopTo2 = IterationTemp; X <= loopTo2; X++)
                        {
                            XTemp = X1 - X * StepX;
                            // Vorschruppen bis an den Rohteildurchmesser
                            if (XTemp <= PocketSizeX / 2d + StepXY)
                            {
                                XTemp = PocketSizeX / 2d + StepXY;
                            }
                            YTemp = Y1 - X * StepY;
                            if (YTemp <= PocketSizeX / 2d + StepXY)
                            {
                                YTemp = PocketSizeX / 2d + StepXY;
                            }
                            // Rechteckige Verfahrbewegung
                            if (X < 1)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            else
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Kreiszapfen auf Rohteildurchmesser ausbilden
                        // Radien ermitteln 
                        R1 = PocketSizeX / 2d * 1.4142d;
                        R2 = PocketSizeX / 2d;
                        // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
                        ItX = (R1 - R2) / StepXY;
                        Iteration = (int)Round(ItX);
                        Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
                        IterationTemp = Iteration - 1;
                        // nur eine Schleife, wenn Iteration = 0 ist
                        if (Iteration == 0)
                        {
                            Iteration = 1;
                        }
                        // Zustellung in X und Y ermitteln - Resultat = "StepX"
                        StepX = StepXY;
                        IterationTemp = (int)Round((R1 - R2) / StepXY);
                        IterationTemp = Conversion.Int(IterationTemp);
                        IterationTemp--;
                        if (IterationTemp * StepXY >= R1 - R2 + StepXY) // vermeidet Leerfahrt auf letzter Bahn einer Ebene
                        {
                            IterationTemp--;
                        }
                        // Innere Schleife "Umrandung"
                        for (int M = Iteration; M >= 0; M -= 1)
                        {
                            XTemp = R1 - M * StepXY;
                            if (XTemp < R2 + StepXY)
                            {
                                XTemp = R2 + StepXY;
                            }
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            // wenn Aufmaß in X oder in Y gleich 0 ist (nur Zapfen schlichten)
            // Radien ermitteln 
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String277") + Constants.vbCrLf);
            R1 = PocketSizeX / 2d;
            R2 = PocketSizeY / 2d;
            // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
            ItX = (R1 - R2) / StepXY;
            Iteration = (int)Round(ItX);
            Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
            // Äußere Schleife in Z
            var loopTo3 = ZLoop;
            for (N = 1; N <= loopTo3; N++)
            {
                Z = -(N * StepZ);
                Z1 = Z + StepZ;
                // restliche Zustellung bis Frästiefe
                if (Z < -DepthZ)
                    Z = -DepthZ;
                // Startposition in X und Y
                StartPosX = R1 + 2d * StepXY;
                StartPosY = 0d;
                if (N < 2)
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                // Form2.Ausgabe.AppendText("G0 Z" + Z1.ToString("##0.0", Globalization.CultureInfo.InvariantCulture) + vbCrLf)
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Innere Schleife "Umrandung"
                for (int X = 0, loopTo4 = Iteration; X <= loopTo4; X++)
                {
                    XTemp = R2 + X * StepXY;
                    if (XTemp < R2 + StepXY)
                    {
                        XTemp = R2 + StepXY;
                    }
                    if (X < 1)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                // Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", Globalization.CultureInfo.InvariantCulture) + vbCrLf)
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String367");
        }
        #endregion

        #region Zyklus Ringnut
        public static void Ringnut()
        {
            // interne Variablen definieren
            int N;
            int Iteration;
            int IterationTemp;
            double StepXY1;
            double StepXY2;
            double DepthZ_ORG;
            int ZLoop;
            double Z;
            double Z1;
            double Z_Temp;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX / 2d < PocketSizeY / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String284"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX / 2d - PocketSizeY / 2d <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String285"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text) | PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String286"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));                               // Zustellung in X/Y
            FPlunge = 800;                                                       // Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));                                   // Zyklusposition in Z
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
            Iteration = (int)Round((PocketSizeX / 2d - PocketSizeY / 2d) / StepXY);
            Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
            IterationTemp = Iteration - 1;
            // nur eine Schleife, wenn Iteration = 0 ist
            if (Iteration == 0)
            {
                Iteration = 1;
            }
            StartPosX = PocketSizeY / 2d + StepXY;
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            if (StartPosZ >= 0d)
                StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
            if (StartPosZ < 0d)
                StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
            StartPosZ -= StepZ;
            DepthZ_ORG = DepthZ / StepZ;
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // Äußere Schleife in Z
            var loopTo = ZLoop;
            for (N = 1; N <= loopTo; N++)
            {
                Z = -(N * StepZ);
                Z1 = Z + StepZ;
                // wenn Z > Endtiefe dann Zustellung vierteln
                if (Z > -DepthZ)
                {
                    Z_Temp = -(StepZ / 4d);
                }
                else
                {
                    Z_Temp = (-DepthZ - Z1) / 4d;
                }
                // den Rest bis Endtiefe ermitteln
                if (Z < -DepthZ)
                    Z = -DepthZ;
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                // Innere Schleife
                for (int X = 0, loopTo1 = IterationTemp; X <= loopTo1; X++)
                {
                    // Zustellung in X und Y ermitteln
                    StepXY1 = StartPosX + X * StepXY;
                    StepXY2 = PocketSizeX / 2d - StepXY;
                    if (StepXY1 > StepXY2)
                    {
                        StepXY1 = StepXY2;
                    }
                    // erste Bahn (am Innendurchmesser), Z in Helix zustellen
                    if (X == 0)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // zweite Bahn (am Innendurchmesser) in letzter Z-Zustellung
                        if (N == ZLoop)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    else
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-StepXY1).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            StartPosX = 0d;
            StartPosY = 0d;
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String368");
        }
        #endregion

        #region Zyklus Bohrzyklus
        public static void Bohrzyklus()
        {
            // interne Variablen definieren
            int N;
            int X;
            int ZLoop;
            double DepthZ_ORG;
            double Z;
            double Z1;
            double Z_Temp;
            string Koment;
            // Eingabefehler abfangen
            ErrorMessage(true);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (DepthBs < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String293"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (DepthRs < 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String294"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (DepthAst > 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String295"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Drehwinkel > 90d | Drehwinkel < -90)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String296"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            for (N = 0; N <= 12; N++)
            {
                if (BP[N, 0] != 10000d & BP[N, 1] != 10000d)
                {
                    if (BP[N, 0] > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
                    {
                        Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String301") + (N + 1) + My.MyProject.Forms.Form1.rm.GetString("String302"), (MsgBoxStyle)48, "Eingabefehler");
                        My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                        return;
                    }
                    if (BP[N, 1] > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
                    {
                        Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String301") + (N + 1) + My.MyProject.Forms.Form1.rm.GetString("String303"), (MsgBoxStyle)48, "Eingabefehler");
                        My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                        return;
                    }
                }
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));        	// Zustellung in X/Y
            FPlunge = 800;                                                       		// Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));          	// Zyklusposition in Z
            Z = 0d;
            // interne Variablen
            N = 0;
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
        weiter:
            ;
            // ist eine Bohrposition vorhanden
            if (BP[N, 0] != 10000d & BP[N, 1] != 10000d)
            {
                Koment = My.MyProject.Forms.Form1.rm.GetString("String297") + (N + 1) + " ";
                if (BP[N, 2] != 10000d)
                {
                    Koment = Koment + "- " + BP[N, 2] + "° ";
                }
                Koment += My.MyProject.Forms.Form1.rm.GetString("String298");
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Koment + Constants.vbCrLf);
                StartPosX = BP[N, 0];
                StartPosY = BP[N, 1];
                // Positionieren
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Anzentrieren
                Z = StartPosZ - 0.3d;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Rückzug auf Z=0
                Z = 0d;
                Z_Temp = Z;                  // Merker für aktuelle Bohrtiefe
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Anzahl der Zustellungen ermitteln
                DepthZ_ORG = DepthZ / StepZ;
                ZLoop = (int)Round(DepthZ / StepZ);
                if (DepthZ_ORG > ZLoop)
                {
                    ZLoop++;
                }
                // bohren an Bohrposition beginnt
                Z1 = 0d;                      // Merker für Spanbruch
                Z1 -= DepthBs;
                var loopTo = ZLoop;
                for (X = 1; X <= loopTo; X++)
                {
                    // Bohrtiefe um Zustellung erhöhen
                    Z -= StepZ;
                    // den Rest bis Endtiefe ermitteln
                    if (Z < -DepthZ)
                        Z = -DepthZ;
                    if (Z < Z1 & DepthBs > 0d)
                    {
                        // ist geplante Bohrtiefe < Spanbruch
                        // bohren nur bis Spanbruch
                        Z_Temp = Z - Z1;
                        Z -= Z_Temp;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Rückzug bis Spanbruch
                        Z += DepthRs;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // auf Position zurück fahren
                        Z -= DepthRs;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // weiter bis Zustelltiefe bohren
                        Z += Z_Temp;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Spanbruch um ein weiter setzen
                        Z1 -= DepthBs;
                    }
                    else
                    {
                        // bohren bis Zustelltiefe ohne Spanbruch
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // ist die Bohrtiefe tiefer als Ausspantiefe, dann ausspanen
                    if (Z < DepthAst)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + (-2).ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
            }
            N++;
            if (BP[N, 0] != 10000d & BP[N, 1] != 10000d & N < 60)
                goto weiter;
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            StartPosX = 0d;
            StartPosY = 0d;
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Show();
        }
        #endregion

        #region Zyklus Dichtungsnut
        public static void Dichtungsnut()
        {
            // interne Variablen definieren
            int N;
            int Iteration;
            int IterationTemp;
            double DepthZ_ORG;
            int ZLoop;
            double Z;
            double Z1;
            double PocketX;
            double PocketY;
            double XX;
            double YY;
            int X;
            double XTemp;
            double YTemp;
            double Radius_Temp;
            double RTemp;
            double XTemp_R;
            double YTemp_R;
            double StartPosX1;
            double StartPosY1;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String310"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String311"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX / 2d - OFinX / 2d <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String312"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY / 2d - OFinY / 2d <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String312"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Radius > PocketSizeX / 2d | Radius > PocketSizeY / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String312"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Radius < DT / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String312"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));        	// Zustellung in X/Y
            FPlunge = 800;                                                       		// Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));           	// Zyklusposition in Z
            Z = 0d;
            // interne Variablen
            N = 0;
            PocketX = PocketSizeX / 2d;
            PocketY = PocketSizeY / 2d;
            StartPosX1 = StartPosX;
            StartPosY1 = StartPosY;
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zahl der Umlaeufe ermitteln; Resultat= "$Iteration"
            Iteration = (int)Round((PocketSizeX / 2d - OFinX / 2d) / StepXY);
            Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
            IterationTemp = Iteration - 1;
            // nur eine Schleife, wenn Iteration = 0 ist
            if (Iteration == 0)
            {
                Iteration = 1;
            }
            // Wenn X die längste Seite ist, oder X=Y ist
            if (PocketX >= PocketY)
            {
                StartPosY = -(OFinY / 2d + DT / 2d);
            }
            // Wenn Y die längste Seite ist
            if (PocketY > PocketX)
            {
                StartPosX = OFinX / 2d + DT / 2d;
            }
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            if (StartPosZ >= 0d)
                StartHeight = StartPosZ + StartHeight; 		// Abnullen des Startpunktes der Z-Schleife
            if (StartPosZ < 0d)
                StartHeight = StartPosZ - (-StartHeight); 	// Abnullen des Startpunktes der Z-Schleife
            StartPosZ -= StepZ;
            DepthZ_ORG = DepthZ / StepZ;
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // Äußere Schleife in Z
            var loopTo = ZLoop;
            for (N = 1; N <= loopTo; N++)
            {
                Z = -(N * StepZ);
                Z1 = Z + StepZ;
                // den Rest bis Endtiefe ermitteln
                if (Z < -DepthZ)
                    Z = -DepthZ;
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                // wenn X die längste Seite ist, oder X=Y ist
                if (PocketX >= PocketY)
                {
                    // Werkzeug pendelnd auf Zustelltiefe eintauchen
                    XX = ((PocketSizeX - 2d * Radius) / 2d - DT) / 2d;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z + 2d * (StepZ / 3d)).ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XX).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Nut fräsen
                    var loopTo1 = IterationTemp - 1;
                    for (X = 0; X <= loopTo1; X++)
                    {
                        XTemp = OFinX / 2d + DT / 2d + X * StepXY;
                        YTemp = OFinY / 2d + DT / 2d + X * StepXY;
                        // RTemp = StepXY * (X + 1)
                        // If RTemp < (((PocketSizeX / 2) - (OFinX / 2)) / 2) Then
                        // Radius_Temp = RTemp
                        // Else
                        // Radius_Temp = ((PocketSizeX / 2) - (OFinX / 2)) - RTemp
                        // End If
                        Radius_Temp = Radius;
                        XTemp_R = XTemp - Radius_Temp;
                        YTemp_R = YTemp - Radius_Temp;
                        // Innenkontur und ausräumen
                        if (X > 0)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Ecke links unten
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Ecke links oben
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Ecke rechts oben
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Ecke rechts unten
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // Außenkontur
                    XTemp = PocketSizeX / 2d - DT / 2d;
                    YTemp = PocketSizeY / 2d - DT / 2d;
                    Radius_Temp = Radius - DT / 2d;
                    XTemp_R = XTemp - Radius_Temp;
                    YTemp_R = YTemp - Radius_Temp;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Ecke rechts unten
                    if (Radius_Temp == 0d)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // Ecke rechts oben
                    if (Radius_Temp == 0d)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // Ecke links oben
                    if (Radius_Temp == 0d)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // Ecke links unten
                    if (Radius_Temp == 0d)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                // wenn Y die längste Seite ist
                if (PocketY > PocketX)
                {
                    // Werkzeug pendelnd auf Zustelltiefe eintauchen
                    YY = ((PocketSizeY - 2d * Radius) / 2d - DT) / 2d;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z + 2d * (StepZ / 3d)).ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Nut fräsen
                    var loopTo2 = IterationTemp - 1;
                    for (X = 0; X <= loopTo2; X++)
                    {
                        XTemp = OFinX / 2d + DT / 2d + X * StepXY;
                        YTemp = OFinY / 2d + DT / 2d + X * StepXY;
                        RTemp = StepXY * (X + 1);
                        if (RTemp < (PocketSizeY / 2d - OFinY / 2d) / 2d)
                        {
                            Radius_Temp = RTemp;
                        }
                        else
                        {
                            Radius_Temp = PocketSizeY / 2d - OFinY / 2d - RTemp;
                        }
                        XTemp_R = XTemp - Radius_Temp;
                        YTemp_R = YTemp - Radius_Temp;
                        // Innenkontur und ausräumen
                        if (X > 0)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // Ecke rechts unten
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Ecke links unten
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Ecke links oben
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        // Ecke rechts oben
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // Außenkontur
                    XTemp = PocketSizeX / 2d - DT / 2d;
                    YTemp = PocketSizeY / 2d - DT / 2d;
                    Radius_Temp = Radius;
                    XTemp_R = XTemp - Radius_Temp;
                    YTemp_R = YTemp - Radius_Temp;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Ecke rechts oben
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Ecke links oben
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Radius_Temp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Ecke links unten
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + (-XTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    // Ecke rechts unten
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp_R.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-YTemp_R).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Radius_Temp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                if (Z > -DepthZ)
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                }
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String371");
        }
        #endregion

        #region Zyklus Schräge Flächen
        public static void Abzeilen()
        {
            // interne Variablen definieren
            double A;
            double B;
            int N;
            double Z;
            double Z1;
            double DepthZ_ORG;
            int ZLoop;
            double Iteration;
            int IterationTemp;
            double XTemp;
            double YTemp;
            double Wert1;
            double StartPosX1;
            double StartPosY1;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String310"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String311"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Winkel > 89)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String315"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Benötigte Fläche ab Konturkante berechnen
            A = DepthZ;
            B = Tangens(Winkel) * A;
            if (B > PocketSizeY & (My.MyProject.Forms.Form1.Kante_1.Checked == true | My.MyProject.Forms.Form1.Kante_3.Checked == true))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String316"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (B > PocketSizeX & (My.MyProject.Forms.Form1.Kante_2.Checked == true | My.MyProject.Forms.Form1.Kante_4.Checked == true))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String317"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));         	// Zustellung in X/Y
            FPlunge = 800;                                                                 	// Positioniervorschub
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));               	// Zyklusposition in Z
            Z = 0d;
            // interne Variablen
            N = 0;
            StartPosX1 = StartPosX;
            StartPosY1 = StartPosY;
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Startpunkte in Abhängigkeit der Kante ermitteln
            if (My.MyProject.Forms.Form1.Kante_1.Checked == true)                // untere Kante
            {
                StartPosX = PocketSizeX + DT;
                StartPosY = 0d - DT;
            }
            if (My.MyProject.Forms.Form1.Kante_2.Checked == true)                // linke Kante
            {
                StartPosX = 0d - DT;
                StartPosY = 0d - DT;
            }
            if (My.MyProject.Forms.Form1.Kante_3.Checked == true)                // obere Kante
            {
                StartPosX = 0d - DT;
                StartPosY = PocketSizeY + DT;
            }
            if (My.MyProject.Forms.Form1.Kante_4.Checked == true)                // rechte Kante
            {
                StartPosX = PocketSizeX + DT;
                StartPosY = PocketSizeY + DT;
            }
            // Position anfahren
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            DepthZ_ORG = DepthZ / StepZ;
            // Anzahl der Zustellungen in Z ermitteln; Resultat= "ZLoop"
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // Äußere Schleife in Z
            var loopTo = ZLoop;
            for (N = 1; N <= loopTo; N++)
            {
                // Zustellung in Z errechnen
                Z = -(N * StepZ);
                Z1 = Z + StepZ;
                // den Rest bis Endtiefe ermitteln
                if (Z < -DepthZ)
                    Z = -DepthZ;
                // Fläche neu berechnen
                A = DepthZ + Z;
                B = Tangens(Winkel) * A;
                // Anzahl der seitlichen Zustellungen ermitteln; Resultat= "IterationTemp"
                Iteration = B / StepXY;
                IterationTemp = (int)Round(Conversion.Int(Iteration));
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Ablauf in Abhängigkeit der gewählten Kante
                // untere Kante
                if (My.MyProject.Forms.Form1.Kante_1.Checked == true)
                {
                    // erste Bahn
                    if (Z > -DepthZ)
                    {
                        YTemp = -StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        XTemp = 0d - DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // zu bearbeitende Fläche ist <= Werkzeugradius
                    if (IterationTemp < 1)
                    {
                        YTemp = B - StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        XTemp = 0d - DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        if (Z > -DepthZ)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // zu bearbeitende Fläche ist > Werkzeugradius
                    if (IterationTemp > 0)
                    {
                        for (int X = 0, loopTo1 = IterationTemp; X <= loopTo1; X++)
                        {
                            YTemp = X * StepXY;
                            if (YTemp + StepXY > B)
                                YTemp = B - StepXY;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            XTemp = 0d - DT;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            if (Z > -DepthZ)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                }
                // linke Kante
                if (My.MyProject.Forms.Form1.Kante_2.Checked == true)
                {
                    // erste Bahn
                    if (Z > -DepthZ)
                    {
                        XTemp = -StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        YTemp = PocketSizeY + DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // zu bearbeitende Fläche ist <= Werkzeugradius
                    if (IterationTemp < 1)
                    {
                        XTemp = B - StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        YTemp = PocketSizeY + DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        if (Z > -DepthZ)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // zu bearbeitende Fläche ist > Werkzeugradius
                    if (IterationTemp > 0)
                    {
                        for (int X = 0, loopTo2 = IterationTemp; X <= loopTo2; X++)
                        {
                            XTemp = X * StepXY;
                            if (XTemp + StepXY > B)
                                XTemp = B - StepXY;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            YTemp = PocketSizeY + DT;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            if (Z > -DepthZ)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                }
                // obere Kante
                if (My.MyProject.Forms.Form1.Kante_3.Checked == true)
                {
                    // erste Bahn
                    if (Z > -DepthZ)
                    {
                        YTemp = PocketSizeY + StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        XTemp = PocketSizeX + DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // zu bearbeitende Fläche ist <= Werkzeugradius
                    if (IterationTemp < 1)
                    {
                        YTemp = PocketSizeY - B + StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        XTemp = PocketSizeX + DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        if (Z > -DepthZ)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // zu bearbeitende Fläche ist > Werkzeugradius
                    if (IterationTemp > 0)
                    {
                        for (int X = 0, loopTo3 = IterationTemp; X <= loopTo3; X++)
                        {
                            Wert1 = X * StepXY;
                            if (Wert1 + StepXY > B)
                                Wert1 = B - StepXY;
                            YTemp = PocketSizeY - Wert1;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            XTemp = PocketSizeX + DT;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            if (Z > -DepthZ)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                }
                // rechte Kante
                if (My.MyProject.Forms.Form1.Kante_4.Checked == true)
                {
                    // erste Bahn
                    if (Z > -DepthZ)
                    {
                        XTemp = PocketSizeX + StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        YTemp = 0d - DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // zu bearbeitende Fläche ist <= Werkzeugradius
                    if (IterationTemp < 1)
                    {
                        XTemp = PocketSizeX - B + StepXY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        YTemp = 0d - DT;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        if (Z > -DepthZ)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                    // zu bearbeitende Fläche ist > Werkzeugradius
                    if (IterationTemp > 0)
                    {
                        for (int X = 0, loopTo4 = IterationTemp; X <= loopTo4; X++)
                        {
                            Wert1 = X * StepXY;
                            if (Wert1 + StepXY > B)
                                Wert1 = B - StepXY;
                            XTemp = PocketSizeX - Wert1;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            YTemp = 0d - DT;
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 Y" + YTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            if (Z > -DepthZ)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + XTemp.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartPosZ.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            }
                        }
                    }
                }
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY1.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
        }
        #endregion

        #region Zyklus Nut
        public static void Nut()
        {
            // interne Variablen definieren
            double ItX;
            double ItY;
            var Iteration = default(int);
            int IterationTemp;
            double DepthZ_ORG;
            double StepZ_Temp;
            double PocketX;
            double PocketY;
            var Z = default(double);
            double XX;
            int ZLoop;
            int N;
            int X;
            double Temp_R;
            double WkzRadius;
            var XTemp = default(double);
            var YTemp = default(double);
            // alle Eckpunkte der Nut inkl. Mittelpunkte der Radien und Parameter für den D03-Befehl
            double P1X;
            double P1Y;
            double P0X;
            double P0Y;
            double P2X;
            double P2Y;
            double P2SX;
            double P2SY;
            double P2ZX;
            double P2ZY;
            double P2MX;
            double P2MY;
            double P3X;
            double P3Y;
            double P3SX;
            double P3SY;
            double P3ZX;
            double P3ZY;
            double P3MX;
            double P3MY;
            double P4X;
            double P4Y;
            double P4SX;
            double P4SY;
            double P4ZX;
            double P4ZY;
            double P4MX;
            double P4MY;
            double P5X;
            double P5Y;
            double P5SX;
            double P5SY;
            double P5ZX;
            double P5ZY;
            double P5MX;
            double P5MY;
            double P2I;
            double P2J;
            double P3I;
            double P3J;
            double P4I;
            double P4J;
            double P5I;
            double P5J;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeX <= DT | PocketSizeY <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String323"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX > Conversion.Val(My.MyProject.Forms.Form1.Max_X.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String324"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String325"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX < PocketSizeY)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String326"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (PocketSizeX == PocketSizeY)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String327"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Radius < DT / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String328"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Radius > PocketSizeX / 2d | Radius > PocketSizeY / 2d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String329"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (Drehwinkel > 90d | Drehwinkel < -90)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String296"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            // Konstanten
            StepXY = Conversions.ToDouble(Strings.Format(DT / 2d, "###0.000"));   // Zustellung in X/Y
            StepZFin = Conversions.ToDouble(Strings.Format(1, "###0.000"));       // Zustellung pro Umlauf beim Schlichten
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));      // Zyklusposition in Z
            // interne Variablen
            StepZ_Temp = StepZ / 2d;                                              // halbe Z-Zustellung beim pendeln
            PocketX = (PocketSizeX - DT) / 2d;                                    // halbe Nutlänge
            PocketY = (PocketSizeY - DT) / 2d;                                    // halbe Nutbreite
            WkzRadius = DT / 2d;                                                  // Werkzeugraduis
            N = 0;
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zahl der Umlaeufe ermitteln; Resultat = "$IterationTemp"
            ItX = PocketSizeX / 2d;
            ItY = PocketSizeY / 2d;
            // Sicherheitsüberprüfungen
            if (ItX < 0d)
                ItX = 0d;
            if (ItY < 0d)
                ItY = 0d;
            if (ItX <= ItY)
                Iteration = (int)Round(ItX / StepXY); // wenn X die kürzere Seite ist
            if (ItY <= ItX)
                Iteration = (int)Round(ItY / StepXY); // wenn Y die kürzere Seite ist
            Iteration = (int)Round(Conversion.Int(Iteration + 0.5d));
            IterationTemp = Iteration - 1;
            // Programmabbruch, wenn Iteration = 0 ist
            if (Iteration == 0)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String330"), (MsgBoxStyle)16, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (StartPosZ >= 0d)
                StartHeight = StartPosZ + StartHeight; 		// Abnullen des Startpunktes der Z-Schleife
            if (StartPosZ < 0d)
                StartHeight = StartPosZ - (-StartHeight); 	// Abnullen des Startpunktes der Z-Schleife
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            DepthZ_ORG = DepthZ / StepZ;
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // Äußere Schleife in Z
            // pendelnde Bewegung bei Z-Zustellung
            // Automatische Abfrage nach der längsten Seite
            var loopTo = ZLoop;
            for (N = 1; N <= loopTo; N++)
            {
                Z = -(N * StepZ);
                if (Z < -DepthZ)
                    Z = -DepthZ;
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                XX = (PocketSizeX / 2d - DT) / 2d;
                P0X = X_Position(XX, Drehwinkel);
                P0Y = Y_Position(XX, Drehwinkel);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + P0X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P0Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z + 2d * (StepZ / 3d)).ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-P0X).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-P0Y).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Innere Schleife "Kontur"
                var loopTo1 = IterationTemp;
                for (X = 0; X <= loopTo1; X++)
                {
                    XTemp = PocketX - (IterationTemp - X) - 0.5d;
                    YTemp = X * StepXY;
                    if (X == 0)
                    {
                        // pendeln in der Mitte
                        P0X = X_Position(XTemp, Drehwinkel);
                        P0Y = Y_Position(XTemp, Drehwinkel);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P0X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P0Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + (-P0X).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + (-P0Y).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        // ausfraesen der Nut von innen nach außen
                        if (YTemp <= WkzRadius)
                        {
                            Temp_R = 0d;  // keinen Radius fahren wenn Zustellung < Werkzeugradius
                        }
                        else
                        {
                            Temp_R = YTemp;
                        }
                        // alle benötigten Punkte der Fräsbahn berechnen
                        P1X = -X_Pos(YTemp, Drehwinkel);
                        P1Y = Y_Pos(YTemp, Drehwinkel);
                        // Ecke links oben ohne Radius (Ecke 1)
                        P2X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 1);
                        P2Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 1);
                        // Ecke links unten ohne Radius (Ecke 2)
                        P3X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 2);
                        P3Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 2);
                        // Ecke rechts unten ohne Radius (Ecke 3)
                        P4X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 3);
                        P4Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 3);
                        // Ecke rechts oben ohne Radius (Ecke 4)
                        P5X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 4);
                        P5Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 4);
                        // Ecke links oben mit Radius (Ecke 1)
                        P2SX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 1);
                        P2SY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 1);
                        P2ZX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 1);
                        P2ZY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 1);
                        P2MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 1);
                        P2MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 1);
                        P2I = -(P2SX - P2MX);
                        P2J = P2MY - P2SY;
                        // Ecke links unten mit Radius (Ecke 2)
                        P3SX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 2);
                        P3SY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 2);
                        P3ZX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 2);
                        P3ZY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 2);
                        P3MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 2);
                        P3MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 2);
                        P3I = -(P3SX - P3MX);
                        P3J = P3MY - P3SY;
                        // Ecke rechts unten mit Radius (Ecke 3)
                        P4SX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 3);
                        P4SY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 3);
                        P4ZX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 3);
                        P4ZY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 3);
                        P4MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 3);
                        P4MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 3);
                        P4I = P4MX - P4SX;
                        P4J = P4MY - P4SY;
                        // Ecke rechts oben mit Radius (Ecke 4)
                        P5SX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 4);
                        P5SY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 4);
                        P5ZX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 4);
                        P5ZY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 4);
                        P5MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 4);
                        P5MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 4);
                        P5I = -(P5SX - P5MX);
                        P5J = P5MY - P5SY;
                        // schreiben
                        // wenn Radius <= Werkzeugradius oder YTemp <= Werkzeugradius ist
                        if (Radius <= WkzRadius | YTemp <= WkzRadius)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P3X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P3Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P4X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P4Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P5X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P5Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                        // wenn Radius > Werkzeugradius und YTemp > WkzRadius ist
                        if (Radius > WkzRadius & YTemp > WkzRadius)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Ecke links oben
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P2SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P2ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P2I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P2J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Ecke links unten
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P3SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P3SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P3ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P3ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P3I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P3J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Ecke rechts unten
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P4SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P4SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P4ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P4ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P4I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P4J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            // Ecke rechts oben
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P5SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P5SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P5ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P5ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P5I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P5J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        }
                    }
                }
                // Restmaterial entfernen wenn vorhanden
                if (YTemp < ItY)
                {
                    YTemp = ItY - WkzRadius;
                    // ausfraesen der Nut von innen nach außen
                    if (YTemp <= WkzRadius)
                    {
                        Temp_R = 0d;  // keinen Radius fahren wenn Zustellung < Werkzeugradius
                    }
                    else
                    {
                        Temp_R = Radius - WkzRadius;
                    }   // sonnst Radius - Werkzeugradius ausbilden
                        // alle benötigten Punkte der Fräsbahn berechnen
                    P1X = -X_Pos(YTemp, Drehwinkel);
                    P1Y = Y_Pos(YTemp, Drehwinkel);
                    // Ecke links oben ohne Radius (Ecke 1)
                    P2X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 1);
                    P2Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 1);
                    // Ecke links unten ohne Radius (Ecke 2)
                    P3X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 2);
                    P3Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 2);
                    // Ecke rechts unten ohne Radius (Ecke 3)
                    P4X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 3);
                    P4Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 3);
                    // Ecke rechts oben ohne Radius (Ecke 4)
                    P5X = R_EndPos_X(XTemp, YTemp, Drehwinkel, 4);
                    P5Y = R_EndPos_Y(XTemp, YTemp, Drehwinkel, 4);
                    // Ecke links oben mit Radius (Ecke 1)
                    P2SX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 1);
                    P2SY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 1);
                    P2ZX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 1);
                    P2ZY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 1);
                    P2MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 1);
                    P2MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 1);
                    P2I = -(P2SX - P2MX);
                    P2J = P2MY - P2SY;
                    // Ecke links unten mit Radius (Ecke 2)
                    P3SX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 2);
                    P3SY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 2);
                    P3ZX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 2);
                    P3ZY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 2);
                    P3MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 2);
                    P3MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 2);
                    P3I = -(P3SX - P3MX);
                    P3J = P3MY - P3SY;
                    // Ecke rechts unten mit Radius (Ecke 3)
                    P4SX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 3);
                    P4SY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 3);
                    P4ZX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 3);
                    P4ZY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 3);
                    P4MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 3);
                    P4MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 3);
                    P4I = P4MX - P4SX;
                    P4J = P4MY - P4SY;
                    // Ecke rechts oben mit Radius (Ecke 4)
                    P5SX = R_EndPos_X(XTemp, YTemp - Temp_R, Drehwinkel, 4);
                    P5SY = R_EndPos_Y(XTemp, YTemp - Temp_R, Drehwinkel, 4);
                    P5ZX = R_EndPos_X(XTemp - Temp_R, YTemp, Drehwinkel, 4);
                    P5ZY = R_EndPos_Y(XTemp - Temp_R, YTemp, Drehwinkel, 4);
                    P5MX = R_EndPos_X(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 4);
                    P5MY = R_EndPos_Y(XTemp - Temp_R, YTemp - Temp_R, Drehwinkel, 4);
                    P5I = -(P5SX - P5MX);
                    P5J = P5MY - P5SY;
                    // schreiben
                    // wenn Radius <= Werkzeugradius ist
                    if (Radius <= WkzRadius)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P3X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P3Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P4X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P4Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P5X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P5Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    // wenn Radius > Werkzeugradius ist
                    if (Radius > WkzRadius)
                    {
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P2SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P2ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P2I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P2J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P3SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P3SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P3ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P3ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P3I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P3J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P4SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P4SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P4ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P4ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P4I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P4J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P5SX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P5SY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P5ZX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P5ZY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + P5I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + P5J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            if (Z > -DepthZ)
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String373");
        }
        #endregion

        #region Zyklus runde Nut
        public static void Rundnut()
        {
            // interne Variablen definieren
            double StepZ_Temp;
            int N;
            double DepthZ_ORG;
            double ZustXY;
            int X;
            int ZLoop;
            var Z = default(double);
            double Teilkreisradius;
            double Breite;
            double P1X;
            double P1Y;
            double P2X;
            double P2Y;
            double I;
            double J;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            if (PocketSizeY <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String323"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (TKD / 2d + PocketSizeY > Conversion.Val(My.MyProject.Forms.Form1.Max_Y.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String331"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (TKD <= DT)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String332"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (SW > 360d | SW < -360)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String333"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            if (OW > 360d | OW < -360)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String334"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                return;
            }
            Teilkreisradius = TKD / 2d;
            My.MyProject.Forms.Form2.TKR = (float)Teilkreisradius;
            My.MyProject.Forms.Form2.RN_startangle = (float)SW;
            My.MyProject.Forms.Form2.RN_sweepangle = (float)OW;
            // Konstanten
            StepZFin = Conversions.ToDouble(Strings.Format(1, "###0.000"));     // Zustellung pro Umlauf beim Schlichten
            StartPosZ = Conversions.ToDouble(Strings.Format(0, "###0.000"));    // Zyklusposition in Z
            // interne Variablen
            StepZ_Temp = StepZ / 2d;                                            // halbe Z-Zustellung beim pendeln
            Breite = PocketSizeY / 2d;                                          // halbe Nutbreite
            N = 0;
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Zyklusbeginn -----------------------------------------------------------------------
            if (StartPosZ >= 0d)
                StartHeight = StartPosZ + StartHeight; // Abnullen des Startpunktes der Z-Schleife
            if (StartPosZ < 0d)
                StartHeight = StartPosZ - (-StartHeight); // Abnullen des Startpunktes der Z-Schleife
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            StartPosX = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Teilkreisradius, SW, 0d)));
            StartPosY = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Teilkreisradius, SW, 0d)));
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + StartPosZ.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            DepthZ_ORG = DepthZ / StepZ;
            ZLoop = (int)Round(DepthZ / StepZ);
            if (DepthZ_ORG > ZLoop)
            {
                ZLoop++;
            }
            // Äußere Schleife in Z
            var loopTo = ZLoop;
            for (N = 1; N <= loopTo; N++)
            {
                Z = -(N * StepZ);
                if (Z < -DepthZ)
                    Z = -DepthZ;
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String253") + N.ToString("###") + My.MyProject.Forms.Form1.rm.GetString("String255") + Z.ToString("##0.0") + " )" + Constants.vbCrLf);
                // Bewegung vom rechten zum linken Ende der Nut und Eintauchen in Z
                P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Teilkreisradius, SW, 0d)));
                P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Teilkreisradius, SW, 0d)));
                P2X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Teilkreisradius, SW, OW)));
                P2Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Teilkreisradius, SW, OW)));
                I = -(P1X - AbstX);
                J = -(P1Y - AbstY);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Bewegung vom linken zum rechten Ende der Nut
                P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Teilkreisradius, SW, OW)));
                P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Teilkreisradius, SW, OW)));
                P2X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Teilkreisradius, SW, 0d)));
                P2Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Teilkreisradius, SW, 0d)));
                I = AbstX - P1X;
                J = -(P1Y - AbstY);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Innere Schleife (Kontur)
                X = 0;
                ZustXY = DT / 2d;
                if (Breite < DT)
                {
                    ZustXY = Breite - DT / 2d;
                    Kontur(ZustXY, Teilkreisradius, SW, OW); // Unterprogramm aufrufen
                }
                else
                {
                    X++;
                    ZustXY = X * (DT / 2d);
                    if (Breite < ZustXY + DT / 2d)
                    {
                        goto ende;
                    }
                    Kontur(ZustXY, Teilkreisradius, SW, OW);
                } // Unterprogramm aufrufen
            ende:;
                ZustXY = Breite - DT / 2d;
                Kontur(ZustXY, Teilkreisradius, SW, OW); // Unterprogramm aufrufen
                StartPosX = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Teilkreisradius, SW, 0d)));
                StartPosY = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Teilkreisradius, SW, 0d)));
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            if (Z > -DepthZ)
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + FFinish.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " Z" + Z.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String374");
        }
        #endregion

        #region Zyklus Schrift
        public static void Schrift()
        {
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
        }
        #endregion

        #region Zyklus Gewinde fräsen
        public static void Gewinde_fr()
        {
            // interne Variablen definieren
            int N;
            double DepthZ_ORG;
            var Temp_XY = default(double);
            int ZLoop;
            double Z;
            double Z1;
            var Z_Temp = default(double);
            double SZ;
            double S_TempX;
            double S_TempY;
            SZ = PocketSizeX - OFinX;
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            // Konstanten
            Z = Conversions.ToDouble(Strings.Format(0, "###0.0"));                                             // Zyklusposition in Z
            // interne Variablen
            // Zyklusbeginn -----------------------------------------------------------------------
            // Zyklus vorbereiten (Vorpositionieren und Spindel EIN)
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + StartPosX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + StartPosY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M4 S" + Spin.ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            if (RemoveOFin == true)                                           // Innengewinde wurde ausgewählt
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100" + " Z" + Z.ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String346") + Constants.vbCrLf);
                // Senken
                StepXY = StepXY / 2d + TKD / 2d;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + (-StartPosZ).ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempX = AbstX + StepXY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempY = AbstY - StepXY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-StepXY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempX = AbstX - StepXY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + StepXY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempY = AbstY + StepXY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + StepXY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempX = AbstX + StepXY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-StepXY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z0.5" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String361") + Constants.vbCrLf);
                // Gewinde fräsen
                ZLoop = (int)Round((DepthZ + 0.5d) / StepZ);
                var loopTo = ZLoop;
                for (N = 1; N <= loopTo; N++)
                {
                    if (N == 1)
                    {
                        Z = Round(0.5d - StepZ, 3);
                    }
                    else
                    {
                        Z = Round(Z - StepZ, 3);
                    }
                    // wenn Z > Endtiefe dann Zustellung vierteln
                    Z1 = Z + StepZ;
                    if (Z > -DepthZ)
                    {
                        Z_Temp = -(StepZ / 4d);
                    }
                    else
                    {
                        Z_Temp = (-DepthZ - Z1) / 4d;
                    }
                    // Gewinde fräsen
                    Temp_XY = TKD / 2d - DT / 2d;
                    if (N == 1)
                    {
                        S_TempX = AbstX + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempY = AbstY - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempY = AbstY + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        S_TempY = AbstY - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempY = AbstY + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                Z1 = Z;
                // Rest bis finale Tiefe
                DepthZ_ORG = Round(DepthZ - StartPosZ + Z, 3);
                if (DepthZ_ORG > 0d & DepthZ_ORG < 1d * -Z_Temp)
                {
                    Z = Round(Z - 1d * -Z_Temp, 3);
                    S_TempY = AbstY - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                else if (DepthZ_ORG >= 1d * -Z_Temp & DepthZ_ORG < 2d * -Z_Temp)
                {
                    Z = Round(Z - 2d * -Z_Temp, 3);
                    S_TempY = AbstY - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                else if (DepthZ_ORG >= 2d * -Z_Temp & DepthZ_ORG < 3d * -Z_Temp)
                {
                    Z = Round(Z - 3d * -Z_Temp, 3);
                    S_TempY = AbstY - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempY = AbstY + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                else if (DepthZ_ORG >= 3d * -Z_Temp & DepthZ_ORG < 4d * -Z_Temp)
                {
                    Z = Round(Z - 4d * -Z_Temp, 3);
                    S_TempY = AbstY - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempY = AbstY + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                // am Ende in die Mitte fahren und aus dem Gewinde heraus
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z0.0" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            else                                                                // Außengewinde wurde ausgewählt
            {
                Temp_XY = TKD / 2d - StepXY / 2d;
                S_TempX = AbstX + Temp_XY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Senken
                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String346") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + (-StartPosZ).ToString("#0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempY = AbstY + Temp_XY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempX = AbstX - Temp_XY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempY = AbstY - Temp_XY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                S_TempX = AbstX + Temp_XY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Gewinde fräsen
                ZLoop = (int)Round((DepthZ + 0.5d) / StepZ);
                var loopTo1 = ZLoop;
                for (N = 1; N <= loopTo1; N++)
                {
                    if (N == 1)
                    {
                        Z = Round(0.5d - StepZ, 3);
                    }
                    else
                    {
                        Z = Round(Z - StepZ, 3);
                    }
                    // wenn Z > Endtiefe dann Zustellung vierteln
                    Z1 = Z + StepZ;
                    if (Z > -DepthZ)
                    {
                        Z_Temp = -(StepZ / 4d);
                    }
                    else
                    {
                        Z_Temp = (-DepthZ - Z1) / 4d;
                    }
                    // Gewinde fräsen
                    Temp_XY = TKD / 2d + DT / 2d;
                    if (N == 1)
                    {
                        S_TempX = AbstX + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z0.5" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String361") + Constants.vbCrLf);
                        S_TempY = AbstY + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 F" + FCut.ToString("####", System.Globalization.CultureInfo.InvariantCulture) + " X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempY = AbstY - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                    else
                    {
                        S_TempY = AbstY + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempY = AbstY - Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                        S_TempX = AbstX + Temp_XY;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    }
                }
                Z1 = Z;
                // Rest bis finale Tiefe
                DepthZ_ORG = Round(DepthZ - StartPosZ + Z, 3);
                if (DepthZ_ORG > 0d & DepthZ_ORG < 1d * -Z_Temp)
                {
                    Z = Round(Z - 1d * -Z_Temp, 3);
                    S_TempY = AbstY + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                else if (DepthZ_ORG >= 1d * -Z_Temp & DepthZ_ORG < 2d * -Z_Temp)
                {
                    Z = Round(Z - 2d * -Z_Temp, 3);
                    S_TempY = AbstY + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                else if (DepthZ_ORG >= 2d * -Z_Temp & DepthZ_ORG < 3d * -Z_Temp)
                {
                    Z = Round(Z - 3d * -Z_Temp, 3);
                    S_TempY = AbstY + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempY = AbstY - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                else if (DepthZ_ORG >= 3d * -Z_Temp & DepthZ_ORG < 4d * -Z_Temp)
                {
                    Z = Round(Z - 4d * -Z_Temp, 3);
                    S_TempY = AbstY + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 1d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + (-Temp_XY).ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 2d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempY = AbstY - Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + S_TempY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 3d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    S_TempX = AbstX + Temp_XY;
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + 0.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + Temp_XY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Z" + (Z1 + 4d * Z_Temp).ToString("0.0##", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                    Z1 = Z;
                }
                // am Ende aus dem Gewinde heraus fahren und
                S_TempX = AbstX + TKD / 2d + DT + StepXY;
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + S_TempX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + StartHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + AbstX.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + AbstY.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            }
            // Zyklusende ------------------------------------------------------------------------
            // Programmende
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + SafetyHeight.ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
            if (My.MyProject.Forms.Form1.pp == 1) // Postprozzesor = Mach3
            {
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M30" + Constants.vbCrLf);
            }
            My.MyProject.Forms.Form2.Dateiname = My.MyProject.Forms.Form1.rm.GetString("String376");
        }
        #endregion

        #region Zyklus DXF-Daten gravieren
        public static void DXFexport()
        {
            // interne Variablen definieren
            double PosXalt;
            double PosYalt;
            bool option = true;
            NumberFormatInfo provider = new()
            {
                NumberDecimalSeparator = "."
            };
            // Eingabefehler abfangen
            ErrorMessage(false);
            if (ErrM == true)
            {
                ErrM = false;
                return;
            }
            PosXalt = 0.0;
            PosYalt = 0.0;
            int j = 0;
            if (DepthZ > 0)
                DepthZ = -DepthZ;
            string z = Convert.ToDouble(DepthZ).ToString("###0.0", System.Globalization.CultureInfo.InvariantCulture);
            // Option für Ellipsen wählen
            if (My.MyProject.Forms.Form1.ellipse_option.Checked)
            {
                option = false;
            }
            // G-Code erzeugen
            foreach (DrawingObject obj in My.MyProject.Forms.Form1.objectIdentifier)
            {
                switch (obj.shapeType)
                {
                    case 2: // Linie
                    {
                        Line1 temp = (Line1)My.MyProject.Forms.Form1.gcodeList[obj.indexNo];
                        string x1 = Convert.ToDouble(temp.GetStartPoint.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string y1 = Convert.ToDouble(temp.GetStartPoint.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string x2 = Convert.ToDouble(temp.GetEndPoint.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string y2 = Convert.ToDouble(temp.GetEndPoint.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        x1 = x1.Replace(",", ".");
                        y1 = y1.Replace(",", ".");
                        x2 = x2.Replace(",", ".");
                        y2 = y2.Replace(",", ".");
                        if (j == 0) // erstes Element
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String347") + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F1000 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                            PosXalt = Convert.ToSingle(x2);
                            PosYalt = Convert.ToSingle(y2);
                            j = 1;
                        }
                        else // alle weiteren Elemente
                        {
                            if (PosXalt == Convert.ToSingle(x1) && PosYalt == Convert.ToSingle(y1))  // neuer Startpunkt ist gleich alter Zielpunkt
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                PosXalt = Convert.ToSingle(x2);
                                PosYalt = Convert.ToSingle(y2);
                            }
                            else
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String347") + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F1000 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                PosXalt = Convert.ToSingle(x2);
                                PosYalt = Convert.ToSingle(y2);
                            }
                        }
                        break;
                    }
                    case 3: // Rechteck
                    {
                            Rectangl temp = (Rectangl)My.MyProject.Forms.Form1.gcodeList[obj.indexNo];
                        int index;
                        string name = "";
                        string test = temp.blockName;
                        PointF pointF = new();
                        string x1 = "";
                        string y1 = "";
                        string x2 = "";
                        string y2 = "";
                        foreach (DrawingObject inserttest in My.MyProject.Forms.Form1.objectIdentifier)
                        {
                            if (inserttest.shapeType == 9)
                            {
                                index = inserttest.indexNo;
                                Insert insert = (Insert)My.MyProject.Forms.Form1.drawingList[index];
                                name = insert.name;
                                pointF = insert.startPoint;
                            }
                        }
                        if (test == name)
                        {
                            x1 = Convert.ToDouble(pointF.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            y1 = Convert.ToDouble(-pointF.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            x2 = Convert.ToDouble(pointF.X + temp.GetEndPoint.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            y2 = Convert.ToDouble(-pointF.Y + temp.GetEndPoint.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            x1 = x1.Replace(",", ".");
                            y1 = y1.Replace(",", ".");
                            x2 = x2.Replace(",", ".");
                            y2 = y2.Replace(",", ".");
                            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String348") + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F1000 X" + x1 + " Y" + y2 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + x2 + " Y" + y1 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                        }
                        break;
                    }
                    case 4: // Kreis
                    {
                        if (j == 0)
                            j = 1;
                        Circle1 temp = (Circle1)My.MyProject.Forms.Form1.gcodeList[obj.indexNo];
                        string x1 = Convert.ToDouble(temp.AccessCenterPoint.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string y1 = Convert.ToDouble(temp.AccessCenterPoint.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string r1 = Convert.ToDouble(temp.AccessRadius).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        x1 = x1.Replace(",", ".");
                        y1 = y1.Replace(",", ".");
                        r1 = r1.Replace(",", ".");
                        // Startpunkt
                        var y2 = Convert.ToSingle(temp.AccessCenterPoint.Y) + Convert.ToSingle(temp.AccessRadius);
                        string m1 = Convert.ToDouble(y2).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        m1 = m1.Replace(",", ".");
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String349") + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + m1 + Constants.vbCrLf);
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                        // erster Halbkreis nach -Y
                        y2 = Convert.ToSingle(temp.AccessCenterPoint.Y) - Convert.ToSingle(temp.AccessRadius);
                        m1 = Convert.ToDouble(y2).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        m1 = m1.Replace(",", ".");
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 F1000 X" + x1 + " Y" + m1 + " I0.000" + " J-" + r1 + Constants.vbCrLf);
                        // zweiter Halbkreis nach +Y
                        y2 = Convert.ToSingle(temp.AccessCenterPoint.Y) + Convert.ToSingle(temp.AccessRadius);
                        m1 = Convert.ToDouble(y2).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        m1 = m1.Replace(",", ".");
                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + x1 + " Y" + m1 + " I0.000" + " J" + r1 + Constants.vbCrLf);
                        PosXalt = Convert.ToSingle(x1);
                        PosYalt = Convert.ToSingle(m1);
                        break;
                    }
                    case 5: // Polylinie
                    {
                        Polyline temp = (Polyline)My.MyProject.Forms.Form1.gcodeList[obj.indexNo];
                        foreach (Line1 obj1 in temp.listOfLines1)
                        {
                            string x1 = Convert.ToDouble(obj1.GetStartPoint.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            string y1 = Convert.ToDouble(obj1.GetStartPoint.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            string x2 = Convert.ToDouble(obj1.GetEndPoint.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            string y2 = Convert.ToDouble(obj1.GetEndPoint.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            x1 = x1.Replace(",", ".");
                            y1 = y1.Replace(",", ".");
                            x2 = x2.Replace(",", ".");
                            y2 = y2.Replace(",", ".");
                            if (j == 0) // erstes Element
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String350") + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F1000 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                PosXalt = Convert.ToSingle(x2);
                                PosYalt = Convert.ToSingle(y2);
                                j = 1;
                            }
                            else // alle weiteren Elemente
                            {
                                if (PosXalt == Convert.ToSingle(x1) && PosYalt == Convert.ToSingle(y1))  // neuer Startpunkt ist gleich alter Zielpunkt
                                {
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                    PosXalt = Convert.ToSingle(x2);
                                    PosYalt = Convert.ToSingle(y2);
                                }
                                else
                                {
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String350") + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F1000 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                    PosXalt = Convert.ToSingle(x2);
                                    PosYalt = Convert.ToSingle(y2);
                                }
                            }
                        }
                        break;
                    }
                    case 6: // Bogen
                    {
                        Arc1 temp = (Arc1)My.MyProject.Forms.Form1.gcodeList[obj.indexNo];
                        var bogenr = temp.AccessRadius;
                        var startw = temp.AccessStartAngle;
                        var endw = temp.AccessSweepAngle;
                        var centerX = temp.AccessCenterPoint.X;
                        var centerY = temp.AccessCenterPoint.Y;
                        var startpx = bogenr * Sinus(90 - startw) + centerX;
                        var startpy = bogenr * Cosinus(90 - startw) + centerY;
                        var endpx = bogenr * Sinus(90 - endw) + centerX;
                        var endpy = bogenr * Cosinus(90 - endw) + centerY;
                        string x1 = Convert.ToDouble(startpx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string y1 = Convert.ToDouble(startpy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string x2 = Convert.ToDouble(endpx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        string y2 = Convert.ToDouble(endpy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                        x1 = x1.Replace(",", ".");
                        y1 = y1.Replace(",", ".");
                        x2 = x2.Replace(",", ".");
                        y2 = y2.Replace(",", ".");
                        // erstes Element
                        if (j == 0)
                        {
                            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String351") + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                            if (startw > endw)  // ccw (G3)
                            {
                                var abstandx = centerX - startpx;
                                var abstandy = centerY - startpy;
                                string a1 = Convert.ToDouble(abstandx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                string b1 = Convert.ToDouble(abstandy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                a1 = a1.Replace(",", ".");
                                b1 = b1.Replace(",", ".");
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + x2 + " Y" + y2 + " I" + a1 + " J" + b1 + Constants.vbCrLf);
                            }
                            else                // cw (G2)
                            {
                                var abstandx = centerX - startpx;
                                var abstandy = centerY - startpy;
                                string a1 = Convert.ToDouble(abstandx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                 string b1 = Convert.ToDouble(abstandy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                a1 = a1.Replace(",", ".");
                                b1 = b1.Replace(",", ".");
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + x2 + " Y" + y2 + " I" + a1 + " J" + b1 + Constants.vbCrLf);
                            }
                            PosXalt = Convert.ToSingle(x2);
                            PosYalt = Convert.ToSingle(y2);
                            j = 1;
                        }
                        else
                        {
                            if (PosXalt == Convert.ToSingle(x1) && PosYalt == Convert.ToSingle(y1))  // neuer Startpunkt ist gleich alter Zielpunkt
                            {
                                if (startw > endw)  // ccw (G3)
                                {
                                    var abstandx = centerX - startpx;
                                    var abstandy = centerY - startpy;
                                    string a1 = Convert.ToDouble(abstandx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string b1 = Convert.ToDouble(abstandy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    a1 = a1.Replace(",", ".");
                                    b1 = b1.Replace(",", ".");
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + x2 + " Y" + y2 + " I" + a1 + " J" + b1 + Constants.vbCrLf);
                                }
                                else                // cw (G2)
                                {
                                    var abstandx = centerX - startpx;
                                    var abstandy = centerY - startpy;
                                    string a1 = Convert.ToDouble(abstandx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string b1 = Convert.ToDouble(abstandy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    a1 = a1.Replace(",", ".");
                                    b1 = b1.Replace(",", ".");
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + x2 + " Y" + y2 + " I" + a1 + " J" + b1 + Constants.vbCrLf);
                                }
                                PosXalt = Convert.ToSingle(x2);
                                PosYalt = Convert.ToSingle(y2);
                            }
                            else
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                                My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                                if (startw > endw)  // ccw (G3)
                                {
                                    var abstandx = centerX - startpx;
                                    var abstandy = centerY - startpy;
                                    string a1 = Convert.ToDouble(abstandx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string b1 = Convert.ToDouble(abstandy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                     a1 = a1.Replace(",", ".");
                                    b1 = b1.Replace(",", ".");
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + x2 + " Y" + y2 + " I" + a1 + " J" + b1 + Constants.vbCrLf);
                                }
                                else                // cw (G2)
                                {
                                    var abstandx = centerX - startpx;
                                    var abstandy = centerY - startpy;
                                    string a1 = Convert.ToDouble(abstandx).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string b1 = Convert.ToDouble(abstandy).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    a1 = a1.Replace(",", ".");
                                    b1 = b1.Replace(",", ".");
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + x2 + " Y" + y2 + " I" + a1 + " J" + b1 + Constants.vbCrLf);
                                }
                                PosXalt = Convert.ToSingle(x2);
                                PosYalt = Convert.ToSingle(y2);
                            }
                        }
                        break;
                    }
                    case 7: // Ellipse
                    {
                        Ellipse temp = (Ellipse)My.MyProject.Forms.Form1.gcodeList[obj.indexNo];
                        var radius = temp.AccessRadius;
                        var centerX = temp.AccessCenterPoint.X;
                        var centerY = temp.AccessCenterPoint.Y;
                        PointF P1 = new();
                        PointF P2 = new();
                        float rd1, rd2;
                        float e, c, c1, p, p1, xi, yj, tempy;
                        My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String352") + Constants.vbCrLf);
                        int steps = 2;
                        int count = 0;
                        if (temp.AccessCenterPoint1.X == 0)
                        {
                            rd1 = (float)Math.Abs(temp.AccessCenterPoint1.Y * radius);
                            rd2 = temp.AccessCenterPoint1.Y;
                            e = (float)Sqrt(1 - (Pow(rd1, 2) / Pow(rd2, 2)));
                            c = (float)Pow(rd1, 2) / rd2;
                            c1 = rd1 * e;
                            p = rd2 * e;
                            p1 = (float)Pow(rd2, 2) / rd1;
                            tempy = rd2 - p;
                        }
                        else
                        {
                            rd1 = temp.AccessCenterPoint1.Y;
                            rd2 = (float)Math.Abs(temp.AccessCenterPoint1.Y * radius);
                            e = (float)Sqrt(1 - (Pow(rd2, 2) / Pow(rd1, 2)));
                            c = rd1 * e;
                            c1 = (float)Pow(rd1, 2) / rd2;
                            p = (float)Pow(rd2, 2) / rd1;
                            p1 = rd2 * e;
                            tempy = rd1 - p1;
                        }
                        if (!option)
                        {
                            do
                            {
                                if (count == 0)
                                {
                                    P1.X = centerX + (float)(rd1 * Cosinus(count));
                                    P1.Y = centerY + (float)(rd2 * Sinus(count));
                                    P2.X = centerX + (float)(rd1 * Cosinus(count + steps));
                                    P2.Y = centerY + (float)(rd2 * Sinus(count + steps));
                                    string x1 = Convert.ToDouble(P1.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string y1 = Convert.ToDouble(P1.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string x2 = Convert.ToDouble(P2.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string y2 = Convert.ToDouble(P2.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    x1 = x1.Replace(",", ".");
                                    y1 = y1.Replace(",", ".");
                                    x2 = x2.Replace(",", ".");
                                    y2 = y2.Replace(",", ".");
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F1000 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                    count += steps;
                                }
                                else
                                {
                                    P1.X = centerX + (float)(rd1 * Cosinus(count));
                                    P1.Y = centerY + (float)(rd2 * Sinus(count));
                                    P2.X = centerX + (float)(rd1 * Cosinus(count + steps));
                                    P2.Y = centerY + (float)(rd2 * Sinus(count + steps));
                                    string x1 = Convert.ToDouble(P1.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string y1 = Convert.ToDouble(P1.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string x2 = Convert.ToDouble(P2.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    string y2 = Convert.ToDouble(P2.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                                    x1 = x1.Replace(",", ".");
                                    y1 = y1.Replace(",", ".");
                                    x2 = x2.Replace(",", ".");
                                    y2 = y2.Replace(",", ".");
                                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + x2 + " Y" + y2 + Constants.vbCrLf);
                                    count += steps;
                                }
                            }
                            while (count < 360);
                            PosXalt = P2.X;
                            PosYalt = P2.Y;
                        }
                        else
                        {
                            P1.X = centerX + c;
                            P1.Y = centerY + p;
                            P2.X = centerX + c;
                            P2.Y = centerY + -p;
                            string x1 = Convert.ToDouble(P1.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            string y1 = Convert.ToDouble(P1.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            string x2 = Convert.ToDouble(P2.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            string y2 = Convert.ToDouble(P2.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            x1 = x1.Replace(",", ".");
                            y1 = y1.Replace(",", ".");
                            x2 = x2.Replace(",", ".");
                            y2 = y2.Replace(",", ".");
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 X" + x1 + " Y" + y1 + Constants.vbCrLf);
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F100 Z" + z + Constants.vbCrLf);
                            xi = -rd2;
                            yj = -p;
                            string xi1 = Convert.ToDouble(xi).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            xi1 = xi1.Replace(",", ".");
                            string yj1 = Convert.ToDouble(yj).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            yj1 = yj1.Replace(",", ".");
                            // Bogen rechts oben nach rechts unten
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + x2 + " Y" + y2 + " I" + xi1 + " J" + yj1 + Constants.vbCrLf);
                            P2.X = centerX + -c;
                            P2.Y = centerY + -p;
                            x2 = Convert.ToDouble(P2.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            y2 = Convert.ToDouble(P2.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            x2 = x2.Replace(",", ".");
                            y2 = y2.Replace(",", ".");
                            xi = -c;
                            yj = c;
                            xi1 = Convert.ToDouble(xi).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            xi1 = xi1.Replace(",", ".");
                            yj1 = Convert.ToDouble(yj).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            yj1 = yj1.Replace(",", ".");
                            // Bogen rechts unten nach links unten
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + x2 + " Y" + y2 + " I" + xi1 + " J" + yj1 + Constants.vbCrLf);
                            P2.X = centerX + -c;
                            P2.Y = centerY + p;
                            x2 = Convert.ToDouble(P2.X).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            y2 = Convert.ToDouble(P2.Y).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            x2 = x2.Replace(",", ".");
                            y2 = y2.Replace(",", ".");
                            xi = rd2;
                            yj = p;
                            xi1 = Convert.ToDouble(xi).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            xi1 = xi1.Replace(",", ".");
                            yj1 = Convert.ToDouble(yj).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            yj1 = yj1.Replace(",", ".");
                            // Bogen links unten nach links oben
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + x2 + " Y" + y2 + " I" + xi1 + " J" + yj1 + Constants.vbCrLf);
                            xi = c;
                            yj = -c;
                            xi1 = Convert.ToDouble(xi).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            xi1 = xi1.Replace(",", ".");
                            yj1 = Convert.ToDouble(yj).ToString("###0.000", System.Globalization.CultureInfo.InvariantCulture);
                            yj1 = yj1.Replace(",", ".");
                            // Bogen links oben nach rechts oben
                            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + x1 + " Y" + y1 + " I" + xi1 + " J" + yj1 + Constants.vbCrLf);
                            PosXalt = P2.X;
                            PosYalt = P2.Y;
                        }
                        break;
                    }
                }
            }
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z3.0" + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText(My.MyProject.Forms.Form1.rm.GetString("String254") + Constants.vbCrLf);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
        }
        #endregion

        #region Funktionen
        // Fehlermeldungen
        public static void ErrorMessage(bool wkz)
        {
            // Grundeinstellungen
            if (DT <= 0d | DT > 16d)
            {
                if (!wkz)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String238"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                }
                else
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String289"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                }
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (Spin <= 0)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String240"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (Spin > 25000)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String241"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (StartHeight <= 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String242"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (StartHeight > Conversion.Val(My.MyProject.Forms.Form1.Max_Z.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String243"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (DepthZ <= 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String244"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (DepthZ > Conversion.Val(My.MyProject.Forms.Form1.Max_Z.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String245"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (StepZ <= 0d)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String246"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (FFinish <= 0)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String247"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (FFinish > Conversion.Val(My.MyProject.Forms.Form1.Max_F.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String248"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (FCut <= 0)
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String249"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
            if (FCut > Conversion.Val(My.MyProject.Forms.Form1.Max_F.Text))
            {
                Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String250"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                My.MyProject.Forms.Form1.Fehler = Conversions.ToBoolean(1);
                ErrM = true;
                return;
            }
        }
        // berechnen der Sinusfunktion
        public static double Sinus(double Wert)
        {
            double Temp;
            Temp = Wert * PI / 180.0d;                      // Konvertierung von Grad in Bogenmaß
            return Sin(Temp);                               // Rückgabe als Sinus(Winkel)
        }
        // berechnen der Arcussinusfunktion
        public static double Arcsinus(double Wert)
        {
            double Temp;
            Temp = Asin(Wert) * (180.0d / PI);             // Konvertierung von Grad in Bogenmaß
            return Temp;                                    // Rückgabe als Arcsinus(Winkel)
        }
        // berechnen der Cosinusfunktion
        public static double Cosinus(double Wert)
        {
            double Temp;
            Temp = Wert * PI / 180.0d;                      // Konvertierung von Grad in Bogenmaß
            return Cos(Temp);                                // Rückgabe als Cosinus(Winkel)
        }
        // berechnen der Tangensfunktion
        public static double Tangens(double Wert)
        {
            double Temp;
            Temp = Wert * PI / 180.0d;                      // Konvertierung von Grad in Bogenmaß
            return Tan(Temp);                                // Rückgabe als Tangens(Winkel)
        }
        // berechnen der neuen X-Position
        public static double X_Position(double Wert, double D_Winkel)
        {
            double Temp;
            Temp = Wert * Cosinus(D_Winkel);
            return Temp;
        }
        // berechnen der neuen Y-Position
        public static double Y_Position(double Wert, double D_Winkel)
        {
            double Temp;
            Temp = Wert * Cosinus(90d - D_Winkel);
            return Temp;
        }
        // berechnen der neuen X-Position ohne Radius
        public static double X_Pos(double Wert, double D_Winkel)
        {
            double Temp;
            Temp = Sinus(D_Winkel) * Wert;
            return Temp;
        }
        // berechnen der neuen Y-Position ohne Radius
        public static double Y_Pos(double Wert, double D_Winkel)
        {
            double Temp;
            Temp = Cosinus(D_Winkel) * Wert;
            return Temp;
        }
        // berechnen der neuen X-Position mit Radius
        public static double R_EndPos_X(double Wert_X, double Wert_Y, double D_Winkel, int Ecke)
        {
            var A = default(double);
            var B = default(double);
            double C;
            double D;
            double E;
            double F;
            switch (Ecke)
            {
                case 1:
                    {
                        A = -Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));       // Wurzel aus X^2 und Y^2
                        B = Wert_Y / A;                                     // Verfahrweg Y / A
                        break;
                    }
                case 2:
                    {
                        A = -Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));       // Wurzel aus X^2 und Y^2
                        B = -Wert_Y / A;                                    // Verfahrweg -Y / A
                        break;
                    }
                case 3:
                    {
                        A = Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));        // Wurzel aus X^2 und Y^2
                        B = -Wert_Y / A;                                    // Verfahrweg -Y / A
                        break;
                    }
                case 4:
                    {
                        A = Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));        // Wurzel aus X^2 und Y^2
                        B = Wert_Y / A;                                     // Verfahrweg -Y / A
                        break;
                    }
            }
            C = Arcsinus(B);                         // Sinus alpha von B
            D = C + D_Winkel;                        // C + Drehwinkel
            E = Cosinus(D);                          // Cosinus von D
            F = A * E;                               // Seitelänge in X
            return F;
        }
        // berechnen der neuen Y-Position mit Radius
        public static double R_EndPos_Y(double Wert_X, double Wert_Y, double D_Winkel, int Ecke)
        {
            var A = default(double);
            var B = default(double);
            double C;
            double D;
            double E;
            double F;
            switch (Ecke)
            {
                case 1:
                    {
                        A = -Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));       // Wurzel aus X^2 und Y^2
                        B = Wert_Y / A;                                     // Verfahrweg Y / A
                        break;
                    }
                case 2:
                    {
                        A = -Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));       // Wurzel aus X^2 und Y^2
                        B = -Wert_Y / A;                                    // Verfahrweg -Y / A
                        break;
                    }
                case 3:
                    {
                        A = Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));        // Wurzel aus X^2 und Y^2
                        B = -Wert_Y / A;                                    // Verfahrweg -Y / A
                        break;
                    }
                case 4:
                    {
                        A = Sqrt(Pow(Wert_X, 2d) + Pow(Wert_Y, 2d));        // Wurzel aus X^2 und Y^2
                        B = Wert_Y / A;                                     // Verfahrweg -Y / A
                        break;
                    }
            }
            C = Arcsinus(B);                         // Sinus alpha von B
            D = C + D_Winkel;                        // C + Drehwinkel
            E = Sinus(D);                            // Sinus von D
            F = A * E;                               // Seitelänge in X
            return F;
        }
        // berechnen der X-Position von Punkt auf Kreis
        public static object Punkt_X_Kreis(double Tk_radius, double Startwinkel, double Oeffnungswinkel)
        {
            double X;
            X = Tk_radius * Sinus(90d - (Startwinkel + Oeffnungswinkel));
            return X;
        }
        // berechnen der Y-Position von Punkt auf Kreis
        public static object Punkt_Y_Kreis(double Tk_radius, double Startwinkel, double Oeffnungswinkel)
        {
            double Y;
            Y = Tk_radius * Cosinus(90d - (Startwinkel + Oeffnungswinkel));
            return Y;
        }
        // berechnen der Bogenhöhe
        public static object Bogenhoehe(double Tk_radius, double Oeffnungswinkel)
        {
            double I;
            I = Tk_radius * (1d - Cosinus(Oeffnungswinkel / 2d));
            return I;
        }
        // fräsen der Kontur einer runden Nut
        public static void Kontur(double Zust, double Tk_radius, double SW, double OW)
        {
            double P1X;
            double P1Y;
            double P2X;
            double P2Y;
            double I;
            double J;
            // Zustellung in X und Y
            P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius + Zust, SW, 0d)));
            P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius + Zust, SW, 0d)));
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 X" + P1X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P1Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Bewegung vom rechten zum linken Ende der Nut
            P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius + Zust, SW, 0d)));
            P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius + Zust, SW, 0d)));
            P2X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius + Zust, SW, OW)));
            P2Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius + Zust, SW, OW)));
            I = -(P1X - AbstX);
            J = -(P1Y - AbstY);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Radius am linken Ende der Nut
            P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius + Zust, SW, OW)));
            P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius + Zust, SW, OW)));
            P2X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius - Zust, SW, OW)));
            P2Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius - Zust, SW, OW)));
            I = Conversions.ToDouble(Operators.SubtractObject(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius, SW, OW)), P1X));
            J = Conversions.ToDouble(Operators.SubtractObject(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius, SW, OW)), P1Y));
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Bewegung vom linken zum rechten Ende der Nut
            P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius - Zust, SW, OW)));
            P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius - Zust, SW, OW)));
            P2X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius - Zust, SW, 0d)));
            P2Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius - Zust, SW, 0d)));
            I = AbstX - P1X;
            J = -(P1Y - AbstY);
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G2 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
            // Radius am rechten Ende der Nut
            P1X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius - Zust, SW, 0d)));
            P1Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius - Zust, SW, 0d)));
            P2X = Conversions.ToDouble(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius + Zust, SW, 0d)));
            P2Y = Conversions.ToDouble(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius + Zust, SW, 0d)));
            I = Conversions.ToDouble(Operators.SubtractObject(Operators.AddObject(AbstX, Punkt_X_Kreis(Tk_radius, SW, 0d)), P1X));
            J = Conversions.ToDouble(Operators.SubtractObject(Operators.AddObject(AbstY, Punkt_Y_Kreis(Tk_radius, SW, 0d)), P1Y));
            My.MyProject.Forms.Form2.Ausgabe.AppendText("G3 X" + P2X.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " Y" + P2Y.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " I" + I.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + " J" + J.ToString("##0.000", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
        }
        #endregion
    }
}