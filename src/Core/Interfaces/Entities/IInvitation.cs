 
namespace Core.Interfaces.Entities
{
    public interface IInvitation
    {
        public string Message { get; set; }
        public bool IsAccepted { get; set; }  

    }
}
