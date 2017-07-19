using System.Collections.Generic;
using XamarinFormsAssistant.Assistant.Enums;

namespace XamarinFormsAssistant.Assistant.Fragments.Managers
{
    public static class CommandDictionaryManager
    {
        private static readonly Dictionary<string, SpeechCommand> SpeechCommandDictionary = new Dictionary<string, SpeechCommand>()
        {
            {"NUEVO",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"ALTA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"NUEVA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"NOU",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"NOVA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"OTRO",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"OTRA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Create} },
            {"MODIFICA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"CAMBIA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"SUSTITUYE",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"REEMPLAZA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"REEMPLAÇA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"CANVIA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"SUBSTITUEIX",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Update} },
            {"BORRA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"BORRAR",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"BORRO",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"ELIMINA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"ESBORRA",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"ESBORRAR",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"ESBORRO",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"DESTRUYE",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"DESTRUEIX",new SpeechCommand() {SpeechCommandType = SpeechCommandType.Delete} },
            {"REINICIA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"REINICIO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"RESET", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"RESETEA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"RESETEO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"COMIENZA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"COMIENZO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"COMENÇA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"EMPIEZA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"EMPIEZO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Reset} },
            {"ACEPTAR", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"ACCEPTAR", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"ACEPTO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"ACEPTA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"ACCEPTO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"ACCEPTA", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"OK", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"OKAY", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Accept} },
            {"REPITE", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Repeat} },
            {"REPETIR", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Repeat} },
            {"REPETEIX", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Repeat} },
            {"REPITEMELO", new SpeechCommand() {SpeechCommandType = SpeechCommandType.Repeat} },





        };
        public static SpeechCommand FindCommand(string possibleCommand)
        {
            possibleCommand = possibleCommand.ToUpper();
            if (SpeechCommandDictionary.ContainsKey(possibleCommand))
            {
                var result =  SpeechCommandDictionary[possibleCommand];
                result.OriginalTextFragment = possibleCommand;
                return result;
            }
            return null;
        }
    }
}