using api_web_ban_giay.Dtos.Product;
using api_web_ban_giay.Dtos.TradeMark;
using api_web_ban_giay.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
       /* public static GetFormHomeDto ToGetFormHomeDto(this Trademark trademark)
        {
            return new GetFormHomeDto
            {
                Id = trademark.Id,
                Name = trademark.Name,
                CountProduct = trademark.Products.Count
            };
        }*/
    }
    
}
