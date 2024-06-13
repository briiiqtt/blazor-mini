using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ProjectR.DTOs;

namespace BlazorSignalRApp.Hubs;

public class ChatHub : Hub
{
    private readonly GroupManager groupManager;
    //ConcurrentDictionary(스레드안전한 딕셔너리)로 유저-그룹을 맵핑

    private static string GetSystemMessageJson(string message)
    {
        //시스템메시지로 보낼 문자열을 받아 클라이언트가 파싱할 수 있는 json형태로 반환함
        return JsonConvert.SerializeObject(new Message("시스템", message));
    }

    public ChatHub(GroupManager _groupManager)
    {
        groupManager = _groupManager;
    }

    public async Task SendMessage(string json)
    {
        //그룹을 찾고, 그 그룹의 클라이언트들에게 메시지를 수신시키는 이벤트를 발생시킴
        groupManager.TryGetGroupName(Context.ConnectionId, out string? currentGroupName);

        if (currentGroupName != null)
        {
            await Clients.Group(currentGroupName).SendAsync("ReceiveMessage", json);
        }
    }

    public async Task SwitchGroup(string groupName)
    {

        groupManager.TryGetGroupName(Context.ConnectionId, out string? previousGroupName);
        if (previousGroupName != null)
        {
            //그룹을 찾고, 그룹이 있다면 제거
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, previousGroupName);
            groupManager.TryRemoveUserFromGroup(Context.ConnectionId, out string? _);
            await Clients.Group(previousGroupName).SendAsync("ReceiveMessage", ChatHub.GetSystemMessageJson($"{Context.ConnectionId} 님이 {groupName}번 방을 떠났습니다."));
        }

        //새 그룹에 추가
        groupManager.TryAddUserToGroup(Context.ConnectionId, groupName);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", ChatHub.GetSystemMessageJson($"{Context.ConnectionId} 님이 {groupName}번 방에 참여하였습니다."));
    }

    public async Task LeaveGroup()
    {
        groupManager.TryGetGroupName(Context.ConnectionId, out string? groupName);
        if (groupName != null)
        {
            //그룹을 찾고, 그룹이 있다면 제거
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", ChatHub.GetSystemMessageJson($"{Context.ConnectionId} 님이 {groupName}번 방을 떠났습니다."));
        }
    }
}