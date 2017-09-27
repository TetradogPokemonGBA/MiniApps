/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 26/09/2017
 * Hora: 21:07
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat.Extension;
using Microsoft.Win32;
using PokemonGBAFrameWork;
using Gabriel.Cat.Wpf;
namespace MiniApps
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		static readonly Type Creditos=typeof(PokemonGBAFrameWork.Creditos);
		//poner aqui el nombre de la clase y en el metodo de la imagen si tengo el icono lo pongo :)
		const string SISTEMAMTBW="SistemaMTBW";
		const string SCRIPTSDEGATILLOCONDICIONMAYORAFF="ScriptsDeGatilloCondicionMayorAFF";
		const string QUITARTUTORIALBATALLAOAK="QuitarTutorialBatallaOak";
		const string QUITARSISTEMADEAYUDAROJOYVERDE="QuitarSistemaDeAyudaRojoYVerde";
		const string QUITARDIARIOPARTIDA="QuitarDiarioPartida";
		const string MOSINMEDALAS="MOSinMedallas";
		const string BORRARMOS="BorrarMos";
		const string IMPEDIRCAPTURAVIASCRIPT="ImpedirCapturaViaScript";
		const string GANAREXPERIENCIAALCAPTURARUNPOKEMON="GanarExperienciaAlCapturarUnPokemon";
		const string EVOLUCIONARSINNATIONALDEX="EvolucionarSinNationalDex";
		//de momento no esta acabada asi que no la pongo :)
		//const string POKEMONEDITORSCRIPTING="PokemonEditorScripting";
		//const string SHINYZER="Shinyzer";
		//const string MUGSHOTS="Mugshots";
		//const string ANIMACIONSPRITESCOMOESMERALDA="AnimacionSprites";
		static readonly string[] MiniApps={SISTEMAMTBW,SCRIPTSDEGATILLOCONDICIONMAYORAFF,QUITARTUTORIALBATALLAOAK,QUITARSISTEMADEAYUDAROJOYVERDE,QUITARDIARIOPARTIDA,MOSINMEDALAS,BORRARMOS,IMPEDIRCAPTURAVIASCRIPT,GANAREXPERIENCIAALCAPTURARUNPOKEMON,EVOLUCIONARSINNATIONALDEX};
		
		RomGba rom;
		EdicionPokemon edicion;
		Compilacion compilacion;
		
		public Window1()
		{
			InitializeComponent();
		}
		void MiCargar_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog opnFile=new OpenFileDialog();
			opnFile.Filter="Pokemon GBA|*.gba";
			if(opnFile.ShowDialog().GetValueOrDefault())
			{
				rom=new RomGba(opnFile.FileName);
				edicion=EdicionPokemon.GetEdicionPokemon(rom);
				compilacion=Compilacion.GetCompilacion(rom,edicion);
				txtNombreRom.Text=rom.Nombre+"("+edicion.NombreCompleto+"-"+edicion.Idioma+")";
				//poner icono con la imagen de la edición
				PonAplicaciones();
				
			}else if(rom!=null)
				MessageBox.Show("No se ha cambiado la rom");
			else MessageBox.Show("No se ha cargado nada...");
		}

		void PonAplicaciones()
		{
			
			SwitchImg swMiniApp;
			ugApps.Children.Clear();
			for(int i=0;i<MiniApps.Length;i++){
				swMiniApp=PonSistema(MiniApps[i]);
				if(swMiniApp!=null)
					ugApps.Children.Add(swMiniApp);
			}
			
		}

		SwitchImg PonSistema(string miniApp)
		{
			SwitchImg swMiniApp=null;

			
			if((bool)Metodo(miniApp,"Compatible",edicion,compilacion))
			{
				swMiniApp=new SwitchImg();
				swMiniApp.Name=miniApp;
				//pongo la imagen
				PonImagenes(swMiniApp,miniApp);
				//si esta on
				swMiniApp.EstadoOn=(bool)Metodo(miniApp,"EstaActivado",rom,edicion,compilacion);
				//pongo el click
				swMiniApp.SwitchChanged+=(s,estaOff)=>{
					if(!estaOff)
					{
						Metodo(((SwitchImg)s).Name,"Desactivar",rom,edicion,compilacion);
					}
					else{
						Metodo(((SwitchImg)s).Name,"Activar",rom,edicion,compilacion);
					}
					try{
						rom.Save();
					}catch{
						if(MessageBox.Show("No se ha podido guardar, esto puede ser debido a que se use la rom con otro programa,cierralo y dale a Yes","Problemas al guardar",MessageBoxButton.YesNo,MessageBoxImage.Information)==MessageBoxResult.Yes)
							try{
							rom.Save();}catch{
							MessageBox.Show("El problema persiste...pruebe de reiniciar y volver ha aplicar la mejora");
						}
						
					}
				};
				swMiniApp.ToolTip=miniApp;
				//pongo los creditos en el Tag
				swMiniApp.Tag=(PokemonGBAFrameWork.Creditos)Type.GetType(Creditos.AssemblyQualifiedName.Replace("Creditos",miniApp)).GetField("Creditos",BindingFlags.Static|BindingFlags.Public).GetValue(new PokemonGBAFrameWork.Creditos());
			}
			return swMiniApp;
			
		}

		void PonImagenes(SwitchImg swMiniApp, string miniApp)
		{
			switch(miniApp)
			{
				case SISTEMAMTBW:
					swMiniApp.ImgOn=Resource.SistemaMTBW.ToImage().Source;
					swMiniApp.ImgOff=Resource.SistemaMTBW.ChangeColor(Gabriel.Cat.PixelColors.GrayScale).ToImage().Source;
					break;
				default:
					//pongo una imagen con el on y el off con el nombre de la miniapp
					swMiniApp.ImgOn=GetBitmap(miniApp,System.Drawing.Brushes.Red).ToImage().Source;
					swMiniApp.ImgOff=GetBitmap(miniApp,System.Drawing.Brushes.GreenYellow).ToImage().Source;
					break;
			}
		}
		Bitmap GetBitmap(string text,System.Drawing.Brush brushFont)
		{//source https://stackoverflow.com/questions/6311545/c-sharp-write-text-on-bitmap
			const int HEIGHT=100;
			const int WIDTH=200;
			Bitmap bmp = new Bitmap(WIDTH,HEIGHT);

			RectangleF rectf = new RectangleF(0, 0,WIDTH, HEIGHT);

			Graphics g = Graphics.FromImage(bmp);

			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;
			g.PixelOffsetMode = PixelOffsetMode.HighQuality;
			g.DrawString(text, new Font("Arial",15), brushFont, rectf);

			g.Flush();

			return bmp;
		}
		object Metodo(string miniApp,string nombreMetodo,params object[] parametros)
		{
			
			Type tipo;
			Type[] tiposParametros=new Type[parametros.Length];
			MethodInfo metodo;
			tipo =Type.GetType (Creditos.AssemblyQualifiedName.Replace("Creditos",miniApp));
			for(int i=0;i<tiposParametros.Length;i++)
				tiposParametros[i]=parametros[i].GetType();
			
			metodo = tipo.GetMethod(nombreMetodo, tiposParametros);
			return metodo.Invoke(null, parametros);
		}
		void MiSobre_Click(object sender, RoutedEventArgs e)
		{

			new Creditos(ugApps).ShowDialog();
			
		}
	}
}