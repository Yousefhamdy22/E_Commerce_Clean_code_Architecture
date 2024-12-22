using DashBoard.Core.Entities;
using Shared.Dtos;

namespace DashBoard.Application.DTOs
{
    public class DashboardProductDto : ProductDto 
    {

        public int Stock { get; set; }
        public int SolidItems { get; set; }
        public string IsAvalible { get; set; }
        public int? averageRate { get; set; }
        public string DisplayCategory { get; set; }
        public DateTime? CreatedDate { get; set; }


    }
}
