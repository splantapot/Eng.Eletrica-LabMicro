/*
 * SharpDevelop
 * Usuário: Tiago
 * Data: 23/09/2013
 * Hora: 17:03
 */
namespace HIDTerminal
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.DefinirPIDeVID = new System.Windows.Forms.Button();
			this.vidNumber = new System.Windows.Forms.NumericUpDown();
			this.pidNumber = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.sendHexNumber = new System.Windows.Forms.RadioButton();
			this.sendString = new System.Windows.Forms.RadioButton();
			this.sendNumber = new System.Windows.Forms.RadioButton();
			this.Enviar = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.minBufferLength = new System.Windows.Forms.NumericUpDown();
			this.maxBufferLength = new System.Windows.Forms.NumericUpDown();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.Limpar = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.DecFormat = new System.Windows.Forms.RadioButton();
			this.HexFormat = new System.Windows.Forms.RadioButton();
			this.ASCIIFormat = new System.Windows.Forms.RadioButton();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.Status = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.vidNumber)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pidNumber)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.minBufferLength)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.maxBufferLength)).BeginInit();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.DefinirPIDeVID);
			this.groupBox1.Controls.Add(this.vidNumber);
			this.groupBox1.Controls.Add(this.pidNumber);
			this.groupBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(2, 5);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(382, 59);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Device";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label2.Location = new System.Drawing.Point(151, 28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 19);
			this.label2.TabIndex = 4;
			this.label2.Text = "VID";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.label1.Location = new System.Drawing.Point(13, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 19);
			this.label1.TabIndex = 3;
			this.label1.Text = "PID";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DefinirPIDeVID
			// 
			this.DefinirPIDeVID.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DefinirPIDeVID.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DefinirPIDeVID.Location = new System.Drawing.Point(296, 19);
			this.DefinirPIDeVID.Name = "DefinirPIDeVID";
			this.DefinirPIDeVID.Size = new System.Drawing.Size(75, 33);
			this.DefinirPIDeVID.TabIndex = 2;
			this.DefinirPIDeVID.Text = "DEFINIR";
			this.DefinirPIDeVID.UseVisualStyleBackColor = true;
			this.DefinirPIDeVID.Click += new System.EventHandler(this.DefinirPIDeVIDClick);
			// 
			// vidNumber
			// 
			this.vidNumber.BackColor = System.Drawing.SystemColors.Info;
			this.vidNumber.Location = new System.Drawing.Point(202, 27);
			this.vidNumber.Maximum = new decimal(new int[] {
									65535,
									0,
									0,
									0});
			this.vidNumber.Name = "vidNumber";
			this.vidNumber.Size = new System.Drawing.Size(73, 20);
			this.vidNumber.TabIndex = 1;
			this.toolTip1.SetToolTip(this.vidNumber, "VID");
			this.vidNumber.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// pidNumber
			// 
			this.pidNumber.BackColor = System.Drawing.SystemColors.Info;
			this.pidNumber.ForeColor = System.Drawing.SystemColors.WindowText;
			this.pidNumber.Location = new System.Drawing.Point(64, 27);
			this.pidNumber.Maximum = new decimal(new int[] {
									65535,
									0,
									0,
									0});
			this.pidNumber.Name = "pidNumber";
			this.pidNumber.Size = new System.Drawing.Size(73, 20);
			this.pidNumber.TabIndex = 0;
			this.toolTip1.SetToolTip(this.pidNumber, "PID");
			this.pidNumber.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.Transparent;
			this.groupBox2.Controls.Add(this.sendHexNumber);
			this.groupBox2.Controls.Add(this.sendString);
			this.groupBox2.Controls.Add(this.sendNumber);
			this.groupBox2.Controls.Add(this.Enviar);
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(5, 70);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(379, 84);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Enviar";
			// 
			// sendHexNumber
			// 
			this.sendHexNumber.Cursor = System.Windows.Forms.Cursors.Hand;
			this.sendHexNumber.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sendHexNumber.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.sendHexNumber.Location = new System.Drawing.Point(77, 28);
			this.sendHexNumber.Name = "sendHexNumber";
			this.sendHexNumber.Size = new System.Drawing.Size(99, 24);
			this.sendHexNumber.TabIndex = 5;
			this.sendHexNumber.Text = "Hex Number";
			this.toolTip1.SetToolTip(this.sendHexNumber, "Separe os números com espaços");
			this.sendHexNumber.UseVisualStyleBackColor = true;
			this.sendHexNumber.CheckedChanged += new System.EventHandler(this.SendHexNumberCheckedChanged);
			// 
			// sendString
			// 
			this.sendString.Checked = true;
			this.sendString.Cursor = System.Windows.Forms.Cursors.Hand;
			this.sendString.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sendString.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.sendString.Location = new System.Drawing.Point(182, 28);
			this.sendString.Name = "sendString";
			this.sendString.Size = new System.Drawing.Size(59, 24);
			this.sendString.TabIndex = 4;
			this.sendString.TabStop = true;
			this.sendString.Text = "String";
			this.sendString.UseVisualStyleBackColor = true;
			this.sendString.CheckedChanged += new System.EventHandler(this.SendStringCheckedChanged);
			// 
			// sendNumber
			// 
			this.sendNumber.Cursor = System.Windows.Forms.Cursors.Hand;
			this.sendNumber.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.sendNumber.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.sendNumber.Location = new System.Drawing.Point(10, 28);
			this.sendNumber.Name = "sendNumber";
			this.sendNumber.Size = new System.Drawing.Size(71, 24);
			this.sendNumber.TabIndex = 3;
			this.sendNumber.Text = "Number";
			this.toolTip1.SetToolTip(this.sendNumber, "Separe os números com espaços");
			this.sendNumber.UseVisualStyleBackColor = true;
			this.sendNumber.CheckedChanged += new System.EventHandler(this.SendNumberCheckedChanged);
			// 
			// Enviar
			// 
			this.Enviar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Enviar.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Enviar.Location = new System.Drawing.Point(311, 56);
			this.Enviar.Name = "Enviar";
			this.Enviar.Size = new System.Drawing.Size(64, 24);
			this.Enviar.TabIndex = 1;
			this.Enviar.Text = "ENVIAR";
			this.Enviar.UseVisualStyleBackColor = true;
			this.Enviar.Click += new System.EventHandler(this.EnviarClick);
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(10, 58);
			this.textBox1.MaxLength = 64;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(277, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1KeyDown);
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.Color.Transparent;
			this.groupBox3.Controls.Add(this.minBufferLength);
			this.groupBox3.Controls.Add(this.maxBufferLength);
			this.groupBox3.Controls.Add(this.checkBox2);
			this.groupBox3.Controls.Add(this.richTextBox1);
			this.groupBox3.Controls.Add(this.checkBox1);
			this.groupBox3.Controls.Add(this.Limpar);
			this.groupBox3.Controls.Add(this.groupBox4);
			this.groupBox3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(4, 160);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(379, 299);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Receber";
			// 
			// minBufferLength
			// 
			this.minBufferLength.BackColor = System.Drawing.SystemColors.Info;
			this.minBufferLength.ForeColor = System.Drawing.SystemColors.WindowText;
			this.minBufferLength.Location = new System.Drawing.Point(294, 19);
			this.minBufferLength.Maximum = new decimal(new int[] {
									63,
									0,
									0,
									0});
			this.minBufferLength.Name = "minBufferLength";
			this.minBufferLength.Size = new System.Drawing.Size(39, 20);
			this.minBufferLength.TabIndex = 6;
			this.toolTip1.SetToolTip(this.minBufferLength, "Exibir Bytes");
			this.minBufferLength.ValueChanged += new System.EventHandler(this.MinBufferLengthValueChanged);
			// 
			// maxBufferLength
			// 
			this.maxBufferLength.BackColor = System.Drawing.SystemColors.Info;
			this.maxBufferLength.ForeColor = System.Drawing.SystemColors.WindowText;
			this.maxBufferLength.Location = new System.Drawing.Point(335, 19);
			this.maxBufferLength.Maximum = new decimal(new int[] {
									63,
									0,
									0,
									0});
			this.maxBufferLength.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.maxBufferLength.Name = "maxBufferLength";
			this.maxBufferLength.Size = new System.Drawing.Size(38, 20);
			this.maxBufferLength.TabIndex = 5;
			this.toolTip1.SetToolTip(this.maxBufferLength, "Exibir Bytes");
			this.maxBufferLength.Value = new decimal(new int[] {
									63,
									0,
									0,
									0});
			this.maxBufferLength.ValueChanged += new System.EventHandler(this.MaxBufferLengthValueChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBox2.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.checkBox2.Location = new System.Drawing.Point(215, 39);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(73, 24);
			this.checkBox2.TabIndex = 5;
			this.checkBox2.Text = "AUTO SCROLL";
			this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox2CheckedChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBox1.DetectUrls = false;
			this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTextBox1.HideSelection = false;
			this.richTextBox1.Location = new System.Drawing.Point(11, 73);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(362, 220);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "";
			// 
			// checkBox1
			// 
			this.checkBox1.Checked = true;
			this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.checkBox1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.checkBox1.Location = new System.Drawing.Point(215, 21);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(73, 20);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "AUTO LIMPAR";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// Limpar
			// 
			this.Limpar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.Limpar.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Limpar.Location = new System.Drawing.Point(294, 41);
			this.Limpar.Name = "Limpar";
			this.Limpar.Size = new System.Drawing.Size(79, 29);
			this.Limpar.TabIndex = 2;
			this.Limpar.Text = "LIMPAR";
			this.Limpar.UseVisualStyleBackColor = true;
			this.Limpar.Click += new System.EventHandler(this.LimparClick);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.DecFormat);
			this.groupBox4.Controls.Add(this.HexFormat);
			this.groupBox4.Controls.Add(this.ASCIIFormat);
			this.groupBox4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox4.Location = new System.Drawing.Point(12, 16);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(197, 51);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Formato";
			// 
			// DecFormat
			// 
			this.DecFormat.Cursor = System.Windows.Forms.Cursors.Hand;
			this.DecFormat.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DecFormat.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.DecFormat.Location = new System.Drawing.Point(129, 18);
			this.DecFormat.Name = "DecFormat";
			this.DecFormat.Size = new System.Drawing.Size(59, 24);
			this.DecFormat.TabIndex = 2;
			this.DecFormat.Text = "Dec";
			this.DecFormat.UseVisualStyleBackColor = true;
			this.DecFormat.CheckedChanged += new System.EventHandler(this.DecFormatCheckedChanged);
			// 
			// HexFormat
			// 
			this.HexFormat.Cursor = System.Windows.Forms.Cursors.Hand;
			this.HexFormat.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HexFormat.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.HexFormat.Location = new System.Drawing.Point(66, 18);
			this.HexFormat.Name = "HexFormat";
			this.HexFormat.Size = new System.Drawing.Size(57, 24);
			this.HexFormat.TabIndex = 1;
			this.HexFormat.Text = "Hex";
			this.HexFormat.UseVisualStyleBackColor = true;
			this.HexFormat.CheckedChanged += new System.EventHandler(this.HexFormatCheckedChanged);
			// 
			// ASCIIFormat
			// 
			this.ASCIIFormat.Checked = true;
			this.ASCIIFormat.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ASCIIFormat.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ASCIIFormat.ForeColor = System.Drawing.SystemColors.AppWorkspace;
			this.ASCIIFormat.Location = new System.Drawing.Point(6, 18);
			this.ASCIIFormat.Name = "ASCIIFormat";
			this.ASCIIFormat.Size = new System.Drawing.Size(64, 24);
			this.ASCIIFormat.TabIndex = 0;
			this.ASCIIFormat.TabStop = true;
			this.ASCIIFormat.Text = "ASCII";
			this.ASCIIFormat.UseVisualStyleBackColor = true;
			this.ASCIIFormat.CheckedChanged += new System.EventHandler(this.ASCIIFormatCheckedChanged);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 3500;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// Status
			// 
			this.Status.BackColor = System.Drawing.Color.Transparent;
			this.Status.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Status.ForeColor = System.Drawing.Color.Red;
			this.Status.Location = new System.Drawing.Point(2, 460);
			this.Status.Name = "Status";
			this.Status.Size = new System.Drawing.Size(211, 19);
			this.Status.TabIndex = 4;
			this.Status.Text = "Desconectado";
			this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 10;
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ReshowDelay = 100;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Teal;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(388, 480);
			this.Controls.Add(this.Status);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HIDTerminal";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.vidNumber)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pidNumber)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.minBufferLength)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.maxBufferLength)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.NumericUpDown minBufferLength;
		private System.Windows.Forms.RadioButton sendHexNumber;
		private System.Windows.Forms.NumericUpDown maxBufferLength;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.Button Limpar;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label Status;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.RadioButton ASCIIFormat;
		private System.Windows.Forms.RadioButton HexFormat;
		private System.Windows.Forms.RadioButton DecFormat;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton sendNumber;
		private System.Windows.Forms.RadioButton sendString;
		private System.Windows.Forms.NumericUpDown pidNumber;
		private System.Windows.Forms.NumericUpDown vidNumber;
		private System.Windows.Forms.Button DefinirPIDeVID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button Enviar;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
