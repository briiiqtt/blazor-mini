using System.Collections.Concurrent;

public class GroupManager
{
  private readonly ConcurrentDictionary<string, string> UserGroups = new ConcurrentDictionary<string, string>();

  public bool TryAddUserToGroup(string connectionId, string groupName)
  {
    return UserGroups.TryAdd(connectionId, groupName);
  }

  public bool TryRemoveUserFromGroup(string connectionId, out string? groupName)
  {
    return UserGroups.TryRemove(connectionId, out groupName);
  }

  public bool TryGetGroupName(string connectionId, out string? groupName)
  {
    return UserGroups.TryGetValue(connectionId, out groupName);
  }

}