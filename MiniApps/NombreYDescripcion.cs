/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/10/2017
 * Hora: 3:53
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace MiniApps
{
	/// <summary>
	/// Description of NombreYDescripcion.
	/// </summary>
	public class NombreYDescripcion
	{
		public enum Mostrar
		{
			Nombre,
			Descripcion,
			Ambos
		}
		public static Mostrar MostrarEnToString=Mostrar.Nombre;
		public static char CaracterSeparadorAmbos=' ';
		string nombre;
		string descripcion;
		public NombreYDescripcion(string nombre,string descripcion)
		{
			this.nombre=nombre;
			this.descripcion=descripcion;
		}

		public string Nombre {
			get {
				return nombre;
			}
			set {
				nombre = value;
			}
		}

		public string Descripcion {
			get {
				return descripcion;
			}
			set {
				descripcion = value;
			}
		}
		public override string ToString()
		{
			string toString;
			switch (MostrarEnToString) {
				case Mostrar.Nombre:
					toString=Nombre;
					break;
				case Mostrar.Descripcion:
					toString=Descripcion;
					break;
				default:
					toString=Nombre+CaracterSeparadorAmbos+Descripcion;
					break;
			}
			return toString;
		}
	}
}
