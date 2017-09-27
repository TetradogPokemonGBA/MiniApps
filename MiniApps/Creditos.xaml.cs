/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 27/09/2017
 * Hora: 20:30
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Wpf;

namespace MiniApps
{
	/// <summary>
	/// Interaction logic for Creditos.xaml
	/// </summary>
	public partial class Creditos : Window
	{
		public Creditos(UniformGrid ugApps)
		{
			InitializeComponent();
			
			PokemonGBAFrameWork.Creditos creditos;
			SwitchImg sw;
			StringBuilder strCreditosYLinkCodigo=new StringBuilder();
			strCreditosYLinkCodigo.Append("Esta aplicación esta desarrollada por pikachu240(wahackforo)\nLas imagenes de las miniApps hechas por Sangus103(wahackforo)\n");
			//creditos
			if(ugApps.Children.Count==0)
				strCreditosYLinkCodigo.Append("Los autores aparecen cuando cargas una rom\n");
			else
				for(int i=0;i<ugApps.Children.Count;i++)
			{
				sw=(SwitchImg)ugApps.Children[i];
				creditos=sw.Tag as PokemonGBAFrameWork.Creditos;
				strCreditosYLinkCodigo.Append(creditos.ToString(sw.Name));
				strCreditosYLinkCodigo.Append("\n");
			}
			
			strCreditosYLinkCodigo.Append("La aplicación es GNU quieres ver su código fuente?");
			txtCreditos.Text=strCreditosYLinkCodigo.ToString();
		}
		void BtnNo_Click(object sender=null, RoutedEventArgs e=null)
		{
			this.Close();
		}
		void BtnYes_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/TetradogPokemonGBA/MiniApps");
			BtnNo_Click();
			
		}
	}
}