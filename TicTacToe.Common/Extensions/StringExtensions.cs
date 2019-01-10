using Newtonsoft.Json;

namespace TicTacToe.Common.Extensions{
    public static class StringExtensions{
        public static T Deserialize<T>(this string data){
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}