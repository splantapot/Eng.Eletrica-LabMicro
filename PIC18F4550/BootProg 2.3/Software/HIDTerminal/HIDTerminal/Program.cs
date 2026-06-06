/*
 * SharpDevelop
 * Usuário: Tiago
 * Data: 23/09/2013
 * Hora: 17:03
 */
using System;
using System.Windows.Forms;

namespace HIDTerminal
{
	internal sealed class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
	}
}
