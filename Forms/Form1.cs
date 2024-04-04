using System;
using System.Drawing;
using static System.Math;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Resources;

namespace NC_Tool
{

    public partial class Form1
    {
        #region Variablen
        // Globale Einstellungen und Variablen
        public string Pfad;
        public int Sprache;
        public string Infostring;
        public bool Fehler;
        public int Rand;
        public Image PF_Picture;
        public List<List<string>> gList = [];
        public float GX = 0;
        public float GY = 0;
        public int pp = 0;
        public ArrayList listMasterArray = [];
        public string inputFileTxt;
        public double XMax, XMin;
        public double YMax, YMin;
        public double ZMax, ZMin;
        public double DxfXMax, DxfXMin;
        public double DxfYMax, DxfYMin;
        public double scaleX = 1;
        public double scaleY = 1;
        public double mainScale;
        public Point aPoint;
        public bool sizeChanged = false;
        public Point startPoint;
        public Point endPoint;
        public static Point exPoint;
        public ArrayList drawingList;
        public ArrayList gcodeList;
        public ArrayList objectIdentifier;
        public bool onCanvas = false;
        public Polyline thePolyLine = null;
        public Polyline thePolyLine1 = null;
        public Rectangl theRectangle = null;
        public Rectangl theRectangle1 = null;
        public bool polyLineStarting = true;
        public bool CanIDraw = false;
        public FileInfo theSourceFile;
        public string DxfDatei;
        public Rectangle highlightedRegion = new(0, 0, 0, 0);
        public int objektindex;
        public int objekttype;
        public string typeName;
        public ResourceManager rm;
        public string StatusText;
        public int Fontweidht;
        #endregion
        public Form1()
        {
            InitializeComponent();
        }
// ------------------------------------------------------------------------------------------------
        #region Menüaktionen
        // erster Programmstart
        private void Form1_Load(object sender, EventArgs e)
        {
            startPoint = new Point(0, 0);
            endPoint = new Point(0, 0);
            exPoint = new Point(0, 0);
            XMax = this.PictureBox17.Size.Width;
            YMax = this.PictureBox17.Size.Height / 2;
            SelectNextControl(Abbrechen_1, true, true, true, true);
            TB_1.SelectAll();
            PF_Picture = My.Resources.Resources.Bild01_01;
            //Infostring = "Planfräsen einer Oberfläche mit programmiertem Vorschub Zeilenförmig über das Werkstück." + Constants.vbCrLf + "1. Die Maschine fährt das Werkzeug zum Startpunkt." + Constants.vbCrLf + "2. Das Werkzeug wird dann in Z mit programmierter Zustellung verfahren." + Constants.vbCrLf + "3. Die Außenkanten werden bearbeitet." + Constants.vbCrLf + "4. Die Oberfläche wird abgezeilt." + Constants.vbCrLf + "5. Danach wird das Werkzeug zum Startpunkt zurück geführt." + Constants.vbCrLf + "Dieser Vorgang wiederholt sich, bis die gewünschte Tiefe erreicht ist.";
            Pfad = Application.StartupPath;
            Speicherpfad.Text = Pfad;
            Load_Config();
            Translate();
            Schriftarten.Schriftsatz = 1;
            Schriftarten.Font_lesen();
            Infostring = rm.GetString("String123");
            Info_01.Text = Infostring;
            timer1.Start();
        }
        // Wechsel der Registerkarten
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatusText = "";
            // Planfräsen
            if (TabControl1.SelectedTab.Name == "TabPage1")
            {
                SelectNextControl(Abbrechen_1, true, true, true, true);
                TB_1.SelectAll();
                Infostring = rm.GetString("String123");
                Info_01.Text = Infostring;
            }
            // Rechtecktasche
            if (TabControl1.SelectedTab.Name == "TabPage2")
            {
                SelectNextControl(Abbrechen_2, true, true, true, true);
                TB_20.SelectAll();
                Infostring = rm.GetString("String155");
                Info_02.Text = Infostring;
            }
            // Rechteckzapfen
            if (TabControl1.SelectedTab.Name == "TabPage3")
            {
                SelectNextControl(Abbrechen_3, true, true, true, true);
                TB_40.SelectAll();
                Infostring = rm.GetString("String162");
                Info_03.Text = Infostring;
            }
            // Kreistasche
            if (TabControl1.SelectedTab.Name == "TabPage4")
            {
                SelectNextControl(Abbrechen_4, true, true, true, true);
                TB_60.SelectAll();
                Infostring = rm.GetString("String187");
                Info_04.Text = Infostring;
            }
            // Kreiszapfen
            if (TabControl1.SelectedTab.Name == "TabPage5")
            {
                SelectNextControl(Abbrechen_5, true, true, true, true);
                TB_70.SelectAll();
                Infostring = rm.GetString("String188");
                Info_05.Text = Infostring;
            }
            // Ringnut
            if (TabControl1.SelectedTab.Name == "TabPage6")
            {
                SelectNextControl(Abbrechen_6, true, true, true, true);
                TB_81.SelectAll();
                Infostring = rm.GetString("String189");
                Info_06.Text = Infostring;
            }
            // Bohrtabelle
            if (TabControl1.SelectedTab.Name == "TabPage7")
            {
                SelectNextControl(Abbrechen_7, true, true, true, true);
                TB_90.SelectAll();
                Infostring = rm.GetString("String190");
                Info_07.Text = Infostring;
            }
            // BB Lochkreis
            if (TabControl1.SelectedTab.Name == "TabPage8")
            {
                SelectNextControl(Abbrechen_8, true, true, true, true);
                TB_100.SelectAll();
                Infostring = rm.GetString("String191");
                Info_08.Text = Infostring;
            }
            // Dichtungsnut
            if (TabControl1.SelectedTab.Name == "TabPage9")
            {
                SelectNextControl(Abbrechen_9, true, true, true, true);
                TB_120.SelectAll();
                Infostring = rm.GetString("String192");
                Info_09.Text = Infostring;
            }
            // schräge Flächen
            if (TabControl1.SelectedTab.Name == "TabPage10")
            {
                SelectNextControl(Abbrechen_10, true, true, true, true);
                TB_140.SelectAll();
                Infostring = rm.GetString("String131");
                Info_10.Text = Infostring;
            }
            // Nut
            if (TabControl1.SelectedTab.Name == "TabPage11")
            {
                SelectNextControl(Abbrechen_11, true, true, true, true);
                TB_200.SelectAll();
                Infostring = rm.GetString("String181");
                Info_11.Text = Infostring;
            }
            // BB Linien
            if (TabControl1.SelectedTab.Name == "TabPage12")
            {
                SelectNextControl(Abbrechen_12, true, true, true, true);
                TB_150.SelectAll();
                Infostring = rm.GetString("String193");
                Info_12.Text = Infostring;
            }
            // runde Nut
            if (TabControl1.SelectedTab.Name == "TabPage13")
            {
                SelectNextControl(Abbrechen_13, true, true, true, true);
                TB_170.SelectAll();
                Infostring = rm.GetString("String196");
                Info_13.Text = Infostring;
            }
            // Schrift
            if (TabControl1.SelectedTab.Name == "TabPage14")
            {
                SelectNextControl(Abbrechen_14, true, true, true, true);
                TB_220.SelectAll();
                Infostring = rm.GetString("String194");
                Info_14.Text = Infostring;
            }
            // Gewinde
            if (TabControl1.SelectedTab.Name == "TabPage15")
            {
                TB_251.SelectedIndex = 0;
                Infostring = rm.GetString("String147");
                Info_15.Text = Infostring;
                SelectNextControl(Abbrechen_15, true, true, true, true);
            }
            // Tiefloch
            if (TabControl1.SelectedTab.Name == "TabPage16")
            {
                SelectNextControl(Abbrechen_16, true, true, true, true);
                TB_260.SelectAll();
                Infostring = rm.GetString("String137");
                Info_16.Text = Infostring;
            }
            // DXF
            if (TabControl1.SelectedTab.Name == "TabPage17")
            {
                SelectNextControl(Abbrechen_17, true, true, true, true);
                TB_280.SelectAll();
                Infostring = rm.GetString("String195");
                Info_17.Text = Infostring;
            }
        }
        // Menüpunkt Einstellungen gewählt
        private void EinstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabControl1.Visible = false;
        }
        // Sprache deutsch gewählt
        private void DeutschToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sprache = 1;
            Save_Config();
            Translate();
            int tab = TabControl1.SelectedIndex;
            InfoText(tab);
        }
        // Sprache englisch gewählt
        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sprache = 2;
            Save_Config();
            Translate();
            int tab = TabControl1.SelectedIndex;
            InfoText(tab);
        }
        // Programm beenden
        private void BeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Zyklen
        // Planfräsen
        private void PlanfräsenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_1, true, true, true, true);
            TB_1.SelectAll();
            Infostring = rm.GetString("String123");
            Info_01.Text = Infostring;
            TabControl1.SelectTab(0);
            TabControl1.Visible = true;
        }
        // Schräge
        private void SchrägeFaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_10, true, true, true, true);
            TB_140.SelectAll();
            Infostring = rm.GetString("String131");
            Info_10.Text = Infostring;
            TabControl1.SelectTab(1);
            TabControl1.Visible = true;
        }
        // Rechtecktasche
        private void RechtecktascheToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_2, true, true, true, true);
            TB_20.SelectAll();
            Infostring = rm.GetString("String155");
            Info_02.Text = Infostring;
            TabControl1.SelectTab(2);
            TabControl1.Visible = true;
        }
        // Rechteckzapfen
        private void RechteckzapfenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_3, true, true, true, true);
            TB_40.SelectAll();
            Infostring = rm.GetString("String162");
            Info_03.Text = Infostring;
            TabControl1.SelectTab(3);
            TabControl1.Visible = true;
        }
        // Kreistasche
        private void KreistascheToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_4, true, true, true, true);
            TB_60.SelectAll();
            Infostring = rm.GetString("String187");
            Info_04.Text = Infostring;
            TabControl1.SelectTab(4);
            TabControl1.Visible = true;
        }
        // Kreiszapfen
        private void KreiszapfenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_5, true, true, true, true);
            TB_70.SelectAll();
            Infostring = rm.GetString("String188");
            Info_05.Text = Infostring;
            TabControl1.SelectTab(5);
            TabControl1.Visible = true;
        }
        // Nut
        private void NutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_11, true, true, true, true);
            TB_200.SelectAll();
            Infostring = rm.GetString("String181");
            Info_11.Text = Infostring;
            TabControl1.SelectTab(6);
            TabControl1.Visible = true;
        }
        // Ringnut
        private void RingnutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_6, true, true, true, true);
            TB_81.SelectAll();
            Infostring = rm.GetString("String189");
            Info_06.Text = Infostring;
            TabControl1.SelectTab(7);
            TabControl1.Visible = true;
        }
        // Dichtungsnut
        private void DichtungsnutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_9, true, true, true, true);
            TB_120.SelectAll();
            Infostring = rm.GetString("String192");
            Info_09.Text = Infostring;
            TabControl1.SelectTab(8);
            TabControl1.Visible = true;
        }
        // Bohrtabelle
        private void BohrtabelleToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_7, true, true, true, true);
            TB_90.SelectAll();
            Infostring = rm.GetString("String190");
            Info_07.Text = Infostring;
            TabControl1.SelectTab(9);
            TabControl1.Visible = true;
        }
        // BohrbildLochkreis
        private void BohrbildLochkreisToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_8, true, true, true, true);
            TB_100.SelectAll();
            Infostring = rm.GetString("String191");
            Info_08.Text = Infostring;
            TabControl1.SelectTab(10);
            TabControl1.Visible = true;
        }
        // Bohrbild auf Linien
        private void BohrbildAufLinienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_12, true, true, true, true);
            TB_150.SelectAll();
            Infostring = rm.GetString("String193");
            Info_12.Text = Infostring;
            TabControl1.SelectTab(11);
            TabControl1.Visible = true;
        }
        // Runde Nut
        private void RundeNutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_13, true, true, true, true);
            TB_170.SelectAll();
            Infostring = rm.GetString("String196");
            Info_13.Text = Infostring;
            TabControl1.SelectTab(12);
            TabControl1.Visible = true;
        }
        // Buchstaben und Schriftzüge
        private void SchriftGavierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Font_Art.SelectedIndex = 0;
            SelectNextControl(Abbrechen_14, true, true, true, true);
            TB_220.SelectAll();
            Infostring = rm.GetString("String194");
            Info_14.Text = Infostring;
            TabControl1.SelectTab(13);
            TabControl1.Visible = true;
        }
        // Gewinde fräsen
        private void GewindeFräsenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TB_251.SelectedIndex = 0;
            SelectNextControl(Abbrechen_15, true, true, true, true);
            TB_250.SelectAll();
            Infostring = rm.GetString("String147");
            Info_15.Text = Infostring;
            PF_Picture = My.Resources.Resources.Bild15_01;
            PictureBox15.Image = PF_Picture;
            TabControl1.SelectTab(14);
            TabControl1.Visible = true;
        }
        // Tiefloch bohren
        private void TieflochBohrenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_16, true, true, true, true);
            TB_260.SelectAll();
            Infostring = rm.GetString("String137");
            Info_16.Text = Infostring;
            TabControl1.SelectTab(15);
            TabControl1.Visible = true;
        }
        // DXF wandeln
        private void DxfWandelnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectNextControl(Abbrechen_17, true, true, true, true);
            TB_280.SelectAll();
            Infostring = rm.GetString("String195");
            Info_17.Text = Infostring;
            TabControl1.SelectTab(16);
            TabControl1.Visible = true;
        }
        // Infofenster anzeigen
        private void ÜberToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            My.MyProject.Forms.AboutBox1.Show();
        }
        #endregion
        // ------------------------------------------------------------------------------------------------
        #region Zyklus Planfräsen in X und Y-Richtung
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_3_GotFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = My.Resources.Resources.Bild01_02;
            Info_01.Text = rm.GetString("String124");
        }
        private void TB_3_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_4_GotFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = My.Resources.Resources.Bild01_03;
            Info_01.Text = rm.GetString("String125");
        }
        private void TB_4_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_5_GotFocus(object sender, EventArgs e)
        {
            double FT;
            double ZS;
            PictureBox1.Image = My.Resources.Resources.Bild01_04;
            Info_01.Text = rm.GetString("String126");
            // wenn finale Tiele kleiner als Zustellung: Zustellung anpassen
            FT = -Convert.ToDouble(TB_4.Text);
            ZS = Conversions.ToDouble(TB_5.Text);
            if (FT < ZS)
                TB_5.Text = FT.ToString();
        }
        private void TB_5_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_6_GotFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = My.Resources.Resources.Bild01_05;
            Info_01.Text = rm.GetString("String127");
        }
        private void TB_6_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Hilfe anzeigen (Abmessung X)
        private void TB_7_GotFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = My.Resources.Resources.Bild01_06;
            Info_01.Text = rm.GetString("String128");
        }
        private void TB_7_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Hilfe anzeigen (Abmessung Y)
        private void TB_8_GotFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = My.Resources.Resources.Bild01_07;
            Info_01.Text = rm.GetString("String129");
        }
        private void TB_8_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_9_GotFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = My.Resources.Resources.Bild01_08;
            Info_01.Text = rm.GetString("String130");
        }
        private void TB_9_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
            Info_01.Text = Infostring;
        }
        // Methode 1 (abzeilen von Z=0 nach Z+) wurde gewählt
        private void Methode1_GotFocus(object sender, EventArgs e)
        {
            PF_Picture = My.Resources.Resources.Bild01_01;
            PictureBox1.Image = PF_Picture;
        }
        private void Methode1_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
        }
        // Methode 2 (von Außen nach Innen) wurde gewählt
        private void Methode2_GotFocus(object sender, EventArgs e)
        {
            PF_Picture = My.Resources.Resources.Bild01_09;
            PictureBox1.Image = PF_Picture;
        }
        private void Methode2_LostFocus(object sender, EventArgs e)
        {
            PictureBox1.Image = PF_Picture;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_1_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_1.Text = "8,0";
            TB_2.Text = "4000";
            TB_3.Text = "3,0";
            TB_4.Text = "-1,0";
            TB_5.Text = "0,5";
            TB_6.Text = "200";
            TB_7.Text = "100,0";
            TB_8.Text = "50,0";
            TB_9.Text = "800";
            Methode1.Checked = true;
            Methode2.Checked = false;
            PF_Picture = My.Resources.Resources.Bild01_01;
            PictureBox1.Image = PF_Picture;
            SelectNextControl(Abbrechen_1, true, true, true, true);
        }
        // G-Code für Planfraesen erzeugen
        private void Gcode_1_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            int BFX;
            int BFY;
            double FT;
            double ZS;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_1.Text = TB_1.Text.Replace(".", ",");
                TB_2.Text = TB_2.Text.Replace(".", ",");
                TB_3.Text = TB_3.Text.Replace(".", ",");
                TB_4.Text = TB_4.Text.Replace(".", ",");
                TB_5.Text = TB_5.Text.Replace(".", ",");
                TB_6.Text = TB_6.Text.Replace(".", ",");
                TB_7.Text = TB_7.Text.Replace(".", ",");
                TB_8.Text = TB_8.Text.Replace(".", ",");
                TB_9.Text = TB_9.Text.Replace(".", ",");
                // Dateikopf
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String236") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_1.Text + " mm )" + Constants.vbCrLf);
                BFX = (int)Round(Conversion.Val(TB_7.Text) + Conversion.Val(TB_1.Text));
                BFY = (int)Round(Conversion.Val(TB_8.Text) + Conversion.Val(TB_1.Text));
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                // wenn finale Tiele kleiner als Zustellung: Zustellung anpassen
                FT = -Convert.ToDouble(TB_4.Text);
                ZS = Conversions.ToDouble(TB_5.Text);
                if (FT < ZS)
                    TB_5.Text = FT.ToString();
                Zyklen.DT = Conversions.ToDouble(TB_1.Text);                            // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_2.Text);                         // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_3.Text);                   // Startposition Z über Werkstück
                Zyklen.DepthZ = FT;                                                     // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_5.Text);                         // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_6.Text);                      // Vorschub in Z
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_7.Text);                   // Größe in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_8.Text);                   // Größe in Y
                Zyklen.FCut = Conversions.ToInteger(TB_9.Text);                         // Vorschub fräsen
                if (Methode1.Checked == true)                                           // Methode 1 gewählt
                {
                    Zyklen.RemoveOFin = false;
                }
                else if (Methode2.Checked == true)                                      // Methode 2 gewählt
                {
                    Zyklen.RemoveOFin = true;
                }
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Zyklus aufrufen
                Fehler = Conversions.ToBoolean(0);
                Zyklen.Planfraesen();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / (double)BFX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (double)BFY);
                }
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);      // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);      // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                        // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                        // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                       // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                       // Drehwinkel
                My.MyProject.Forms.Form2.Befehl = "Planfraesen";
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                // Dateiname
                My.MyProject.Forms.Form2.Dateiname = rm.GetString("String362");
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Rechtecktasche fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_22_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_02;
            Info_02.Text = rm.GetString("String124");
        }
        private void TB_22_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_23_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_03;
            Info_02.Text = rm.GetString("String156");
        }
        private void TB_23_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_24_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_04;
            Info_02.Text = rm.GetString("String126");
        }
        private void TB_24_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_25_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_05;
            Info_02.Text = rm.GetString("String127");
        }
        private void TB_25_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Taschengröße in X)
        private void TB_26_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_06;
            Info_02.Text = rm.GetString("String157");
        }
        private void TB_26_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Taschengröße in Y)
        private void TB_27_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_07;
            Info_02.Text = rm.GetString("String158");
        }
        private void TB_27_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_28_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_08;
            Info_02.Text = rm.GetString("String130");
        }
        private void TB_28_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Eckenradius)
        private void TB_29_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_09;
            Info_02.Text = rm.GetString("String159");
        }
        private void TB_29_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (nur schlichten)
        private void Schlicht_1_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_10;
            Info_02.Text = rm.GetString("String160");
        }
        private void Schlicht_1_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // Hilfe anzeigen (Schlichtaufmaß)
        private void TB_30_GotFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_10;
            Info_02.Text = rm.GetString("String161");
        }
        private void TB_30_LostFocus(object sender, EventArgs e)
        {
            PictureBox2.Image = My.Resources.Resources.Bild02_01;
            Info_02.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_2_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_20.Text = "8,0";
            TB_21.Text = "4000";
            TB_22.Text = "3,0";
            TB_23.Text = "-5,0";
            TB_24.Text = "0,6";
            TB_25.Text = "200";
            TB_26.Text = "80,0";
            TB_27.Text = "30,0";
            TB_28.Text = "800";
            TB_29.Text = "4,0";
            Schlicht_1.Checked = false;
            TB_30.Text = "0,0";
            SelectNextControl(Abbrechen_2, true, true, true, true);
        }
        // bei Änderung des Werkzeugdurchmesser - Radius anpassen
        private void TB_20_TextChanged(object sender, EventArgs e)
        {
            double R;
            R = Conversion.Val(TB_20.Text) / 2d;
            TB_29.Text = R.ToString("##0.0", System.Globalization.CultureInfo.CurrentCulture);
        }
        // wenn "nur schlichten" ausgeschaltet, dann Schlichtaufmaß auf 0 setzen
        private void Schlicht_1_CheckedChanged(object sender, EventArgs e)
        {
            if (Schlicht_1.Checked == false)
            {
                TB_30.Text = "0,0";
            }
        }
        // G-Code für Rechtecktasche erzeugen
        private void Gcode_2_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            int BFX;
            int BFY;
            // wenn "nur schlichten" ausgeschaltet, dann Schlichtaufmaß auf 0 setzen
            if (Schlicht_1.Checked == false)
            {
                TB_30.Text = 0.ToString();
            }
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_20.Text = TB_20.Text.Replace(".", ",");
                TB_21.Text = TB_21.Text.Replace(".", ",");
                TB_22.Text = TB_22.Text.Replace(".", ",");
                TB_23.Text = TB_23.Text.Replace(".", ",");
                TB_24.Text = TB_24.Text.Replace(".", ",");
                TB_25.Text = TB_25.Text.Replace(".", ",");
                TB_26.Text = TB_26.Text.Replace(".", ",");
                TB_27.Text = TB_27.Text.Replace(".", ",");
                TB_28.Text = TB_28.Text.Replace(".", ",");
                TB_29.Text = TB_29.Text.Replace(".", ",");
                TB_30.Text = TB_30.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                if (Schlicht_1.Checked == false)
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String261") + Constants.vbCrLf);
                }
                else
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String262") + Constants.vbCrLf);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_20.Text + " mm )" + Constants.vbCrLf);
                BFX = (int)Round(Conversion.Val(TB_26.Text) + Conversion.Val(TB_20.Text));
                BFY = (int)Round(Conversion.Val(TB_27.Text) + Conversion.Val(TB_20.Text));
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_20.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_21.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_22.Text);                  // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_23.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_24.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_25.Text);                     // Vorschub in Z
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_26.Text);                  // Größe in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_27.Text);                  // Größe in Y
                Zyklen.FCut = Conversions.ToInteger(TB_28.Text);                        // Vorschub fräsen
                Zyklen.Radius = Conversions.ToDouble(TB_29.Text);                       // Eckenradius
                Zyklen.RemoveOFin = Schlicht_1.Checked;                                 // nur schlichten
                Zyklen.OFin = Conversions.ToDouble(TB_30.Text);                         // Schlichtaufmass in X/Y
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition bei schruppen: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Rechtecktasche();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / (double)BFX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (double)BFY);
                }
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);      // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);      // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.Radius * (double)Skalierung);           // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                        // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                       // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                       // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Rechtecktasche";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Rechteckzapfen fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_42_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_02;
            Info_03.Text = rm.GetString("String124");
        }
        private void TB_42_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_43_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_03;
            Info_03.Text = rm.GetString("String165");
        }
        private void TB_43_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_44_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_04;
            Info_03.Text = rm.GetString("String126");
        }
        private void TB_44_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_45_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_05;
            Info_03.Text = rm.GetString("String127");

        }
        private void TB_45_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Zapfengröße in X)
        private void TB_46_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_06;
            Info_03.Text = rm.GetString("String166");
        }
        private void TB_46_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Zapfengröße in Y)
        private void TB_47_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_07;
            Info_03.Text = rm.GetString("String167");
        }
        private void TB_47_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_48_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_08;
            Info_03.Text = rm.GetString("String130");
        }
        private void TB_48_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Eckenradius)
        private void TB_49_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_09;
            Info_03.Text = rm.GetString("String168");
        }
        private void TB_49_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Restmaterial in X)
        private void TB_50_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_10;
            Info_03.Text = rm.GetString("String169");
        }
        private void TB_50_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // Hilfe anzeigen (Restmaterial in Y)
        private void TB_51_GotFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_10;
            Info_03.Text = rm.GetString("String170");
        }
        private void TB_51_LostFocus(object sender, EventArgs e)
        {
            PictureBox3.Image = My.Resources.Resources.Bild03_01;
            Info_03.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_3_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_40.Text = "8,0";
            TB_41.Text = "4000";
            TB_42.Text = "3,0";
            TB_43.Text = "-5,0";
            TB_44.Text = "0,6";
            TB_45.Text = "200";
            TB_46.Text = "20,0";
            TB_47.Text = "20,0";
            TB_48.Text = "800";
            TB_49.Text = "0,0";
            TB_50.Text = "30,0";
            TB_51.Text = "5,0";
            SelectNextControl(Abbrechen_3, true, true, true, true);
        }
        // G-Code für Rechtecktzapfen erzeugen
        private void Gcode_3_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            int BFX;
            int BFY;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_40.Text = TB_40.Text.Replace(".", ",");
                TB_41.Text = TB_41.Text.Replace(".", ",");
                TB_42.Text = TB_42.Text.Replace(".", ",");
                TB_43.Text = TB_43.Text.Replace(".", ",");
                TB_44.Text = TB_44.Text.Replace(".", ",");
                TB_45.Text = TB_45.Text.Replace(".", ",");
                TB_46.Text = TB_46.Text.Replace(".", ",");
                TB_47.Text = TB_47.Text.Replace(".", ",");
                TB_48.Text = TB_48.Text.Replace(".", ",");
                TB_49.Text = TB_49.Text.Replace(".", ",");
                TB_50.Text = TB_50.Text.Replace(".", ",");
                TB_51.Text = TB_51.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String263") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_40.Text + " mm )" + Constants.vbCrLf);
                BFX = (int)Round(Conversion.Val(TB_50.Text) * 2d + Conversion.Val(TB_46.Text) + Conversion.Val(TB_40.Text));
                BFY = (int)Round(Conversion.Val(TB_51.Text) * 2d + Conversion.Val(TB_47.Text) + Conversion.Val(TB_40.Text));
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_40.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_41.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_42.Text);                  // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_43.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_44.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_45.Text);                     // Vorschub in Z
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_46.Text);                  // Größe in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_47.Text);                  // Größe in Y
                Zyklen.FCut = Conversions.ToInteger(TB_48.Text);                        // Vorschub fräsen
                Zyklen.Radius = Conversions.ToDouble(TB_49.Text);                       // Eckenradius
                Zyklen.OFinX = Conversions.ToDouble(TB_50.Text);                        // Aufmaß Rohteil in X
                Zyklen.OFinY = Conversions.ToDouble(TB_51.Text);                        // Aufmaß Rohteil in Y
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition bei schruppen: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Rechteckzapfen();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 20;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / (double)BFX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (double)BFY);
                }
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Startpunkt des Zyklus in X1
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - (Zyklen.PocketSizeX + Zyklen.OFinX * 2d) / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX1 = (int)Round(Temp);
                // Startpunkt des Zyklus in Y1
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.PocketSizeY + Zyklen.OFinY * 2d) / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY1 = (int)Round(Temp);
                // Startpunkt des Zyklus in Z1
                My.MyProject.Forms.Form2.NpZ1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);                              // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);                              // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                                   // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.Radius * (double)Skalierung);                                   // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                                                // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = (int)Round((Zyklen.PocketSizeX + Zyklen.OFinX * 2d) * (double)Skalierung);       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = (int)Round((Zyklen.PocketSizeY + Zyklen.OFinY * 2d) * (double)Skalierung);       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                                  // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                                               // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                                               // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Rechteckzapfen";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Kreistasche fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_62_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_02;
            Info_04.Text = rm.GetString("String124");
        }
        private void TB_62_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_63_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_03;
            Info_04.Text = rm.GetString("String173");
        }
        private void TB_63_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_64_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_05;
            Info_04.Text = rm.GetString("String126");
        }
        private void TB_64_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_65_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_04;
            Info_04.Text = rm.GetString("String127");
        }
        private void TB_65_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (Fertigteildurchmesser)
        private void TB_66_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_06;
            Info_04.Text = rm.GetString("String353");
        }
        private void TB_66_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_67_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_07;
            Info_04.Text = rm.GetString("String130");
        }
        private void TB_67_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (nur schlichten)
        private void Schlicht_2_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_08;
            Info_04.Text = rm.GetString("String174");
        }
        private void Schlicht_2_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // Hilfe anzeigen (Schlichtaufmaß)
        private void TB_68_GotFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_09;
            Info_04.Text = rm.GetString("String175");
        }
        private void TB_68_LostFocus(object sender, EventArgs e)
        {
            PictureBox4.Image = My.Resources.Resources.Bild04_01;
            Info_04.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_4_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_60.Text = "8,0";
            TB_61.Text = "4000";
            TB_62.Text = "3,0";
            TB_63.Text = "-5,0";
            TB_64.Text = "0,6";
            TB_65.Text = "200";
            TB_66.Text = "40,0";
            TB_67.Text = "800";
            Schlicht_2.Checked = false;
            TB_68.Text = "40,0";
            SelectNextControl(Abbrechen_4, true, true, true, true);
        }
        // wenn "nur schlichten" ausgeschaltet, dann Druchmesser Rohteil auf Druchmesser Fertigteil setzen
        private void Schlicht_2_CheckedChanged(object sender, EventArgs e)
        {
            if (Schlicht_2.Checked == false)
            {
                TB_68.Text = TB_66.Text;
            }
        }
        // G-Code für Kreistasche erzeugen
        private void Gcode_4_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_60.Text = TB_60.Text.Replace(".", ",");
                TB_61.Text = TB_61.Text.Replace(".", ",");
                TB_62.Text = TB_62.Text.Replace(".", ",");
                TB_63.Text = TB_63.Text.Replace(".", ",");
                TB_64.Text = TB_64.Text.Replace(".", ",");
                TB_65.Text = TB_65.Text.Replace(".", ",");
                TB_66.Text = TB_66.Text.Replace(".", ",");
                TB_67.Text = TB_67.Text.Replace(".", ",");
                TB_68.Text = TB_68.Text.Replace(".", ",");
                // wenn "nur schlichten" ausgeschaltet, dann Druchmesser Rohteil auf Druchmesser Fertigteil setzen
                if (Schlicht_2.Checked == false)
                {
                    TB_68.Text = TB_66.Text;
                }
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String279") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_40.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_60.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_61.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_62.Text);                  // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_63.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_64.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_65.Text);                     // Vorschub in Z
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_66.Text);                  // Fertigteildurchmesser
                Zyklen.FCut = Conversions.ToInteger(TB_67.Text);                        // Vorschub fräsen
                Zyklen.RemoveOFin = Schlicht_2.Checked;                                 // nur schlichten
                Zyklen.OFinX = Conversions.ToDouble(TB_68.Text);                        // Rohteildurchmesser
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition bei schruppen: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Kreistasche();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Zyklen.PocketSizeX);
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);          // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);          // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);               // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.PocketSizeX / 2d * (double)Skalierung);     // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                            // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                           // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                           // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                           // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                           // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Kreistasche";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Kreiszapfen fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_72_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_02;
            Info_05.Text = rm.GetString("String124");
        }
        private void TB_72_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_73_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_03;
            Info_05.Text = rm.GetString("String176");
        }
        private void TB_73_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_74_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_05;
            Info_05.Text = rm.GetString("String126");
        }
        private void TB_74_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_75_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_04;
            Info_05.Text = rm.GetString("String127");
        }
        private void TB_75_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_76_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_06;
            Info_05.Text = rm.GetString("String130");
        }
        private void TB_76_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Druchmesser Rohteil)
        private void TB_77_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_08;
            Info_05.Text = rm.GetString("String177");
        }
        private void TB_77_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Durchmesser Fertigteil)
        private void TB_78_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_07;
            Info_05.Text = rm.GetString("String178");
        }
        private void TB_78_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Restmaterial in X)
        private void TB_79_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_09;
            Info_05.Text = rm.GetString("String179");
        }
        private void TB_79_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // Hilfe anzeigen (Restmaterial in Y)
        private void TB_80_GotFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_09;
            Info_05.Text = rm.GetString("String180");
        }
        private void TB_80_LostFocus(object sender, EventArgs e)
        {
            PictureBox5.Image = My.Resources.Resources.Bild05_01;
            Info_05.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_5_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_70.Text = "8,0";
            TB_71.Text = "4000";
            TB_72.Text = "3,0";
            TB_73.Text = "-5,0";
            TB_74.Text = "0,6";
            TB_75.Text = "200";
            TB_76.Text = "800";
            TB_77.Text = "40,0";
            TB_78.Text = "36,0";
            TB_79.Text = "30,0";
            TB_80.Text = "5,0";
            SelectNextControl(Abbrechen_5, true, true, true, true);
        }
        // G-Code für Kreiszapfen erzeugen
        private void Gcode_5_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            int BFX;
            int BFY;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_70.Text = TB_70.Text.Replace(".", ",");
                TB_71.Text = TB_71.Text.Replace(".", ",");
                TB_72.Text = TB_72.Text.Replace(".", ",");
                TB_73.Text = TB_73.Text.Replace(".", ",");
                TB_74.Text = TB_74.Text.Replace(".", ",");
                TB_75.Text = TB_75.Text.Replace(".", ",");
                TB_76.Text = TB_76.Text.Replace(".", ",");
                TB_77.Text = TB_77.Text.Replace(".", ",");
                TB_78.Text = TB_78.Text.Replace(".", ",");
                TB_79.Text = TB_79.Text.Replace(".", ",");
                TB_80.Text = TB_80.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String278") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_70.Text + " mm )" + Constants.vbCrLf);
                BFX = (int)Round(Conversion.Val(TB_79.Text) * 2d + Conversion.Val(TB_77.Text) + Conversion.Val(TB_70.Text));
                BFY = (int)Round(Conversion.Val(TB_80.Text) * 2d + Conversion.Val(TB_77.Text) + Conversion.Val(TB_70.Text));
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_70.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_71.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_72.Text);                  // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_73.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_74.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_75.Text);                     // Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_76.Text);                        // Vorschub fräsen
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_77.Text);                  // Rohteildurchmesser
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_78.Text);                  // Fertigteildurchmesser
                Zyklen.OFinX = Conversions.ToDouble(TB_79.Text);                        // Aufmaß Rohteil in X 
                Zyklen.OFinY = Conversions.ToDouble(TB_80.Text);                        // Aufmaß Rohteil in Y 
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Kreiszapfen();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (double)BFX);
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Startpunkt des Zyklus in X1
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - (Zyklen.PocketSizeX + Zyklen.OFinX * 2d) / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX1 = (int)Round(Temp);
                // Startpunkt des Zyklus in Y1
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.PocketSizeY + Zyklen.OFinY * 2d) / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY1 = (int)Round(Temp);
                // Startpunkt des Zyklus in Z1
                My.MyProject.Forms.Form2.NpZ1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);                              // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);                              // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                                   // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.PocketSizeY / 2d * (double)Skalierung);                         // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                                                // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = (int)Round((Zyklen.PocketSizeX + Zyklen.OFinX * 2d) * (double)Skalierung);       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = (int)Round((Zyklen.PocketSizeY + Zyklen.OFinY * 2d) * (double)Skalierung);       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                                  // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                                               // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                                               // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Kreiszapfen";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Ringnut fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_83_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_02;
            Info_06.Text = rm.GetString("String124");
        }
        private void TB_83_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_84_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_03;
            Info_06.Text = rm.GetString("String205");
        }
        private void TB_84_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_85_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_04;
            Info_06.Text = rm.GetString("String126");
        }
        private void TB_85_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_86_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_05;
            Info_06.Text = rm.GetString("String127");
        }
        private void TB_86_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_87_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_06;
            Info_06.Text = rm.GetString("String130");
        }
        private void TB_87_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // Hilfe anzeigen (Außendurchmesser)
        private void TB_88_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_07;
            Info_06.Text = rm.GetString("String206");
        }
        private void TB_88_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // Hilfe anzeigen (Innendurchmesser)
        private void TB_89_GotFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_08;
            Info_06.Text = rm.GetString("String207");
        }
        private void TB_89_LostFocus(object sender, EventArgs e)
        {
            PictureBox6.Image = My.Resources.Resources.Bild06_01;
            Info_06.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_6_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_81.Text = "8,0";
            TB_82.Text = "4000";
            TB_83.Text = "3,0";
            TB_84.Text = "-5,0";
            TB_85.Text = "0,6";
            TB_86.Text = "200";
            TB_87.Text = "800";
            TB_88.Text = "40,0";
            TB_89.Text = "22,0";
            SelectNextControl(Abbrechen_6, true, true, true, true);
        }
        // G-Code für Ringnut erzeugen
        private void Gcode_6_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_81.Text = TB_81.Text.Replace(".", ",");
                TB_82.Text = TB_82.Text.Replace(".", ",");
                TB_83.Text = TB_83.Text.Replace(".", ",");
                TB_84.Text = TB_84.Text.Replace(".", ",");
                TB_85.Text = TB_85.Text.Replace(".", ",");
                TB_86.Text = TB_86.Text.Replace(".", ",");
                TB_87.Text = TB_87.Text.Replace(".", ",");
                TB_88.Text = TB_88.Text.Replace(".", ",");
                TB_89.Text = TB_89.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String283") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_81.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_81.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_82.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_83.Text);                  // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_84.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_85.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_86.Text);                     // Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_87.Text);                        // Vorschub fräsen
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_88.Text);                  // Außendurchmesser
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_89.Text);                  // Innendurchmesser
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Ringnut();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Zyklen.PocketSizeX);
                // Startpunkt des Zyklus in X
                My.MyProject.Forms.Form2.NpX = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                My.MyProject.Forms.Form2.NpZ1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);              // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);              // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                   // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.PocketSizeY / 2d * (double)Skalierung);         // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                                // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);             // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);             // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                  // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = (int)Round(Zyklen.PocketSizeX / 2d * (double)Skalierung);        // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                               // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Ringnut";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Bohrtabelle
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_92_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_02;
            Info_07.Text = rm.GetString("String124");
        }
        private void TB_92_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_93_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_03;
            Info_07.Text = rm.GetString("String138");
        }
        private void TB_93_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_94_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_04;
            Info_07.Text = rm.GetString("String126");
        }
        private void TB_94_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_95_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_05;
            Info_07.Text = rm.GetString("String139");
        }
        private void TB_95_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // Hilfe anzeigen (Bohrtiefe Spanbruch)
        private void TB_96_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_06;
            Info_07.Text = rm.GetString("String140");
        }
        private void TB_96_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // Hilfe anzeigen (Rückzug bis Spanbruch)
        private void TB_97_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_07;
            Info_07.Text = rm.GetString("String141");
        }
        private void TB_97_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // Hilfe anzeigen (Ausspantiefe)
        private void TB_98_GotFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_08;
            Info_07.Text = rm.GetString("String142");
        }
        private void TB_98_LostFocus(object sender, EventArgs e)
        {
            PictureBox7.Image = My.Resources.Resources.Bild07_01;
            Info_07.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_7_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_90.Text = "3,0";
            TB_91.Text = "2000";
            TB_92.Text = "3,0";
            TB_93.Text = "-10,0";
            TB_94.Text = "0,8";
            TB_95.Text = "40";
            TB_96.Text = "2,0";
            TB_97.Text = "0,2";
            TB_98.Text = "-5,0";
            BT1X.Text = "10,0";
            BT1Y.Text = "10,0";
            BT2X.Text = "10,0";
            BT2Y.Text = "30,0";
            BT3X.Text = "70,0";
            BT3Y.Text = "30,0";
            BT4X.Text = "70,0";
            BT4Y.Text = "10,0";
            BT5X.Text = "";
            BT5Y.Text = "";
            BT6X.Text = "";
            BT6Y.Text = "";
            BT7X.Text = "";
            BT7Y.Text = "";
            BT8X.Text = "";
            BT8Y.Text = "";
            BT9X.Text = "";
            BT9Y.Text = "";
            BT10X.Text = "";
            BT10Y.Text = "";
            BT11X.Text = "";
            BT11Y.Text = "";
            BT12X.Text = "";
            BT12Y.Text = "";
            BT13X.Text = "";
            BT13Y.Text = "";
            SelectNextControl(Abbrechen_7, true, true, true, true);
        }
        // Textkorrektur bei Eingabe
        private void BT1X_LostFocus(object sender, EventArgs e)
        {
            BT1X.Text = BT1X.Text.Replace(".", ",");
            BT1X.Text = BT1X.Text.Replace("..", ",");
        }
        private void BT1Y_LostFocus(object sender, EventArgs e)
        {
            BT1Y.Text = BT1Y.Text.Replace(".", ",");
            BT1Y.Text = BT1Y.Text.Replace("..", ",");
        }
        private void BT2X_LostFocus(object sender, EventArgs e)
        {
            BT2X.Text = BT2X.Text.Replace(".", ",");
            BT2X.Text = BT2X.Text.Replace("..", ",");
        }
        private void BT2Y_LostFocus(object sender, EventArgs e)
        {
            BT2Y.Text = BT2Y.Text.Replace(".", ",");
            BT2Y.Text = BT2Y.Text.Replace("..", ",");
        }
        private void BT3X_LostFocus(object sender, EventArgs e)
        {
            BT3X.Text = BT3X.Text.Replace(".", ",");
            BT3X.Text = BT3X.Text.Replace("..", ",");
        }
        private void BT3Y_LostFocus(object sender, EventArgs e)
        {
            BT3Y.Text = BT3Y.Text.Replace(".", ",");
            BT3Y.Text = BT3Y.Text.Replace("..", ",");
        }
        private void BT4X_LostFocus(object sender, EventArgs e)
        {
            BT4X.Text = BT4X.Text.Replace(".", ",");
            BT4X.Text = BT4X.Text.Replace("..", ",");
        }
        private void BT4Y_LostFocus(object sender, EventArgs e)
        {
            BT4Y.Text = BT4Y.Text.Replace(".", ",");
            BT4Y.Text = BT4Y.Text.Replace("..", ",");
        }
        private void BT5X_LostFocus(object sender, EventArgs e)
        {
            BT5X.Text = BT5X.Text.Replace(".", ",");
            BT5X.Text = BT5X.Text.Replace("..", ",");
        }
        private void BT5Y_LostFocus(object sender, EventArgs e)
        {
            BT5Y.Text = BT5Y.Text.Replace(".", ",");
            BT5Y.Text = BT5Y.Text.Replace("..", ",");
        }
        private void BT6X_LostFocus(object sender, EventArgs e)
        {
            BT6X.Text = BT6X.Text.Replace(".", ",");
            BT6X.Text = BT6X.Text.Replace("..", ",");
        }
        private void BT6Y_LostFocus(object sender, EventArgs e)
        {
            BT6Y.Text = BT6Y.Text.Replace(".", ",");
            BT6Y.Text = BT6Y.Text.Replace("..", ",");
        }
        private void BT7X_LostFocus(object sender, EventArgs e)
        {
            BT7X.Text = BT7X.Text.Replace(".", ",");
            BT7X.Text = BT7X.Text.Replace("..", ",");
        }
        private void BT7Y_LostFocus(object sender, EventArgs e)
        {
            BT7Y.Text = BT7Y.Text.Replace(".", ",");
            BT7Y.Text = BT7Y.Text.Replace("..", ",");
        }
        private void BT8X_LostFocus(object sender, EventArgs e)
        {
            BT8X.Text = BT8X.Text.Replace(".", ",");
            BT8X.Text = BT8X.Text.Replace("..", ",");
        }
        private void BT8Y_LostFocus(object sender, EventArgs e)
        {
            BT8Y.Text = BT8Y.Text.Replace(".", ",");
            BT8Y.Text = BT8Y.Text.Replace("..", ",");
        }
        private void BT9X_LostFocus(object sender, EventArgs e)
        {
            BT9X.Text = BT9X.Text.Replace(".", ",");
            BT9X.Text = BT9X.Text.Replace("..", ",");
        }
        private void BT9Y_LostFocus(object sender, EventArgs e)
        {
            BT9Y.Text = BT9Y.Text.Replace(".", ",");
            BT9Y.Text = BT9Y.Text.Replace("..", ",");
        }
        private void BT10X_LostFocus(object sender, EventArgs e)
        {
            BT10X.Text = BT10X.Text.Replace(".", ",");
            BT10X.Text = BT10X.Text.Replace("..", ",");
        }
        private void BT10Y_LostFocus(object sender, EventArgs e)
        {
            BT10Y.Text = BT10Y.Text.Replace(".", ",");
            BT10Y.Text = BT10Y.Text.Replace("..", ",");
        }
        private void BT11X_LostFocus(object sender, EventArgs e)
        {
            BT11X.Text = BT11X.Text.Replace(".", ",");
            BT11X.Text = BT11X.Text.Replace("..", ",");
        }
        private void BT11Y_LostFocus(object sender, EventArgs e)
        {
            BT11Y.Text = BT11Y.Text.Replace(".", ",");
            BT11Y.Text = BT11Y.Text.Replace("..", ",");
        }
        private void BT12X_LostFocus(object sender, EventArgs e)
        {
            BT12X.Text = BT12X.Text.Replace(".", ",");
            BT12X.Text = BT12X.Text.Replace("..", ",");
        }
        private void BT12Y_LostFocus(object sender, EventArgs e)
        {
            BT12Y.Text = BT12Y.Text.Replace(".", ",");
            BT12Y.Text = BT12Y.Text.Replace("..", ",");
        }
        private void BT13X_LostFocus(object sender, EventArgs e)
        {
            BT13X.Text = BT13X.Text.Replace(".", ",");
            BT13X.Text = BT13X.Text.Replace("..", ",");
        }
        private void BT13Y_LostFocus(object sender, EventArgs e)
        {
            BT13Y.Text = BT13Y.Text.Replace(".", ",");
            BT13Y.Text = BT13Y.Text.Replace("..", ",");
        }
        // Bohrtabelle löschen
        private void BT_loeschen_Click(object sender, EventArgs e)
        {
            StatusText = "";
            Control Next_Contr;
            int Temp;
            int Temp1;
            Next_Contr = GetNextControl(TB_97, true);
            Temp = Next_Contr.TabIndex + 1;
            Temp1 = Temp + 25;
            for (int N = Temp, loopTo = Temp1; N <= loopTo; N++)
            {
                Next_Contr = GetNextControl(Next_Contr, true);
                Next_Contr.Text = "";
            }
        }
        // G-Code für Bohrtabelle erzeugen
        private void Gcode_7_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            int Table_Pos;
            double WertX;
            double WertY;
            float Skalierung;
            float Xtemp;
            float Ytemp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_90.Text = TB_90.Text.Replace(".", ",");
                TB_91.Text = TB_91.Text.Replace(".", ",");
                TB_92.Text = TB_92.Text.Replace(".", ",");
                TB_93.Text = TB_93.Text.Replace(".", ",");
                TB_94.Text = TB_94.Text.Replace(".", ",");
                TB_95.Text = TB_95.Text.Replace(".", ",");
                TB_96.Text = TB_96.Text.Replace(".", ",");
                TB_97.Text = TB_97.Text.Replace(".", ",");
                TB_98.Text = TB_98.Text.Replace(".", ",");
                Table_Pos = 0;
                // Dateikopf
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String287") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String288") + TB_90.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                // Bohrpositionen
                Table_Pos = 0;
                Xtemp = 0f;
                Ytemp = 0f;
                // Spalte 1
                if (BT1X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT1X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT1Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT1Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 2
                if (BT2X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT2X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT2Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT2Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 3
                if (BT3X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT3X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT3Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT3Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 4
                if (BT4X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT4X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT4Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT4Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 5
                if (BT5X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT5X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT5Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT5Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 6
                if (BT6X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT6X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT6Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT6Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 7
                if (BT7X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT7X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT7Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT7Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 8
                if (BT8X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT8X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT8Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT8Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 9
                if (BT9X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT9X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT9Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT9Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 10
                if (BT10X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT10X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT10Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT10Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 11
                if (BT11X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT11X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT11Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT11Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 12
                if (BT12X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT12X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT12Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT12Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Table_Pos++;
                // Spalte 13
                if (BT13X.Text != "")
                {
                    WertX = Conversions.ToDouble(BT13X.Text);
                    Zyklen.BP[Table_Pos, 0] = WertX;
                    if (WertX > (double)Xtemp)
                        Xtemp = (float)WertX;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                if (BT13Y.Text != "")
                {
                    WertY = Conversions.ToDouble(BT13Y.Text);
                    Zyklen.BP[Table_Pos, 1] = WertY;
                    if (WertY > (double)Ytemp)
                        Ytemp = (float)WertY;
                }
                else
                {
                    Zyklen.BP[Table_Pos, 1] = 10000d;
                }
                Zyklen.BP[Table_Pos, 2] = 10000d;
                Zyklen.DT = Conversions.ToDouble(TB_90.Text);                           // Bohrerdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_91.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_92.Text);                  // Sicherheits-Abstand
                Zyklen.DepthZ = -Convert.ToDouble(TB_93.Text);                          // Bohrtiefe
                Zyklen.StepZ = Conversions.ToDouble(TB_94.Text);                        // Zustelltiefe
                Zyklen.FFinish = Conversions.ToInteger(TB_95.Text);                     // Vorschub Zustellung
                Zyklen.FCut = Conversions.ToInteger(TB_95.Text);                        // 
                Zyklen.DepthBs = Conversions.ToDouble(TB_96.Text);                      // Bohrtiefe Spanbruch
                Zyklen.DepthRs = Conversions.ToDouble(TB_97.Text);                      // Rückzug bis Spanbruch
                Zyklen.DepthAst = Conversions.ToDouble(TB_98.Text);                     // Ausspantiefe
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                 // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Bohrzyklus();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Xtemp += 20f;
                Ytemp += 20f;
                Rand = 60;
                Skalierung = (My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Xtemp;
                // Startpunkt des Zyklus in X
                My.MyProject.Forms.Form2.NpX = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - (double)(Xtemp * Skalierung / 2f));
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (double)(Ytemp * Skalierung / 2f));
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Xtemp * Skalierung);                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Ytemp * Skalierung);                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                    // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                    // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                   // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                   // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                   // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                   // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                   // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Bohrtabelle";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                {
                    My.MyProject.Forms.Form2.Dateiname = rm.GetString("String369");
                }
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Bohrbild Lochkreis
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_102_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_02;
            Info_08.Text = rm.GetString("String124");
        }
        private void TB_102_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_103_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_03;
            Info_08.Text = rm.GetString("String138");
        }
        private void TB_103_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_104_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_04;
            Info_08.Text = rm.GetString("String126");
        }
        private void TB_104_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_105_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_05;
            Info_08.Text = rm.GetString("String139");
        }
        private void TB_105_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Bohrtiefe Spanbruch)
        private void TB_106_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_06;
            Info_08.Text = rm.GetString("String140");
        }
        private void TB_106_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Rückzug bis Spanbruch)
        private void TB_107_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_07;
            Info_08.Text = rm.GetString("String141");
        }
        private void TB_107_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Ausspantiefe)
        private void TB_108_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_08;
            Info_08.Text = rm.GetString("String142");
        }
        private void TB_108_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Lochkreis Durchmesser)
        private void TB_109_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_09;
            Info_08.Text = rm.GetString("String218");
        }
        private void TB_109_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Anzahl der Bohrungen)
        private void TB_110_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_10;
            Info_08.Text = rm.GetString("String219");
        }
        private void TB_110_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Startwinkel)
        private void TB_111_GotFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_11;
            Info_08.Text = rm.GetString("String220");
        }
        private void TB_111_LostFocus(object sender, EventArgs e)
        {
            PictureBox8.Image = My.Resources.Resources.Bild08_01;
            Info_08.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_8_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_100.Text = "3,0";
            TB_101.Text = "2000";
            TB_102.Text = "3,0";
            TB_103.Text = "-10,0";
            TB_104.Text = "0,8";
            TB_105.Text = "40";
            TB_106.Text = "2,0";
            TB_107.Text = "0,2";
            TB_108.Text = "-5,0";
            TB_109.Text = "40,0";
            TB_110.Text = "6";
            TB_111.Text = "30";
            SelectNextControl(Abbrechen_8, true, true, true, true);
        }
        // G-Code für Bohrbild Lochkreis erzeugen
        private void Gcode_8_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            int N;
            int M;
            float Skalierung;
            double SW;
            double C;
            double Step1;
            var WertX = default(double);
            var WertY = default(double);
            var Temp1 = default(double);
            float Temp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_100.Text = TB_100.Text.Replace(".", ",");
                TB_101.Text = TB_101.Text.Replace(".", ",");
                TB_102.Text = TB_102.Text.Replace(".", ",");
                TB_103.Text = TB_103.Text.Replace(".", ",");
                TB_104.Text = TB_104.Text.Replace(".", ",");
                TB_105.Text = TB_105.Text.Replace(".", ",");
                TB_106.Text = TB_106.Text.Replace(".", ",");
                TB_107.Text = TB_107.Text.Replace(".", ",");
                TB_108.Text = TB_108.Text.Replace(".", ",");
                TB_109.Text = TB_109.Text.Replace(".", ",");
                TB_110.Text = TB_110.Text.Replace(".", ",");
                TB_111.Text = TB_111.Text.Replace(".", ",");
                // Fehlermeldungen
                if (Conversions.ToDouble(TB_109.Text) <= 0d)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String304"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    return;
                }
                if (Conversions.ToDouble(TB_109.Text) > Conversion.Val(Max_X.Text) | Conversions.ToDouble(TB_109.Text) > Conversion.Val(Max_Y.Text))
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String305"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    return;
                }
                if (Conversions.ToDouble(TB_110.Text) <= 0d)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String306"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    return;
                }
                if (Conversions.ToDouble(TB_110.Text) > 12d)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String307"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    return;
                }
                if (Conversions.ToDouble(TB_111.Text) < 0d | Conversions.ToDouble(TB_111.Text) > 360d)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String308"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    return;
                }
                // Dateikopf
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String300") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String288") + TB_90.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                // Bohrpositionen
                var loopTo = (int)Round(Conversion.Val(TB_110.Text) - 1d);
                // Anzahl der Bohrungen
                for (N = 0; N <= loopTo; N++)
                {
                    C = Conversion.Val(TB_109.Text) / 2d;               // Radius des Lochkreis
                    SW = 360d / Conversion.Val(TB_110.Text);            // Schrittwinkel = 360° / Anzahl Bohrungen
                    Step1 = Conversion.Val(TB_111.Text);                // Startwinkel
                    // erste Bohrung (nur Startwinkel)
                    if (N == 0)
                    {
                        Temp1 = Step1;
                    }
                    // alle anderen Startwinkel + Schrittwinkel
                    if (N > 0)
                    {
                        Temp1 += SW;
                    }
                    // Werte für X und Y berechnen
                    if (Temp1 == 0d | Temp1 == 360d)
                    {
                        WertX = C;
                        WertY = 0d;
                    }
                    else if (Temp1 > 0d & Temp1 < 90d)
                    {
                        WertX = Sinus(90d - Temp1) * C;
                        WertY = Sinus(Temp1) * C;
                    }
                    else if (Temp1 == 90d)
                    {
                        WertX = 0d;
                        WertY = C;
                    }
                    else if (Temp1 > 90d & Temp1 < 180d)
                    {
                        WertX = -(Sinus(Temp1 - 90d) * C);
                        WertY = Sinus(Temp1) * C;
                    }
                    else if (Temp1 == 180d)
                    {
                        WertX = -C;
                        WertY = 0d;
                    }
                    else if (Temp1 > 180d & Temp1 < 270d)
                    {
                        WertX = -(Sinus(270d - Temp1) * C);
                        WertY = Sinus(Temp1) * C;
                    }
                    else if (Temp1 == 270d)
                    {
                        WertX = 0d;
                        WertY = -C;
                    }
                    else if (Temp1 > 270d & Temp1 < 360d)
                    {
                        WertX = Sinus(Temp1 - 270d) * C;
                        WertY = Sinus(Temp1) * C;
                    }
                    // Werte in die Bohrtabelle übernehmen
                    Zyklen.BP[N, 0] = WertX;
                    Zyklen.BP[N, 1] = WertY;
                    Zyklen.BP[N, 2] = Temp1;
                }
                // Rest der Bohrtabelle mit ungültig füllen
                for (M = N; M <= 12; M++)
                {
                    Zyklen.BP[M, 0] = 10000d;
                    Zyklen.BP[M, 1] = 10000d;
                    Zyklen.BP[M, 2] = 10000d;
                }
                // Zyklusparameter
                Zyklen.DT = Conversions.ToDouble(TB_100.Text);                          // Bohrerdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_101.Text);                       // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_102.Text);                 // Sicherheits-Abstand
                Zyklen.DepthZ = -Convert.ToDouble(TB_103.Text);                         // Bohrtiefe
                Zyklen.StepZ = Conversions.ToDouble(TB_104.Text);                       // Zustelltiefe
                Zyklen.FFinish = Conversions.ToInteger(TB_105.Text);                    // Vorschub Zustellung
                Zyklen.FCut = Conversions.ToInteger(TB_105.Text);                       // 
                Zyklen.DepthBs = Conversions.ToDouble(TB_106.Text);                     // Bohrtiefe Spanbruch
                Zyklen.DepthRs = Conversions.ToDouble(TB_107.Text);                     // Rückzug bis Spanbruch
                Zyklen.DepthAst = Conversions.ToDouble(TB_108.Text);                    // Ausspantiefe
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_109.Text);
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Bohrzyklus();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                Temp = (float)(Zyklen.PocketSizeX + Zyklen.DT);
                Skalierung = (My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Temp;
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);          // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);          // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);               // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.PocketSizeX / 2d * (double)Skalierung);     // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                            // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                           // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                           // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                           // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                           // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Lochkreis";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                {
                    My.MyProject.Forms.Form2.Dateiname = rm.GetString("String370");
                }
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Dichtungsnut fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_122_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_02;
            Info_09.Text = rm.GetString("String124");
        }
        private void TB_122_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_123_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_03;
            Info_09.Text = rm.GetString("String214");
        }
        private void TB_123_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_124_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_04;
            Info_09.Text = rm.GetString("String126");
        }
        private void TB_124_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_125_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_05;
            Info_09.Text = rm.GetString("String127");
        }
        private void TB_125_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_126_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_06;
            Info_09.Text = rm.GetString("String130");
        }
        private void TB_126_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Abmessung X)
        private void TB_127_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_07;
            Info_09.Text = rm.GetString("String215");
        }
        private void TB_127_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Abmessung Y)
        private void TB_128_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_08;
            Info_09.Text = rm.GetString("String216");
        }
        private void TB_128_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Nutbreite)
        private void TB_129_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_09;
            Info_09.Text = rm.GetString("String217");
        }
        private void TB_129_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // Hilfe anzeigen (Eckenradius)
        private void TB_130_GotFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_10;
            Info_09.Text = rm.GetString("String159");
        }
        private void TB_130_LostFocus(object sender, EventArgs e)
        {
            PictureBox9.Image = My.Resources.Resources.Bild09_01;
            Info_09.Text = Infostring;
        }
        // bei Änderung des Werkzeugdurchmesser - Radius anpassen
        private void TB_120_TextChanged(object sender, EventArgs e)
        {
            double R;
            R = Conversion.Val(TB_120.Text) / 2d;
            TB_130.Text = R.ToString("##0.0", System.Globalization.CultureInfo.CurrentCulture);
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_9_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_120.Text = "3,0";
            TB_121.Text = "8000";
            TB_122.Text = "3,0";
            TB_123.Text = "-3,0";
            TB_124.Text = "0,7";
            TB_125.Text = "200";
            TB_126.Text = "800";
            TB_127.Text = "50,0";
            TB_128.Text = "30,0";
            TB_129.Text = "5,0";
            TB_130.Text = "4,0";
            SelectNextControl(Abbrechen_9, true, true, true, true);
        }
        // G-Code für Dichtungsnut erzeugen
        private void Gcode_9_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_120.Text = TB_120.Text.Replace(".", ",");
                TB_121.Text = TB_121.Text.Replace(".", ",");
                TB_122.Text = TB_122.Text.Replace(".", ",");
                TB_123.Text = TB_123.Text.Replace(".", ",");
                TB_124.Text = TB_124.Text.Replace(".", ",");
                TB_125.Text = TB_125.Text.Replace(".", ",");
                TB_126.Text = TB_126.Text.Replace(".", ",");
                TB_127.Text = TB_127.Text.Replace(".", ",");
                TB_128.Text = TB_128.Text.Replace(".", ",");
                TB_129.Text = TB_129.Text.Replace(".", ",");
                TB_130.Text = TB_130.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String309") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_120.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_120.Text);                                              // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_121.Text);                                           // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_122.Text);                                     // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_123.Text);                                             // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_124.Text);                                           // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_125.Text);                                        // Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_126.Text);                                           // Vorschub fräsen
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_127.Text);                                     // Abmessung in X aussen
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_128.Text);                                     // Abmessung in Y aussen
                Zyklen.OFinX = Conversions.ToDouble(TB_127.Text) - Conversions.ToDouble(TB_129.Text) * 2d;  // Abmessung in X innen
                Zyklen.OFinY = Conversions.ToDouble(TB_128.Text) - Conversions.ToDouble(TB_129.Text) * 2d;  // Abmessung in Y innen
                Zyklen.Radius = Conversions.ToDouble(TB_130.Text);                                          // Eckenradius
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                                       // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Dichtungsnut();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Zyklen.PocketSizeX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / Zyklen.PocketSizeY);
                }
                // Startpunkt des Zyklus in X
                My.MyProject.Forms.Form2.NpX = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.OFinX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.OFinY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                My.MyProject.Forms.Form2.NpZ1 = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                // Temp = (Zyklen.Radius - (Me.TB_129.Text / 2)) * Skalierung
                Temp = (float)((Zyklen.Radius - 1d) * (double)Skalierung);
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.OFinX * (double)Skalierung);                // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.OFinY * (double)Skalierung);                // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);               // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Temp);                                             // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                            // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);         // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);         // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);              // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = (int)Round(Zyklen.Radius * (double)Skalierung);              // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                           // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Dichtungsnut";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus schräge Flächen fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_142_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_02;
            Info_10.Text = rm.GetString("String124");

        }
        private void TB_142_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_143_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_03;
            Info_10.Text = rm.GetString("String125");
        }
        private void TB_143_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_144_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_04;
            Info_10.Text = rm.GetString("String126");
        }
        private void TB_144_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_145_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_05;
            Info_10.Text = rm.GetString("String127");

        }
        private void TB_145_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_146_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_06;
            Info_10.Text = rm.GetString("String130");
        }
        private void TB_146_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Abmessung X)
        private void TB_147_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_07;
            Info_10.Text = rm.GetString("String132");
        }
        private void TB_147_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Abmessung Y)
        private void TB_148_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_08;
            Info_10.Text = rm.GetString("String133");
        }
        private void TB_148_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (Winkel)
        private void TB_149_GotFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_09;
            Info_10.Text = rm.GetString("String134");

        }
        private void TB_149_LostFocus(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // Hilfe anzeigen (ausgewählte Fräsfläche)
        private void GroupBox5_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void Label138_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void RectangleShape1_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void Kante_1_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void Kante_2_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void Kante_3_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void Kante_4_MouseMove(object sender, MouseEventArgs e)
        {
            Anzeigen();
        }
        private void Anzeigen()
        {
            if (Kante_1.Checked)
            {
                PictureBox10.Image = My.Resources.Resources.Bild10_10;
                Info_10.Text = rm.GetString("String135");
            }
            if (Kante_2.Checked)
            {
                PictureBox10.Image = My.Resources.Resources.Bild10_11;
                Info_10.Text = rm.GetString("String136");
            }
            if (Kante_3.Checked)
            {
                PictureBox10.Image = My.Resources.Resources.Bild10_12;
                Info_10.Text = rm.GetString("String135");
            }
            if (Kante_4.Checked)
            {
                PictureBox10.Image = My.Resources.Resources.Bild10_13;
                Info_10.Text = rm.GetString("String136");
            }
        }
        private void GroupBox5_MouseLeave(object sender, EventArgs e)
        {
            PictureBox10.Image = My.Resources.Resources.Bild10_01;
            Info_10.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_10_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_140.Text = "8,0";
            TB_141.Text = "4000";
            TB_142.Text = "3,0";
            TB_143.Text = "-3,0";
            TB_144.Text = "0,3";
            TB_145.Text = "200";
            TB_146.Text = "800";
            TB_147.Text = "100,0";
            TB_148.Text = "50,0";
            TB_149.Text = "45";
            Kante_4.Checked = false;
            Kante_3.Checked = false;
            Kante_2.Checked = false;
            Kante_1.Checked = true;
            SelectNextControl(Abbrechen_10, true, true, true, true);
        }
        // G-Code für schräge Fläche erzeugen
        private void Gcode_10_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            string Kante;
            float Skalierung;
            float Temp;
            int BFX;
            int BFY;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_140.Text = TB_140.Text.Replace(".", ",");
                TB_142.Text = TB_142.Text.Replace(".", ",");
                TB_143.Text = TB_143.Text.Replace(".", ",");
                TB_144.Text = TB_144.Text.Replace(".", ",");
                TB_145.Text = TB_145.Text.Replace(".", ",");
                TB_146.Text = TB_146.Text.Replace(".", ",");
                TB_147.Text = TB_147.Text.Replace(".", ",");
                TB_148.Text = TB_148.Text.Replace(".", ",");
                TB_149.Text = TB_149.Text.Replace(".", ",");
                // Dateikopf
                Kante = rm.GetString("String318");
                if (Kante_2.Checked == true)
                    Kante = rm.GetString("String319");
                if (Kante_4.Checked == true)
                    Kante = rm.GetString("String319");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String320") + Kante + " )" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_140.Text + " mm )" + Constants.vbCrLf);
                BFX = (int)Round(Conversion.Val(TB_147.Text) + Conversion.Val(TB_140.Text));
                BFY = (int)Round(Conversion.Val(TB_148.Text) + Conversion.Val(TB_140.Text));
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_140.Text);                          // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_141.Text);                       // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_142.Text);                 // Startposition Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_143.Text);                         // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_144.Text);                       // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_145.Text);                    // Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_146.Text);                       // Vorschub fräsen
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_147.Text);                 // Größe in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_148.Text);                 // Größe in Y
                Zyklen.Winkel = Conversions.ToInteger(TB_149.Text);                     // Winkel
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                // Zyklus aufrufen
                Fehler = Conversions.ToBoolean(0);
                Zyklen.Abzeilen();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 40;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / (double)BFX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (double)BFY);
                }
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);      // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);      // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                        // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                        // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                       // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                       // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Abzeilen";
                // Dateinamen vorbelegen und Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                {
                    My.MyProject.Forms.Form2.Dateiname = rm.GetString("String372");
                    My.MyProject.Forms.Form2.Show();
                }
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Nuten fräsen
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_202_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_02;
            Info_11.Text = rm.GetString("String124");
        }
        private void TB_202_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_203_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_03;
            Info_11.Text = rm.GetString("String183");
        }
        private void TB_203_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_204_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_04;
            Info_11.Text = rm.GetString("String126");
        }
        private void TB_204_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_205_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_05;
            Info_11.Text = rm.GetString("String127");
        }
        private void TB_205_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_206_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_08;
            Info_11.Text = rm.GetString("String130");
        }
        private void TB_206_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Taschengröße in X)
        private void TB_207_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_06;
            Info_11.Text = rm.GetString("String184");
        }
        private void TB_207_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Taschengröße in Y)
        private void TB_208_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_07;
            Info_11.Text = rm.GetString("String185");
        }
        private void TB_208_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Eckenradius)
        private void TB_209_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_09;
            Info_11.Text = rm.GetString("String159");
        }
        private void TB_209_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // Hilfe anzeigen (Drehwinkel)
        private void TB_210_GotFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_10;
            Info_11.Text = rm.GetString("String186");
        }
        private void TB_210_LostFocus(object sender, EventArgs e)
        {
            PictureBox11.Image = My.Resources.Resources.Bild11_01;
            Info_11.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_11_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_200.Text = "3,0";
            TB_201.Text = "4000";
            TB_202.Text = "3,0";
            TB_203.Text = "-4,0";
            TB_204.Text = "0,6";
            TB_205.Text = "200";
            TB_206.Text = "800";
            TB_207.Text = "60,0";
            TB_208.Text = "10,0";
            TB_209.Text = "3,0";
            TB_210.Text = "0,0";
            SelectNextControl(Abbrechen_11, true, true, true, true);
        }
        // bei Änderung des Werkzeugdurchmesser - Radius anpassen
        private void TB_200_TextChanged(object sender, EventArgs e)
        {
            double R;
            R = Conversion.Val(TB_200.Text) / 2d;
            TB_209.Text = R.ToString("##0.0", System.Globalization.CultureInfo.CurrentCulture);
        }
        // G-Code für Nut erzeugen
        private void Gcode_11_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            double Drehung;
            float Skalierung;
            float Temp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_200.Text = TB_200.Text.Replace(".", ",");
                TB_201.Text = TB_201.Text.Replace(".", ",");
                TB_202.Text = TB_202.Text.Replace(".", ",");
                TB_203.Text = TB_203.Text.Replace(".", ",");
                TB_204.Text = TB_204.Text.Replace(".", ",");
                TB_205.Text = TB_205.Text.Replace(".", ",");
                TB_206.Text = TB_206.Text.Replace(".", ",");
                TB_207.Text = TB_207.Text.Replace(".", ",");
                TB_208.Text = TB_208.Text.Replace(".", ",");
                TB_209.Text = TB_209.Text.Replace(".", ",");
                TB_210.Text = TB_210.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String321") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_200.Text + " mm )" + Constants.vbCrLf);
				// Drehwinkel
                Drehung = Conversions.ToDouble(TB_210.Text);
                if (Drehung < 90d & Drehung > -90 & Drehung != 0d & Drehung != 90d)
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + Drehung.ToString("##0.0") + "° )" + Constants.vbCrLf);
                }
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_200.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_201.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_202.Text);                  // Sicherheitsabstand Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_203.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_204.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_205.Text);                     // Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_206.Text);                        // Vorschub fräsen
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_207.Text);                  // Größe in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_208.Text);                  // Größe in Y
                Zyklen.Radius = Conversions.ToDouble(TB_209.Text);                       // Eckenradius
                Zyklen.Drehwinkel = Conversions.ToDouble(TB_210.Text);                   // Drehwinkel
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                    // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition bei schruppen: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Nut();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Zyklen.PocketSizeX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / Zyklen.PocketSizeY);
                }
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);      // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);      // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = (int)Round(Zyklen.Radius * (double)Skalierung);           // Radius
                My.MyProject.Forms.Form2.Wks[4] = (int)Round(Zyklen.Drehwinkel);                            // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                       // Radius
                My.MyProject.Forms.Form2.Wks1[4] = (int)Round(Zyklen.Drehwinkel);                           // Drehwinkel
                My.MyProject.Forms.Form2.Befehl = "Nut";
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Bohrbild auf Linien
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_152_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_02;
            Info_12.Text = rm.GetString("String124");
        }
        private void TB_152_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_153_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_03;
            Info_12.Text = rm.GetString("String138");
        }
        private void TB_153_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_154_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_04;
            Info_12.Text = rm.GetString("String126");
        }
        private void TB_154_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_155_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_05;
            Info_12.Text = rm.GetString("String139");
        }
        private void TB_155_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Bohrtiefe Spanbruch)
        private void TB_156_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_06;
            Info_12.Text = rm.GetString("String140");
        }
        private void TB_156_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Ausspantiefe)
        private void TB_157_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_07;
            Info_12.Text = rm.GetString("String142");
        }
        private void TB_157_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Startpunkt in X)
        private void TB_158_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_08;
            Info_12.Text = rm.GetString("String197");
        }
        private void TB_158_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Startpunkt in Y)
        private void TB_159_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_08;
            Info_12.Text = rm.GetString("String198");
        }
        private void TB_159_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Abstand in X)
        private void TB_160_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_09;
            Info_12.Text = rm.GetString("String199");
        }
        private void TB_160_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Abstand in Y)
        private void TB_161_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_10;
            Info_12.Text = rm.GetString("String200");
        }
        private void TB_161_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Anzahl der Spalten)
        private void TB_162_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_11;
            Info_12.Text = rm.GetString("String201");
        }
        private void TB_162_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Anzahl der Zeilen)
        private void TB_163_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_11;
            Info_12.Text = rm.GetString("String202");
        }
        private void TB_163_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // Hilfe anzeigen (Drehwinkel)
        private void TB_164_GotFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_12;
            Info_12.Text = rm.GetString("String203");
        }
        private void TB_164_LostFocus(object sender, EventArgs e)
        {
            PictureBox12.Image = My.Resources.Resources.Bild12_01;
            Info_12.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_12_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_150.Text = "3,0";
            TB_151.Text = "2000";
            TB_152.Text = "3,0";
            TB_153.Text = "-10,0";
            TB_154.Text = "0,8";
            TB_155.Text = "40";
            TB_156.Text = "2,0";
            TB_157.Text = "-5,0";
            TB_158.Text = "10,0";
            TB_159.Text = "10,0";
            TB_160.Text = "10,0";
            TB_161.Text = "10,0";
            TB_162.Text = "5";
            TB_163.Text = "3";
            TB_164.Text = "0,0";
            SelectNextControl(Abbrechen_12, true, true, true, true);
        }
        // G-Code für Bohrbild auf Linien erzeugen
        private void Gcode_12_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            int N;
            int M;
            int Table_Pos;
            double WertX;
            double WertY;
            double AbstX;
            double AbstY;
            float Skalierung;
            var Xtemp = default(float);
            var Ytemp = default(float);
            float Winkel;
            float Temp_X1;
            float Temp_X2;
            float Temp_Y1;
            float Temp_Y2;
            int loopTo;
            int loopTo1;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_150.Text = TB_150.Text.Replace(".", ",");
                TB_151.Text = TB_151.Text.Replace(".", ",");
                TB_152.Text = TB_152.Text.Replace(".", ",");
                TB_153.Text = TB_153.Text.Replace(".", ",");
                TB_154.Text = TB_154.Text.Replace(".", ",");
                TB_155.Text = TB_155.Text.Replace(".", ",");
                TB_156.Text = TB_156.Text.Replace(".", ",");
                TB_157.Text = TB_157.Text.Replace(".", ",");
                TB_158.Text = TB_158.Text.Replace(".", ",");
                TB_159.Text = TB_159.Text.Replace(".", ",");
                TB_160.Text = TB_160.Text.Replace(".", ",");
                TB_161.Text = TB_161.Text.Replace(".", ",");
                TB_162.Text = TB_162.Text.Replace(".", ",");
                TB_163.Text = TB_163.Text.Replace(".", ",");
                TB_164.Text = TB_164.Text.Replace(".", ",");
                // Dateikopf
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String336") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String288") + TB_150.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                // Bohrpositionen
                Table_Pos = 0;
                Winkel = Conversions.ToSingle(TB_164.Text);
                AbstX = Conversions.ToDouble(TB_160.Text);
                AbstY = Conversions.ToDouble(TB_161.Text);
                loopTo = (int)Round(Conversion.Val(TB_163.Text) - 1d);
                loopTo1 = (int)Round(Conversion.Val(TB_162.Text) - 1d);
                Temp_X1 = 0f;
                Temp_X2 = 0f;
                Temp_Y1 = 0f;
                Temp_Y2 = 0f;
                if (Winkel == 0f)  // Drehwinkel = 0
                {
                    for (N = 0; N <= loopTo; N++)       // Zeile
                    {
                        for (M = 0; M <= loopTo1; M++)  // Spalte   
                        {
                            WertX = (M + 1) * AbstX;
                            WertY = (N + 1) * AbstY;
                            Zyklen.BP[Table_Pos, 0] = WertX;
                            Zyklen.BP[Table_Pos, 1] = WertY;
                            Zyklen.BP[Table_Pos, 2] = 10000d;
                            Xtemp = (float)WertX;
                            Ytemp = (float)WertY;
                            Table_Pos++;
                        }
                    }
                    Zyklen.BP[Table_Pos, 0] = 10000d;
                }
                else    // Drehwinkel <> 0                                            
                {
                    for (N = 0; N <= loopTo; N++)       // Zeile
                    {
                        for (M = 0; M <= loopTo1; M++)  // Spalte   
                        {
                            WertX = Zyklen.R_EndPos_X((M + 1) * AbstX, (N + 1) * AbstY, (double)Winkel, 4);
                            WertY = Zyklen.R_EndPos_Y((M + 1) * AbstX, (N + 1) * AbstY, (double)Winkel, 4);
                            Zyklen.BP[Table_Pos, 0] = WertX;
                            Zyklen.BP[Table_Pos, 1] = WertY;
                            Zyklen.BP[Table_Pos, 2] = 10000d;
                            if (WertX > (double)Temp_X1)
                                Temp_X1 = (float)WertX;
                            if (WertX < (double)Temp_X2)
                                Temp_X2 = (float)WertX;
                            if (WertY > (double)Temp_Y1)
                                Temp_Y1 = (float)WertY;
                            if (WertY < (double)Temp_Y2)
                                Temp_Y2 = (float)WertY;
                            Xtemp = -Temp_X2 + Temp_X1;
                            Ytemp = -Temp_Y2 + Temp_Y1;
                            Table_Pos++;
                        }
                    }
                }
                Xtemp = (float)((double)Xtemp + Zyklen.BP[0, 0]);
                Ytemp = (float)((double)Ytemp + Zyklen.BP[0, 1]);
                Zyklen.DT = Conversions.ToDouble(TB_150.Text);                           // Bohrerdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_151.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_152.Text);                  // Sicherheits-Abstand
                Zyklen.DepthZ = -Convert.ToDouble(TB_153.Text);                          // Bohrtiefe
                Zyklen.StepZ = Conversions.ToDouble(TB_154.Text);                        // Zustelltiefe
                Zyklen.FFinish = Conversions.ToInteger(TB_155.Text);                     // Vorschub Zustellung
                Zyklen.FCut = Conversions.ToInteger(TB_155.Text);                        // 
                Zyklen.DepthBs = Conversions.ToDouble(TB_156.Text);                      // Bohrtiefe Spanbruch
                Zyklen.DepthRs = 0.2d;                                                   // Rückzug bis Spanbruch
                Zyklen.DepthAst = Conversions.ToDouble(TB_157.Text);                     // Ausspantiefe
                Zyklen.Drehwinkel = Conversions.ToDouble(TB_164.Text);                   // Drehwinkel
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                    // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Bohrzyklus();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                if (Xtemp >= Ytemp)
                {
                    Skalierung = (My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Xtemp;
                }
                else
                {
                    Skalierung = (My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / Ytemp;
                }
                // Startpunkt des Zyklus in X
                My.MyProject.Forms.Form2.NpX = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - (double)(Xtemp * Skalierung / 2f));
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (double)(Ytemp * Skalierung / 2f));
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Xtemp * Skalierung);                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Ytemp * Skalierung);                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                    // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                    // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = (int)Round(Temp_X2 * Skalierung);                    // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = (int)Round(Temp_Y2 * Skalierung);                    // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                   // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                   // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                   // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Bohrmatrix";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                {
                    My.MyProject.Forms.Form2.Dateiname = rm.GetString("String403");
                }
                return;
        }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Runde Nut
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_172_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_02;
            Info_13.Text = rm.GetString("String124");
        }
        private void TB_172_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_173_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_03;
            Info_13.Text = rm.GetString("String183");
        }
        private void TB_173_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_174_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_05;
            Info_13.Text = rm.GetString("String126");
        }
        private void TB_174_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_175_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_12;
            Info_13.Text = rm.GetString("String127");
        }
        private void TB_175_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_176_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_04;
            Info_13.Text = rm.GetString("String130");
        }
        private void TB_176_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Mitte in X)
        private void TB_177_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_06;
            Info_13.Text = rm.GetString("String208");
        }
        private void TB_177_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Mitte in Y)
        private void TB_178_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_07;
            Info_13.Text = rm.GetString("String209");
        }
        private void TB_178_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Teilkreisdurchmesser)
        private void TB_179_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_08;
            Info_13.Text = rm.GetString("String210");
        }
        private void TB_179_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Seitenlänge)
        private void TB_180_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_09;
            Info_13.Text = rm.GetString("String211");
        }
        private void TB_180_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Startwinkel)
        private void TB_181_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_10;
            Info_13.Text = rm.GetString("String212");
        }
        private void TB_181_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // Hilfe anzeigen (Öffnungswinkel)
        private void TB_182_GotFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_11;
            Info_13.Text = rm.GetString("String213");
        }
        private void TB_182_LostFocus(object sender, EventArgs e)
        {
            PictureBox13.Image = My.Resources.Resources.Bild13_01;
            Info_13.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_13_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_170.Text = "3,0";
            TB_171.Text = "4000";
            TB_172.Text = "3,0";
            TB_173.Text = "-0,6";
            TB_174.Text = "0,6";
            TB_175.Text = "200";
            TB_176.Text = "800";
            TB_177.Text = "0,0";
            TB_178.Text = "0,0";
            TB_179.Text = "40,0";
            TB_180.Text = "5,0";
            TB_181.Text = "60,0";
            TB_182.Text = "60,0";
            SelectNextControl(Abbrechen_13, true, true, true, true);
        }
        // G-Code für runde Nut erzeugen
        private void Gcode_13_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            double Dimension_X;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_170.Text = TB_170.Text.Replace(".", ",");
                TB_171.Text = TB_171.Text.Replace(".", ",");
                TB_172.Text = TB_172.Text.Replace(".", ",");
                TB_173.Text = TB_173.Text.Replace(".", ",");
                TB_174.Text = TB_174.Text.Replace(".", ",");
                TB_175.Text = TB_175.Text.Replace(".", ",");
                TB_176.Text = TB_176.Text.Replace(".", ",");
                TB_177.Text = TB_177.Text.Replace(".", ",");
                TB_178.Text = TB_178.Text.Replace(".", ",");
                TB_179.Text = TB_179.Text.Replace(".", ",");
                TB_180.Text = TB_180.Text.Replace(".", ",");
                TB_181.Text = TB_181.Text.Replace(".", ",");
                TB_182.Text = TB_182.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String335") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_170.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_170.Text);                           // Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_171.Text);                        // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_172.Text);                  // Sicherheitsabstand Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_173.Text);                          // finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_174.Text);                        // Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_175.Text);                     // Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_176.Text);                        // Vorschub fräsen
                Zyklen.AbstX = Conversions.ToDouble(TB_177.Text);                        // Mitte in X
                Zyklen.AbstY = Conversions.ToDouble(TB_178.Text);                        // Mitte in Y
                Zyklen.TKD = Conversions.ToDouble(TB_179.Text);                          // Teilkreisdurchmesser
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_180.Text);                  // Seitenlänge
                Zyklen.SW = Conversions.ToDouble(TB_181.Text);                           // Startwinkel
                Zyklen.OW = Conversions.ToDouble(TB_182.Text);                           // Öffnungswinkel
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                    // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Rundnut();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                Dimension_X = Zyklen.AbstX * 2d;
                if (Dimension_X >= Zyklen.AbstY + Zyklen.TKD / 2d + Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Dimension_X);
                }
                else if (Zyklen.AbstY == 0d)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (Zyklen.AbstY + Zyklen.TKD + Zyklen.PocketSizeY));
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / (Zyklen.AbstY + Zyklen.TKD / 2d + Zyklen.PocketSizeY));
                }
                if (Dimension_X == 0d)
                {
                    Dimension_X = Zyklen.TKD;
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d);
                }
                else
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Dimension_X / 2d * (double)Skalierung);
                }
                // Startpunkt des Zyklus in X
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.AbstY + Zyklen.TKD / 2d + Zyklen.PocketSizeY) / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Dimension_X * (double)Skalierung);                                             // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round((Zyklen.AbstY + Zyklen.TKD / 2d + Zyklen.PocketSizeY) * (double)Skalierung);   // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);                                           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                                                        // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                                                        // Drehwinkel
                My.MyProject.Forms.Form2.Wks2[0] = (int)Round(Zyklen.TKD / 2d);                                                             // Teilkreisradius
                My.MyProject.Forms.Form2.Wks2[1] = (int)Round(Zyklen.PocketSizeY);                                                          // Seitenlänge
                My.MyProject.Forms.Form2.Wks2[2] = (int)Round(Zyklen.SW);                                                                   // Startwinkel
                My.MyProject.Forms.Form2.Wks2[3] = (int)Round(Zyklen.OW);                                                                   // Öffnungswinkel
                My.MyProject.Forms.Form2.Wks2[4] = 0;                                                                                       // Drehwinkel
                My.MyProject.Forms.Form2.Befehl = "runde Nut";
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Buchstaben und Schriftzüge
        // Hilfe anzeigen (Fräserdurchmesser)
        private void TB_220_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = rm.GetString("String221");
        }
        private void TB_220_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Spindeldrehzahl)
        private void TB_221_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = rm.GetString("String222");
        }
        private void TB_221_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_222_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_02;
            Info_14.Text = rm.GetString("String124");
        }
        private void TB_222_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_223_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_03;
            Info_14.Text = rm.GetString("String223");
        }
        private void TB_223_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_224_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_03;
            Info_14.Text = rm.GetString("String224");
        }
        private void TB_224_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_225_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_04;
            Info_14.Text = rm.GetString("String127");
        }
        private void TB_225_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_226_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_05;
            Info_14.Text = rm.GetString("String130");
        }
        private void TB_226_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Texteingabe)
        private void TB_227_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = rm.GetString("String225");
        }
        private void TB_227_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Schrifthöhe)
        private void TB_228_GotFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_06;
            Info_14.Text = rm.GetString("String226");
        }
        private void TB_228_LostFocus(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // Hilfe anzeigen (Schriftart)
        private void Font_Art_Enter(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = rm.GetString("String227");
        }
        // Schrifart wurde gewählt
        private void Font_Art_SelectedIndexChanged(object sender, EventArgs e)
        {
            gList.Clear();
            Schriftarten.Schriftsatz = Font_Art.SelectedIndex + 1;
            switch (Font_Art.SelectedIndex + 1)
            {
                case 1:
                    Fontweidht = 12;
                    break;
                case 2:
                    Fontweidht = 13;
                    break;
            }
            Schriftarten.Font_lesen();
        }
        private void Font_Art_Leave(object sender, EventArgs e)
        {
            PictureBox14.Image = My.Resources.Resources.Bild14_01;
            Info_14.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_14_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_220.Text = "1,0";
            TB_221.Text = "10000";
            TB_222.Text = "3,0";
            TB_223.Text = "-0,5";
            TB_224.Text = "0,5";
            TB_225.Text = "200";
            TB_226.Text = "800";
            TB_227.Text = "Beispiel";
            TB_228.Text = "18";
            Font_Art.SelectedIndex = 0;
            TB_220.SelectAll();
            SelectNextControl(Abbrechen_14, true, true, true, true);
        }
        // G-Code für Schrift erzeugen
        private void Gcode_14_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            float theFlyheight;
            float theCutheight;
            float theFontheight;
            float theSpace;
            string Flyheight;
            string Cutheight;
            float Fontheight = 0;
            float Fontheight1 = 0;
            float Fontwidht = 0;
            float Scalefactor;
            float Xofs = 0;
            int zspeed;
            int fspeed;
            bool step = false;
            bool step1 = false;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_220.Text = TB_220.Text.Replace(".", ",");
                TB_221.Text = TB_221.Text.Replace(".", ",");
                TB_222.Text = TB_222.Text.Replace(".", ",");
                TB_223.Text = TB_223.Text.Replace(".", ",");
                TB_224.Text = TB_224.Text.Replace(".", ",");
                TB_225.Text = TB_225.Text.Replace(".", ",");
                TB_226.Text = TB_226.Text.Replace(".", ",");
                TB_228.Text = TB_228.Text.Replace(".", ",");
                TB_229.Text = TB_229.Text.Replace(".", ",");
                Fehler = false;
                //Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_220.Text);                      //Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_221.Text);                   //Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_222.Text);             //Sicherheitsabstand Z über Werkstück
                Zyklen.DepthZ = -Convert.ToDouble(TB_223.Text);                     //finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_224.Text);                   //Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_225.Text);                //Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_226.Text);                   //Vorschub fräsen
                Zyklen.BoT = Conversions.ToInteger(TB_228.Text);                    //Schrifthöhe
                //Zyklus aufrufen
                Zyklen.Schrift();
                if (Fehler == true )
                {
                    return;
                }
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String337") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_220.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklus vorbereiten Werkzeug wählen
                My.MyProject.Forms.Form2.Ausgabe.AppendText("T2 M6" + Constants.vbCrLf);
                // Vorpositionieren und Spindel EIN
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Conversions.ToDouble(SH1.Text).ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Conversions.ToInteger(TB_221.Text).ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                // Code generieren
                theFlyheight = Conversions.ToSingle(TB_222.Text);       // Sicherheitshöhe
                theCutheight = Convert.ToSingle(TB_223.Text);           // Frästiefe
                theFontheight = Convert.ToSingle(TB_228.Text);          // Schriftgröße
                theSpace = Fontweidht + Convert.ToInt16(TB_229.Text);   // Abstand zwischen den Zeichen
                zspeed = Convert.ToInt16(TB_225.Text);                  // Vorschub Z
                fspeed = Convert.ToInt16(TB_226.Text);                  // Vorschub Fräsen
                if (theFlyheight > 0.5)
                {
                    Flyheight = "Z" + theFlyheight.ToString("0.000");
                }
                else
                {
                    Flyheight = "Z5.0";
                }
                Flyheight = Flyheight.Replace(",", ".");
                Cutheight = "Z" + theCutheight.ToString("0.000");
                Cutheight = Cutheight.Replace(",", ".");
                if (theFontheight < 0.5)
                {
                    theFontheight = 0.5f;
                }
                Scalefactor = theFontheight / 18.0f;
                if (TB_227.Text != "")
                {
                    string thestring = TB_227.Text;
                    int stringlaenge = thestring.Length;
                    foreach (char thechar in thestring)
                    {
                        string tempstring = gList[1][3];
                        foreach (List<string> gcodestringlist in gList)
                        {
                            string atempstring = gcodestringlist[0];
                            if (gcodestringlist[0][0] == thechar)
                            {
                                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String338") + thechar + " )" + Constants.vbCrLf);
                                int numberofitems = gcodestringlist.Count();
                                for (int i = 1; i < (numberofitems); i++)
                                {
                                    string ls = gcodestringlist[i];
                                    if (ls.Trim() == "ZF")
                                    {
                                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 " + Flyheight + Constants.vbCrLf);
                                        step = true;
                                    }
                                    else if (ls.Trim() == "ZC")
                                    {
                                        My.MyProject.Forms.Form2.Ausgabe.AppendText("G1 F" + zspeed + " " + Cutheight + Constants.vbCrLf);
                                        step1 = true;
                                    }
                                    else
                                    {
                                        Getcoords(ls);
                                        float Xnew = GX + Xofs;
                                        float Ynew = GY;
                                        Xnew *= Scalefactor;
                                        Ynew *= Scalefactor;
                                        string Xnewcoord = Xnew.ToString("0.000");
                                        string Ynewcoord = Ynew.ToString("0.000");
                                        string newstring = "";
                                        if (step == true)
                                        {
                                            newstring = "G0 X" + Xnewcoord + " Y" + Ynewcoord;
                                            newstring = newstring.Replace(",", ".");
                                            step = false;
                                        }
                                        else if (step1 == true)
                                        {
                                            newstring = "G1 F" + fspeed + " X" + Xnewcoord + " Y" + Ynewcoord;
                                            newstring = newstring.Replace(",", ".");
                                            step1 = false;
                                        }
                                        else
                                        {
                                            newstring = "G1 X" + Xnewcoord + " Y" + Ynewcoord;
                                            newstring = newstring.Replace(",", ".");
                                        }
                                        My.MyProject.Forms.Form2.Ausgabe.AppendText(newstring + Constants.vbCrLf);
                                        if (Xnew > Fontwidht)
                                        {
                                            Fontwidht = Xnew;
                                        }
                                        if (Ynew > Fontheight)
                                        {
                                            Fontheight = Ynew;
                                        }
                                        if (Ynew < 0)
                                        {
                                            Fontheight1 = -Ynew;
                                        }
                                    }
                                }
                                Xofs += theSpace;
                            }
                        }
                    }
                }
                //Zyklusende ------------------------------------------------------------------------
                //Programmende
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String254") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M5" + Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("M30");
                }
                My.MyProject.Forms.Form2.Dateiname =rm.GetString("String375");
                Fontheight += Fontheight1;
                // Skalierung
                Rand = 60;
                if (Fontwidht >= Fontheight)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Fontwidht);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / Fontheight);
                }
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d);
                Temp -= (float)(Fontwidht / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d);
                Temp -= (float)(Fontheight / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d) - theFlyheight);
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Fontwidht * (double)Skalierung);               // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Fontheight * (double)Skalierung);              // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(theFlyheight * (double)Skalierung);            // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                        // Radius
                My.MyProject.Forms.Form2.Wks[4] = (int)Round(Fontheight1 * (double)Skalierung);             // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                       // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                       // Drehwinkel
                My.MyProject.Forms.Form2.Befehl = "Schrift";
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0) { 
                }
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Gewinde fräsen
        // Gewinde wird ausgewählt
        private void TB_250_SelectedIndexChanged(object sender, EventArgs e)
        {
            int GS;
            GS = TB_251.SelectedIndex;
            switch (GS)
            {
                case 0:
                    {
                        TB_254.Text = "6000";
                        TB_255.Text = "170";
                        break;
                    }
                case 1:
                    {
                        TB_254.Text = "5600";
                        TB_255.Text = "280";
                        break;
                    }
                case 2:
                    {
                        TB_254.Text = "2900";
                        TB_255.Text = "160";
                        break;
                    }
                case 3:
                    {
                        TB_254.Text = "2400";
                        TB_255.Text = "170";
                        break;
                    }
                case 4:
                    {
                        TB_254.Text = "1500";
                        TB_255.Text = "120";
                        break;
                    }
            }
        }
        // Innengewinde ausgewählt
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Infostring = rm.GetString("String147");
            Info_15.Text = Infostring;
        }
        private void RadioButton1_GotFocus(object sender, EventArgs e)
        {
            PF_Picture = My.Resources.Resources.Bild15_01;
            PictureBox15.Image = PF_Picture;
            TB_253.Enabled = true;
        }
        private void RadioButton1_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
        }
        // Außengewinde ausgewählt
        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Infostring = rm.GetString("String148");
            Info_15.Text = Infostring;
        }
        private void RadioButton2_GotFocus(object sender, EventArgs e)
        {
            PF_Picture = My.Resources.Resources.Bild15_01_1;
            PictureBox15.Image = PF_Picture;
            TB_253.Enabled = false;
        }
        private void RadioButton2_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
        }
        // Hilfe anzeigen (Gewindedurchmesser)
        private void TB_250_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_02;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_02_1;
            }
            Info_15.Text = rm.GetString("String149");
        }
        private void TB_250_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Gewindesteigung)
        private void TB_251_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_03;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_03_1;
            }
            Info_15.Text = rm.GetString("String150");

        }
        private void TB_251_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_252_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_04;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_04_1;
            }
            Info_15.Text = rm.GetString("String151");
        }
        private void TB_252_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Versatz)
        private void TB_253_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_08;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_01;
            }
            Info_15.Text = rm.GetString("String152");
        }
        private void TB_253_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Drehzahl)
        private void TB_254_GotFocus(object sender, EventArgs e)
        {
        }
        private void TB_254_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_255_GotFocus(object sender, EventArgs e)
        {
        }
        private void TB_255_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Abstand in X)
        private void TB_256_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_06;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_06_1;
            }
            Info_15.Text = rm.GetString("String153");
        }
        private void TB_256_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Abstand in Y)
        private void TB_257_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_07;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_07_1;
            }
            Info_15.Text = rm.GetString("String154");
        }
        private void TB_257_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Werstücklänge in X)
        private void TB_258_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_10;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_10_1;
            }
            Info_15.Text = rm.GetString("String145");
        }
        private void TB_258_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // Hilfe anzeigen (Werstückbreite in Y)
        private void TB_259_GotFocus(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_11;
            }
            else
            {
                PictureBox15.Image = My.Resources.Resources.Bild15_11_1;
            }
            Info_15.Text = rm.GetString("String146");
        }
        private void TB_259_LostFocus(object sender, EventArgs e)
        {
            PictureBox15.Image = PF_Picture;
            Info_15.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_15_Click(object sender, EventArgs e)
        {
            StatusText = "";
            RadioButton2.Checked = false;
            RadioButton1.Checked = true;
            TB_250.Text = "3.0";
            TB_251.SelectedIndex = 0;
            TB_252.Text = "6.0";
            TB_253.Text = "0.0";
            TB_254.Text = "6000";
            TB_255.Text = "170";
            TB_256.Text = "25.0";
            TB_257.Text = "10.0";
            TB_258.Text = "50.0";
            TB_259.Text = "20.0";
            RadioButton1.Focus();
            Infostring = rm.GetString("String147");
            Info_15.Text = Infostring;
            SelectNextControl(Abbrechen_15, true, true, true, true);
        }
        // G-Code für Gewinde fräsen erzeugen
        private void Gcode_15_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            var Temp = default(float);
            double TTemp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_250.Text = TB_250.Text.Replace(".", ",");
                TB_251.Text = TB_251.Text.Replace(".", ",");
                TB_252.Text = TB_252.Text.Replace(".", ",");
                TB_253.Text = TB_253.Text.Replace(".", ",");
                TB_254.Text = TB_254.Text.Replace(".", ",");
                TB_255.Text = TB_255.Text.Replace(".", ",");
                TB_256.Text = TB_256.Text.Replace(".", ",");
                TB_257.Text = TB_257.Text.Replace(".", ",");
                TB_258.Text = TB_258.Text.Replace(".", ",");
                TB_259.Text = TB_259.Text.Replace(".", ",");
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String339") + TB_250.Text + rm.GetString("String340") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_251.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter übergeben
                int GD;
                Zyklen.RemoveOFin = false;
                if (RadioButton1.Checked == true)
                {
                    Zyklen.RemoveOFin = true;
                }
                if (RadioButton2.Checked == true)
                {
                    Zyklen.RemoveOFin = false;
                }
                GD = TB_251.SelectedIndex;
                switch (GD)
                {
                    case 0: // M3
                        {
                            Zyklen.DT = 2.4d;                               // Werkzeugdurchmesser
                            Zyklen.TKD = Conversions.ToDouble(TB_250.Text); // Gewindedurchmesser
                            Zyklen.StepXY = 0.6d;                           // Auslenkung beim Senken
                            Zyklen.StartPosZ = 0.22d;                       // Senktiefe
                            break;
                        }
                    case 1: // M4
                        {
                            Zyklen.DT = 3.1d;                               // Werkzeugdurchmesser
                            Zyklen.TKD = Conversions.ToDouble(TB_250.Text); // Gewindedurchmesser
                            Zyklen.StepXY = 0.8d;                           // Auslenkung beim Senken
                            Zyklen.StartPosZ = 0.22d;                       // Senktiefe
                            break;
                        }
                    case 2: // M5
                        {
                            Zyklen.DT = 3.8d;                               // Werkzeugdurchmesser
                            Zyklen.TKD = Conversions.ToDouble(TB_250.Text); // Gewindedurchmesser
                            Zyklen.StepXY = 1.0d;                           // Auslenkung beim Senken
                            Zyklen.StartPosZ = 0.22d;                       // Senktiefe
                            break;
                        }
                    case 3: // M6
                        {
                            Zyklen.DT = 4.6d;                               // Werkzeugdurchmesser
                            Zyklen.TKD = Conversions.ToDouble(TB_250.Text); // Gewindedurchmesser
                            Zyklen.StepXY = 1.0d;                           // Auslenkung beim Senken
                            Zyklen.StartPosZ = 0.25d;                       // Senktiefe
                            break;
                        }
                    case 4: // M8
                        {
                            Zyklen.DT = 6.2d;                               // Werkzeugdurchmesser
                            Zyklen.TKD = Conversions.ToDouble(TB_250.Text); // Gewindedurchmesser
                            Zyklen.StepXY = 1.2d;                           // Auslenkung beim Senken
                            Zyklen.StartPosZ = 0.35d;                       // Senktiefe
                            break;
                        }
                }
                Zyklen.StepZ = Conversions.ToDouble(TB_251.SelectedItem);               // Gewindesteigung
                if (Conversion.Val(TB_252.Text) > 0d)
                {
                    TTemp = Conversion.Val(TB_252.Text) + 0.5d + Zyklen.StartPosZ;
                }
                else
                {
                    TTemp = -Conversion.Val(TB_252.Text) + 0.5d + Zyklen.StartPosZ;
                }
                Zyklen.StartHeight = 3.0d;                                              // Sicherheitsabstand Z über Werkstück
                Zyklen.DepthZ = TTemp;                                                  // finale Tiefe in Z (eingegebene Tiefe + 0,5 + Senktiefe)
                Zyklen.StepZFin = Conversions.ToDouble(TB_253.Text);                    // Versatz in -Z
                Zyklen.Spin = Conversions.ToInteger(TB_254.Text);                       // Spindeldrehzahl
                Zyklen.FCut = Conversions.ToInteger(TB_255.Text);                       // Vorschub fräsen
                Zyklen.AbstX = Conversions.ToDouble(TB_256.Text);                       // Abstand in X
                Zyklen.AbstY = Conversions.ToDouble(TB_257.Text);                       // Abstand in Y
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_258.Text);                 // Werkstücklänge in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_259.Text);                 // Werkstückbreite in Y
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                // Startposition: X = 0, Y = 0
                Zyklen.StartPosX = 0d;
                Zyklen.StartPosY = 0d;
                Fehler = Conversions.ToBoolean(0);
                // Zyklus aufrufen
                Zyklen.Gewinde_fr();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                if (Zyklen.PocketSizeX >= Zyklen.PocketSizeY)
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Zyklen.PocketSizeX);
                }
                else
                {
                    Skalierung = (float)((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand) / Zyklen.PocketSizeY);
                }
                // Startpunkt des Zyklus in X
                if (Zyklen.AbstX > 0d)                             // Antastpunkt links
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX * (double)Skalierung / 2d + Zyklen.AbstX * (double)Skalierung);
                }
                if (Zyklen.AbstX == 0d)                             // Antastpunkt mitte
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d);
                }
                if (Zyklen.AbstX < 0d)                             // Antastpunkt rechts
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d + Zyklen.PocketSizeX * (double)Skalierung / 2d - Zyklen.AbstX * (double)Skalierung);
                }
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                if (Zyklen.AbstY > 0d)                             // Antastpunkt unten
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d + Zyklen.PocketSizeY * (double)Skalierung / 2d - Zyklen.AbstY * (double)Skalierung);
                }
                if (Zyklen.AbstY == 0d)                             // Antastpunkt mitte
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d);
                }
                if (Zyklen.AbstY < 0d)                             // Antastpunkt oben
                {
                    Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.PocketSizeY * (double)Skalierung / 2d + Zyklen.AbstY * (double)Skalierung);
                }
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - Zyklen.DepthZ * (double)Skalierung);
                My.MyProject.Forms.Form2.NpZ = (int)Round(Temp);
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);      // Werkstücklänge in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);      // Werkstückbreite in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                        // 
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                        // 
                if (RadioButton1.Checked == true)
                {
                    My.MyProject.Forms.Form2.Wks2[0] = 0;                                // Zapfendurchmesser
                    My.MyProject.Forms.Form2.Wks2[1] = 0;                                // Zapfenhöhe
                }
                else
                {
                    My.MyProject.Forms.Form2.Wks2[0] = (int)Round(Zyklen.TKD * (double)Skalierung);          // Zapfendurchmesser
                    My.MyProject.Forms.Form2.Wks2[1] = (int)Round((TTemp + 1d) * (double)Skalierung);
                }         // Zapfenhöhe
                My.MyProject.Forms.Form2.Wks2[2] = 0;                                                       // 
                My.MyProject.Forms.Form2.Wks2[3] = 0;                                                       // 
                My.MyProject.Forms.Form2.Wks2[4] = 0;                                                       // 
                My.MyProject.Forms.Form2.Befehl = "Gewindefr";
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                    My.MyProject.Forms.Form2.Show();
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus Tiefloch bohren
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_262_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_02;
            Info_16.Text = rm.GetString("String124");
        }
        private void TB_262_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_263_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_03;
            Info_16.Text = rm.GetString("String138");
        }
        private void TB_263_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_08.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_264_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_04;
            Info_16.Text = rm.GetString("String126");
        }
        private void TB_264_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_265_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_05;
            Info_16.Text = rm.GetString("String139");
        }
        private void TB_265_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Bohrtiefe Spanbruch)
        private void TB_266_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_06;
            Info_16.Text = rm.GetString("String140");
        }
        private void TB_266_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Rückzug bis Spanbruch)
        private void TB_267_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_07;
            Info_16.Text = rm.GetString("String141");
        }
        private void TB_267_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Ausspantiefe)
        private void TB_268_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_08;
            Info_16.Text = rm.GetString("String142");
        }
        private void TB_268_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Abstand in X)
        private void TB_269_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_09;
            Info_16.Text = rm.GetString("String143");
        }
        private void TB_269_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Abstand in Y)
        private void TB_270_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_10;
            Info_16.Text = rm.GetString("String144");
        }
        private void TB_270_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Werstücklänge in X)
        private void TB_271_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_11;
            Info_16.Text = rm.GetString("String145");
        }
        private void TB_271_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // Hilfe anzeigen (Werstückbreite in Y)
        private void TB_272_GotFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_12;
            Info_16.Text = rm.GetString("String146");
        }
        private void TB_272_LostFocus(object sender, EventArgs e)
        {
            PictureBox16.Image = My.Resources.Resources.Bild16_01;
            Info_16.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_16_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_260.Text = "3.0";
            TB_261.Text = "1400";
            TB_262.Text = "3.0";
            TB_263.Text = "-10.0";
            TB_264.Text = "0.8";
            TB_265.Text = "40";
            TB_266.Text = "2.0";
            TB_267.Text = "0.2";
            TB_268.Text = "-5.0";
            TB_269.Text = "25.0";
            TB_270.Text = "10.0";
            TB_271.Text = "50.0";
            TB_272.Text = "20.0";
            Infostring = rm.GetString("String137");
            Info_16.Text = Infostring;
            SelectNextControl(Abbrechen_16, true, true, true, true);
        }
        // G-Code für Tiefloch bohren erzeugen
        private void Gcode_16_Click(object sender, EventArgs e)
        {
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            float Temp;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_260.Text = TB_260.Text.Replace(".", ",");
                TB_261.Text = TB_261.Text.Replace(".", ",");
                TB_262.Text = TB_262.Text.Replace(".", ",");
                TB_263.Text = TB_263.Text.Replace(".", ",");
                TB_264.Text = TB_264.Text.Replace(".", ",");
                TB_265.Text = TB_265.Text.Replace(".", ",");
                TB_266.Text = TB_266.Text.Replace(".", ",");
                TB_267.Text = TB_267.Text.Replace(".", ",");
                TB_268.Text = TB_268.Text.Replace(".", ",");
                TB_269.Text = TB_269.Text.Replace(".", ",");
                TB_270.Text = TB_270.Text.Replace(".", ",");
                TB_271.Text = TB_271.Text.Replace(".", ",");
                TB_272.Text = TB_272.Text.Replace(".", ",");
                // Fehlermeldungen
                if (Conversions.ToDouble(TB_260.Text) <= 0d)
                {
                    Interaction.MsgBox(My.MyProject.Forms.Form1.rm.GetString("String341"), (MsgBoxStyle)48, My.MyProject.Forms.Form1.rm.GetString("String239"));
                    return;
                }
                // Dateikopf
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String342") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String288") + TB_260.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklusparameter
                Zyklen.DT = Conversions.ToDouble(TB_260.Text);                          // Bohrerdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_261.Text);                       // Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_262.Text);                 // Sicherheits-Abstand
                Zyklen.DepthZ = -Convert.ToDouble(TB_263.Text);                         // Bohrtiefe
                Zyklen.StepZ = Conversions.ToDouble(TB_264.Text);                       // Zustelltiefe
                Zyklen.FFinish = Conversions.ToInteger(TB_265.Text);                    // Vorschub Zustellung
                Zyklen.FCut = Conversions.ToInteger(TB_265.Text);                       // 
                Zyklen.DepthBs = Conversions.ToDouble(TB_266.Text);                     // Bohrtiefe Spanbruch
                Zyklen.DepthRs = Conversions.ToDouble(TB_267.Text);                     // Rückzug bis Spanbruch
                Zyklen.DepthAst = Conversions.ToDouble(TB_268.Text);                    // Ausspantiefe
                Zyklen.SafetyHeight = Conversions.ToDouble(SH1.Text);                   // Absolute Sicherheitshöhe vor und nach Zyklus
                Zyklen.PocketSizeX = Conversions.ToDouble(TB_271.Text);                 // Werkstückabmessung in X
                Zyklen.PocketSizeY = Conversions.ToDouble(TB_272.Text);                 // Werkstückabmessung in Y
                // Startposition
                Zyklen.StartPosX = Conversions.ToDouble(TB_269.Text);                   // Abstand in X
                Zyklen.StartPosY = Conversions.ToDouble(TB_270.Text);                   // Abstand in Y
                for (int i = 0; i <=12; i++)
                {
                    Zyklen.BP[i, 0] = 10000d;
                    Zyklen.BP[i, 1] = 10000d;
                    Zyklen.BP[i, 2] = 10000d;
                }
                Zyklen.BP[0, 0] = Zyklen.StartPosX;
                Zyklen.BP[0, 1] = Zyklen.StartPosY;
                Zyklen.BP[0, 2] = 10000d;
                Fehler = false;
                // Zyklus aufrufen
                Zyklen.Bohrzyklus();
                // Variablen und Dateinamen vorbelegen und Editor anzeigen
                // Skalierung
                Rand = 60;
                Temp = (float)(Zyklen.PocketSizeX + Zyklen.DT);
                Skalierung = (My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand) / Temp;
                // Startpunkt des Zyklus in X
                Temp = (float)(My.MyProject.Forms.Form2.Zeichenfeld.Size.Width / 2d - Zyklen.PocketSizeX / 2d * (double)Skalierung);
                My.MyProject.Forms.Form2.NpX = (int)Round(Temp);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(Temp);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round(My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d - (Zyklen.SafetyHeight + Zyklen.StartHeight));
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(Zyklen.PocketSizeX * (double)Skalierung);          // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(Zyklen.PocketSizeY * (double)Skalierung);          // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(Zyklen.DepthZ * (double)Skalierung);               // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                            // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                            // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                           // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                           // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                           // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                           // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                           // Drehwinkel
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                My.MyProject.Forms.Form2.Befehl = "Bohrung";
                // Editor anzeigen
                if (Conversions.ToInteger(Fehler) == 0)
                {
                    My.MyProject.Forms.Form2.Dateiname = rm.GetString("String404");
                }
                return;
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        }
        #endregion

        #region Zyklus DXF-Daten gravieren
        // neue Datei einlesen
        private void LoadDXF_Click(object sender, EventArgs e)
        {
            StatusText = "";
            drawingList = [];
            gcodeList = [];
            objectIdentifier = [];
            inputFileTxt = "";
            dateiname.Text = "";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\DxfSamples\\";
            openFileDialog1.Filter = "dxf files (*.dxf)|*.dxf|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            PictureBox17.BackgroundImage = null;
            PictureBox17.Image = null;
            PictureBox17.Invalidate();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                inputFileTxt = openFileDialog1.FileName;
                if (inputFileTxt.Length > 0)
                {
                    StatusText = rm.GetString("String405");
                    dateiname.Text = inputFileTxt;
                    ReadFromFile(inputFileTxt);
                }
            }
            openFileDialog1.Dispose();
            StatusText = rm.GetString("String343");
            ZeichneDXF();
            TB_280.Focus();
            TB_280.SelectAll();
        }
        // Hilfe anzeigen (Fräserdurchmesser)
        private void TB_280_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String228");
        }
        private void TB_280_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // Hilfe anzeigen (Spindeldrehzahl)
        private void TB_281_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String222");
        }
        private void TB_281_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // Hilfe anzeigen (Sicherheitsabstand)
        private void TB_282_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String124");
        }
        private void TB_282_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // Hilfe anzeigen (Tiefe)
        private void TB_283_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String229");
        }
        private void TB_283_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // Hilfe anzeigen (Zustellung in Z)
        private void TB_284_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String224");
        }
        private void TB_284_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub Zustellung)
        private void TB_285_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String127");
        }
        private void TB_285_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // Hilfe anzeigen (Vorschub fräsen)
        private void TB_286_GotFocus(object sender, EventArgs e)
        {
            Info_17.Text = rm.GetString("String130");
        }
        private void TB_286_LostFocus(object sender, EventArgs e)
        {
            Info_17.Text = Infostring;
        }
        // verwerfen und Standardwerte eintragen
        private void Abbrechen_17_Click(object sender, EventArgs e)
        {
            StatusText = "";
            TB_280.Text = "1.0";
            TB_281.Text = "12000";
            TB_282.Text = "3.0";
            TB_283.Text = "-0.5";
            TB_284.Text = "0.5";
            TB_285.Text = "200";
            TB_286.Text = "800";
            Infostring = rm.GetString("String195");
            SelectNextControl(Abbrechen_17, true, true, true, true);
        }
        // G-Code für DXF-Daten gravieren erzeugen
        private void Gcode_17_Click(object sender, EventArgs e)
        {
            if (dateiname.Text == "")
            {
                Interaction.MsgBox(rm.GetString("String233"), (MsgBoxStyle)16, rm.GetString("String232"));
                goto ende;
            }
            if (ZMin != 0 || ZMax != 0)
            {
                Interaction.MsgBox(rm.GetString("String234"), (MsgBoxStyle)16, rm.GetString("String232"));
                goto ende;
            }
            string G01;
            string G02;
            string G03;
            string G04;
            float Skalierung;
            try
            {
                StatusText = rm.GetString("String354");
                // Eingabefehler korrigieren
                TB_280.Text = TB_280.Text.Replace(".", ",");
                TB_281.Text = TB_281.Text.Replace(".", ",");
                TB_282.Text = TB_282.Text.Replace(".", ",");
                TB_283.Text = TB_283.Text.Replace(".", ",");
                TB_284.Text = TB_284.Text.Replace(".", ",");
                TB_285.Text = TB_285.Text.Replace(".", ",");
                Fehler = false;
                // Dateikopf
                My.MyProject.Forms.Form2.Ausgabe.Clear();
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String235") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String344") + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String345") + DxfDatei + " )" + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText(rm.GetString("String237") + TB_220.Text + " mm )" + Constants.vbCrLf);
                // Maschinenparameter
                G01 = "G21";
                G02 = "";
                G03 = "";
                G04 = "G40";
                if (RB_1.Checked == true)
                {
                    G01 = "G20";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_2.Checked == true)
                {
                    G01 = "G21";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G01 + " ");
                }
                if (RB_3.Checked == true)
                {
                    G02 = "G90";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (RB_4.Checked == true)
                {
                    G02 = "G91.1";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G02 + " ");
                }
                if (CB_1.Checked == true)
                {
                    G03 = "G64";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G03 + " ");
                }
                if (RB_5.Checked == true)
                {
                    G04 = "G40";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_6.Checked == true)
                {
                    G04 = "G41";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                if (RB_7.Checked == true)
                {
                    G04 = "G42";
                    My.MyProject.Forms.Form2.Ausgabe.AppendText(G04);
                }
                My.MyProject.Forms.Form2.Ausgabe.AppendText(Constants.vbCrLf);
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("G17" + Constants.vbCrLf);
                }
                // Zyklus vorbereiten Werkzeug wählen
                My.MyProject.Forms.Form2.Ausgabe.AppendText("T2 M6" + Constants.vbCrLf);
                // Vorpositionieren und Spindel EIN
                My.MyProject.Forms.Form2.Ausgabe.AppendText("G0 Z" + Conversions.ToDouble(SH1.Text).ToString("##0.0", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                My.MyProject.Forms.Form2.Ausgabe.AppendText("M3 S" + Conversions.ToInteger(TB_221.Text).ToString("#####", System.Globalization.CultureInfo.InvariantCulture) + Constants.vbCrLf);
                var fraestiefe = Convert.ToDouble(TB_283.Text);
                if (fraestiefe > 0)
                    fraestiefe = -fraestiefe;
                // Zyklusparameter übergeben
                Zyklen.DT = Conversions.ToDouble(TB_280.Text);                      //Werkzeugdurchmesser
                Zyklen.Spin = Conversions.ToInteger(TB_281.Text);                   //Spindeldrehzahl
                Zyklen.StartHeight = Conversions.ToDouble(TB_282.Text);             //Sicherheitsabstand Z über Werkstück
                Zyklen.DepthZ = -fraestiefe;                                        //finale Tiefe in Z
                Zyklen.StepZ = Conversions.ToDouble(TB_284.Text);                   //Zustellung in Z
                Zyklen.FFinish = Conversions.ToInteger(TB_285.Text);                //Vorschub in Z
                Zyklen.FCut = Conversions.ToInteger(TB_286.Text);                   //Vorschub fräsen
                //Zyklus aufrufen
                Zyklen.DXFexport();
                if (Fehler == true)
                {
                    return;
                }
                //Zyklusende ------------------------------------------------------------------------
                //Programmende
                if (pp == 1) // Postprozzesor = Mach3
                {
                    My.MyProject.Forms.Form2.Ausgabe.AppendText("M30");
                }
                My.MyProject.Forms.Form2.Dateiname = DxfDatei;
                Fehler = false;
                // Skalierung
                Rand = 60;
                if (XMax >= YMax)
                {
                    Skalierung = (float)(Conversions.ToSingle((My.MyProject.Forms.Form2.Zeichenfeld.Size.Width - Rand)) / XMax);
                }
                else
                {
                    Skalierung = (float)(Conversions.ToSingle((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height - Rand)) / YMax);
                }
                // Startpunkt des Zyklus in X
                My.MyProject.Forms.Form2.NpX = (int)Round(XMin);
                // Startpunkt des Zyklus in Y
                My.MyProject.Forms.Form2.NpY = (int)Round(YMin);
                // Startpunkt des Zyklus in Z
                My.MyProject.Forms.Form2.NpZ = (int)Round((My.MyProject.Forms.Form2.Zeichenfeld.Size.Height / 2d) + fraestiefe);
                // Fertigmaße des Zyklus
                My.MyProject.Forms.Form2.Wks[0] = (int)Round(XMax * (double)Skalierung);                    // Abmessung in X
                My.MyProject.Forms.Form2.Wks[1] = (int)Round(YMax * (double)Skalierung);                    // Abmessung in Y
                My.MyProject.Forms.Form2.Wks[2] = (int)Round(fraestiefe * (double)Skalierung);              // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks[3] = 0;                                                        // Radius
                My.MyProject.Forms.Form2.Wks[4] = 0;                                                        // Drehwinkel
                My.MyProject.Forms.Form2.Wks1[0] = 0;                                                       // Abmessung in X
                My.MyProject.Forms.Form2.Wks1[1] = 0;                                                       // Abmessung in Y
                My.MyProject.Forms.Form2.Wks1[2] = 0;                                                       // Abmessung in -Z
                My.MyProject.Forms.Form2.Wks1[3] = 0;                                                       // Radius
                My.MyProject.Forms.Form2.Wks1[4] = 0;                                                       // Drehwinkel
                My.MyProject.Forms.Form2.Befehl = "DxfData";
                My.MyProject.Forms.Form2.Faktor = Skalierung;
                // Editor anzeigen
                if (Fehler == false)
                {
                    My.MyProject.Forms.Form2.Show();
                    statusLabel.Text = "";
                }
            }
            catch
            {
                Interaction.MsgBox(rm.GetString("String231"), (MsgBoxStyle)16, rm.GetString("String232"));
            }
        ende:;
        }
        #endregion

        // ------------------------------------------------------------------------------------------------

        #region Einstellungen
        // Speicherpfad der *.nc Dateien wählen
        private void Suche_Click(object sender, EventArgs e)
        {
            var result = Ordner.ShowDialog();
            if (result == DialogResult.OK)
            {
                Speicherpfad.Text = Ordner.SelectedPath;
            }
        }
        // Postprozzesor gewählt
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pp = ComboBox1.SelectedIndex;
        }
        // Einstellungen beenden
        private void Schließen_Click(object sender, EventArgs e)
        {
            Save_Config();
            TabControl1.Visible = true;
        }
        // Einstellungen beenden ohne zu speichern
        private void Abbrechen_Click(object sender, EventArgs e)
        {
            TabControl1.Visible = true;
        }
        // Einstellungen aus "config.txt" laden
        private void Load_Config()
        {
            string Datei;
            System.IO.StreamReader Datei1;
            int a;
            Datei = Pfad + @"\config.txt";
            if (System.IO.File.Exists(Datei) == false)
            {
                Save_Config();
                MessageBox.Show(rm.GetString("String355"), rm.GetString("String356"));
            }
            Datei1 = My.MyProject.Computer.FileSystem.OpenTextFileReader(Datei);
            Speicherpfad.Text = Datei1.ReadLine();
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                pp = 0;
            if (a == 1)
                pp = 1;
            ComboBox1.SelectedIndex = pp;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_1.Checked = false;
            if (a == 1)
                RB_1.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_2.Checked = false;
            if (a == 1)
                RB_2.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_3.Checked = false;
            if (a == 1)
                RB_3.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_4.Checked = false;
            if (a == 1)
                RB_4.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                CB_1.Checked = false;
            if (a == 1)
                CB_1.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_5.Checked = false;
            if (a == 1)
                RB_5.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_6.Checked = false;
            if (a == 1)
                RB_6.Checked = true;
            a = Conversions.ToInteger(Datei1.ReadLine());
            if (a == 0)
                RB_7.Checked = false;
            if (a == 1)
                RB_7.Checked = true;
            Max_X.Text = Datei1.ReadLine();
            Max_Y.Text = Datei1.ReadLine();
            Max_Z.Text = Datei1.ReadLine();
            Max_F.Text = Datei1.ReadLine();
            SH1.Text = Datei1.ReadLine();
            Sprache = Conversions.ToInteger(Datei1.ReadLine());
            Datei1.Close();
        }
        // Einstellungen in "config.txt" speichern
        private void Save_Config()
        {
            string Datei;
            System.IO.StreamWriter Datei1;
            Datei = Pfad + @"\config.txt";
            Datei1 = My.MyProject.Computer.FileSystem.OpenTextFileWriter(Datei, false);
            Datei1.WriteLine(Speicherpfad.Text);
            if (pp == 0)
                Datei1.WriteLine("0");
            if (pp == 1)
                Datei1.WriteLine("1");
            if (RB_1.Checked == false)
                Datei1.WriteLine("0");
            if (RB_1.Checked == true)
                Datei1.WriteLine("1");
            if (RB_2.Checked == false)
                Datei1.WriteLine("0");
            if (RB_2.Checked == true)
                Datei1.WriteLine("1");
            if (RB_3.Checked == false)
                Datei1.WriteLine("0");
            if (RB_3.Checked == true)
                Datei1.WriteLine("1");
            if (RB_4.Checked == false)
                Datei1.WriteLine("0");
            if (RB_4.Checked == true)
                Datei1.WriteLine("1");
            if (CB_1.Checked == false)
                Datei1.WriteLine("0");
            if (CB_1.Checked == true)
                Datei1.WriteLine("1");
            if (RB_5.Checked == false)
                Datei1.WriteLine("0");
            if (RB_5.Checked == true)
                Datei1.WriteLine("1");
            if (RB_6.Checked == false)
                Datei1.WriteLine("0");
            if (RB_6.Checked == true)
                Datei1.WriteLine("1");
            if (RB_7.Checked == false)
                Datei1.WriteLine("0");
            if (RB_7.Checked == true)
                Datei1.WriteLine("1");
            Datei1.WriteLine(Max_X.Text);
            Datei1.WriteLine(Max_Y.Text);
            Datei1.WriteLine(Max_Z.Text);
            Datei1.WriteLine(Max_F.Text);
            Datei1.WriteLine(SH1.Text);
            Datei1.WriteLine(Sprache);
            Datei1.Close();
        }
        #endregion

        #region diverse Funktionen
        // berechnen der Sinusfunktion
        public double Sinus(double Wert)
        {
            double Temp;
            Temp = PI * Wert / 180.0d;                      // Konvertierung von Grad in Bogenmaß
            return Sin(Temp);                                // Rückgabe als Sinus(Winkel)
        }
        // Stringlänge berechnen (Breite der Schrift)
        public string Stringlaenge(string Wert)
        {
            double laenge;
            int x;
            int n;
            string b;
            laenge = 0d;
            x = Strings.Len(Wert);
            b = "";
            var loopTo = x;
            for (n = 1; n <= loopTo; n++)
                b = Strings.Mid(Wert, n, 1);
            switch (b ?? "")
            {
                case "A":
                    {
                        laenge += 7.15d;
                        break;
                    }
                case "B":
                    {
                        break;
                    }
            }
            return laenge.ToString();
        }
        public void Getcoords(string thestring)
        {
            string theXbit;
            string theYbit;
            string nstring = thestring.Trim().ToUpper();
            int Xpos = nstring.LastIndexOf("X");
            int Ypos = nstring.LastIndexOf("Y");
            if (Xpos == -1)
            {
                nstring = "X" + Convert.ToString(GX) + " " + nstring;
                Xpos = nstring.LastIndexOf("X");
                Ypos = nstring.LastIndexOf("Y");
                //MessageBox.Show(rm.GetString("String357"));
                //Application.Exit();
            }
            else if (Ypos == -1)
            {
                nstring = nstring + " Y" + Convert.ToString(GY);
                Xpos = nstring.LastIndexOf("X");
                Ypos = nstring.LastIndexOf("Y");
            }
            if (Ypos > Xpos)
            {
                theYbit = nstring.Substring(Ypos + 1);
                theXbit = nstring.Substring(1, Ypos - 1).Trim();
                GX = Convert.ToSingle(theXbit);
                GY = Convert.ToSingle(theYbit);
            }
        }
        // Treadsicheres schreiben in die Statuszeile
        public void SetText(string text)
        {
            //if (statusLabel.)
        }
        // Sprache umstellen
        private void Translate()
        {
            rm = new ResourceManager("NC_Tool.Languages.de_Lang", Assembly.GetExecutingAssembly());
            if (Sprache == 2)
            {
                rm = new ResourceManager("NC_Tool.Languages.en_Lang", Assembly.GetExecutingAssembly());
            }
            // Formular 1
            this.Text = rm.GetString("String3");
            #region Menü
            ProgrammToolStripMenuItem.Text = rm.GetString("String4");
            EinstellungenToolStripMenuItem.Text = rm.GetString("String2");
            SpracheStripMenuItem.Text = rm.GetString("String5");
            BeendenToolStripMenuItem.Text = rm.GetString("String1");
            ZyklenToolStripMenuItem1.Text = rm.GetString("String7");
            ÜberToolStripMenuItem1.Text = rm.GetString("String6");
            FlächenbearbeitungToolStripMenuItem1.Text = rm.GetString("String8");
            StandardbohrenGewindeFräsenToolStripMenuItem.Text = rm.GetString("String9");
            TaschenToolStripMenuItem1.Text = rm.GetString("String10");
            ZapfenToolStripMenuItem1.Text = rm.GetString("String11");
            NutenFräsenToolStripMenuItem.Text = rm.GetString("String12");
            BbohrbilderToolStripMenuItem1.Text = rm.GetString("String13");
            SonderzyklenToolStripMenuItem1.Text = rm.GetString("String14");
            PlanfräsenToolStripMenuItem1.Text = rm.GetString("String15");
            SchrägeFaseToolStripMenuItem1.Text = rm.GetString("String16");
            TieflochBohrenToolStripMenuItem1.Text = rm.GetString("String17");
            GewindeFräsenToolStripMenuItem1.Text = rm.GetString("String18");
            RechtecktascheToolStripMenuItem1.Text = rm.GetString("String19");
            KreistascheToolStripMenuItem1.Text = rm.GetString("String20");
            RechteckzapfenToolStripMenuItem1.Text = rm.GetString("String21");
            KreiszapfenToolStripMenuItem1.Text = rm.GetString("String22");
            NutToolStripMenuItem1.Text = rm.GetString("String23");
            RingnutToolStripMenuItem1.Text = rm.GetString("String24");
            DichtungsnutToolStripMenuItem1.Text = rm.GetString("String25");
            RundeNutToolStripMenuItem1.Text = rm.GetString("String26");
            BohrtabelleToolStripMenuItem1.Text = rm.GetString("String27");
            BohrbildLochkreisToolStripMenuItem1.Text = rm.GetString("String28");
            BohrbildAufLinienToolStripMenuItem.Text = rm.GetString("String29");
            SchriftGavierenToolStripMenuItem.Text = rm.GetString("String30");
            DxfWandelnToolStripMenuItem.Text = rm.GetString("String31");
            NutenToolStripMenuItem.Text = rm.GetString("String122");
            #endregion
            #region Einstellungen
            Label14.Text = rm.GetString("String32");
            RB_1.Text = rm.GetString("String33");
            RB_2.Text = rm.GetString("String34");
            RB_3.Text = rm.GetString("String35");
            RB_4.Text = rm.GetString("String36");
            CB_1.Text = rm.GetString("String37");
            RB_5.Text = rm.GetString("String38");
            RB_6.Text = rm.GetString("String39");
            RB_7.Text = rm.GetString("String40");
            Label61.Text = rm.GetString("String41");
            Label65.Text = rm.GetString("String42");
            Label26.Text = rm.GetString("String43");
            Label4.Text = rm.GetString("String44");
            schließen.Text = rm.GetString("String45");
            abbrechen.Text = rm.GetString("String46");
            #endregion
            #region Buttons
            Abbrechen_1.Text = rm.GetString("String48");
            Abbrechen_2.Text = rm.GetString("String48");
            Abbrechen_3.Text = rm.GetString("String48");
            Abbrechen_4.Text = rm.GetString("String48");
            Abbrechen_5.Text = rm.GetString("String48");
            Abbrechen_6.Text = rm.GetString("String48");
            Abbrechen_7.Text = rm.GetString("String48");
            Abbrechen_8.Text = rm.GetString("String48");
            Abbrechen_9.Text = rm.GetString("String48");
            Abbrechen_10.Text = rm.GetString("String48");
            Abbrechen_11.Text = rm.GetString("String48");
            Abbrechen_12.Text = rm.GetString("String48");
            Abbrechen_13.Text = rm.GetString("String48");
            Abbrechen_14.Text = rm.GetString("String48");
            Abbrechen_15.Text = rm.GetString("String48");
            Abbrechen_16.Text = rm.GetString("String48");
            Abbrechen_17.Text = rm.GetString("String48");
            Gcode_1.Text = rm.GetString("String49");
            Gcode_2.Text = rm.GetString("String49");
            Gcode_3.Text = rm.GetString("String49");
            Gcode_4.Text = rm.GetString("String49");
            Gcode_5.Text = rm.GetString("String49");
            Gcode_6.Text = rm.GetString("String49");
            Gcode_7.Text = rm.GetString("String49");
            Gcode_8.Text = rm.GetString("String49");
            Gcode_9.Text = rm.GetString("String49");
            Gcode_10.Text = rm.GetString("String49");
            Gcode_11.Text = rm.GetString("String49");
            Gcode_12.Text = rm.GetString("String49");
            Gcode_13.Text = rm.GetString("String49");
            Gcode_14.Text = rm.GetString("String49");
            Gcode_15.Text = rm.GetString("String49");
            Gcode_16.Text = rm.GetString("String49");
            Gcode_17.Text = rm.GetString("String49");
            #endregion
            #region Überschriften
            Label1.Text = rm.GetString("String47");
            Label2.Text = rm.GetString("String50");
            Label21.Text = rm.GetString("String51");
            Label39.Text = rm.GetString("String52");
            Label49.Text = rm.GetString("String53");
            Label66.Text = rm.GetString("String54");
            Label76.Text = rm.GetString("String27");
            Label102.Text = rm.GetString("String28");
            Label115.Text = rm.GetString("String55");
            Label125.Text = rm.GetString("String56");
            Label139.Text = rm.GetString("String12");
            Label151.Text = rm.GetString("String29");
            Label167.Text = rm.GetString("String26");
            Label181.Text = rm.GetString("String57");
            Label192.Text = rm.GetString("String18");
            label204.Text = rm.GetString("String58");
            label218.Text = rm.GetString("String31");
            #endregion
            #region Eingabe & Beschriftung
            Label3.Text = rm.GetString("String59");         //Fräserdurchmesser (mm)
            Label15.Text = rm.GetString("String59");
            Label37.Text = rm.GetString("String59");
            Label40.Text = rm.GetString("String59");
            Label55.Text = rm.GetString("String59");
            Label75.Text = rm.GetString("String59");
            Label124.Text = rm.GetString("String59");
            Label136.Text = rm.GetString("String59");
            Label149.Text = rm.GetString("String59");
            Label175.Text = rm.GetString("String59");
            Label188.Text = rm.GetString("String59");
            Label226.Text = rm.GetString("String59");
            Label99.Text = rm.GetString("String60");        //Bohrerdurchmesser(mm)
            Label111.Text = rm.GetString("String60");
            Label160.Text = rm.GetString("String60");
            label213.Text = rm.GetString("String60");
            Label12.Text = rm.GetString("String61");        //Spindeldrehzahl in U/min
            Label41.Text = rm.GetString("String61");
            Label196.Text = rm.GetString("String61");
            Label9.Text = rm.GetString("String61");
            Label36.Text = rm.GetString("String61");
            Label129.Text = rm.GetString("String61");
            label212.Text = rm.GetString("String61");
            Label54.Text = rm.GetString("String61");
            Label148.Text = rm.GetString("String61");
            Label74.Text = rm.GetString("String61");
            Label159.Text = rm.GetString("String61");
            Label123.Text = rm.GetString("String61");
            Label174.Text = rm.GetString("String61");
            Label98.Text = rm.GetString("String61");
            Label110.Text = rm.GetString("String61");
            Label187.Text = rm.GetString("String61");
            Label225.Text = rm.GetString("String61");
            Label10.Text = rm.GetString("String67");        //Sicherheits - Abstand(mm)
            Label131.Text = rm.GetString("String67");
            label211.Text = rm.GetString("String67");
            Label16.Text = rm.GetString("String67");
            Label35.Text = rm.GetString("String67");
            Label42.Text = rm.GetString("String67");
            Label53.Text = rm.GetString("String67");
            Label147.Text = rm.GetString("String67");
            Label73.Text = rm.GetString("String67");
            Label158.Text = rm.GetString("String67");
            Label122.Text = rm.GetString("String67");
            Label173.Text = rm.GetString("String67");
            Label97.Text = rm.GetString("String67");
            Label109.Text = rm.GetString("String67");
            Label186.Text = rm.GetString("String67");
            Label224.Text = rm.GetString("String67");
            Label5.Text = rm.GetString("String68");         //Finale Tiefe in Z (mm)
            Label135.Text = rm.GetString("String68");
            Label17.Text = rm.GetString("String68");
            Label34.Text = rm.GetString("String68");
            Label43.Text = rm.GetString("String68");
            Label52.Text = rm.GetString("String68");
            Label146.Text = rm.GetString("String68");
            Label72.Text = rm.GetString("String68");
            Label121.Text = rm.GetString("String68");
            Label172.Text = rm.GetString("String68");
            Label185.Text = rm.GetString("String68");
            Label223.Text = rm.GetString("String68");
            Label6.Text = rm.GetString("String69");         //Zustellung in Z (mm)
            Label134.Text = rm.GetString("String69");
            Label18.Text = rm.GetString("String69");
            Label33.Text = rm.GetString("String69");
            Label44.Text = rm.GetString("String69");
            Label51.Text = rm.GetString("String69");
            Label145.Text = rm.GetString("String69");
            Label71.Text = rm.GetString("String69");
            Label120.Text = rm.GetString("String69");
            Label171.Text = rm.GetString("String69");
            Label95.Text = rm.GetString("String69");
            Label184.Text = rm.GetString("String69");
            Label220.Text = rm.GetString("String69");
            Label13.Text = rm.GetString("String65");        //Vorschub Zustellung mm/min
            Label128.Text = rm.GetString("String65");
            label208.Text = rm.GetString("String65");
            Label19.Text = rm.GetString("String65");
            Label32.Text = rm.GetString("String65");
            Label45.Text = rm.GetString("String65");
            Label50.Text = rm.GetString("String65");
            Label144.Text = rm.GetString("String65");
            Label70.Text = rm.GetString("String65");
            Label155.Text = rm.GetString("String65");
            Label119.Text = rm.GetString("String65");
            Label180.Text = rm.GetString("String65");
            Label94.Text = rm.GetString("String65");
            Label106.Text = rm.GetString("String65");
            Label182.Text = rm.GetString("String65");
            Label221.Text = rm.GetString("String65");
            Label11.Text = rm.GetString("String66");        //Vorschub Fräsen in mm/min
            Label130.Text = rm.GetString("String66");
            Label199.Text = rm.GetString("String66");
            Label24.Text = rm.GetString("String66");
            Label29.Text = rm.GetString("String66");
            Label47.Text = rm.GetString("String66");
            Label56.Text = rm.GetString("String66");
            Label141.Text = rm.GetString("String66");
            Label69.Text = rm.GetString("String66");
            Label118.Text = rm.GetString("String66");
            Label170.Text = rm.GetString("String66");
            Label183.Text = rm.GetString("String66");
            Label222.Text = rm.GetString("String66");
            Label7.Text = rm.GetString("String70");         //Abmessung in X (mm)
            Label133.Text = rm.GetString("String70");
            label215.Text = rm.GetString("String70");
            Label202.Text = rm.GetString("String70");
            Label22.Text = rm.GetString("String70");
            Label31.Text = rm.GetString("String70");
            Label143.Text = rm.GetString("String70");
            Label117.Text = rm.GetString("String70");
            Label8.Text = rm.GetString("String71");         //Abmessung in Y(mm)
            Label132.Text = rm.GetString("String71");
            label214.Text = rm.GetString("String71");
            Label201.Text = rm.GetString("String71");
            Label23.Text = rm.GetString("String71");
            Label30.Text = rm.GetString("String71");
            Label142.Text = rm.GetString("String71");
            Label116.Text = rm.GetString("String71");
            label217.Text = rm.GetString("String110");      //Mitte in X
            Label198.Text = rm.GetString("String110");
            Label168.Text = rm.GetString("String110");
            label216.Text = rm.GetString("String109");      //Mitte in Y
            Label197.Text = rm.GetString("String109");
            Label176.Text = rm.GetString("String109");
            Label25.Text = rm.GetString("String74");        //Eckenradius
            Label28.Text = rm.GetString("String74");
            Label140.Text = rm.GetString("String74");
            Label127.Text = rm.GetString("String74");
            Label20.Text = rm.GetString("String76");        //Schlichtaufmaß
            Label27.Text = rm.GetString("String163");        //Aufmaß Rohteil in X (mm)
            Label59.Text = rm.GetString("String163");
            Label38.Text = rm.GetString("String164");        //Aufmaß Rohteil in Y (mm)
            Label60.Text = rm.GetString("String164");
            Label48.Text = rm.GetString("String171");        //Durchmesser Rohteil (mm)
            Label57.Text = rm.GetString("String171");
            Label46.Text = rm.GetString("String172");        //Durchmesser Fertigteil (mm)
            Label58.Text = rm.GetString("String172");
            Label150.Text = rm.GetString("String182");        //Verdrehwinkel in Grad
            Label126.Text = rm.GetString("String204");
            Label137.Text = rm.GetString("String77");
            GroupBox5.Text = rm.GetString("String78");
            Label138.Text = rm.GetString("String79");
            Label101.Text = rm.GetString("String95");
            Label100.Text = rm.GetString("String91");
            Label95.Text = rm.GetString("String92");
            Label96.Text = rm.GetString("String93");
            Label78.Text = rm.GetString("String94");
            Label112.Text = rm.GetString("String96");
            Label113.Text = rm.GetString("String97");
            Label114.Text = rm.GetString("String98");
            Label103.Text = rm.GetString("String89");
            Label77.Text = rm.GetString("String89");
            Label104.Text = rm.GetString("String95");
            Label105.Text = rm.GetString("String91");
            Label107.Text = rm.GetString("String92");
            Label108.Text = rm.GetString("String93");
            Label166.Text = rm.GetString("String99");
            Label165.Text = rm.GetString("String100");
            Label163.Text = rm.GetString("String101");
            Label164.Text = rm.GetString("String102");
            Label162.Text = rm.GetString("String103");
            Label161.Text = rm.GetString("String104");
            Label152.Text = rm.GetString("String89");
            Label153.Text = rm.GetString("String84");
            Label154.Text = rm.GetString("String91");
            Label156.Text = rm.GetString("String92");
            Label157.Text = rm.GetString("String93");
            Label151.Text = rm.GetString("String105");
            Label179.Text = rm.GetString("String106");
            Label169.Text = rm.GetString("String96");
            Label178.Text = rm.GetString("String107");
            Label177.Text = rm.GetString("String108");
            label190.Text = rm.GetString("String111");
            Label191.Text = rm.GetString("String112");
            Label189.Text = rm.GetString("String113");
            RadioButton2.Text = rm.GetString("String114");
            RadioButton1.Text = rm.GetString("String115");
            Label200.Text = rm.GetString("String116");
            Label193.Text = rm.GetString("String117");
            Label194.Text = rm.GetString("String118");
            Label195.Text = rm.GetString("String119");
            label205.Text = rm.GetString("String89");
            label206.Text = rm.GetString("String90");
            label207.Text = rm.GetString("String91");
            label209.Text = rm.GetString("String92");
            label210.Text = rm.GetString("String93");
            ellipse_option.Text = rm.GetString("String120");
            loadDXF.Text = rm.GetString("String121");
            Methode1.Text = rm.GetString("String63");
            Methode2.Text = rm.GetString("String64");
            Schlicht_1.Text = "       " + rm.GetString("String75");
            Schlicht_2.Text = "       " + rm.GetString("String75");
            Label68.Text = rm.GetString("String86");
            Label67.Text = rm.GetString("String85");
            BT_loeschen.Text = rm.GetString("String88");
            GroupBox6.Text = rm.GetString("String230");
            label219.Text = rm.GetString("String407");
            #endregion
            #region Infos und Meldungen
            Infostring = rm.GetString("String123");
            Info_01.Text = Infostring;
            #endregion
        }
        // schaltet Infotexte auf aktueller Seite nach Sprachwechsel um
        public void InfoText(int tab)
        {
            switch (tab)
            {
                case 0:     // Planfräsen
                    {
                        Infostring = rm.GetString("String123");
                        Info_01.Text = Infostring;
                        break;
                    }
                case 1:     // schräge Flächen
                    {
                        Infostring = rm.GetString("String131");
                        Info_10.Text = Infostring;
                        break;
                    }
                case 2:     // Rechtecktasche
                    {
                        Infostring = rm.GetString("String155");
                        Info_02.Text = Infostring;
                        break;
                    }
                case 3:     // Rechteckzapfen
                    {
                        Infostring = rm.GetString("String162");
                        Info_03.Text = Infostring;
                        break;
                    }
                case 4:     // Kreistasche
                    {
                        Infostring = rm.GetString("String187");
                        Info_04.Text = Infostring;
                        break;
                    }
                case 5:     // Kreiszapfen
                    {
                        Infostring = rm.GetString("String188");
                        Info_05.Text = Infostring;
                        break;
                    }
                case 6:     // Nut
                    {
                        Infostring = rm.GetString("String181");
                        Info_11.Text = Infostring;
                        break;
                    }
                case 7:     // Ringnut
                    {
                        Infostring = rm.GetString("String189");
                        Info_06.Text = Infostring;
                        break;
                    }
                case 8:     // Dichtungsnut
                    {
                        Infostring = rm.GetString("String192");
                        Info_09.Text = Infostring;
                        break;
                    }
                case 9:     // Bohrtabelle
                    {
                        Infostring = rm.GetString("String190");
                        Info_07.Text = Infostring;
                        break;
                    }
                case 10:    // BB Lochkreis
                    {
                        Infostring = rm.GetString("String191");
                        Info_08.Text = Infostring;
                        break;
                    }
                case 11:    // BB Linien
                    {
                        Infostring = rm.GetString("String193");
                        Info_12.Text = Infostring;
                        break;
                    }
                case 12:    // runde Nut
                    {
                        Infostring = rm.GetString("String196");
                        Info_13.Text = Infostring;
                        break;
                    }
                case 13:    // Schrift
                    {
                        Infostring = rm.GetString("String194");
                        Info_14.Text = Infostring;
                        break;
                    }
                case 14:    // Gewinde
                    {
                        if (RadioButton1.Checked == true)
                        {
                            Infostring = rm.GetString("String147");
                        }
                        if (RadioButton2.Checked == true)
                        {
                            Infostring = rm.GetString("String148");
                        }
                        Info_15.Text = Infostring;
                        break;
                    }
                case 15:    // Tiefloch
                    {
                        Infostring = rm.GetString("String137");
                        Info_16.Text = Infostring;
                        break;
                    }
                case 16:    // DXF
                    {
                        Infostring = rm.GetString("String195");
                        Info_17.Text = Infostring;
                        break;
                    }
            }
        }
        // Aktualisierung Statuszeile
        private void timer1_Tick(object sender, EventArgs e)
        {
            statusLabel.Text = StatusText;
        }
        #endregion

        #region DXF Funktionen
        // DXF-Datei einlesen und auswerten
        public void ReadFromFile(string textFile)
        {
            string line1, line2;
            XMin = 0; YMin = 0;
            XMax = 0; YMax = 0;
            theSourceFile = new FileInfo(textFile);
            DxfDatei = theSourceFile.Name;
            StreamReader reader = null;
            try
            {
                reader = theSourceFile.OpenText();          // Der Reader ist eingestellt ...
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.FileName.ToString() + rm.GetString("String358"));
            }
            catch
            {
                MessageBox.Show(rm.GetString("String359"));
                return;
            }
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "9" && line2 == "$EXTMIN")
                     SizeMinModule(reader);
                if (line1 == "9" && line2 == "$EXTMAX")
                    SizeMaxModule(reader);
                if (line1 == "0" && line2 == "INSERT")
                    InsertModule(reader);
                if (line1 == "0" && line2 == "TEXT")
                    TextModule(reader);
                if (line1 == "0" && line2 == "BLOCK")
                    BlockModule(reader);
                if (line1 == "0" && line2 == "LINE")
                    LineModule(reader);
                if (line1 == "0" && line2 == "LWPOLYLINE")
                    LwPolylineModule(reader);
                if (line1 == "0" && line2 == "POLYLINE")
                    PolylineModule(reader);
                if (line1 == "0" && line2 == "CIRCLE")
                    CircleModule(reader);
                if (line1 == "0" && line2 == "ELLIPSE")
                    EllipseModule(reader);
                if (line1 == "0" && line2 == "ARC")
                    ArcModule(reader);
            }
            while (line2 != "EOF");
            // Skalierung
            scaleX = (double)(this.PictureBox17.Size.Width - 10) / (double)(Math.Abs(XMax) - Math.Abs(XMin));
            scaleY = (double)(this.PictureBox17.Size.Height - 10) / (double)(Math.Abs(YMax) - Math.Abs(YMin));
            mainScale = Math.Min(scaleX, scaleY);
            reader.DiscardBufferedData();
            theSourceFile = null;
            reader.Close();
        }
        // Zeile1 und Zeile2 - Werte zuzuweisen
        private void GetLineCouple(StreamReader theReader, out string line1, out string line2)
        {
            System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            string decimalSeparator = ci.NumberFormat.CurrencyDecimalSeparator;
            line1 = line2 = "";
            if (theReader == null)
                return;
            line1 = theReader.ReadLine();
            if (line1 != null)
            {
                line1 = line1.Trim();
                line1 = line1.Replace('.', decimalSeparator[0]);
            }
            line2 = theReader.ReadLine();
            if (line2 != null)
            {
                line2 = line2.Trim();
                line2 = line2.Replace('.', decimalSeparator[0]);
            }
        }
        // Interpretiert die minimale Größe
        private void SizeMinModule(StreamReader reader)
        {
            string line1, line2;
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    DxfXMin = Convert.ToDouble(line2);
                }
                if (line1 == "20")
                {
                    DxfYMin = Convert.ToDouble(line2);
                }
            }
            while (line1 != "30");
        }
        // Interpretiert die maximale Größe
        private void SizeMaxModule(StreamReader reader)
        {
            string line1, line2;
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    DxfXMax = Convert.ToDouble(line2);
                }
                if (line1 == "20")
                {
                    DxfYMax = Convert.ToDouble(line2);
                }
            }
            while (line1 != "30");
        }
        // Interpretiert Linienobjekte                          (Objekt 2) 
        private void LineModule(StreamReader reader)
        {
            string line1, line2;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                    if (x1 > XMax)
                        XMax = x1;
                    if (x1 < XMin)
                        XMin = x1;
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                    if (y1 > YMax)
                        YMax = y1;
                    if (y1 < YMin)
                        YMin = y1;
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "11")
                {
                    x2 = Convert.ToDouble(line2);
                    if (x2 > XMax)
                        XMax = x2;

                    if (x2 < XMin)
                        XMin = x2;
                }
                if (line1 == "21")
                {
                    y2 = Convert.ToDouble(line2);
                    if (y2 > YMax)
                        YMax = y2;
                    if (y2 < YMin)
                        YMin = y2;
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
            }
            while (line1 != "31");
            int ix = drawingList.Add(new Line(new Point((int)x1, (int)-y1), new Point((int)x2, (int)-y2), Color.White, 1));
            int ix1 = gcodeList.Add(new Line1(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), Color.White, 1));
            objectIdentifier.Add(new DrawingObject(2, ix));
        }
        // Interpretiert einen Block mit Linien (Rechteck)      (Objekt 3)
        private void BlockModule(StreamReader reader)
        {
            string line1, line2;
            line1 = "0";
            line2 = "0";
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            int counter = 0;
            int openOrClosed = 0;
            ArrayList pointListStart = [];
            ArrayList pointListEnd = [];
            ArrayList layers = [];
            PointF Startpunkt = new();
            PointF Endpunkt = new();
            PointF SortStart = new();
            PointF SortEnd = new();
            string layer = "";
            string blockName = "";
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "2")
                {
                    blockName = line2;
                }
                if (line1 == "0" && line2 == "LINE")
                {
                    do
                    {
                        GetLineCouple(reader, out line1, out line2);
                        if (line1 == "70")
                            openOrClosed = Convert.ToInt32(line2);
                        if (line1 == "8")
                        {
                            layer = line2;
                        }
                        if (line1 == "10")
                        {
                            x1 = Convert.ToDouble(line2);
                            if (x1 > XMax)
                                XMax = x1;
                            if (x1 < XMin)
                                XMin = x1;
                        }
                        if (line1 == "20")
                        {
                            y1 = Convert.ToDouble(line2);
                            if (y1 > YMax)
                                YMax = y1;
                            if (y1 < YMin)
                                YMin = y1;
                        }
                        if (line1 == "30")
                        {
                            ZMax = Convert.ToDouble(line2);
                        }
                        if (line1 == "11")
                        {
                            x2 = Convert.ToDouble(line2);
                            if (x2 > XMax)
                                XMax = x2;

                            if (x2 < XMin)
                                XMin = x2;
                        }
                        if (line1 == "21")
                        {
                            y2 = Convert.ToDouble(line2);
                            if (y2 > YMax)
                                YMax = y2;
                            if (y2 < YMin)
                                YMin = y2;
                            layers.Add(layer);
                            pointListStart.Add(new PointF((float)x1, (float)y1));
                            pointListEnd.Add(new PointF((float)x2, (float)y2));
                            counter++;
                        }
                        if (line1 == "31")
                        {
                            ZMin = Convert.ToDouble(line2);
                        }
                    }
                    while (line1 != "31");
                }
            }
            while (line2 != "ENDBLK");
            if (pointListStart.Count == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    Startpunkt = (PointF)pointListStart[i];
                    Endpunkt = (PointF)pointListEnd[i];
                    x1 = Startpunkt.X; y1 = Startpunkt.Y;
                    x2 = Endpunkt.X; y2 = Endpunkt.Y;
                    // sortieren
                    for (int j = 0; j < 4; j++)
                    {
                        SortStart = (PointF)pointListStart[j];
                        SortEnd = (PointF)pointListEnd[j];
                        if (SortStart.X < x1)
                           x1 = SortStart.X;
                        if (SortStart.Y < y1)
                            y1 = SortStart.Y;
                        if (SortEnd.X > x2)
                            x2 = SortEnd.X;
                        if (SortEnd.Y > y2)
                            y2 = SortEnd.Y;
                    }
                }
                int ix = drawingList.Add(new Rectangl(new Point((int)x1, (int)-y1), new Point((int)x2, (int)-y2), Color.White, Color.Red, 1, 0, blockName));
                int ix1 = gcodeList.Add(new Rectangl(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), Color.White, Color.Red, 1, 0, blockName));
                objectIdentifier.Add(new DrawingObject(3, ix));
            }
        }
        // Interpretiert Kreisobjekte                           (Objekt 4)
        private void CircleModule(StreamReader reader)
        {
            string line1, line2;
            double x1 = 0;
            double y1 = 0;
            double radius = 0;
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
                if (line1 == "40")
                {
                    radius = Convert.ToDouble(line2);
                    if ((x1 + radius) > XMax)
                        XMax = x1 + radius;
                    if ((x1 - radius) < XMin)
                        XMin = x1 - radius;
                    if (y1 + radius > YMax)
                        YMax = y1 + radius;
                    if ((y1 - radius) < YMin)
                        YMin = y1 - radius;
                }
            }
            while (line1 != "40");
            int ix = drawingList.Add(new Circle(new Point((int)x1, (int)-y1), radius, Color.White, Color.Red, 1));
            int ix1 = gcodeList.Add(new Circle1(new PointF((float)x1, (float)y1), radius, Color.White, Color.Red, 1));
            objectIdentifier.Add(new DrawingObject(4, ix));
        }
        // Interpretiert Polylinienobjekte                      (Objekt 5)
        private void LwPolylineModule(StreamReader reader)
        {
            string line1, line2;
            line1 = "0";
            line2 = "0";
            double x1 = 0;
            double y1 = 0;
            thePolyLine = new Polyline(Color.White, 1);
            thePolyLine1 = new Polyline(Color.White, 1);
            int ix = drawingList.Add(thePolyLine);
            int ix1 = gcodeList.Add(thePolyLine1);
            objectIdentifier.Add(new DrawingObject(5, ix));
            int counter = 0;
            int numberOfVertices = 1;
            int openOrClosed = 0;
            ArrayList pointList = [];
            ArrayList pointList1 = [];
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "90")
                    numberOfVertices = Convert.ToInt32(line2);
                if (line1 == "70")
                    openOrClosed = Convert.ToInt32(line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                    if (x1 > XMax)
                        XMax = x1;
                    if (x1 < XMin)
                        XMin = x1;
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                    if (y1 > YMax)
                        YMax = y1;
                    if (y1 < YMin)
                        YMin = y1;
                    pointList.Add(new Point((int)x1, (int)-y1));
                    pointList1.Add(new PointF((float)x1, (float)y1));
                    counter++;
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
            }
            while (counter < numberOfVertices);
            for (int i = 1; i < numberOfVertices; i++)
            {
                thePolyLine.AppendLine(new Line((Point)pointList[i - 1], (Point)pointList[i], Color.White, 1));
                thePolyLine1.AppendLine1(new Line1((PointF)pointList1[i - 1], (PointF)pointList1[i], Color.White, 1));
            }
            if (openOrClosed == 1)
            {
                thePolyLine.AppendLine(new Line((Point)pointList[numberOfVertices - 1], (Point)pointList[0], Color.White, 1));
                thePolyLine1.AppendLine1(new Line1((PointF)pointList[numberOfVertices - 1], (PointF)pointList[0], Color.White, 1));
            }
        }
        // Interpretiert Polylinienobjekte                      (Objekt 5)
        private void PolylineModule(StreamReader reader)
        {
            string line1, line2;
            line1 = "0";
            line2 = "0";
            double x1 = 0;
            double y1 = 0;
            thePolyLine = new Polyline(Color.White, 1);
            thePolyLine1 = new Polyline(Color.White, 1);
            int ix = drawingList.Add(thePolyLine);
            int ix1 = gcodeList.Add(thePolyLine1);
            objectIdentifier.Add(new DrawingObject(5, ix));
            int counter = 0;
            int numberOfVertices = 1;
            int openOrClosed = 0;
            ArrayList pointList = [];
            ArrayList pointList1 = [];
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "90")
                    numberOfVertices = Convert.ToInt32(line2);
                if (line1 == "70")
                    openOrClosed = Convert.ToInt32(line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                    if (x1 > XMax)
                        XMax = x1;
                    if (x1 < XMin)
                        XMin = x1;
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                    if (y1 > YMax)
                        YMax = y1;
                    if (y1 < YMin)
                        YMin = y1;
                    pointList.Add(new Point((int)x1, (int)-y1));
                    pointList1.Add(new PointF((float)x1, (float)y1));
                    counter++;
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
            }
            while (line2 != "SEQEND");
            for (int i = 2; i < counter; i++)
            {
                thePolyLine.AppendLine(new Line((Point)pointList[i - 1], (Point)pointList[i], Color.White, 1));
                thePolyLine1.AppendLine1(new Line1((PointF)pointList1[i - 1], (PointF)pointList1[i], Color.White, 1));
            }
            //if (openOrClosed == 1)
            //{
            //    thePolyLine.AppendLine(new Line((Point)pointList[counter - 1], (Point)pointList[0], Color.White, 1));
            //    thePolyLine1.AppendLine1(new Line1((PointF)pointList1[counter - 1], (PointF)pointList1[0], Color.White, 1));
            //}
        }
        // Interpretiert Bogenobjekte                           (Objekt 6)
        private void ArcModule(StreamReader reader)
        {
            string line1, line2;
            double x1 = 0;
            double y1 = 0;
            double radius = 0;
            double angle1 = 0;
            double angle2 = 0;
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                    if (x1 > XMax)
                        XMax = x1;
                    if (x1 < XMin)
                        XMin = x1;
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                    if (y1 > YMax)
                        YMax = y1;
                    if (y1 < YMin)
                        YMin = y1;
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
                if (line1 == "40")
                {
                    radius = Convert.ToDouble(line2);
                    if ((x1 + radius) > XMax)
                        XMax = x1 + radius;
                    if ((x1 - radius) < XMin)
                        XMin = x1 - radius;
                    if (y1 + radius > YMax)
                        YMax = y1 + radius;
                    if ((y1 - radius) < YMin)
                        YMin = y1 - radius;
                }
                if (line1 == "50")
                    angle1 = Convert.ToDouble(line2);
                if (line1 == "51")
                    angle2 = Convert.ToDouble(line2);
            }
            while (line1 != "51");
            int ix = drawingList.Add(new Arc(new Point((int)x1, (int)-y1), radius, angle1, angle2, Color.White, Color.Red, 1));
            int ix1 = gcodeList.Add(new Arc1(new PointF((float)x1, (float)y1), radius, angle1, angle2, Color.White, Color.Red, 1));
            objectIdentifier.Add(new DrawingObject(6, ix));
        }
        // Interpretiert Ellipseobjekte                         (Objekt 7)
        private void EllipseModule(StreamReader reader)
        {
            string line1, line2;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double faktor = 0;
            double angle1 = 0;
            double angle2 = 0;
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "11")
                {
                    x2 = Convert.ToDouble(line2);
                }
                if (line1 == "21")
                {
                    y2 = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
                if (line1 == "40")
                {
                    faktor = Convert.ToSingle(line2);
                }
                if (line1 == "41")
                {
                    angle1 = Convert.ToSingle(line2);
                }
                if (line1 == "42")
                {
                    angle2 = Convert.ToSingle(line2);
                }
            }
            while (line1 != "42");
            int ix = drawingList.Add(new Ellipse(new PointF((float)x1, (float)-y1), new PointF((float)x2, (float)y2), (float)faktor, angle1, angle2, Color.White, Color.Red, 1));
            int ix1 = gcodeList.Add(new Ellipse(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), (float)faktor, angle1, angle2, Color.White, Color.Red, 1));
            objectIdentifier.Add(new DrawingObject(7, ix));
        }
        // Interpretiert einen Text-Eintrag                     (Objekt 8)
        private void TextModule(StreamReader reader)
        {
            string line1, line2;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double textheight = 0;
            string blocktext = "";
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "10")
                {
                    x1 = Convert.ToDouble(line2);
                }
                if (line1 == "20")
                {
                    y1 = Convert.ToDouble(line2);
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "11")
                {
                    x2 = Convert.ToDouble(line2);
                }
                if (line1 == "21")
                {
                    y2 = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
                if (line1 == "40")
                {
                    textheight = Convert.ToDouble(line2);
                }
                if (line1 == "1")
                {
                    blocktext = line2;
                }
            }
            while (line1 != "1");
            int ix = drawingList.Add(new Texte(new PointF((float)x1, (float)-y1), textheight, blocktext));
            int ix1 = gcodeList.Add(new Texte(new PointF((float)x1, (float)-y1), textheight, blocktext));
            objectIdentifier.Add(new DrawingObject(8, ix));
        }
        // Interpretiert einen Insert-Eintrag                   (Objekt 9)
        private void InsertModule(StreamReader reader)
        {
            string line1, line2;
            string InsertName = "";
            PointF InsertPoint = new();
            do
            {
                GetLineCouple(reader, out line1, out line2);
                if (line1 == "2")
                {
                    InsertName = line2;
                }
                if (line1 == "10")
                {
                    InsertPoint.X = (float)Convert.ToDouble(line2);
                }
                if (line1 == "20")
                {
                    InsertPoint.Y = (float)Convert.ToDouble(line2);
                }
                if (line1 == "30")
                {
                    ZMax = Convert.ToDouble(line2);
                }
                if (line1 == "31")
                {
                    ZMin = Convert.ToDouble(line2);
                }
            }
            while (line1 != "30");
            int ix = drawingList.Add(new Insert(new PointF(InsertPoint.X, -InsertPoint.Y), InsertName));
            int ix1 = gcodeList.Add(new Insert(new PointF(InsertPoint.X, InsertPoint.Y), InsertName));
            objectIdentifier.Add(new DrawingObject(9, ix));
        }
        public class DrawingObject
        {
            public int shapeType;
            public int indexNo;
            public DrawingObject(int shapeID, int ix)
            {
                shapeType = shapeID;
                indexNo = ix;

            }
        }
        public abstract class Shape
        {
            protected Color contourColor;
            protected Color fillColor;
            protected int lineWidth;
            public int shapeIdentifier;
            public int rotation;
            public bool highlighted;

            public abstract Color AccessContourColor
            {
                get;
                set;
            }

            public abstract Color AccessFillColor
            {
                get;
                set;
            }

            public abstract int AccessLineWidth
            {
                get;
                set;
            }

            public abstract int AccessRotation
            {
                get;
                set;
            }

            public abstract void Draw(Pen pen, Graphics g);
            public abstract bool Highlight(Pen pen, Graphics g, Point point);
        }
        public void ZeichneDXF() {
            Graphics g = PictureBox17.CreateGraphics();
            Draw(g);
        }
        public void Draw(Graphics g)
        {
            Pen lePen = new(Color.Black, 1);
            g.TranslateTransform(2, this.PictureBox17.Size.Height - 5);
            if (YMin < 0)
                g.TranslateTransform(0, -(int)Math.Abs(YMin));                  // Transformiert den Ursprungspunkt in die untere linke Ecke des Fensters.
            if (XMin < 0)
                g.TranslateTransform((int)Math.Abs(XMin), 0);
            foreach (DrawingObject obj in objectIdentifier)                     // geht durch die Objekte
            {
                switch (obj.shapeType)
                {
                    case 2:             // Linie
                        {
                            Line temp = (Line)drawingList[obj.indexNo];
                            lePen.Width = temp.AccessLineWidth;
                            highlightedRegion.Location = temp.GetStartPoint;
                            highlightedRegion.Width = temp.GetStartPoint.X - temp.GetEndPoint.X;
                            highlightedRegion.Height = temp.GetStartPoint.Y - temp.GetEndPoint.Y;
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(lePen, g, mainScale);
                            break;
                        }
                    case 3:             // Rechteck 
                        {
                            Rectangl temp = (Rectangl)drawingList[obj.indexNo];
                            int index;
                            string name = "";
                            PointF pointF = new();
                            foreach (DrawingObject inserttest in objectIdentifier)
                            {
                                if (inserttest.shapeType == 9)
                                {
                                    index = inserttest.indexNo;
                                    Insert insert = (Insert)drawingList[index];
                                    name = insert.name;
                                    pointF = insert.startPoint;
                                }
                            }
                            lePen.Width = temp.AccessLineWidth;
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(lePen, g, mainScale, name, pointF, temp.blockName);
                            break;
                        }
                    case 4:             // Kreis
                        {
                            Circle temp = (Circle)drawingList[obj.indexNo];
                            lePen.Width = temp.AccessLineWidth;
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(lePen, g, mainScale);
                            break;
                        }
                    case 5:             // Polylinie
                        {
                            Polyline temp = (Polyline)drawingList[obj.indexNo];
                            lePen.Width = temp.AccessLineWidth;
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(lePen, g, mainScale);
                            break;
                        }
                    case 6:             // Bogen
                        {
                            Arc temp = (Arc)drawingList[obj.indexNo];
                            lePen.Width = temp.AccessLineWidth;
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(lePen, g, mainScale);
                            break;
                        }
                    case 7:             // Ellipse
                        {
                            Ellipse temp = (Ellipse)drawingList[obj.indexNo];
                            lePen.Width = temp.AccessLineWidth;
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(lePen, g, mainScale);
                            break;
                        }
                    case 8:             // Text
                        {
                            Texte temp = (Texte)drawingList[obj.indexNo];
                            if (mainScale == 0)
                                mainScale = 1;
                            temp.Draw(g, temp.textString, temp.textHeight, mainScale);
                            break;
                        }
                }
            }
            lePen.Dispose();
        }
        #region Insert class
        public class Insert
        {
            public PointF startPoint;
            public string name;
            public int shapeIdentifier;
            public Insert(PointF start, string insertname)
            {
                startPoint = start;
                name = insertname;
                shapeIdentifier = 9;
            }
        }
        #endregion
        #region Line class
        public class Line : Shape
        {
            protected Point startPoint;
            protected Point endPoint;
            public Line(Point start, Point end, Color color, int w)
            {
                startPoint = start;
                endPoint = end;
                contourColor = color;
                lineWidth = w;
                shapeIdentifier = 1;
                rotation = 0;
            }
            public Line()
            {

            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawLine(pen, startPoint, endPoint);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                //g.DrawLine(pen, startPoint, endPoint);
                float startX = (float)startPoint.X * (float)scale;
                float startY = (float)startPoint.Y * (float)scale;
                float endX = (float)endPoint.X * (float)scale;
                float endY = (float)endPoint.Y * (float)scale;
                g.DrawLine(pen, startX, startY, endX, endY);
            }
            public virtual Point GetStartPoint
            {
                get
                {
                    return startPoint;
                }
            }
            public virtual Point GetEndPoint
            {
                get
                {
                    return endPoint;
                }
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                GraphicsPath areaPath;
                Pen areaPen;
                Region areaRegion;
                // Create path which contains wide line
                // for easy mouse selection
                areaPath = new GraphicsPath();
                areaPen = new Pen(Color.Red, 7);
                try
                {
                    areaPath.AddLine(GetStartPoint.X, GetStartPoint.Y, GetEndPoint.X, GetEndPoint.Y);
                    // startPoint and EndPoint are class members of type Point
                    areaPath.Widen(areaPen);
                }
                catch
                {
                    MessageBox.Show("Nicht genügend Arbeitsspeicher!");
                    return false;
                }
                // Create region from the path
                areaRegion = new Region(areaPath);
                if (areaRegion.IsVisible(point) == true)
                {
                    //g.DrawLine(pen, GetStartPoint, GetEndPoint);
                    //g.DrawLine(pen, GetStartPoint.X, GetStartPoint.Y , GetEndPoint.X, GetEndPoint.Y);
                    areaPath.Dispose();
                    areaPen.Dispose();
                    areaRegion.Dispose();
                    return true;
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                GraphicsPath areaPath;
                Pen areaPen;
                Region areaRegion;
                // Create path which contains wide line
                // for easy mouse selection
                areaPath = new GraphicsPath();
                areaPen = new Pen(Color.Red, 7);
                try
                {
                    areaPath.AddLine((float)GetStartPoint.X * (float)scale, (float)GetStartPoint.Y * (float)scale, (float)GetEndPoint.X * (float)scale, (float)GetEndPoint.Y * (float)scale);
                    // startPoint and EndPoint are class members of type Point
                    areaPath.Widen(areaPen);
                }
                catch
                {
                    MessageBox.Show(My.MyProject.Forms.Form1.rm.GetString("String360"));
                    return false;
                }
                // Create region from the path
                try
                {
                    areaRegion = new Region(areaPath);
                }
                catch
                {
                    return false;
                }
                if (areaRegion.IsVisible(point) == true)
                {
                    //g.DrawLine(pen, GetStartPoint, GetEndPoint);
                    //g.DrawLine(pen, (float)GetStartPoint.X* (float)scale, (float)GetStartPoint.Y * (float)scale, (float)GetEndPoint.X* (float)scale, (float)GetEndPoint.Y* (float)scale);
                    areaPath.Dispose();
                    areaPen.Dispose();
                    areaRegion.Dispose();
                    return true;
                }
                areaPath.Dispose();
                areaPen.Dispose();
                areaRegion.Dispose();
                return false;
            }
        }
        public class Line1 : Shape
        {
            protected PointF startPoint;
            protected PointF endPoint;
            public Line1(PointF start, PointF end, Color color, int w)
            {
                startPoint = start;
                endPoint = end;
                contourColor = color;
                lineWidth = w;
                shapeIdentifier = 1;
                rotation = 0;
            }
            public Line1()
            {

            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawLine(pen, startPoint, endPoint);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                //g.DrawLine(pen, startPoint, endPoint);
                g.DrawLine(pen, (float)startPoint.X * (float)scale, (float)startPoint.Y * (float)scale, (float)endPoint.X * (float)scale, (float)endPoint.Y * (float)scale);
            }
            public virtual PointF GetStartPoint
            {
                get
                {
                    return startPoint;
                }
            }
            public virtual PointF GetEndPoint
            {
                get
                {
                    return endPoint;
                }
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                GraphicsPath areaPath;
                Pen areaPen;
                Region areaRegion;
                // Create path which contains wide line
                // for easy mouse selection
                areaPath = new GraphicsPath();
                areaPen = new Pen(Color.Red, 7);
                try
                {
                    areaPath.AddLine(GetStartPoint.X, GetStartPoint.Y, GetEndPoint.X, GetEndPoint.Y);
                    // startPoint and EndPoint are class members of type Point
                    areaPath.Widen(areaPen);
                }
                catch
                {
                    MessageBox.Show("Nicht genügend Arbeitsspeicher!");
                    return false;
                }
                // Create region from the path
                areaRegion = new Region(areaPath);
                if (areaRegion.IsVisible(point) == true)
                {
                    //g.DrawLine(pen, GetStartPoint, GetEndPoint);
                    //g.DrawLine(pen, GetStartPoint.X, GetStartPoint.Y , GetEndPoint.X, GetEndPoint.Y);
                    areaPath.Dispose();
                    areaPen.Dispose();
                    areaRegion.Dispose();
                    return true;
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                GraphicsPath areaPath;
                Pen areaPen;
                Region areaRegion;
                // Create path which contains wide line
                // for easy mouse selection
                areaPath = new GraphicsPath();
                areaPen = new Pen(Color.Red, 7);
                try
                {
                    areaPath.AddLine((float)GetStartPoint.X * (float)scale, (float)GetStartPoint.Y * (float)scale, (float)GetEndPoint.X * (float)scale, (float)GetEndPoint.Y * (float)scale);
                    // startPoint and EndPoint are class members of type Point
                    areaPath.Widen(areaPen);
                }
                catch
                {
                    MessageBox.Show(My.MyProject.Forms.Form1.rm.GetString("String360"));
                    return false;
                }
                // Create region from the path
                try
                {
                    areaRegion = new Region(areaPath);
                }
                catch
                {
                    return false;
                }
                if (areaRegion.IsVisible(point) == true)
                {
                    //g.DrawLine(pen, GetStartPoint, GetEndPoint);
                    //g.DrawLine(pen, (float)GetStartPoint.X* (float)scale, (float)GetStartPoint.Y * (float)scale, (float)GetEndPoint.X* (float)scale, (float)GetEndPoint.Y* (float)scale);
                    areaPath.Dispose();
                    areaPen.Dispose();
                    areaRegion.Dispose();
                    return true;
                }
                areaPath.Dispose();
                areaPen.Dispose();
                areaRegion.Dispose();
                return false;
            }
        }
        #endregion
        #region Rectangle class
        public class Rectangl : Line1
        {
            public string blockName;

            public Rectangl(PointF start, PointF end, Color color, Color fill, int w, int angle, string name)
            {
                startPoint = start;
                endPoint = end;
                contourColor = color;
                fillColor = fill;
                lineWidth = w;
                shapeIdentifier = 3;
                rotation = angle;
                blockName = name;
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                if (AccessRotation != 0)
                {
                    DrawRotatedRectangle(pen, g);
                    return;
                }
                g.DrawRectangle(pen, GetStartPoint.X, GetStartPoint.Y, (GetStartPoint.X + GetEndPoint.X), (GetStartPoint.Y + GetEndPoint.Y));
            }
            public void Draw(Pen pen, Graphics g, double scale, string block, PointF pointF, string test)
            {
                float transY = 0;
                float transX = 0;
                if (My.MyProject.Forms.Form1.YMin < 0)
                {
                    transY = -(int)Math.Abs(My.MyProject.Forms.Form1.YMin);
                }
                if (My.MyProject.Forms.Form1.XMin < 0)
                {
                    transX = (int)Math.Abs(My.MyProject.Forms.Form1.XMin);
                }
                float recStartX = GetStartPoint.X;
                float recStartY = GetStartPoint.Y;
                float recEndX = GetEndPoint.X;
                float recEndY = GetEndPoint.Y;
                if (test == block)
                {
                    recStartX = pointF.X;
                    recStartY = pointF.Y;
                    float recX = recStartX * (float)scale;
                    float recY = (recStartY + recEndY) * (float)scale;
                    float recW = recEndX * (float)scale;
                    float recH = -(recEndY * (float)scale);
                    g.DrawRectangle(pen, (int)recX, (int)recY, (int)recW, (int)recH);
                }
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
            }
            private void DrawRotatedRectangle(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                PointF P1 = GetStartPoint;
                PointF P2 = GetEndPoint;
                PointF P3 = new(P2.X, P1.Y);
                PointF P4 = new(P1.X, P2.Y);
                PointF center = new(P1.X + (P3.X - P1.X) / 2, P1.Y + (P4.Y - P1.Y) / 2);
                int angle = AccessRotation;
                if (angle != 0)
                {
                    P1 = CalculateRotatedNewPoint(P1, center, angle);   //Top left
                    P3 = CalculateRotatedNewPoint(P3, center, angle);   //Bottom right
                    P2 = CalculateRotatedNewPoint(P2, center, angle);   //Top right
                    P4 = CalculateRotatedNewPoint(P4, center, angle);   //Bottom left
                    g.DrawLine(pen, P1, P3);
                    g.DrawLine(pen, P3, P2);
                    g.DrawLine(pen, P2, P4);
                    g.DrawLine(pen, P4, P1);
                    return;
                }
            }
            private PointF CalculateRotatedNewPoint(PointF P, PointF center, int angle)
            {
                double angleRad = angle * 1 / 57.2957;
                PointF tempPoint = new(P.X - center.X, P.Y - center.Y);
                double radius = Math.Sqrt((tempPoint.X * tempPoint.X) + (tempPoint.Y * tempPoint.Y));
                double radiant1 = Math.Acos(tempPoint.X / radius);
                if (tempPoint.X < 0 && tempPoint.Y < 0)
                    radiant1 = -radiant1;
                if (tempPoint.X > 0 && tempPoint.Y < 0)
                    radiant1 = -radiant1;
                double radiant2 = Math.Asin(tempPoint.Y / radius);
                radiant1 += angleRad;
                radiant2 += angleRad;
                double temp;
                temp = radius * Math.Cos(radiant1);
                P.X = (int)temp + (int)center.X;
                temp = radius * Math.Sin(radiant1);
                P.Y = (int)temp + (int)center.Y;
                return P;
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                PointF P1 = GetStartPoint;
                PointF P2 = GetEndPoint;
                PointF P3 = new(P2.X, P1.Y);
                PointF P4 = new(P1.X, P2.Y);
                if (AccessRotation != 0)
                {
                    PointF bottom = new(0, 0);
                    PointF top = new(0, 0);
                    PointF left = new(0, 0);
                    PointF right = new(0, 0);
                    PointF center = new(P1.X + (P3.X - P1.X) / 2, P1.Y + (P4.Y - P1.Y) / 2);
                    P1 = CalculateRotatedNewPoint(P1, center, AccessRotation);
                    P2 = CalculateRotatedNewPoint(P2, center, AccessRotation);
                    P3 = CalculateRotatedNewPoint(P3, center, AccessRotation);
                    P4 = CalculateRotatedNewPoint(P4, center, AccessRotation);
                    int maxX = (int)Math.Max(P1.X, P2.X);
                    maxX = (int)Math.Max(maxX, P3.X);
                    maxX = (int)Math.Max(maxX, P4.X);
                    if (maxX == P1.X)
                        right = P1;
                    if (maxX == P2.X)
                        right = P2;
                    if (maxX == P3.X)
                        right = P3;
                    if (maxX == P4.X)
                        right = P4;
                    int minX = (int)Math.Min(P1.X, P2.X);
                    minX = (int)Math.Min(minX, P3.X);
                    minX = (int)Math.Min(minX, P4.X);
                    if (minX == P1.X)
                        left = P1;
                    if (minX == P2.X)
                        left = P2;
                    if (minX == P3.X)
                        left = P3;
                    if (minX == P4.X)
                        left = P4;
                    int maxY = (int)Math.Max(P1.Y, P2.Y);
                    maxY = (int)Math.Max(maxY, P3.Y);
                    maxY = (int)Math.Max(maxY, P4.Y);
                    if (maxY == P1.Y)
                        bottom = P1;
                    if (maxY == P2.Y)
                        bottom = P2;
                    if (maxY == P3.Y)
                        bottom = P3;
                    if (maxY == P4.Y)
                        bottom = P4;
                    int minY = (int)Math.Min(P1.Y, P2.Y);
                    minY = (int)Math.Min(minY, P3.Y);
                    minY = (int)Math.Min(minY, P4.Y);
                    if (minY == P1.Y)
                        top = P1;
                    if (minY == P2.Y)
                        top = P2;
                    if (minY == P3.Y)
                        top = P3;
                    if (minY == P4.Y)
                        top = P4;
                    double c1 = CheckPosition(left, top, point);
                    double c2 = CheckPosition(right, top, point);
                    double c3 = CheckPosition(right, bottom, point);
                    double c4 = CheckPosition(left, bottom, point);
                    if ((c1 > 0 && c2 > 0 && c3 < 0 && c4 < 0))
                    {
                        pen.Color = Color.LightGreen;
                        Draw(pen, g);
                        return true;
                    }
                }
                else
                {
                    int maxX = (int)Math.Max(P1.X, P2.X);
                    maxX = (int)Math.Max(maxX, P3.X);
                    maxX = (int)Math.Max(maxX, P4.X);
                    int minX = (int)Math.Min(P1.X, P2.X);
                    minX = (int)Math.Min(minX, P3.X);
                    minX = (int)Math.Min(minX, P4.X);
                    int maxY = (int)Math.Max(P1.Y, P2.Y);
                    maxY = (int)Math.Max(maxY, P3.Y);
                    maxY = (int)Math.Max(maxY, P4.Y);
                    int minY = (int)Math.Min(P1.Y, P2.Y);
                    minY = (int)Math.Min(minY, P3.Y);
                    minY = (int)Math.Min(minY, P4.Y);
                    if (point.X > minX && point.X < maxX && point.Y > minY && point.Y < maxY)
                    {
                        pen.Color = Color.LightGreen;
                        //	pen.Width = 1;
                        Draw(pen, g);
                        return true;
                    }
                }
                return false;
            }
            private double CheckPosition(PointF P1, PointF P2, PointF current)
            {
                double m = (double)(P2.Y - P1.Y) / (P2.X - P1.X);
                return ((current.Y - P1.Y) - (m * (current.X - P1.X)));
            }
            //public void AppendStartpoint(Line theLine)
            //{
            //    pointListStart.Add(theLine);
            //}
            //public void AppendEndpoint(Line theLine)
            //{
            //    pointListEnd.Add(theLine);
            //}
        }
        #endregion
        #region Ellipse Class
        public class Ellipse : Shape
        {
            private PointF centerPoint;
            private PointF centerPoint1;
            private double faktor;
            private double startAngle;
            private double endAngle;
            public Ellipse(PointF center, PointF center1, double r, double startangle, double endangle, Color color1, Color color2, int w)
            {
                centerPoint = center;
                centerPoint1 = center1;
                faktor = r;
                startAngle = startangle;
                endAngle = endangle;
                contourColor = color1;
                fillColor = color2;
                lineWidth = w;
                shapeIdentifier = 7;
                rotation = 0;
            }
            public double AccessStartAngle
            {
                get
                {
                    return startAngle;
                }

            }
            public double AccessEndAngle
            {
                get
                {
                    return endAngle;
                }
            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public PointF AccessCenterPoint
            {
                get
                {
                    return centerPoint;
                }
                set
                {
                    centerPoint = value;
                }
            }
            public PointF AccessCenterPoint1
            {
                get
                {
                    return centerPoint1;
                }
                set
                {
                    centerPoint1 = value;
                }
            }
            public double AccessRadius
            {
                get
                {
                    return faktor;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                PointF P1;
                float rd1, rd2;
                if (centerPoint1.X == 0)
                {
                    rd1 = (float)Math.Abs(centerPoint1.Y * faktor);
                    rd2 = centerPoint1.Y;
                }
                else
                {
                    rd1 = centerPoint1.Y;
                    rd2 = (float)Math.Abs(centerPoint1.Y * faktor);
                }
                P1 = centerPoint;
                P1.X = centerPoint.X - rd1;
                P1.Y = centerPoint.Y - rd2;
                float sA = (float)(startAngle * 180 / Math.PI);
                float eA = (float)(endAngle * 180 / Math.PI);
                eA -= sA;
                if (eA == 0) g.DrawEllipse(pen, P1.X, P1.Y, rd1 * 2, rd2 * 2);
                else g.DrawArc(pen, P1.X, P1.Y, rd1 * 2, rd2 * 2, 0, 360);
                //g.DrawEllipse(pen, centerPoint.X - (int)radius, centerPoint.Y - (int)radius, (int)radius * 2, (int)radius * 2);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                PointF P1;
                float rd1, rd2;
                if (centerPoint1.X == 0)
                {
                    rd1 = (float)Math.Abs(centerPoint1.Y * faktor);
                    rd2 = centerPoint1.Y;
                }
                else
                {
                    rd1 = centerPoint1.Y;
                    rd2 = (float)Math.Abs(centerPoint1.Y * faktor);
                }
                P1 = centerPoint;
                rd1 *= (float)scale;
                rd2 *= (float)scale;
                P1.X = (centerPoint.X - rd1) * (float)scale;
                P1.Y = (centerPoint.Y - rd2) * (float)scale;
                float sA = (float)(startAngle * 180 / Math.PI);
                float eA = (float)(endAngle * 180 / Math.PI);
                eA -= sA;
                if (eA == 0) g.DrawEllipse(pen, P1.X, P1.Y, rd1 * 2, rd2 * 2);
                else g.DrawArc(pen, P1.X, P1.Y, rd1 * 2, rd2 * 2, 0, 360);               
                //g.DrawEllipse(pen, (float)centerPoint.X * (float)scale - (float)radius * (float)scale, (float)centerPoint.Y * (float)scale - (float)radius * (float)scale, (float)radius * 2 * (float)scale, (float)radius * 2 * (float)scale);
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                PointF center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = (int)center.Y - rad;
                int check2y = (int)center.Y + rad;
                int check3x = (int)center.X + rad;
                int check4x = (int)center.X - rad;
                double result = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y) - faktor * faktor;
                if (result < 0)
                {
                    //pen.Color = Color.Red;
                    //g.DrawEllipse(pen, centerPoint.X - (int) radius, centerPoint.Y - (int)radius, (int)radius*2, (int)radius*2);
                    return true;
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                PointF center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = (int)center.Y - rad;
                int check2y = (int)center.Y + rad;
                int check3x = (int)center.X + rad;
                int check4x = (int)center.X - rad;
                double result = (point.X - center.X * (float)scale) * (point.X - center.X * (float)scale) + (point.Y - center.Y * (float)scale) * (point.Y - center.Y * (float)scale) - faktor * faktor * (float)scale * (float)scale;
                if (result < 0)
                {
                    //pen.Color = Color.Red;
                    //g.DrawEllipse(pen, (float)centerPoint.X*(float)scale - (float) radius*(float)scale, (float)centerPoint.Y*(float)scale - (float)radius*(float)scale, (float)radius*2*(float)scale, (float)radius*2*(float)scale);
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region Circle Class
        public class Circle : Shape
        {
            private Point centerPoint;
            private double radius;
            public Circle(Point center, double r, Color color1, Color color2, int w)
            {
                centerPoint = center;
                radius = r;
                contourColor = color1;
                fillColor = color2;
                lineWidth = w;
                shapeIdentifier = 3;
                rotation = 0;
            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public Point AccessCenterPoint
            {
                get
                {
                    return centerPoint;
                }
                set
                {
                    centerPoint = value;
                }
            }
            public double AccessRadius
            {
                get
                {
                    return radius;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawEllipse(pen, centerPoint.X - (int)radius, centerPoint.Y - (int)radius, (int)radius * 2, (int)radius * 2);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawEllipse(pen, (float)centerPoint.X * (float)scale - (float)radius * (float)scale, (float)centerPoint.Y * (float)scale - (float)radius * (float)scale, (float)radius * 2 * (float)scale, (float)radius * 2 * (float)scale);
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                Point center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = center.Y - rad;
                int check2y = center.Y + rad;
                int check3x = center.X + rad;
                int check4x = center.X - rad;
                double result = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y) - radius * radius;

                if (result < 0)
                {
                    //pen.Color = Color.Red;
                    //g.DrawEllipse(pen, centerPoint.X - (int) radius, centerPoint.Y - (int)radius, (int)radius*2, (int)radius*2);
                    return true;
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                Point center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = center.Y - rad;
                int check2y = center.Y + rad;
                int check3x = center.X + rad;
                int check4x = center.X - rad;
                double result = (point.X - center.X * (float)scale) * (point.X - center.X * (float)scale) + (point.Y - center.Y * (float)scale) * (point.Y - center.Y * (float)scale) - radius * radius * (float)scale * (float)scale;
                if (result < 0)
                {
                    //pen.Color = Color.Red;
                    //g.DrawEllipse(pen, (float)centerPoint.X*(float)scale - (float) radius*(float)scale, (float)centerPoint.Y*(float)scale - (float)radius*(float)scale, (float)radius*2*(float)scale, (float)radius*2*(float)scale);
                    return true;
                }
                return false;
            }
        }
        public class Circle1 : Shape
        {
            private PointF centerPoint;
            private double radius;
            public Circle1(PointF center, double r, Color color1, Color color2, int w)
            {
                centerPoint = center;
                radius = r;
                contourColor = color1;
                fillColor = color2;
                lineWidth = w;
                shapeIdentifier = 3;
                rotation = 0;
            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public PointF AccessCenterPoint
            {
                get
                {
                    return centerPoint;
                }
                set
                {
                    centerPoint = value;
                }
            }
            public double AccessRadius
            {
                get
                {
                    return radius;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawEllipse(pen, centerPoint.X - (int)radius, centerPoint.Y - (int)radius, (int)radius * 2, (int)radius * 2);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawEllipse(pen, (float)centerPoint.X * (float)scale - (float)radius * (float)scale, (float)centerPoint.Y * (float)scale - (float)radius * (float)scale, (float)radius * 2 * (float)scale, (float)radius * 2 * (float)scale);
            }
            //public override bool Highlight(Pen pen, Graphics g, PointF point)
            //{
            //    PointF center = AccessCenterPoint;
            //    int rad = (int)AccessRadius;
            //    int check1y = (int)center.Y - rad;
            //    int check2y = (int)center.Y + rad;
            //    int check3x = (int)center.X + rad;
            //    int check4x = (int)center.X - rad;
            //    double result = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y) - radius * radius;
            //    if (result < 0)
            //    {
            //        //pen.Color = Color.Red;
            //        //g.DrawEllipse(pen, centerPoint.X - (int) radius, centerPoint.Y - (int)radius, (int)radius*2, (int)radius*2);
            //        return true;
            //    }
            //    return false;
            //}
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                PointF center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = (int)center.Y - rad;
                int check2y = (int)center.Y + rad;
                int check3x = (int)center.X + rad;
                int check4x = (int)center.X - rad;
                double result = (point.X - center.X * (float)scale) * (point.X - center.X * (float)scale) + (point.Y - center.Y * (float)scale) * (point.Y - center.Y * (float)scale) - radius * radius * (float)scale * (float)scale;
                if (result < 0)
                {
                    //pen.Color = Color.Red;
                    //g.DrawEllipse(pen, (float)centerPoint.X*(float)scale - (float) radius*(float)scale, (float)centerPoint.Y*(float)scale - (float)radius*(float)scale, (float)radius*2*(float)scale, (float)radius*2*(float)scale);
                    return true;
                }
                return false;
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region Polyline Class
        public class Polyline : Shape
        {
            public ArrayList listOfLines;
            public ArrayList listOfLines1;
            public Polyline(Color color, int w)
            {
                listOfLines = [];
                listOfLines1 = [];
                contourColor = color;
                lineWidth = w;
            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                foreach (Line obj in listOfLines)
                {
                    obj.Draw(pen, g);
                }
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                foreach (Line obj in listOfLines)
                {
                    obj.Draw(pen, g, scale);
                }
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                foreach (Line obj in listOfLines)
                {
                    //obj.Draw(pen, g);
                    if (obj.Highlight(pen, g, point))
                    {
                        //pen.Color = Color.Red;
                        //Draw(pen, g);
                        return true;
                    }
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                foreach (Line obj in listOfLines)
                {
                    //obj.Draw(pen, g);

                    if (obj.Highlight(pen, g, point, scale))
                    {
                        //pen.Color = Color.Red;
                        //Draw(pen, g, scale);
                        return true;
                    }
                }
                return false;
            }
            public void AppendLine(Line theLine)
            {
                listOfLines.Add(theLine);
            }
            public void AppendLine1(Line1 theLine1)
            {
                listOfLines1.Add(theLine1);
            }
        }
        #endregion
        #region Arc Class
        public class Arc : Shape
        {
            private Point centerPoint;
            private double radius;
            private double startAngle;
            private double sweepAngle;
            public Arc(Point center, double r, double startangle, double sweepangle, Color color1, Color color2, int w)
            {
                centerPoint = center;
                radius = r;
                startAngle = startangle;
                sweepAngle = sweepangle;
                contourColor = color1;
                fillColor = color2;
                lineWidth = w;
                shapeIdentifier = 3;
                rotation = 0;
            }
            public double AccessStartAngle
            {
                get
                {
                    return startAngle;
                }

            }
            public double AccessSweepAngle
            {
                get
                {
                    return sweepAngle;
                }
            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public Point AccessCenterPoint
            {
                get
                {
                    return centerPoint;
                }
                set
                {
                    centerPoint = value;
                }
            }
            public double AccessRadius
            {
                get
                {
                    return radius;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawArc(pen, (float)centerPoint.X - (float)radius, (float)centerPoint.Y - (float)radius, (float)radius * 2, (float)radius * 2, -(float)startAngle, -360 + (float)startAngle - (float)sweepAngle);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                //g.DrawEllipse(pen, (float) centerPoint.X* (float)scale - (float) radius* (float)scale, (float)centerPoint.Y * (float)scale - (float)radius* (float)scale, (float)radius*2* (float)scale, (float)radius*2* (float)scale);

                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                float tempAngle = 0;
                if (sweepAngle < startAngle)
                {
                    tempAngle = -360 + (float)startAngle - (float)sweepAngle;
                }
                /*else if (startAngle > 180 && sweepAngle > 180)
                {
                    tempAngle = startAngle - sweepAngle;
                }*/
                else
                    tempAngle = (float)startAngle - (float)sweepAngle;
                g.DrawArc(pen, (float)centerPoint.X * (float)scale - (float)radius * (float)scale, (float)centerPoint.Y * (float)scale - (float)radius * (float)scale, (float)radius * 2 * (float)scale, (float)radius * 2 * (float)scale, -(float)startAngle, tempAngle);
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                Point center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = center.Y - rad;
                int check2y = center.Y + rad;
                int check3x = center.X + rad;
                int check4x = center.X - rad;
                double result = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y) - radius * radius;
                if (result < 0)
                {
                    //pen.Color = Color.Yellow;
                    //g.DrawEllipse(pen, centerPoint.X - (int) radius, centerPoint.Y - (int)radius, (int)radius*2, (int)radius*2);
                    return true;
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                Point center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = center.Y - rad;
                int check2y = center.Y + rad;
                int check3x = center.X + rad;
                int check4x = center.X - rad;
                double result = (point.X - center.X * (float)scale) * (point.X - center.X * (float)scale) + (point.Y - center.Y * (float)scale) * (point.Y - center.Y * (float)scale) - radius * radius * (float)scale * (float)scale;
                if (result < 0)
                {
                    //pen.Color = Color.Yellow;
                    float tempAngle = 0;
                    if (sweepAngle < startAngle)
                        tempAngle = -360 + (float)startAngle - (float)sweepAngle;
                    else
                        tempAngle = (float)startAngle - (float)sweepAngle;
                    //g.DrawArc(pen, (float)centerPoint.X*(float)scale - (float) radius*(float)scale, (float)centerPoint.Y*(float)scale - (float)radius*(float)scale, (float)radius*2*(float)scale, (float)radius*2*(float)scale, -startAngle, tempAngle);
                    return true;
                }
                return false;
            }
        }
        public class Arc1 : Shape
        {
            private PointF centerPoint;
            private double radius;
            private double startAngle;
            private double sweepAngle;
            public Arc1(PointF center, double r, double startangle, double sweepangle, Color color1, Color color2, int w)
            {
                centerPoint = center;
                radius = r;
                startAngle = startangle;
                sweepAngle = sweepangle;
                contourColor = color1;
                fillColor = color2;
                lineWidth = w;
                shapeIdentifier = 3;
                rotation = 0;
            }
            public double AccessStartAngle
            {
                get
                {
                    return startAngle;
                }

            }
            public double AccessSweepAngle
            {
                get
                {
                    return sweepAngle;
                }
            }
            public override Color AccessContourColor
            {
                get
                {
                    return contourColor;
                }
                set
                {
                    contourColor = value;
                }
            }
            public override Color AccessFillColor
            {
                get
                {
                    return fillColor;
                }
                set
                {
                    fillColor = value;
                }
            }
            public override int AccessLineWidth
            {
                get
                {
                    return lineWidth;
                }
                set
                {
                    lineWidth = value;
                }

            }
            public override int AccessRotation
            {
                get
                {
                    return rotation;
                }
                set
                {
                    rotation = value;
                }
            }
            public PointF AccessCenterPoint
            {
                get
                {
                    return centerPoint;
                }
                set
                {
                    centerPoint = value;
                }
            }
            public double AccessRadius
            {
                get
                {
                    return radius;
                }
            }
            public override void Draw(Pen pen, Graphics g)
            {
                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                g.DrawArc(pen, (float)centerPoint.X - (float)radius, (float)centerPoint.Y - (float)radius, (float)radius * 2, (float)radius * 2, -(float)startAngle, -360 + (float)startAngle - (float)sweepAngle);
            }
            public void Draw(Pen pen, Graphics g, double scale)
            {
                //g.DrawEllipse(pen, (float) centerPoint.X* (float)scale - (float) radius* (float)scale, (float)centerPoint.Y * (float)scale - (float)radius* (float)scale, (float)radius*2* (float)scale, (float)radius*2* (float)scale);

                if (highlighted)
                {
                    pen.Color = Color.Red;
                    highlighted = false;
                }
                float tempAngle = 0;
                if (sweepAngle < startAngle)
                {
                    tempAngle = -360 + (float)startAngle - (float)sweepAngle;
                }
                /*else if (startAngle > 180 && sweepAngle > 180)
                {
                    tempAngle = startAngle - sweepAngle;
                }*/
                else
                    tempAngle = (float)startAngle - (float)sweepAngle;
                    g.DrawArc(pen, (float)centerPoint.X * (float)scale - (float)radius * (float)scale, (float)centerPoint.Y * (float)scale - (float)radius * (float)scale, (float)radius * 2 * (float)scale, (float)radius * 2 * (float)scale, -(float)startAngle, tempAngle);
            }
            public override bool Highlight(Pen pen, Graphics g, Point point)
            {
                PointF center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = (int)center.Y - rad;
                int check2y = (int)center.Y + rad;
                int check3x = (int)center.X + rad;
                int check4x = (int)center.X - rad;
                double result = (point.X - center.X) * (point.X - center.X) + (point.Y - center.Y) * (point.Y - center.Y) - radius * radius;
                if (result < 0)
                {
                    //pen.Color = Color.Yellow;
                    //g.DrawEllipse(pen, centerPoint.X - (int) radius, centerPoint.Y - (int)radius, (int)radius*2, (int)radius*2);
                    return true;
                }
                return false;
            }
            public bool Highlight(Pen pen, Graphics g, Point point, double scale)
            {
                PointF center = AccessCenterPoint;
                int rad = (int)AccessRadius;
                int check1y = (int)center.Y - rad;
                int check2y = (int)center.Y + rad;
                int check3x = (int)center.X + rad;
                int check4x = (int)center.X - rad;
                double result = (point.X - center.X * (float)scale) * (point.X - center.X * (float)scale) + (point.Y - center.Y * (float)scale) * (point.Y - center.Y * (float)scale) - radius * radius * (float)scale * (float)scale;
                if (result < 0)
                {
                    //pen.Color = Color.Yellow;
                    float tempAngle = 0;
                    if (sweepAngle < startAngle)
                        tempAngle = -360 + (float)startAngle - (float)sweepAngle;
                    else
                        tempAngle = (float)startAngle - (float)sweepAngle;
                    //g.DrawArc(pen, (float)centerPoint.X*(float)scale - (float) radius*(float)scale, (float)centerPoint.Y*(float)scale - (float)radius*(float)scale, (float)radius*2*(float)scale, (float)radius*2*(float)scale, -startAngle, tempAngle);
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region Text class
        public class Texte
        {
            public PointF startPoint;
            public double textHeight;
            public string textString;
            public int shapeIdentifier;
            public Texte(PointF start, double texth,string textname)
            {
                startPoint = start;
                textHeight = texth;
                textString = textname;
                shapeIdentifier = 8;
            }
            public void Draw(Graphics g, string text, double hight, double scale)
            {
                Font drawFont = new("Arial", 8);
                SolidBrush drawBrush = new(Color.Black);
                PointF drawPoint = new((float)startPoint.X * (float)scale, ((float)startPoint.Y - (float)hight) * (float)scale);
                g.DrawString(text, drawFont, drawBrush, drawPoint);
            }
            public virtual PointF GetStartPoint
            {
                get
                {
                    return startPoint;
                }
            }
        }
        #endregion
        #endregion
    }
}