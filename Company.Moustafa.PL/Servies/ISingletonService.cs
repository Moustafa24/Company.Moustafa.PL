namespace Company.Moustafa.PL.Servies
{
    public interface ISingletonService
    {

        public Guid Guid { get; set; }
        string GetGuid();
    }
}
