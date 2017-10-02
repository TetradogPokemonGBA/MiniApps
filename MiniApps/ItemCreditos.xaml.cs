/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/10/2017
 * Hora: 4:14
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Extension;
using Gabriel.Cat.Wpf;

namespace MiniApps
{
	/// <summary>
	/// Interaction logic for ItemCreditos.xaml
	/// </summary>
	public partial class ItemCreditos : UserControl
	{
		public ItemCreditos(SwitchImg sw)
		{
			PokemonGBAFrameWork.Creditos creditos;
			NombreYDescripcion nombreYDescripcion;
			
			InitializeComponent();
			
			nombreYDescripcion=sw.ToolTip as NombreYDescripcion;
			creditos=sw.Tag as PokemonGBAFrameWork.Creditos;
			txtNombreApp.Text=nombreYDescripcion.Nombre;
			txtDescripcion.Text=nombreYDescripcion.Descripcion;
			txtCreditos.Text=creditos.ToString();
			imgMiniApp.SetImage(sw.ImgOn.ToBitmap());
		}
		void TxtCreditos_GiveFeedback(object sender, GiveFeedbackEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}