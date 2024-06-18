using api_web_ban_giay.Dtos.Product;
using api_web_ban_giay.Dtos.TradeMark;
using api_web_ban_giay.Models;

namespace api_web_ban_giay.Mappers
{
    public static class TrademarkMapper
    {
        public static Trademark ToCreateTradeMarRequestkDto(this CreateTradeMarkDto trademarkDto)
        {
            return new Trademark
            {
                Name = trademarkDto.Name
            };
        }
        public static Trademark ToUpdateDto(this UpdateDto updateDto)
        {
            return new Trademark
            {
                Id = updateDto.Id,
                Name = updateDto.Name,
            };
        }
    }
}
