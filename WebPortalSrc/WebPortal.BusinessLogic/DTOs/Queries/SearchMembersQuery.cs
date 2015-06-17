using WebPortal.Entities.Members;
namespace WebPortal.BusinessLogic.DTOs.Queries
{
    public class SearchMembersQuery{
        /// <summary>
        /// Country the member resides in
        /// </summary>               
        public int? CountryId { get; set; }
     
        /// <summary>
        /// Region of a country the member resides in
        /// </summary>                 
        public int? RegionStateId { get; set; }

        /// <summary>
        /// City in a country the member resides in
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MinimalAge { get; set; }

        /// <summary>
        /// Maximal age of the member to find
        /// </summary>
        public int? MaximalAge { get; set; }

        /// <summary>
        /// Gender of the member to search
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Search among online users
        /// </summary>                    
        public bool? IsOnline { get; set; }
    }
}
