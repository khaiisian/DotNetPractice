using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetPractice.RestApiWithNLayer.Features.MinTheinKha
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinTheinKhaRedoController : ControllerBase
    {
        private async Task<Model?> getData()
        {
            var json = await System.IO.File.ReadAllTextAsync("data.json");
            var data = JsonConvert.DeserializeObject<Model>(json);
            return data;
        }

        private int numberReplace(string number)
        {
            number = number.Replace("၁", "1");
            number = number.Replace("၂", "2");
            number = number.Replace("၃", "3");
            number = number.Replace("၄", "4");
            number = number.Replace("၅", "5");
            number = number.Replace("၆", "6");
            number = number.Replace("၇", "7");
            number = number.Replace("၈", "8");
            number = number.Replace("၉", "9");
            number = number.Replace("၁၀", "10");
            return Convert.ToInt32(number);
        }

        [HttpGet("questions")]
        public async Task<IActionResult> getQuestionsAsync()
        {
            var data = await getData();
            return Ok(data.questions);
        }

        [HttpGet("numbers")]
        public async Task<IActionResult> getNumbersAsync()
        {
            var data = await getData();
            return Ok(data.numberList);
        }

        [HttpGet("questionNo/anwerNo")]
        public async Task<IActionResult> getResultAsync(int questionNo, string answerNo)
        {
            int AnsNo = numberReplace(answerNo);
            var data = await getData();
            var result = data.answers.FirstOrDefault(x => x.questionNo == questionNo && x.answerNo == AnsNo);
            return Ok(result);
        }

        
    }


    public class Model
    {
        public Question[] questions { get; set; }
        public Answer[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Questions
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answers
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }

}
