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
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
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
		//poner aqui el nombre de la clase y en el metodo de la imagen también
		const string SISTEMAMTBW="SistemaMTBW";
		static readonly string[] MiniApps={SISTEMAMTBW};
		
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
			string nameSpace="PokemonGbaFrameWork."+miniApp;

			
			if((bool)Metodo(nameSpace,"Compatible",edicion,compilacion))
			{
				swMiniApp=new SwitchImg();
				//pongo la imagen
				PonImagenes(swMiniApp,miniApp);
				//pongo el click
				swMiniApp.SwitchChanged+=(estaOn)=>{
					if(estaOn)
					{
						Metodo(nameSpace,"Desactivar",rom,edicion,compilacion);
					}
					else{
						Metodo(nameSpace,"Activar",rom,edicion,compilacion);
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
				//si esta on
				swMiniApp.EstadoOn=(bool)Metodo(nameSpace,"EstaActivado",rom,edicion,compilacion);
			}
			return swMiniApp;
			
		}

		void PonImagenes(SwitchImg swMiniApp, string miniApp)
		{
			switch(miniApp)
			{
					case SISTEMAMTBW:
					
					break;
			}
		}

		object Metodo(string nameSpace,string nombreMetodo,params object[] parametros)
		{
			Type tipo = Type.GetType(nameSpace);
			Type[] tiposParametros=new Type[parametros.Length];
			MethodInfo metodo;
			
			for(int i=0;i<tiposParametros.Length;i++)
				tiposParametros[i]=parametros[i].GetType();
			
			metodo = tipo.GetMethod(nombreMetodo, tiposParametros);
			return metodo.Invoke(null, parametros);
		}
		void MiSobre_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}