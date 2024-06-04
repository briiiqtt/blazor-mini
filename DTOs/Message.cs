using Newtonsoft.Json;

namespace ProjectR.DTOs
{
    public class Message
    {
        public string UserName { get; set; }
        public string Content { get; set; }


        public Message(string _userName, string _content)
        {
            UserName = _userName;
            Content = _content;
        }
    }
}