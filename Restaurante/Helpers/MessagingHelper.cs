namespace Restaurant.Infraestructure.Helpers
{
    public class MessagingHelper
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }

    public class MessagingHelper<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Obj { get; set; }
    }
}
