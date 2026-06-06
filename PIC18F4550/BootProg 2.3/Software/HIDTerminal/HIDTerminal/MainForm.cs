using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using UsbLibrary;
using Nini.Ini;
using Nini.Config;

namespace HIDTerminal
{
	public partial class MainForm : Form
	{
		
		Configs config = new Configs();
		UsbHidPort usbHidPort1 = new UsbHidPort();
		private IniConfigSource Pref = null;
		
		public MainForm()
		{
			InitializeComponent();
			
			#region Carregar Prefs
			
			try
			{
				Pref = new IniConfigSource(Environment.CurrentDirectory + @"\HidTerminal.Settings.ini");
			}catch(Exception)
			{
				return;
			}
			
			try
			{
				int i = Pref.Configs["Setting"].GetInt("FormatoEnviar");
				switch(i)
				{
					case 0: sendNumber.Checked = true; break;
					case 1: sendHexNumber.Checked = true; break;
					case 2: sendString.Checked = true; break;
					default: sendNumber.Checked = true; break;
				}
			}catch(Exception)
			{
				sendNumber.Checked = true;
			}
			
			try
			{
				int i = Pref.Configs["Setting"].GetInt("FormatoReceber");
				switch(i)
				{
					case 0: ASCIIFormat.Checked = true; break;
					case 1: HexFormat.Checked = true; break;
					case 2: DecFormat.Checked = true; break;
					default: ASCIIFormat.Checked = true; break;
				}
			}catch(Exception)
			{
				ASCIIFormat.Checked = true;
			}
			
			try
			{
				 checkBox1.Checked = Pref.Configs["Setting"].GetBoolean("AutoLimpar");
				
			}catch(Exception)
			{
				checkBox1.Checked = true;
			}
			
			try
			{
				 checkBox2.Checked = Pref.Configs["Setting"].GetBoolean("AutoScroll");
				
			}catch(Exception)
			{
				checkBox2.Checked = false;
			}
			
			try
			{
				minBufferLength.Value = Pref.Configs["Setting"].GetInt("Min");
			}catch(Exception)
			{
				minBufferLength.Value = 0;
			}
			
			try
			{
				maxBufferLength.Value = Pref.Configs["Setting"].GetInt("Max");
			}catch(Exception)
			{
				maxBufferLength.Value = 63;
			}
			
			try
			{
				pidNumber.Value = usbHidPort1.ProductId = Pref.Configs["Setting"].GetInt("PID");
			}catch(Exception)
			{
				pidNumber.Value = 0;
			}
			
			try
			{
				vidNumber.Value = usbHidPort1.VendorId = Pref.Configs["Setting"].GetInt("VID");
			}catch(Exception)
			{
				vidNumber.Value = 0;
			}
			
			#endregion
			
			usbHidPort1.OnDataRecieved += ReceberDados;
			usbHidPort1.OnDeviceArrived += AlgumDispEncontrado;
			usbHidPort1.OnDeviceRemoved += AlgumDispRemovido;
			usbHidPort1.OnSpecifiedDeviceArrived += Encontrado;
			usbHidPort1.OnSpecifiedDeviceRemoved += Removido;
			usbHidPort1.CheckDevicePresent();
		}
		
		
		#region Código obrigatório
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			usbHidPort1.RegisterHandle(Handle);
		}
		protected override void WndProc(ref Message m)
		{
			usbHidPort1.ParseMessages(ref m);
			base.WndProc(ref m); // pass message on to base form
		}
		#endregion
		
		void DefinirPIDeVIDClick(object sender, EventArgs e)
		{
			Pref.Configs["Setting"].Set("PID", pidNumber.Value);
			Pref.Configs["Setting"].Set("VID", vidNumber.Value);
			Pref.Save();
			Application.Restart();
		}
		
		void Removido(object sender, EventArgs e)
		{
			Status.Text = "Desconectado";
			config.DeviceConnected = false;
		}
		
		void Encontrado(object sender, EventArgs e)
		{
			Status.Text = "Conectado";
			config.DeviceConnected = true;
		}
		
		void AlgumDispRemovido(object sender, EventArgs e)
		{
			Status.Text = "Algum Dispositivo foi Desconectado";
			config.DeviceConnected = false;
		}
		
		void AlgumDispEncontrado(object sender, EventArgs e)
		{
			Status.Text = "Algum Dispositivo foi Conectado";
			config.DeviceConnected = false;
		}
		
		void ReceberDados(object sender, DataRecievedEventArgs e)
		{
			if(config.DeviceConnected == false) return;
			
			if(checkBox1.Checked) richTextBox1.Text = "";
			
			switch(config.ReceiveFormat)
			{
				case (int)Configs.RECEIVE_TYPES.ASCII:
					for (int i = (int)minBufferLength.Value; i <= (int)maxBufferLength.Value; i++)
					{
						richTextBox1.Text += (char)e.data[i+1];
					}
					richTextBox1.Text += "\r";
					break;
					
				case (int)Configs.RECEIVE_TYPES.DEC:
					for (int i = (int)minBufferLength.Value; i <= (int)maxBufferLength.Value; i++)
					{
						richTextBox1.Text += e.data[i+1].ToString() + " ";
					}
					richTextBox1.Text += "\r";
					break;
					
				case (int)Configs.RECEIVE_TYPES.HEX:
					for (int i = (int)minBufferLength.Value; i <= (int)maxBufferLength.Value; i++)
					{
						richTextBox1.Text += e.data[i+1].ToString("X2") + " ";
					}
					richTextBox1.Text += "\r";
					break;
			}
			
			if(checkBox2.Checked)
			{
				richTextBox1.SelectionStart = richTextBox1.Text.Length;
				richTextBox1.ScrollToCaret();
			}
			
		}
		
		#region SEND FORMAT
		void SendNumberCheckedChanged(object sender, EventArgs e)
		{
			config.SendFormat = (int)Configs.SEND_TYPES.NUMBER;
			textBox1.Text = "";
			textBox1.MaxLength = 256;
			Pref.Configs["Setting"].Set("FormatoEnviar", 0);
			Pref.Save();
		}
		
		void SendHexNumberCheckedChanged(object sender, EventArgs e)
		{
			config.SendFormat = (int)Configs.SEND_TYPES.NUMBER_HEX;
			textBox1.Text = "";
			textBox1.MaxLength = 192;
			Pref.Configs["Setting"].Set("FormatoEnviar", 1);
			Pref.Save();
		}
		
		void SendStringCheckedChanged(object sender, EventArgs e)
		{
			config.SendFormat = (int)Configs.SEND_TYPES.STRING;
			textBox1.Text = "";
			textBox1.MaxLength = 64;
			Pref.Configs["Setting"].Set("FormatoEnviar", 2);
			Pref.Save();
		}
		#endregion
		
		#region RECEIVE FORMAT
		void ASCIIFormatCheckedChanged(object sender, EventArgs e)
		{
			config.ReceiveFormat = (int)Configs.RECEIVE_TYPES.ASCII;
			Pref.Configs["Setting"].Set("FormatoReceber", 0);
			Pref.Save();
		}
		
		void HexFormatCheckedChanged(object sender, EventArgs e)
		{
			config.ReceiveFormat = (int)Configs.RECEIVE_TYPES.HEX;
			Pref.Configs["Setting"].Set("FormatoReceber", 1);
			Pref.Save();
		}
		
		void DecFormatCheckedChanged(object sender, EventArgs e)
		{
			config.ReceiveFormat = (int)Configs.RECEIVE_TYPES.DEC;
			Pref.Configs["Setting"].Set("FormatoReceber", 2);
			Pref.Save();
		}
		#endregion
		
		void EnviarClick(object sender, EventArgs e)
		{
			if(config.DeviceConnected == false) return;
			
			byte[] Buffer = new byte[65];
			switch (config.SendFormat)
			{
				case (int)Configs.SEND_TYPES.NUMBER:
					string[] numbers = textBox1.Text.Split(' ');
					for (int i = 0; i < (numbers.Length); i++)
					{
						Buffer[i+1] = (byte) int.Parse(numbers[i]);
					}
					usbHidPort1.SpecifiedDevice.SendData(Buffer);
					break;
					
				case (int)Configs.SEND_TYPES.NUMBER_HEX:
					numbers = textBox1.Text.Split(' ');
					for (int i = 0; i < (numbers.Length); i++)
					{
						Buffer[i+1] = (byte) int.Parse(numbers[i], System.Globalization.NumberStyles.HexNumber);
					}
					usbHidPort1.SpecifiedDevice.SendData(Buffer);
					break;
				
				case (int)Configs.SEND_TYPES.STRING:
					for (int i = 0; i < (textBox1.Text.Length) || i < 64; i++)
					{
						Buffer[i+1] = (byte)textBox1.Text[i];
					}
					usbHidPort1.SpecifiedDevice.SendData(Buffer);
					break;
			}
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			usbHidPort1.CheckDevicePresent();
		}
		
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			usbHidPort1.Dispose();
			System.Diagnostics.Process.GetCurrentProcess().Close();
		}
		
		void LimparClick(object sender, EventArgs e)
		{
			richTextBox1.Text = "";
		}
		
		void TextBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(config.SendFormat == (int)Configs.SEND_TYPES.STRING) return;
			
			if(((e.KeyValue >= (int)Keys.D0 && e.KeyValue <= (int)Keys.D9) ||
			  e.KeyValue == (int)Keys.Back || e.KeyValue == (int)Keys.Delete || e.KeyValue == (int)Keys.Space
			  || (e.KeyValue >= (int)Keys.NumPad0 && e.KeyValue <= (int)Keys.NumPad9))
			  || (e.KeyValue >= (int)Keys.A && e.KeyValue <= (int)Keys.F))
			{
				
				if(config.SendFormat == 0 && (e.KeyValue >= (int)Keys.A && e.KeyValue <= (int)Keys.F))
				{
					e.SuppressKeyPress = true;
					return;
				}
			}
			else
			{
				e.SuppressKeyPress = true;
			}
		}
		
		void CheckBox1CheckedChanged(object sender, EventArgs e)
		{
			Pref.Configs["Setting"].Set("AutoLimpar", checkBox1.Checked);
			Pref.Save();
		}
		
		void CheckBox2CheckedChanged(object sender, EventArgs e)
		{
			Pref.Configs["Setting"].Set("AutoScroll", checkBox1.Checked);
			Pref.Save();
		}
		
		void MinBufferLengthValueChanged(object sender, EventArgs e)
		{
			Pref.Configs["Setting"].Set("Min", (int)minBufferLength.Value);
			Pref.Save();
		}
		
		void MaxBufferLengthValueChanged(object sender, EventArgs e)
		{
			Pref.Configs["Setting"].Set("Max", (int)maxBufferLength.Value);
			Pref.Save();
		}
	}
	
	public class Configs
	{
		public enum SEND_TYPES
		{
			NUMBER = 0,
			STRING = 1,
			NUMBER_HEX = 2
			
		}
		
		public enum RECEIVE_TYPES
		{
			ASCII = 0,
			HEX = 1,
			DEC = 2
		}
		
		private int sendType = (int)SEND_TYPES.STRING;
		private int receiveType = (int)RECEIVE_TYPES.ASCII;
		private bool deviceConnected = false;
		
		public int SendFormat
		{
			get
			{
				return sendType;
			}
			set
			{
				sendType = value;
			}
		}
		
		public int ReceiveFormat
		{
			get
			{
				return receiveType;
			}
			set
			{
				receiveType = value;
			}
		}
		
		public bool DeviceConnected
		{
			get
			{
				return deviceConnected;
			}
			set
			{
				deviceConnected = value;
			}
		}
		
	}
}
