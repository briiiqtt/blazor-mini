using MySql.Data.MySqlClient;
using ProjectR.DTOs;

public class CommentModel
{
  public List<Comment>? Read()
  {
    MySqlConnection connection = new MySqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
    //dotnet env에 설정된 값에서 커넥션스트링 값을 찾아와 DB와 연결을 설정
    string sql = "SELECT id, writer_name AS writerName, content, created_at AS createdAt FROM comment ORDER BY id DESC";

    try
    {
      connection.Open();
      MySqlCommand command = new MySqlCommand(sql, connection);
      MySqlDataReader result = command.ExecuteReader();
      //쿼리를 실행
      List<Comment> list = new List<Comment>();
      while (result.Read())
      {
        int id = (int)result["id"];
        string writerName = (string)result["writerName"];
        string content = (string)result["content"];
        DateTime dateTime = (DateTime)result["createdAt"];
        DateTime seoulTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("Asia/Seoul"));
        Comment comment = new Comment(id, writerName, content, seoulTime);
        list.Add(comment);
      }
      //결과물이 담긴 List 반환
      return list;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
      return null;
    }
    finally
    {
      connection.Close();
    }
  }

  public int Write(string writerName, string content)
  {
    if (writerName.Length == 0 || content.Length == 0)
    {
      return 0;
    }
    MySqlConnection connection = new MySqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
    string sql = "INSERT INTO comment (writer_name, content) VALUES (@writerName, @content)";
    
    try
    {
      connection.Open();
      MySqlCommand command = new MySqlCommand(sql, connection);
      command.Parameters.Add("@writerName", MySqlDbType.VarChar);
      command.Parameters["@writerName"].Value = writerName;
      command.Parameters.Add("@content", MySqlDbType.VarChar);
      command.Parameters["@content"].Value = content;
      int affectedRows = command.ExecuteNonQuery();
      return affectedRows;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
      return 0;
    }
    finally
    {
      connection.Close();
    }
  }
}