using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetPractice.RestApiWithNLayer.Features.MyanmarProverbs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MmProverbController : ControllerBase
    {
        private async Task<ProverbModel> getDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("MmProverbs.json");
            var model = JsonConvert.DeserializeObject<ProverbModel>(jsonStr);
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> GetTitleAsync()
        {
            var data = await getDataAsync();
            return Ok(data.Tbl_MMProverbsTitle);
        }

        [HttpGet("title_name")]
        public async Task<IActionResult> getProverbNameAsync(string name)
        {
            var data = await getDataAsync();
            var title_id = data.Tbl_MMProverbsTitle.FirstOrDefault(x=>x.TitleName==name)!.TitleId;
            var lst = data.Tbl_MMProverbs.Where(x=>x.TitleId==title_id);
            var return_lst = lst.Select(x => new MmProverbs_Overview
            {
                TitleId = x.TitleId,
                ProverbId = x.ProverbId,
                ProverbName = x.ProverbName,
            }).ToList();
            if (return_lst is null) return NotFound("no data found");
            return Ok(return_lst);
        }

        [HttpGet("title_id/ proverb_id")]
        public async Task<IActionResult> getProverbsDetailsAsync(int title_id, int proverb_id)
        {
            var data = await getDataAsync();
            var lst = data.Tbl_MMProverbs.Where(x=>x.TitleId == title_id && x.ProverbId == proverb_id).ToList();
            return Ok(lst);
        }
    }
}



public class ProverbModel
{
    public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
    public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
}

public class Tbl_Mmproverbstitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_Mmproverbs
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}

public class MmProverbs_Overview
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
}
