using Common.Query;
using Shop.Domain.SiteEntities;
using static Shop.Domain.SiteEntities.Banner;

namespace Shop.Query.SiteEntities.DTOs;

public class BannerDto : BaseDto
{
    public string Link { get;  set; }
    public string ImageName { get;  set; }
    public BannerPosition Position { get;  set; }
    
}