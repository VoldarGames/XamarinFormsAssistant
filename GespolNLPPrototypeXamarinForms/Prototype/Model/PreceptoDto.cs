namespace XamarinFormsAssistant.Prototype.Model
{
    public class PreceptoDto : EntityBase
    {
        public int IdClient { get; set; }
        public string Article { get; set; }
        public string Apartat { get; set; }

        public string Opcio { get; set; }
        public string Descripcio { get; set; }
        public bool? RetiradaCarnet { get; set; }
        public decimal? ImportReduit { get; set; }
        public decimal? ImportSenseReduir { get; set; }
        public int IdTipusPrecepte { get; set; }
        public int IdNormativa { get; set; }
        public int IdReferenciaPunt { get; set; }

        public int? IdNormativaDGT { get; set; }
        public string Estat { get; set; }
        public string NumConducta { get; set; }
        public string ApartatDGT { get; set; }
        public string OpcioDGT { get; set; }
        public string Codi { get; set; }
        public string Punts { get; set; }
        public string DescripcionNormativa { get; set; }

        public override string ToString()
        {
            return Descripcio;
        }
    }
}