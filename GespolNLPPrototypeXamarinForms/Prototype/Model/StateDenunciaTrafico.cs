namespace XamarinFormsAssistant.Prototype.Model
{
    public class StateDenunciaTrafico : EntityBase
    {
        public StatesDenunciaTrafico Estado { get; set; }

        public int DenunciaTraficoId { get; set; }

        public StateDenunciaTrafico()
        {
            Estado = StatesDenunciaTrafico.Borrador;
        }

        public StateDenunciaTrafico(StatesDenunciaTrafico EstadoDenuncia)
        {
            Estado = EstadoDenuncia;
        }
    }
}