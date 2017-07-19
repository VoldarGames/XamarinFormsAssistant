namespace XamarinFormsAssistant.Assistant.Enums
{
    public enum SpeechCommandType
    {
        Create,   //Cambian contexto del asistente: [comando nombremodulo]... Ejemplo: Nueva Denuncia -> ContextType = typeof(DenunciaTrafico)
        Update,   //Modifican un campo: [comando Property nuevoValor] ... Ejemplo: Modificar Precepto 1234
        Delete,   //Borran un campo: [comando Property] .... Ejemplo: Borrar matr�cula 
        Reset,    //Borra todos los campos: [comando] .... Ejemplo: Comenzar
        Accept,   //Acepta la introducci�n de los datos: [comando] .... Ejemplo: Aceptar
        Cancel,   //Cancela la introducci�n de los datos y vuelve a la pantalla principal: [comando] .... Ejemplo: Cancelar
        Response,  //Comando s�lo existente si Assistant espera respuesta
        Repeat     //Comando que fuerza al asistente a repetir su estado actual.
    }
}