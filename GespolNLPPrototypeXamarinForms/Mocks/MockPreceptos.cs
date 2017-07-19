using System.Collections.Generic;
using XamarinFormsAssistant.Prototype.Model;

namespace XamarinFormsAssistant.Mocks
{
    public static class MockPreceptos
    {
        private static int IdCounter;
        public static List<PreceptoDto> PreceptoList { get; set; } = new List<PreceptoDto>();

        public static void AddPrecepto(string precepto)
        {
            PreceptoList.Add(new PreceptoDto() { Descripcio = precepto, Id = IdCounter});
            IdCounter++;
        }
    }
}