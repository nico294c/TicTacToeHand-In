using Microsoft.AspNetCore.SignalR;


namespace TicTacToeWebAPI
{
    public class GameHub : Hub
    {
        public async Task SendMove(string displayText, int index)
        {
            Console.WriteLine($"Square at index {index} changed to {displayText}.");
            await Clients.All.SendAsync("MoveReceived", displayText, index);
        }

        public async Task SendReset()
        {         
            Console.WriteLine("Reset request sent.");   
            await Clients.All.SendAsync("ResetReceived");            
        }
    }
}
