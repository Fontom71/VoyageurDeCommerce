namespace VoyageurDeCommerce.exception.realisations
{
    /// <summary>Exception levée en cas de problème lors du parsage</summary>
    public class ExceptionParsage : ExceptionVdC
    {
        public ExceptionParsage(string message) : base("Problème de parsage", message)
        {
        }
    }
}
