
namespace Company.Moustafa.PL.Servies
{
    public class ScopedService : IScopedService
    {
        public Guid Guid { get ; set ; }

        public ScopedService()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
           return Guid.ToString();
        }
    }
}
