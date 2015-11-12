using System;
using Gtk;

namespace SerpisAd
{
	public class WindowsHelper
	{
		public static bool ConfirmDelete(Window window){
			MessageDialog messageDialog = new MessageDialog (
				window, 
				DialogFlags.DestroyWithParent, 
				MessageType.Question,
				ButtonsType.YesNo,
				"¿Quieres eliminar el elemento seleccionado?"
				);
			messageDialog.Title = window.Title;
			ResponseType response = (ResponseType)messageDialog.Run ();
			messageDialog.Destroy();
			//Devuelve true si pulsa Yes, false si no pulsa Yes
			return response == ResponseType.Yes;

		}
	}
}

