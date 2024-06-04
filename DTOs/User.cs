namespace ProjectR.DTOs
{
  public class User
  {
    public int Id { get; }
    public string WriterName { get; }
    public string Content { get; }
    public DateTime CreatedAt { get; }

    public User(int _id, string _writerName, string _content, DateTime _createdAt)
    {
      Id = _id;
      WriterName = _writerName;
      Content = _content;
      CreatedAt = _createdAt;
    }

  }

}