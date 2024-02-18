using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NC_Tool
{
    [Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
    public partial class Form2 : Form
    {
        //My.MyProject.Forms.Form1.rm.GetString("String254")
        // Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is not null)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Wird vom Windows Form-Designer benötigt.
        private System.ComponentModel.IContainer components;

        // Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
        // Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
        // Das Bearbeiten mit dem Code-Editor ist nicht möglich.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this._Ausgabe = new System.Windows.Forms.TextBox();
            this.speichern = new System.Windows.Forms.Button();
            this.schließen = new System.Windows.Forms.Button();
            this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this._RectangleShape4 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this._RectangleShape3 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this._RectangleShape2 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Label3 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.ShapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.Zeichenfeld = new System.Windows.Forms.Panel();
            this.drucken = new System.Windows.Forms.Button();
            this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
            this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
            this.PrintPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.Vorschau = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.CNC_Info = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Editor = new System.Windows.Forms.RichTextBox();
            this.Editor_ZN = new System.Windows.Forms.RichTextBox();
            this.ZN = new System.Windows.Forms.CheckBox();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Ausgabe
            // 
            this._Ausgabe.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Ausgabe.HideSelection = false;
            this._Ausgabe.Location = new System.Drawing.Point(5, 54);
            this._Ausgabe.Multiline = true;
            this._Ausgabe.Name = "_Ausgabe";
            this._Ausgabe.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._Ausgabe.Size = new System.Drawing.Size(469, 610);
            this._Ausgabe.TabIndex = 0;
            this._Ausgabe.Visible = false;
            this._Ausgabe.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Ausgabe_MouseClick);
            this._Ausgabe.TextChanged += new System.EventHandler(this.Ausgabe_TextChanged);
            this._Ausgabe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ausgabe_KeyPress);
            // 
            // speichern
            // 
            this.speichern.Location = new System.Drawing.Point(5, 670);
            this.speichern.Name = "speichern";
            this.speichern.Size = new System.Drawing.Size(147, 23);
            this.speichern.TabIndex = 1;
            this.speichern.UseVisualStyleBackColor = true;
            this.speichern.Click += new System.EventHandler(this.Speichern_Click);
            // 
            // schließen
            // 
            this.schließen.Location = new System.Drawing.Point(820, 670);
            this.schließen.Name = "schließen";
            this.schließen.Size = new System.Drawing.Size(154, 23);
            this.schließen.TabIndex = 2;
            this.schließen.UseVisualStyleBackColor = true;
            this.schließen.Click += new System.EventHandler(this.Schließen_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(8, 9);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(0, 13);
            this.Label2.TabIndex = 9;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(487, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(0, 13);
            this.Label1.TabIndex = 10;
            // 
            // _RectangleShape4
            // 
            this._RectangleShape4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._RectangleShape4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this._RectangleShape4.Cursor = System.Windows.Forms.Cursors.Hand;
            this._RectangleShape4.Location = new System.Drawing.Point(53, 35);
            this._RectangleShape4.Name = "_RectangleShape4";
            this._RectangleShape4.SelectionColor = System.Drawing.Color.Transparent;
            this._RectangleShape4.Size = new System.Drawing.Size(42, 41);
            this._RectangleShape4.Click += new System.EventHandler(this.RectangleShape4_Click);
            this._RectangleShape4.MouseHover += new System.EventHandler(this.RectangleShape4_MouseHover);
            // 
            // _RectangleShape3
            // 
            this._RectangleShape3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._RectangleShape3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this._RectangleShape3.Cursor = System.Windows.Forms.Cursors.Hand;
            this._RectangleShape3.Location = new System.Drawing.Point(2, 37);
            this._RectangleShape3.Name = "_RectangleShape3";
            this._RectangleShape3.SelectionColor = System.Drawing.Color.Transparent;
            this._RectangleShape3.Size = new System.Drawing.Size(45, 40);
            this._RectangleShape3.Click += new System.EventHandler(this.RectangleShape3_Click);
            this._RectangleShape3.MouseHover += new System.EventHandler(this.RectangleShape3_MouseHover);
            // 
            // _RectangleShape2
            // 
            this._RectangleShape2.AccessibleDescription = "";
            this._RectangleShape2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this._RectangleShape2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            this._RectangleShape2.Cursor = System.Windows.Forms.Cursors.Hand;
            this._RectangleShape2.Location = new System.Drawing.Point(26, 5);
            this._RectangleShape2.Name = "_RectangleShape2";
            this._RectangleShape2.SelectionColor = System.Drawing.Color.Transparent;
            this._RectangleShape2.Size = new System.Drawing.Size(47, 33);
            this._RectangleShape2.Click += new System.EventHandler(this.RectangleShape2_Click);
            this._RectangleShape2.MouseHover += new System.EventHandler(this.RectangleShape2_MouseHover);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(485, 542);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(0, 13);
            this.Label3.TabIndex = 3;
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel1
            // 
            this.Panel1.BackgroundImage = global::NC_Tool.My.Resources.Resources.Ansicht;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.ShapeContainer2);
            this.Panel1.Location = new System.Drawing.Point(496, 567);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(100, 80);
            this.Panel1.TabIndex = 13;
            // 
            // ShapeContainer2
            // 
            this.ShapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.ShapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.ShapeContainer2.Name = "ShapeContainer2";
            this.ShapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this._RectangleShape3,
            this._RectangleShape4,
            this._RectangleShape2});
            this.ShapeContainer2.Size = new System.Drawing.Size(98, 78);
            this.ShapeContainer2.TabIndex = 0;
            this.ShapeContainer2.TabStop = false;
            // 
            // Zeichenfeld
            // 
            this.Zeichenfeld.BackColor = System.Drawing.Color.Black;
            this.Zeichenfeld.Location = new System.Drawing.Point(480, 30);
            this.Zeichenfeld.Name = "Zeichenfeld";
            this.Zeichenfeld.Size = new System.Drawing.Size(500, 500);
            this.Zeichenfeld.TabIndex = 14;
            this.Zeichenfeld.Paint += new System.Windows.Forms.PaintEventHandler(this.Zeichenfeld_Paint);
            // 
            // drucken
            // 
            this.drucken.Location = new System.Drawing.Point(350, 670);
            this.drucken.Name = "drucken";
            this.drucken.Size = new System.Drawing.Size(124, 23);
            this.drucken.TabIndex = 15;
            this.drucken.UseVisualStyleBackColor = true;
            this.drucken.Click += new System.EventHandler(this.Drucken_Click);
            // 
            // PrintDialog1
            // 
            this.PrintDialog1.UseEXDialog = true;
            // 
            // PrintDocument1
            // 
            this.PrintDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // PrintPreviewDialog1
            // 
            this.PrintPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintPreviewDialog1.Enabled = true;
            this.PrintPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreviewDialog1.Icon")));
            this.PrintPreviewDialog1.Name = "PrintPreviewDialog1";
            this.PrintPreviewDialog1.ShowIcon = false;
            this.PrintPreviewDialog1.Visible = false;
            // 
            // Vorschau
            // 
            this.Vorschau.Location = new System.Drawing.Point(223, 670);
            this.Vorschau.Name = "Vorschau";
            this.Vorschau.Size = new System.Drawing.Size(124, 23);
            this.Vorschau.TabIndex = 16;
            this.Vorschau.UseVisualStyleBackColor = true;
            this.Vorschau.Click += new System.EventHandler(this.Vorschau_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.Location = new System.Drawing.Point(624, 542);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(33, 13);
            this.Label5.TabIndex = 18;
            this.Label5.Text = "Info:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CNC_Info
            // 
            this.CNC_Info.Location = new System.Drawing.Point(627, 567);
            this.CNC_Info.Multiline = true;
            this.CNC_Info.Name = "CNC_Info";
            this.CNC_Info.ReadOnly = true;
            this.CNC_Info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CNC_Info.Size = new System.Drawing.Size(347, 97);
            this.CNC_Info.TabIndex = 19;
            // 
            // Editor
            // 
            this.Editor.BackColor = System.Drawing.Color.White;
            this.Editor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor.Location = new System.Drawing.Point(41, 54);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(433, 610);
            this.Editor.TabIndex = 20;
            this.Editor.Text = "";
            this.Editor.VScroll += new System.EventHandler(this.Editor_VScroll);
            this.Editor.TextChanged += new System.EventHandler(this.Editor_TextChanged);
            // 
            // Editor_ZN
            // 
            this.Editor_ZN.BackColor = System.Drawing.Color.White;
            this.Editor_ZN.Enabled = false;
            this.Editor_ZN.ForeColor = System.Drawing.Color.Black;
            this.Editor_ZN.Location = new System.Drawing.Point(5, 54);
            this.Editor_ZN.Name = "Editor_ZN";
            this.Editor_ZN.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.Editor_ZN.Size = new System.Drawing.Size(60, 610);
            this.Editor_ZN.TabIndex = 21;
            this.Editor_ZN.Text = "";
            // 
            // ZN
            // 
            this.ZN.AutoSize = true;
            this.ZN.Checked = true;
            this.ZN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ZN.Location = new System.Drawing.Point(11, 32);
            this.ZN.Name = "ZN";
            this.ZN.Size = new System.Drawing.Size(15, 14);
            this.ZN.TabIndex = 22;
            this.ZN.UseVisualStyleBackColor = true;
            this.ZN.CheckedChanged += new System.EventHandler(this.ZN_CheckedChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(986, 705);
            this.Controls.Add(this.ZN);
            this.Controls.Add(this.Editor);
            this.Controls.Add(this.Editor_ZN);
            this.Controls.Add(this.CNC_Info);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Vorschau);
            this.Controls.Add(this.drucken);
            this.Controls.Add(this.Zeichenfeld);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.schließen);
            this.Controls.Add(this.speichern);
            this.Controls.Add(this._Ausgabe);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal Button speichern;
        internal Button schließen;
        internal SaveFileDialog SaveFileDialog1;
        internal Label Label2;
        internal Label Label1;
        internal Label Label3;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape _RectangleShape4;

        public virtual Microsoft.VisualBasic.PowerPacks.RectangleShape RectangleShape4
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RectangleShape4;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RectangleShape4 != null)
                {
                    _RectangleShape4.Click -= RectangleShape4_Click;
                    _RectangleShape4.MouseHover -= RectangleShape4_MouseHover;
                }

                _RectangleShape4 = value;
                if (_RectangleShape4 != null)
                {
                    _RectangleShape4.Click += RectangleShape4_Click;
                    _RectangleShape4.MouseHover += RectangleShape4_MouseHover;
                }
            }
        }
        private Microsoft.VisualBasic.PowerPacks.RectangleShape _RectangleShape3;

        public virtual Microsoft.VisualBasic.PowerPacks.RectangleShape RectangleShape3
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RectangleShape3;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RectangleShape3 != null)
                {
                    _RectangleShape3.Click -= RectangleShape3_Click;
                    _RectangleShape3.MouseHover -= RectangleShape3_MouseHover;
                }

                _RectangleShape3 = value;
                if (_RectangleShape3 != null)
                {
                    _RectangleShape3.Click += RectangleShape3_Click;
                    _RectangleShape3.MouseHover += RectangleShape3_MouseHover;
                }
            }
        }
        private Microsoft.VisualBasic.PowerPacks.RectangleShape _RectangleShape2;

        public virtual Microsoft.VisualBasic.PowerPacks.RectangleShape RectangleShape2
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _RectangleShape2;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_RectangleShape2 != null)
                {
                    _RectangleShape2.Click -= RectangleShape2_Click;
                    _RectangleShape2.MouseHover -= RectangleShape2_MouseHover;
                }

                _RectangleShape2 = value;
                if (_RectangleShape2 != null)
                {
                    _RectangleShape2.Click += RectangleShape2_Click;
                    _RectangleShape2.MouseHover += RectangleShape2_MouseHover;
                }
            }
        }
        internal Panel Panel1;
        internal Microsoft.VisualBasic.PowerPacks.ShapeContainer ShapeContainer2;
        internal Panel Zeichenfeld;
        internal Button drucken;
        internal PrintDialog PrintDialog1;
        internal System.Drawing.Printing.PrintDocument PrintDocument1;
        internal PrintPreviewDialog PrintPreviewDialog1;
        internal Button Vorschau;
        internal Label Label5;
        internal TextBox CNC_Info;
        internal ToolTip ToolTip1;
        private TextBox _Ausgabe;
        private RichTextBox Editor;
        private RichTextBox Editor_ZN;
        private CheckBox ZN;

        public virtual TextBox Ausgabe
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return _Ausgabe;
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (_Ausgabe != null)
                {
                    _Ausgabe.TextChanged -= Ausgabe_TextChanged;
                }

                _Ausgabe = value;
                if (_Ausgabe != null)
                {
                    _Ausgabe.TextChanged += Ausgabe_TextChanged;
                }
            }
        }
    }
}