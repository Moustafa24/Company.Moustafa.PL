namespace Company.Moustafa.PL.Servies
{
    public interface ITransientService
    {

        public Guid Guid { get; set; }
        string GetGuid();
    }
}
