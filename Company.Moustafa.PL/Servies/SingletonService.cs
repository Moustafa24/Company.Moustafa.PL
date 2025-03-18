
namespace Company.Moustafa.PL.Servies
{
    public class SingletonService : ISingletonService
    {
        public Guid Guid { get; set; }

        public SingletonService()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
