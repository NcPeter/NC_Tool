using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using static System.Math;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Resources;
using System.Reflection;

namespace NC_Tool
{
    public partial class Form2
    {
        #region Variablen
        public string Dateiname;
        public int Ansicht;                                     // allgemeine Variablen
        public int NpX;
        public int NpY;
        public int NpZ;                                         // Start- oder Antastpunkte der Zyklen (X,Y,Z)
        public int NpX1;
        public int NpY1;
        public int NpZ1;                                        // Start- oder Antastpunkte der Zyklen (X,Y,Z)
        public int[] Wks = new int[5];                          // Fertigmaße und Drehwinkel der Zyklen (X,Y,Z, Radius, Drehwinkel)
        public int[] Wks1 = new int[5];                         // Fertigmaße und Drehwinkel der Zyklen (X,Y,Z, Radius, Drehwinkel)
        public int[] Wks2 = new int[5];                         // Übergabewerte für die runde Nut
        public string[] S = new string[5];                      // Koordinaten als String (X,Y,Z,I,J)
        public int[] P = new int[5];                            // Koordinaten gewandelt in Single (X,Y,Z,I,J)
        public string Befehl;                                   // versch. Zyklus
        public float Faktor;                                    // Scalierung
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;
        public double radius;
        public int X1_2;
        public int X2_2;
        public int Y1_2;
        public int Y2_2;
        public double radius_2;
        public int X2_3;
        public int Y2_3;
        public float TKR;
        public float RN_startangle;
        public float RN_sweepangle;
        private bool myAbsofortZeichnen;
        private readonly PageSettings EinstellungDruckseite = new();
        private string DruckZeichenfolge;
        private string Infotext;
        private readonly System.Drawing.Font DruckFont = new("Courier New", 10f);
        public bool textupdate = false;
        //private DateTime m_TimeStamp;
        // Farben für Highlighting
        public Color farbeText = Color.Black;
        public Color farbeKoment = Color.DarkGreen;
        public Color farbeGx = Color.DarkMagenta;
        public Color farbeG0 = Color.Red;
        public Color farbeG1 = Color.Blue;
        public Color farbeG2 = Color.YellowGreen;
        public Color farbeG3 = Color.Green;
        public Color farbeM = Color.DarkBlue;
        public Color farbeS = Color.DarkRed;
        public Color farbeT = Color.Red;
        // Stifte
        public Pen weiss = new(Brushes.White);
        public Pen gelb = new(Brushes.Yellow);
        public Pen rot = new(Brushes.Red);
        public Pen blau = new(Brushes.Blue);
        public Pen gruen = new(Brushes.Green);
        public Pen hgruen = new(Brushes.YellowGreen);
        #endregion

        #region Enumeration
        public enum ScrollBarType : uint
        {
            SbHorz = 0,
            SbVert = 1,
            SbCtl = 2,
            SbBoth = 3
        }
        public enum Message : uint
        {
            WM_VSCROLL = 0x0115
        }
        public enum ScrollBarCommands : uint
        {
            SB_THUMBPOSITION = 4
        }
        [DllImport("User32.dll")]
        public extern static int GetScrollPos(IntPtr hWnd, int nBar);
        [DllImport("User32.dll")]
        public extern static int SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        #endregion

        public Form2()
        {
            InitializeComponent();
            Ausgabe = _Ausgabe;
            RectangleShape4 = _RectangleShape4;
            RectangleShape3 = _RectangleShape3;
            RectangleShape2 = _RectangleShape2;
            _Ausgabe.Name = "Ausgabe";
            _RectangleShape4.Name = "RectangleShape4";
            _RectangleShape3.Name = "RectangleShape3";
            _RectangleShape2.Name = "RectangleShape2";
        }

        #region Funktionen
        // bei laden des Formulares
        private void Form2_Load(object sender, EventArgs e)
        {
            Translate();
            String inputLanguage = _Ausgabe.Text;
            Regex r = new Regex("\r\n");
            String[] lines = r.Split(inputLanguage);
            foreach (string l in lines)
            {
                ParseLine(l);
            }
            textupdate = true;
            ZN_aktualisieren();
        }
        // G-Code als Datei speichern
        private void Speichern_Click(object sender, EventArgs e)
        {
            string Datei;
            StreamWriter Datei1;
            int a;
            int b;
            string c;
            Ausgabe.Text = _Editor.Text;
            SaveFileDialog1.InitialDirectory = My.MyProject.Forms.Form1.Speicherpfad.Text;
            SaveFileDialog1.FileName = Dateiname + ".nc";
            SaveFileDialog1.Filter = My.MyProject.Forms.Form1.rm.GetString("String388");
            SaveFileDialog1.FilterIndex = 1;
            SaveFileDialog1.RestoreDirectory = true;
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                b = Ausgabe.Lines.Count() - 1;
                Datei = SaveFileDialog1.FileName;
                Datei1 = My.MyProject.Computer.FileSystem.OpenTextFileWriter(Datei, false, System.Text.Encoding.Default);
                var loopTo = b;
                for (a = 0; a <= loopTo; a++)
                {
                    c = Conversions.ToString(Ausgabe.Lines.GetValue(a));
                    Datei1.WriteLine(c);
                }
                Datei1.Close();
            }
        }
        // aktuelles Fenster schließen
        private void Schließen_Click(object sender, EventArgs e)
        {
            Close();
        }
        // erste Anzeige bei Start
        private void Form2_Shown(object sender, EventArgs e)
        {
            var g = Zeichenfeld.CreateGraphics();
            Label1.Text = My.MyProject.Forms.Form1.rm.GetString("String380");
            Ansicht = 1;
            g.Clear(Color.Black);
            Darstellung_Z();
        }
        // der Text im Editor wurde verändert
        private void Ausgabe_TextChanged(object sender, EventArgs e)
        {
            // Anzeige(Ansicht)
        }
        // Ansicht von Oben (Z) aktivieren
        private void RectangleShape2_Click(object sender, EventArgs e)
        {
            var g = Zeichenfeld.CreateGraphics();
            Label1.Text = My.MyProject.Forms.Form1.rm.GetString("String380");
            Ansicht = 1;
            g.Clear(Color.Black);
            Darstellung_Z();
        }
        // Ansicht von Vorn (Y) aktivieren
        private void RectangleShape3_Click(object sender, EventArgs e)
        {
            var g = Zeichenfeld.CreateGraphics();
            Label1.Text = My.MyProject.Forms.Form1.rm.GetString("String386");
            Ansicht = 2;
            g.Clear(Color.Black);
            Darstellung_Y();
        }
        // Ansicht von der Seite (X) aktivieren
        private void RectangleShape4_Click(object sender, EventArgs e)
        {
            var g = Zeichenfeld.CreateGraphics();
            Label1.Text = My.MyProject.Forms.Form1.rm.GetString("String387");
            Ansicht = 3;
            g.Clear(Color.Black);
            Darstellung_X();
        }
        // Grafische Darstellung (Ansicht von Oben)
        private void Darstellung_Z()
        {
            X1 = (int)Round(Zeichenfeld.Width / 2d - Wks[0] / 2d);
            Y1 = (int)Round(Zeichenfeld.Height / 2d - Wks[1] / 2d);
            X2 = Wks[0];
            Y2 = Wks[1];
            X1_2 = NpX1;
            Y1_2 = NpY1;
            X2_2 = Wks1[0];
            Y2_2 = Wks1[1];
            X2_3 = Wks2[0];
            Y2_3 = Wks2[1];
            if (Wks[3] == 0)
            {
                radius = 0.001d;
            }
            else
            {
                radius = Wks[3];
            }
            if (Wks1[3] == 0)
            {
                radius_2 = 0.001d;
            }
            else
            {
                radius_2 = Wks1[3];
            }
            Zeichenfeld.Invalidate();
            myAbsofortZeichnen = true;
        }
        // Grafische Darstellung (Ansicht von Vorn)
        private void Darstellung_Y()
        {
            // Fertigmaße der Zyklen anzeigen
            X1 = (int)Round(Zeichenfeld.Width / 2d - Wks[1] / 2d);
            Y1 = (int)Round(Zeichenfeld.Height / 2d - Wks[2] / 2d);
            X2 = Wks[1];
            Y2 = Wks[2];
            X1_2 = NpY1;
            Y1_2 = NpZ1;
            X2_2 = Wks1[1];
            Y2_2 = Wks1[2];
            X2_3 = Wks2[0];
            Y2_3 = Wks2[1];
            radius = 0.001d;
            radius_2 = 0.001d;
            Zeichenfeld.Invalidate();
            myAbsofortZeichnen = true;
        }
        // Grafische Darstellung (Ansicht von der Seite)
        private void Darstellung_X()
        {
            // Fertigmaße der Zyklen anzeigen
            X1 = (int)Round(Zeichenfeld.Width / 2d - Wks[0] / 2d);
            Y1 = (int)Round(Zeichenfeld.Height / 2d - Wks[2] / 2d);
            X2 = Wks[0];
            Y2 = Wks[2];
            X1_2 = NpX1;
            Y1_2 = NpZ1;
            X2_2 = Wks1[0];
            Y2_2 = Wks1[2];
            X2_3 = Wks2[0];
            Y2_3 = Wks2[1];
            radius = 0.001d;
            radius_2 = 0.001d;
            Zeichenfeld.Invalidate();
            myAbsofortZeichnen = true;
        }
        // Fertigmaße der Zyklen grafisch darstellen
        private void Zeichenfeld_Paint(object sender, PaintEventArgs e)
        {
            if (!myAbsofortZeichnen)
                return;
            var g = Zeichenfeld.CreateGraphics();
            RectangleF locRect;
            // alle Eckpunkte der Nut inkl. Mittelpunkte der Radien und Parameter für den D03-Befehl
            int P1X;
            int P1Y;
            int P2X;
            int P2Y;
            int P3X;
            int P3Y;
            int P4X;
            int P4Y;
            int P5X;
            int P5Y;
            int P2SX;
            int P2SY;
            int P2ZX;
            int P2ZY;
            int P2MX;
            int P2MY;
            int P3SX;
            int P3SY;
            int P3ZX;
            int P3ZY;
            int P3MX;
            int P3MY;
            int P4SX;
            int P4SY;
            int P4ZX;
            int P4ZY;
            int P4MX;
            int P4MY;
            int P5SX;
            int P5SY;
            int P5ZX;
            int P5ZY;
            int P5MX;
            int P5MY;
            int X;
            int Y;
            int widht;
            int height;
            int startAngle;
            int sweepAngle;
            double XTemp;
            double YTemp;
            int FMX;
            int FMY;
            // Stifte
            weiss.Width = 1.0f;
            gelb.Width = 1.0f;
            rot.Width = 1.0f;
            blau.Width = 1.0f;
            gruen.Width = 1.0f;
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Planfraesen")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Rechtecktasche")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Rechteckzapfen")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
                // Fertigmaß + Aufmaß als Rechteck
                {
                    locRect = new RectangleF(X1_2, Y1_2, X2_2, Y2_2);
                }
                DrawRoundedRect(e.Graphics, gelb, locRect, (float)radius_2);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Kreistasche")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Kreiszapfen")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
                // Fertigmaß + Aufmaß als Rechteck
                {
                    locRect = new RectangleF(X1_2, Y1_2, X2_2, Y2_2);
                }
                DrawRoundedRect(e.Graphics, gelb, locRect, (float)radius_2);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Ringnut")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
                // Fertigmaß + Aufmaß als Rechteck
                {
                    locRect = new RectangleF(X1_2, Y1_2, X2_2, Y2_2);
                }
                DrawRoundedRect(e.Graphics, gelb, locRect, (float)radius_2);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Bohrtabelle")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Lochkreis")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Dichtungsnut")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            X1_2 = (int)Round(Zeichenfeld.Width / 2d - X2_2 / 2d);
                            Y1_2 = (int)Round(Zeichenfeld.Height / 2d - Y2_2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
                // Fertigmaß + Aufmaß als Rechteck
                {
                    locRect = new RectangleF(X1_2, Y1_2, X2_2, Y2_2);
                }
                DrawRoundedRect(e.Graphics, gelb, locRect, (float)radius_2);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Abzeilen")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Bohrmatrix")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "runde Nut")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Nut")
            {
                NpX = (int)Round(Zeichenfeld.Width / 2d);     // Mitte Zeichenfeld in X
                NpY = (int)Round(Zeichenfeld.Height / 2d);    // Mitte Zeichenfeld in Y
                XTemp = Wks[0] / 2d;            // Mitte der Nut in X
                YTemp = Wks[1] / 2d + 1d;        // Mitte der Nut in Y
                widht = (int)Round(radius * 2d);
                height = (int)Round(radius * 2d);
                sweepAngle = 90;
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            // alle benötigten Punkte
                            // Startpunkt
                            P1X = (int)Round(NpX + -Zyklen.X_Pos(YTemp, Zyklen.Drehwinkel));
                            P1Y = (int)Round(NpY - Zyklen.Y_Pos(YTemp, Zyklen.Drehwinkel));
                            // Ecke links oben mit Radius (Ecke 1)
                            P2SX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp, Zyklen.Drehwinkel, 1));
                            P2SY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp, Zyklen.Drehwinkel, 1));
                            P2ZX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp, YTemp - radius, Zyklen.Drehwinkel, 1));
                            P2ZY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp, YTemp - radius, Zyklen.Drehwinkel, 1));
                            P2MX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 1));
                            P2MY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 1));
                            X = (int)Round(P2MX - radius);
                            Y = (int)Round(P2MY - radius);
                            startAngle = (int)Round(180d - Zyklen.Drehwinkel);
                            g.DrawLine(weiss, P1X, P1Y, P2SX, P2SY);
                            g.DrawArc(weiss, X, Y, widht, height, startAngle, sweepAngle);
                            // Ecke links unten mit Radius (Ecke 2)
                            P3SX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp, YTemp - radius, Zyklen.Drehwinkel, 2));
                            P3SY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp, YTemp - radius, Zyklen.Drehwinkel, 2));
                            P3ZX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp, Zyklen.Drehwinkel, 2));
                            P3ZY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp, Zyklen.Drehwinkel, 2));
                            P3MX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 2));
                            P3MY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 2));
                            X = (int)Round(P3MX - radius);
                            Y = (int)Round(P3MY - radius);
                            startAngle = (int)Round(90d - Zyklen.Drehwinkel);
                            g.DrawLine(weiss, P2ZX, P2ZY, P3SX, P3SY);
                            g.DrawArc(weiss, X, Y, widht, height, startAngle, sweepAngle);
                            // Ecke rechts unten mit Radius (Ecke 3)
                            P4SX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp, Zyklen.Drehwinkel, 3));
                            P4SY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp, Zyklen.Drehwinkel, 3));
                            P4ZX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp, YTemp - radius, Zyklen.Drehwinkel, 3));
                            P4ZY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp, YTemp - radius, Zyklen.Drehwinkel, 3));
                            P4MX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 3));
                            P4MY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 3));
                            X = (int)Round(P4MX - radius);
                            Y = (int)Round(P4MY - radius);
                            startAngle = (int)Round(0d - Zyklen.Drehwinkel);
                            g.DrawLine(weiss, P3ZX, P3ZY, P4SX, P4SY);
                            g.DrawArc(weiss, X, Y, widht, height, startAngle, sweepAngle);
                            // Ecke rechts oben mit Radius (Ecke 4)
                            P5SX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp, YTemp - radius, Zyklen.Drehwinkel, 4));
                            P5SY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp, YTemp - radius, Zyklen.Drehwinkel, 4));
                            P5ZX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp, Zyklen.Drehwinkel, 4));
                            P5ZY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp, Zyklen.Drehwinkel, 4));
                            P5MX = (int)Round(NpX + Zyklen.R_EndPos_X(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 4));
                            P5MY = (int)Round(NpY - Zyklen.R_EndPos_Y(XTemp - radius, YTemp - radius, Zyklen.Drehwinkel, 4));
                            X = (int)Round(P5MX - radius);
                            Y = (int)Round(P5MY - radius);
                            startAngle = (int)Round(270d - Zyklen.Drehwinkel);
                            g.DrawLine(weiss, P4ZX, P4ZY, P5SX, P5SY);
                            g.DrawArc(weiss, X, Y, widht, height, startAngle, sweepAngle);
                            g.DrawLine(weiss, P5ZX, P5ZY, P1X, P1Y);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            // alle benötigten Punkte berechnen
                            P1X = NpX;
                            P1Y = NpY;
                            // Ecke links oben
                            P2X = (int)Round(NpX - YTemp);
                            P2Y = NpY;
                            // Ecke links unten
                            P3X = (int)Round(NpX - YTemp);
                            P3Y = NpY + Wks[2];
                            // Ecke rechts unten
                            P4X = (int)Round(NpX + YTemp);
                            P4Y = NpY + Wks[2];
                            // Ecke rechts oben
                            P5X = (int)Round(NpX + YTemp);
                            P5Y = NpY;
                            g.DrawLine(weiss, P1X, P1Y, P2X, P2Y);
                            g.DrawLine(weiss, P2X, P2Y, P3X, P3Y);
                            g.DrawLine(weiss, P3X, P3Y, P4X, P4Y);
                            g.DrawLine(weiss, P4X, P4Y, P5X, P5Y);
                            g.DrawLine(weiss, P5X, P5Y, P1X, P1Y);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            // alle benötigten Punkte berechnen
                            P1X = NpX;
                            P1Y = NpY;
                            // Ecke links oben ohne Radius (Ecke 1)
                            P2X = (int)Round(NpX - XTemp);
                            P2Y = NpY;
                            // Ecke links unten ohne Radius (Ecke 2)
                            P3X = (int)Round(NpX - XTemp);
                            P3Y = NpY + Wks[2];
                            // Ecke rechts unten ohne Radius (Ecke 3)
                            P4X = (int)Round(NpX + XTemp);
                            P4Y = NpY + Wks[2];
                            // Ecke rechts oben ohne Radius (Ecke 4)
                            P5X = (int)Round(NpX + XTemp);
                            P5Y = NpY;
                            g.DrawLine(weiss, P1X, P1Y, P2X, P2Y);
                            g.DrawLine(weiss, P2X, P2Y, P3X, P3Y);
                            g.DrawLine(weiss, P3X, P3Y, P4X, P4Y);
                            g.DrawLine(weiss, P4X, P4Y, P5X, P5Y);
                            g.DrawLine(weiss, P5X, P5Y, P1X, P1Y);
                            break;
                        }
                }
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Gewindefr")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
                // Zapfen zeichnen
                if (X2_3 > 0 & Ansicht == 1)
                {
                    P1X = (int)Round(NpX - X2_3 / 2d);
                    P1Y = (int)Round(NpY - X2_3 / 2d);
                    {
                        locRect = new RectangleF(P1X, P1Y, X2_3, X2_3);
                    }
                    DrawRoundedRect(e.Graphics, gelb, locRect, (float)(X2_3 / 2d));
                }
                else if (X2_3 > 0 & Ansicht == 2)
                {
                    P1X = (int)Round(NpY - X2_3 / 2d);
                    if (P1X > Zeichenfeld.Width / 2d)
                    {
                        P1X = (int)Round(Zeichenfeld.Width - NpY - X2_3 / 2d);
                    }
                    if (P1X < Zeichenfeld.Width / 2d)
                    {
                        P1X = (int)Round(Zeichenfeld.Width - NpY - X2_3 / 2d);
                    }
                    P1Y = (int)Round(NpZ - Y2 / 2d - (double)(1f * Faktor));
                    {
                        locRect = new RectangleF(P1X, P1Y, X2_3, Y2_3);
                    }
                    DrawRoundedRect(e.Graphics, gelb, locRect, (float)radius);
                    Y1 = P1Y;
                }
                else if (X2_3 > 0 & Ansicht == 3)
                {
                    P1X = (int)Round(NpX - X2_3 / 2d);
                    P1Y = (int)Round(NpZ - Y2 / 2d - (double)(1f * Faktor));
                    {
                        locRect = new RectangleF(P1X, P1Y, X2_3, Y2_3);
                    }
                    DrawRoundedRect(e.Graphics, gelb, locRect, (float)radius);
                    Y1 = P1Y;
                }
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Schrift")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            if (Befehl == "Bohrung")
            {
                switch (Ansicht)
                {
                    case 1:          // Ansicht von Oben
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 2:          // Ansicht von Vorn
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                    case 3:          // Ansicht von der Seite
                        {
                            X1 = (int)Round(Zeichenfeld.Width / 2d - X2 / 2d);
                            Y1 = (int)Round(Zeichenfeld.Height / 2d - Y2 / 2d);
                            break;
                        }
                }
                // Fertigmaß als Rechteck
                {
                    locRect = new RectangleF(X1, Y1, X2, Y2);
                }
                DrawRoundedRect(e.Graphics, weiss, locRect, (float)radius);
            }
            // ------------------------------------------------------------------------------------------------------
            myAbsofortZeichnen = false;
            if (X2 > X2_2)
            {
                FMX = (int)Round(X2 / Faktor);
                FMY = (int)Round(Y2 / Faktor);
            }
            else
            {
                FMX = (int)Round(X2_2 / Faktor);
                FMY = (int)Round(Y2_2 / Faktor);
            }
            Infotext = My.MyProject.Forms.Form1.rm.GetString("String389") + Constants.vbCrLf + "X: " + FMX.ToString("##0.00", System.Globalization.CultureInfo.InvariantCulture) + " mm" + Constants.vbCrLf + "Y: " + FMY.ToString("##0.00", System.Globalization.CultureInfo.InvariantCulture) + " mm" + Constants.vbCrLf + Constants.vbCrLf + My.MyProject.Forms.Form1.rm.GetString("String390") + Constants.vbCrLf;
            switch (Ansicht)
            {
                case 1:
                    {
                        Fraesbahnausgabe_Z();
                        break;
                    }
                case 2:
                    {
                        Fraesbahnausgabe_Y();
                        break;
                    }
                case 3:
                    {
                        Fraesbahnausgabe_X();
                        break;
                    }
            }
        }
        // Fräsbahnen grafisch darstellen (Ansicht von Oben)
        private void Fraesbahnausgabe_Z()
        {
            var g = Zeichenfeld.CreateGraphics();
            int N;
            int M;
            string Zeile;
            var Inhalt = new string[6];
            string TempB;
            string TempW;
            bool Flag;
            var NeuX = default(int);
            var NeuY = default(int);
            int AltX;
            int AltY;
            var Nullp_Y = default(int);
            var Nullp_X = default(int);
            var I = default(float);
            var J = default(float);
            int X;
            int Y;
            int widht;
            int height;
            var Zaehler = default(int);
            var RadiusTemp = default(int);
            bool schlichten;
            int F1;
            int F2;
            int F3;
            PointF StartAlt = new PointF(0, 0);
            PointF Startpunkt = new PointF(0, 0);
            PointF Endpunkt = new PointF(0, 0);
            PointF Kreismitte = new PointF(0, 0);
            PointF RecLiO = new PointF(0, 0);
            float Radius;
            float RecWidht;
            float RecHidht;
            float AbstandX;
            float AbstandY;
            float StartW;
            float TempStartW;
            float EndW;
            float TempEndW;
            float OeffnungsW;
            float SweepW;
            float Winkel_P1;
            float Winkel_P2;
            // Stifte
            weiss.Width = 1.0f;
            gelb.Width = 1.0f;
            rot.Width = 1.0f;
            blau.Width = 1.0f;
            gruen.Width = 1.0f;
            if (Befehl == "Planfraesen")
            {
                Nullp_X = X1;
                Nullp_Y = Y1 + Y2;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Abzeilen")
            {
                Nullp_X = X1;
                Nullp_Y = Y1 + Y2;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Rechtecktasche")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Rechteckzapfen")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Kreistasche")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Kreiszapfen")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Nut")
            {
                Nullp_X = NpX;
                Nullp_Y = NpY;
            }
            else if (Befehl == "Ringnut")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Dichtungsnut")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Bohrtabelle")
            {
                Nullp_X = X1;
                Nullp_Y = Y1 + Y2;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Lochkreis")
            {
                Nullp_X = (int)Round(NpX + X2 / 2d);
                Nullp_Y = (int)Round(NpY + Y2 / 2d);
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Bohrmatrix")
            {
                Nullp_X = (int)Round(X1 + -X2_2 + -X2_2 / 2d);
                Nullp_Y = (int)Round(Y1 + Y2 + Y2_2 + Y2_2 / 4.5d);
            }
            else if (Befehl == "runde Nut")
            {
                Nullp_X = NpX;
                Nullp_Y = Y1 + Y2;
            }
            else if (Befehl == "Gewindefr")
            {
                Nullp_X = X1;
                Nullp_Y = Y1 + Y2;
            }
            else if (Befehl == "Schrift")
            {
                Nullp_X = NpX;
                Nullp_Y = Y1 + (Y2 - Wks[4]);
            }
            else if (Befehl == "Bohrung")
            {
                Nullp_X = X1;
                Nullp_Y = Y1 + Y2;
            }
            else if (Befehl == "DxfData")
            {
                Nullp_X = X1;
                Nullp_Y = Zeichenfeld.Height - Y1;
            }
            Flag = false;
            schlichten = My.MyProject.Forms.Form1.Schlicht_2.Checked;
            AltX = Nullp_X;
            AltY = Nullp_Y;
            F1 = 0;
            F2 = 0;
            F3 = 0;
            // Fräsbahnen aus Programm auslesen und anzeigen
            for (N = 0; N <= Ausgabe.Lines.Length - 1; N++)
            {
                Zeile = Ausgabe.Lines[N];
                Inhalt = Strings.Split(Zeile, " ");
                // Bewegungen im Eilgang
                if (Inhalt[0] == "G0")
                {
                    F1++;
                    for (M = 0; M <= Inhalt.Length - 1; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Startpunkt.X = StartAlt.X;
                                Endpunkt.X = (float)Round(Convert.ToDouble(TempW), 3);
                                Flag = true;
                            }
                            else if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Startpunkt.Y = StartAlt.Y;
                                Endpunkt.Y = (float)Round(Convert.ToDouble(TempW), 3);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        NeuX = (int)Round((Faktor * Endpunkt.X) + Nullp_X);
                        NeuY = (int)Round((Faktor * -Endpunkt.Y) + Nullp_Y);
                        g.DrawLine(rot, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 2)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        StartAlt.X = Endpunkt.X;
                        StartAlt.Y = Endpunkt.Y;
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
                // Bewegungen im Arbeitsgang
                if (Inhalt[0] == "G1")
                {
                    F2++;
                    for (M = 0; M <= Inhalt.Length - 1; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Startpunkt.X = StartAlt.X;
                                Endpunkt.X = (float)Round(Convert.ToDouble(TempW), 3);
                                Flag = true;
                            }
                            else if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Startpunkt.Y = StartAlt.Y;
                                Endpunkt.Y = (float)Round(Convert.ToDouble(TempW), 3);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        NeuX = (int)Round((Faktor * Endpunkt.X) + Nullp_X);
                        NeuY = (int)Round((Faktor * -Endpunkt.Y) + Nullp_Y);
                        g.DrawLine(blau, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        StartAlt.X = Endpunkt.X;
                        StartAlt.Y = Endpunkt.Y;
                        Flag = false;
                    }
                }
                // Kreisbewegung im Uhrzeigersinn
                if (Inhalt[0] == "G2")
                {
                    F3++;
                    Startpunkt.X = StartAlt.X;
                    Startpunkt.Y = StartAlt.Y;
                    // Zeile aufsplitten und auslesen
                    for (M = 0; M <= Inhalt.Length - 1; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Endpunkt.X = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                            else if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Endpunkt.Y = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                            else if (TempB == "I")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                I = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                            else if (TempB == "J")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                J = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                        }
                    }
                    // Auswertung
                    if (Zyklen.Drehwinkel < 0d)
                        Zyklen.Drehwinkel = -Zyklen.Drehwinkel;
                    // Kreismittelpunkt
                    Kreismitte.X = (float)Round(Startpunkt.X + Convert.ToDouble(I), 3);
                    Kreismitte.Y = (float)Round(Startpunkt.Y + Convert.ToDouble(J), 3);
                    // Radius
                    Radius = (float)Round(Sqrt(Pow((double)I, 2d) + Pow((double)J, 2d)), 1);
                    // Abstand Startpunkt <-> Kreismitte
                    AbstandX = Startpunkt.X - Kreismitte.X;
                    AbstandY = Kreismitte.Y - Startpunkt.Y;
                    // Startwinkel
                    Winkel_P1 = (float)Round(Atan2((double)AbstandY, (double)AbstandX) * (180d / PI));
                    StartW = Winkel_P1;
                    TempStartW = Winkel_P1;
                    if (Winkel_P1 < 0)
                    {
                        StartW = 360 + Winkel_P1;
                        TempStartW = -Winkel_P1;
                    }
                    // Abstand Endpunkt <-> Kreismitte
                    AbstandX = Endpunkt.X - Kreismitte.X;
                    AbstandY = Kreismitte.Y - Endpunkt.Y;
                    // Endwinkel
                    Winkel_P2 = (float)Round(Atan2((double)AbstandY, (double)AbstandX) * (180d / PI));
                    EndW = Winkel_P2;
                    TempEndW = Winkel_P2;
                    if (Winkel_P2 < 0)
                    {
                        TempEndW = -Winkel_P2;
                        EndW = 360 + Winkel_P2;
                    }
                    // Öffnungswinkel
                    OeffnungsW = 0;
                    if (TempStartW == TempEndW)
                    {
                        OeffnungsW = TempStartW + TempEndW;
                    }
                    else if (TempStartW < TempEndW)
                    {
                        OeffnungsW = TempEndW - TempStartW;
                    }
                    else if (TempStartW > TempEndW)
                    {
                        OeffnungsW = TempStartW - TempEndW;
                    }
                    if (StartW < EndW)
                    {
                        SweepW = EndW - StartW;
                    }
                    else
                    {
                        SweepW = TempStartW + EndW;
                    }
                    // Zeichenrechteck Ecke links oben
                    RecLiO.X = Kreismitte.X - Radius;
                    RecLiO.Y = Kreismitte.Y + Radius;
                    // Zeichenrechteck Breite und Höhe
                    RecWidht = 2 * Radius;
                    RecHidht = 2 * Radius;
                    //Rechteck
                    X = Nullp_X + (int)Round((double)RecLiO.X * Faktor, 3);
                    Y = Nullp_Y - (int)Round((double)RecLiO.Y * Faktor, 3);
                    widht = (int)Round((double)RecWidht * Faktor, 3);
                    height = (int)Round((double)RecHidht * Faktor, 3);
                    // zeichnen
                    g.DrawArc(hgruen, X, Y, widht, height, StartW, SweepW);
                    NeuX = (int)Round((Faktor * Endpunkt.X) + Nullp_X);
                    NeuY = (int)Round((Faktor * -Endpunkt.Y) + Nullp_Y);
                    AltX = NeuX;
                    AltY = NeuY;
                    StartAlt.X = Endpunkt.X;
                    StartAlt.Y = Endpunkt.Y;
                }
                // Kreisbewegung gegen den Uhrzeigersinn
                if (Inhalt[0] == "G3")
                {
                    F3++;
                    Startpunkt.X = StartAlt.X;
                    Startpunkt.Y = StartAlt.Y;
                    // Zeile aufsplitten und auslesen
                    for (M = 0; M <= Inhalt.Length - 1; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Endpunkt.X = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                            else if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                Endpunkt.Y = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                            else if (TempB == "I")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                I = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                            else if (TempB == "J")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                TempW = TempW.Replace(".", ",");
                                J = (float)Round(Convert.ToDouble(TempW), 3);
                            }
                        }
                    }
                    //Auswertung
                    var TempVollW = 360 + (float)Zyklen.Drehwinkel;
                    // Kreismittelpunkt
                    Kreismitte.X = (float)Round(Startpunkt.X + Convert.ToDouble(I), 3);
                    Kreismitte.Y = (float)Round(Startpunkt.Y + Convert.ToDouble(J), 3);
                    // Radius
                    Radius = (float)Round(Sqrt(Pow((double)I, 2d) + Pow((double)J, 2d)), 1);
                    // Abstand Startpunkt <-> Kreismitte
                    AbstandX = Startpunkt.X - Kreismitte.X;
                    AbstandY = Kreismitte.Y - Startpunkt.Y;
                    // Startwinkel
                    Winkel_P1 = (float)Round(Atan2((double)AbstandY, (double)AbstandX) * (180d / PI));
                    StartW = Winkel_P1;
                    TempStartW = Winkel_P1;
                    if (Winkel_P1 < 0)
                    {
                        StartW = 360 + Winkel_P1;
                        TempStartW = -Winkel_P1;
                    }
                    // Abstand Endpunkt <-> Kreismitte
                    AbstandX = Endpunkt.X - Kreismitte.X;
                    AbstandY = Kreismitte.Y - Endpunkt.Y;
                    // Endwinkel
                    Winkel_P2 = (float)Round(Atan2((double)AbstandY, (double)AbstandX) * (180d / PI));
                    EndW = Winkel_P2;
                    TempEndW = Winkel_P2;
                    if (Winkel_P2 < 0)
                    {
                        TempEndW = -Winkel_P2;
                        EndW = 360 + Winkel_P2;
                    }
                    // Öffnungswinkel
                    OeffnungsW = (360 - EndW) - (360 - StartW);
                    if (StartW < EndW)
                    {
                        OeffnungsW = (360 - EndW) + StartW;
                    }
                    if (StartW == 0)
                    {
                        OeffnungsW = 360 - EndW;
                    }
                    if (OeffnungsW < 0)
                        OeffnungsW = -OeffnungsW;
                    // Zeichenrechteck Ecke links oben
                    RecLiO.X = Kreismitte.X - Radius;
                    RecLiO.Y = Kreismitte.Y + Radius;
                    // Zeichenrechteck Breite und Höhe
                    RecWidht = 2 * Radius;
                    RecHidht = 2 * Radius;
                    //Rechteck
                    X = Nullp_X + (int)Round((double)RecLiO.X * Faktor, 3);
                    Y = Nullp_Y - (int)Round((double)RecLiO.Y * Faktor, 3);
                    widht = (int)Round((double)RecWidht * Faktor, 3);
                    height = (int)Round((double)RecHidht * Faktor, 3);
                    // zeichnen
                    g.DrawArc(gruen, X, Y, widht, height, EndW, OeffnungsW);
                    NeuX = (int)Round((Faktor * Endpunkt.X) + Nullp_X);
                    NeuY = (int)Round((Faktor * -Endpunkt.Y) + Nullp_Y);
                    AltX = NeuX;
                    AltY = NeuY;
                    StartAlt.X = Endpunkt.X;
                    StartAlt.Y = Endpunkt.Y;
                }
            }
                Infotext = Infotext + F1.ToString("###0", System.Globalization.CultureInfo.InvariantCulture) + My.MyProject.Forms.Form1.rm.GetString("String391") + Constants.vbCrLf + F2.ToString("###0", System.Globalization.CultureInfo.InvariantCulture) + My.MyProject.Forms.Form1.rm.GetString("String392") + Constants.vbCrLf + F3.ToString("###0", System.Globalization.CultureInfo.InvariantCulture) + My.MyProject.Forms.Form1.rm.GetString("String393") + Constants.vbCrLf;
            CNC_Info.Text = Infotext;
        }
        // Fräsbahnen grafisch darstellen (Ansicht von Vorn)
        private void Fraesbahnausgabe_Y()
        {
            var g = Zeichenfeld.CreateGraphics();
            int N;
            int M;
            string Zeile;
            var Inhalt = new string[6];
            string TempB;
            string TempW;
            bool Flag;
            var NeuX = default(int);
            var NeuY = default(int);
            int AltX;
            int AltY;
            var Nullp_Y = default(int);
            var Nullp_X = default(int);
            var Zaehler = default(int);
            var RadiusTemp = default(int);
            bool Flag_G0;
            // Stifte
            weiss.Width = 1.0f;
            gelb.Width = 1.0f;
            rot.Width = 1.0f;
            blau.Width = 1.0f;
            gruen.Width = 1.0f;
            if (Befehl == "Planfraesen")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Abzeilen")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Rechtecktasche")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Rechteckzapfen")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Kreistasche")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Kreiszapfen")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Nut")
            {
                Nullp_X = NpX;
                Nullp_Y = NpY;
            }
            else if (Befehl == "Ringnut")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Dichtungsnut")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Bohrtabelle")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Lochkreis")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Bohrmatrix")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "runde Nut")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "Gewindefr")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "Schrift")
            {
                Nullp_X = X1 + Wks[4];
                Nullp_Y = Y1;
            }
            else if (Befehl == "Bohrung")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "DxfData")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            Flag = false;
            Flag_G0 = false;
            AltX = Nullp_X;
            AltY = Nullp_Y;
            // Fräsbahnen aus Programm auslesen und anzeigen
            var loopTo = Ausgabe.Lines.Length - 1;
            for (N = 0; N <= loopTo; N++)
            {
                Zeile = Ausgabe.Lines[N];
                Inhalt = Strings.Split(Zeile, " ");
                if (Inhalt[0] == "G0")
                {
                    var loopTo1 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo1; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        if (Flag_G0 == true)
                        {
                            g.DrawLine(rot, AltX, AltY, NeuX, NeuY);
                        }
                        if (Zaehler == 2)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                        Flag_G0 = true;
                    }
                }
                else if (Inhalt[0] == "G1")
                {
                    var loopTo2 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo2; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        g.DrawLine(blau, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
                else if (Inhalt[0] == "G2")
                {
                    var loopTo3 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo3; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        g.DrawLine(hgruen, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
                else if (Inhalt[0] == "G3")
                {
                    var loopTo4 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo4; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "Y")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        g.DrawLine(gruen, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
            }
        }
        // Fräsbahnen grafisch darstellen (Ansicht von der Seite)
        private void Fraesbahnausgabe_X()
        {
            var g = Zeichenfeld.CreateGraphics();
            int N;
            int M;
            string Zeile;
            var Inhalt = new string[6];
            string TempB;
            string TempW;
            bool Flag;
            var NeuX = default(int);
            var NeuY = default(int);
            int AltX;
            int AltY;
            var Nullp_Y = default(int);
            var Nullp_X = default(int);
            var Zaehler = default(int);
            var RadiusTemp = default(int);
            bool Flag_G0;
            // Stifte
            weiss.Width = 1.0f;
            gelb.Width = 1.0f;
            rot.Width = 1.0f;
            blau.Width = 1.0f;
            gruen.Width = 1.0f;
            if (Befehl == "Planfraesen")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Abzeilen")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Rechtecktasche")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Rechteckzapfen")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Kreistasche")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Kreiszapfen")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Nut")
            {
                Nullp_X = NpX;
                Nullp_Y = NpY;
            }
            else if (Befehl == "Ringnut")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Dichtungsnut")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Bohrtabelle")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Lochkreis")
            {
                Nullp_X = (int)Round(X1 + X2 / 2d);
                Nullp_Y = Y1;
                Zyklen.Drehwinkel = 0d;
            }
            else if (Befehl == "Bohrmatrix")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "runde Nut")
            {
                Nullp_X = NpX;
                Nullp_Y = Y1;
            }
            else if (Befehl == "Gewindefr")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "Schrift")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "Bohrung")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            else if (Befehl == "DxfData")
            {
                Nullp_X = X1;
                Nullp_Y = Y1;
            }
            Flag = false;
            Flag_G0 = false;
            AltX = Nullp_X;
            AltY = Nullp_Y;
            // Fräsbahnen aus Programm auslesen und anzeigen
            var loopTo = Ausgabe.Lines.Length - 1;
            for (N = 0; N <= loopTo; N++)
            {
                Zeile = Ausgabe.Lines[N];
                Inhalt = Strings.Split(Zeile, " ");
                if (Inhalt[0] == "G0")
                {
                    var loopTo1 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo1; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        if (Flag_G0 == true)
                        {
                            g.DrawLine(rot, AltX, AltY, NeuX, NeuY);
                        }
                        if (Zaehler == 2)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                        Flag_G0 = true;
                    }
                }
                else if (Inhalt[0] == "G1")
                {
                    var loopTo2 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo2; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        g.DrawLine(blau, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
                else if (Inhalt[0] == "G2")
                {
                    var loopTo3 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo3; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        g.DrawLine(hgruen, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
                else if (Inhalt[0] == "G3")
                {
                    var loopTo4 = Inhalt.Length - 1;
                    for (M = 0; M <= loopTo4; M++)
                    {
                        if (!string.IsNullOrEmpty(Inhalt[M]))
                        {
                            TempB = Strings.Mid(Strings.Trim(Inhalt[M]), 1, 1);
                            if (TempB == "X")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuX = (int)Round(Faktor * Conversion.Val(TempW) + Nullp_X);
                                Flag = true;
                            }
                            else if (TempB == "Z")
                            {
                                TempW = Strings.Trim(Inhalt[M]);
                                TempW = Strings.Mid(TempW, 2);
                                NeuY = (int)Round(Faktor * -Conversion.Val(TempW) + Nullp_Y);
                                Flag = true;
                            }
                        }
                    }
                    if (Flag == true)
                    {
                        g.DrawLine(gruen, AltX, AltY, NeuX, NeuY);
                        if (Zaehler == 1)
                        {
                            AltX -= RadiusTemp;
                            Zaehler = 0;
                        }
                        AltX = NeuX;
                        AltY = NeuY;
                        Flag = false;
                    }
                }
            }
        }
        // Rechteck mit abgerundeten Ecken
        private void DrawRoundedRect(Graphics g, Pen pen, RectangleF Rect, float radius)
        {
            var locGrafikPfad = new GraphicsPath();
            float locDurchmesser = radius * 2f;
            var locEckengröße = new SizeF(locDurchmesser, locDurchmesser);
            var locEckbogen = new RectangleF(Rect.Location, locEckengröße);
            // Oben links
            locGrafikPfad.AddArc(locEckbogen, 180f, 90f);
            // Oben rechts
            locEckbogen.X = Rect.Right - locDurchmesser - pen.Width;
            locGrafikPfad.AddArc(locEckbogen, 270f, 90f);
            // Unten rechts
            locEckbogen.Y = Rect.Bottom - locDurchmesser - pen.Width;
            locGrafikPfad.AddArc(locEckbogen, 0f, 90f);
            // Unten links
            locEckbogen.X = Rect.Left;
            locGrafikPfad.AddArc(locEckbogen, 90f, 90f);
            locGrafikPfad.CloseFigure();
            g.DrawPath(pen, locGrafikPfad);
        }
        // CNC-Programm - Druckvorschau
        private void Vorschau_Click(object sender, EventArgs e)
        {
            try
            {
                DruckZeichenfolge = Ausgabe.Text;
                PrintPreviewDialog1.Document = PrintDocument1;
                PrintPreviewDialog1.Width = 1280;
                PrintPreviewDialog1.Height = 960;
                var Ergebnis = PrintPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // CNC-Programm drucken
        private void Drucken_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument1.DefaultPageSettings = EinstellungDruckseite;
                DruckZeichenfolge = Ausgabe.Text;
                PrintDialog1.Document = PrintDocument1;
                PrintPreviewDialog1.Document = PrintDocument1;
                var Ergebnis = PrintDialog1.ShowDialog();
                if (Ergebnis == DialogResult.OK)
                {
                    PrintDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Druck vorbereiten
        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            string StringSeite;
            var strFormat = new StringFormat();
            var Rechteck = new RectangleF(e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Width, e.MarginBounds.Height);
            var GroessenMass = new SizeF(e.MarginBounds.Width, e.MarginBounds.Height - DruckFont.GetHeight(e.Graphics));
            strFormat.Trimming = StringTrimming.Word;
            e.Graphics.MeasureString(DruckZeichenfolge, DruckFont, GroessenMass, strFormat, out int AnzahlZeichen, out int AnzahlZeilen);
            StringSeite = DruckZeichenfolge.Substring(0, AnzahlZeichen);
            e.Graphics.DrawString(StringSeite, DruckFont, Brushes.Black, Rechteck, strFormat);
            if (AnzahlZeichen < DruckZeichenfolge.Length)
            {
                DruckZeichenfolge = DruckZeichenfolge.Substring(AnzahlZeichen);
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                DruckZeichenfolge = Ausgabe.Text;
            }
        }
        // Tooltips
        private void RectangleShape2_MouseHover(object sender, EventArgs e)
        {
            ToolTip1.Show(My.MyProject.Forms.Form1.rm.GetString("String394"), this, 550, 570, 2000);
        }
        private void RectangleShape3_MouseHover(object sender, EventArgs e)
        {
            ToolTip1.Show(My.MyProject.Forms.Form1.rm.GetString("String395"), this, 550, 570, 2000);
        }
        private void RectangleShape4_MouseHover(object sender, EventArgs e)
        {
            ToolTip1.Show(My.MyProject.Forms.Form1.rm.GetString("String396"), this, 550, 570, 2000);
        }
        // Reaktion beim verändern des Textes
        private void _Editor_TextChanged(object sender, EventArgs e)
        {
            Invalidate();
            if (textupdate)
            {
                int start = 0, end = 0;
                // Berechne die Startposition der aktuellen Zeile
                for (start = _Editor.SelectionStart - 1; start > 0; start--)
                {
                    if (_Editor.Text[start] == '\n') { start++; break; }
                }
                // Berechne die Endposition der aktuellen Zeile
                for (end = _Editor.SelectionStart; end < _Editor.Text.Length; end++)
                {
                    if (_Editor.Text[end] == '\n') break;
                }
                // Extrahiere die aktuelle Zeile, die bearbeitet wird
                String line = _Editor.Text.Substring(start, end - start);
                // Den aktuellen Auswahlpunkt des Benutzers sichern
                int selectionStart = _Editor.SelectionStart;
                int selectionLength = _Editor.SelectionLength;
                // Die Zeile in Token aufteilen
                Regex r = new Regex("([ \\t{}();])");
                string[] tokens = r.Split(line);
                int index = start;
                foreach (string token in tokens)
                {
                    _Editor.SelectionStart = index;
                    _Editor.SelectionLength = token.Length;
                    _Editor.SelectionColor = farbeText;
                    // Prüfen, ob das Token ein Schlüsselwort ist.      // Check for a comment.
                    if (token == "(" || token.StartsWith("("))
                    {
                        // Find the start of the comment and then extract the whole comment.
                        int index1 = line.IndexOf("(");
                        string comment = line.Substring(index1, line.Length - index);
                        _Editor.SelectionColor = farbeKoment;
                        _Editor.SelectedText = comment;
                        break;
                    }
                    string[] keywords = { "G" };
                    for (int i = 0; i < keywords.Length; i++)
                    {
                        if (token.Contains(keywords[i]) == true)
                        {
                            _Editor.SelectionColor = farbeGx;
                        }
                    }
                    string[] keywords1 = { "G0" };
                    for (int i = 0; i < keywords1.Length; i++)
                    {
                        if (keywords1[i] == token)
                        {
                            _Editor.SelectionColor = farbeG0;
                        }
                    }
                    string[] keywords2 = { "G1" };
                    for (int i = 0; i < keywords2.Length; i++)
                    {
                        if (keywords2[i] == token)
                        {
                            _Editor.SelectionColor = farbeG1;
                        }
                    }
                    string[] keywords3 = { "G2" };
                    for (int i = 0; i < keywords3.Length; i++)
                    {
                        if (keywords3[i] == token)
                        {
                            _Editor.SelectionColor = farbeG2;
                        }
                    }
                    string[] keywords4 = { "G3" };
                    for (int i = 0; i < keywords4.Length; i++)
                    {
                        if (keywords4[i] == token)
                        {
                            _Editor.SelectionColor = farbeG3;
                        }
                    }
                    string[] keywords5 = { "M" };
                    for (int i = 0; i < keywords5.Length; i++)
                    {
                        if (token.Contains(keywords5[i]) == true)
                        {
                            _Editor.SelectionColor = farbeM;
                        }
                    }
                    string[] keywords6 = { "S" };
                    for (int i = 0; i < keywords6.Length; i++)
                    {
                        if (token.Contains(keywords6[i]) == true)
                        {
                            _Editor.SelectionColor = farbeS;
                        }
                    }
                    string[] keywords7 = { "T" };
                    for (int i = 0; i < keywords7.Length; i++)
                    {
                        if (token.Contains(keywords7[i]) == true)
                        {
                            _Editor.SelectionColor = farbeT;
                        }
                    }
                    index += token.Length;
                }
                _Editor.SelectionStart = selectionStart;
                _Editor.SelectionLength = selectionLength;
                ZN_aktualisieren();
            }
        }
        // Reaktion beim anklicken der Programmliste
        private void Ausgabe_MouseClick(object sender, MouseEventArgs e)
        {

        }
        // Reaktion beim durchblättern der Programmliste
        private void Ausgabe_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        // Syntax hervorheben
        private void ParseLine(string line)
        {
            if (line != "")
            {
                Regex r = new Regex("([ \\t{}():;])");
                string[] tokens = r.Split(line);
                foreach (String token in tokens)
                {
                    // Legen Sie die Standardfarbe und -schriftart des Tokens fest.  
                    _Editor.SelectionColor = farbeText;
                    // Prüfen, ob das Token ein Schlüsselwort ist.      // Check for a comment.
                    if (token == "(" || token.StartsWith("("))
                    {
                        // Find the start of the comment and then extract the whole comment.
                        int index = line.IndexOf("(");
                        string comment = line.Substring(index, line.Length - index);
                        _Editor.SelectionColor = farbeKoment;
                        _Editor.SelectedText = comment;
                        break;
                    }
                    string[] keywords = { "G" };
                    for (int i = 0; i < keywords.Length; i++)
                    {
                        if (token.Contains(keywords[i]) == true)
                        {
                            _Editor.SelectionColor = farbeGx;
                        }
                    }
                    string[] keywords1 = { "G0" };
                    for (int i = 0; i < keywords1.Length; i++)
                    {
                        if (keywords1[i] == token)
                        {
                            _Editor.SelectionColor = farbeG0;
                        }
                    }
                    string[] keywords2 = { "G1" };
                    for (int i = 0; i < keywords2.Length; i++)
                    {
                        if (keywords2[i] == token)
                        {
                            _Editor.SelectionColor = farbeG1;
                        }
                    }
                    string[] keywords3 = { "G2" };
                    for (int i = 0; i < keywords3.Length; i++)
                    {
                        if (keywords3[i] == token)
                        {
                            _Editor.SelectionColor = farbeG2;
                        }
                    }
                    string[] keywords4 = { "G3" };
                    for (int i = 0; i < keywords4.Length; i++)
                    {
                        if (keywords4[i] == token)
                        {
                            _Editor.SelectionColor = farbeG3;
                        }
                    }
                    string[] keywords5 = { "M" };
                    for (int i = 0; i < keywords5.Length; i++)
                    {
                        if (token.Contains(keywords5[i]) == true)
                        {
                            _Editor.SelectionColor = farbeM;
                        }
                    }
                    string[] keywords6 = { "S" };
                    for (int i = 0; i < keywords6.Length; i++)
                    {
                        if (token.Contains(keywords6[i]) == true)
                        {
                            _Editor.SelectionColor = farbeS;
                        }
                    }
                    string[] keywords7 = { "T" };
                    for (int i = 0; i < keywords7.Length; i++)
                    {
                        if (token.Contains(keywords7[i]) == true)
                        {
                            _Editor.SelectionColor = farbeT;
                        }
                    }
                    _Editor.SelectedText = token;
                }
                _Editor.SelectedText = "\n";
            }
        }
        // Scroll-Syncronisation mit Zeilennummern
        private void _Editor_VScroll(object sender, EventArgs e)
        {
            Int32 nPos = Form2.GetScrollPos(_Editor.Handle, 1);
            nPos <<= 16;
            uint wParam = 4 | (uint)nPos;
            Form2.SendMessage(Editor_ZN.Handle, 0x0115, new IntPtr(wParam), new IntPtr(0));
        }
        // Zeilennummern aktualisieren
        private void ZN_aktualisieren()
        {
            Editor_ZN.Clear();
            Editor_ZN.Font = _Editor.Font;
            for (int i = 1; i < _Editor.Lines.Length; i++)
            {
                Editor_ZN.AppendText(i.ToString() + "." + Constants.vbCrLf);
            }
        }
        // Zeilennummern an / aus
        private void ZN_CheckedChanged(object sender, EventArgs e)
        {
            if (ZN.Checked == true)
            {
                Editor_ZN.Visible = true;
                _Editor.Location = new Point(41, 54);
                _Editor.Size = new Size(433, 610);
            }
            else
            {
                Editor_ZN.Visible = false;
                _Editor.Location = new Point(5, 54);
                _Editor.Size = new Size(469, 610);
            }
        }
        // Sprache umstellen
        private void Translate()
        {
            this.Text = My.MyProject.Forms.Form1.rm.GetString("String377");
            Label1.Text = My.MyProject.Forms.Form1.rm.GetString("String380");
            Label2.Text = My.MyProject.Forms.Form1.rm.GetString("String378");
            Label3.Text = My.MyProject.Forms.Form1.rm.GetString("String381");
            ZN.Text = My.MyProject.Forms.Form1.rm.GetString("String379");
            speichern.Text = My.MyProject.Forms.Form1.rm.GetString("String382");
            Vorschau.Text = My.MyProject.Forms.Form1.rm.GetString("String383");
            drucken.Text = My.MyProject.Forms.Form1.rm.GetString("String384");
            schließen.Text = My.MyProject.Forms.Form1.rm.GetString("String385");
        }
        #endregion
    }
}