namespace XamarinFormsAssistant.Prototype.Model
{
    public abstract class PersonaBase : EntityBase
    {
        public int UserId { get; set; }
        public string Nom { get; set; }
        public int Codi { get; set; }
    }
}