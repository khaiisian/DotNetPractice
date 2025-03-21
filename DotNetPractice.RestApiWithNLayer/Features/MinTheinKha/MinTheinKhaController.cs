using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetPractice.RestApiWithNLayer.Features.MinTheinKha
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinTheinKhaController : ControllerBase
    {
        private async Task<Dto?> getData()
        {
            var json = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<Dto>(json);
            return model;
        }

        private int numerReplace(string number)
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

        // api/MinTheinKha/questions
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestion()
        {
            var data = await getData();
            return Ok(data.questions);
        }

        [HttpGet("numbers")]
        public async Task<IActionResult> getNumber()
        {
            var data = await getData();
            return Ok(data.numberList);
        }

        [HttpGet("QuestionNo/AnswerNo")]
        public async Task<IActionResult> getResultAsync(int QuestionNo, string AnswerNo)
        {
            var answerNo = numerReplace(AnswerNo);
            var data = await getData();
            var ans = data.answers.FirstOrDefault(x=>x.questionNo==QuestionNo && x.answerNo== answerNo);
            return Ok(ans);
        }
    }


    public class Dto
    {
        public Question[] questions { get; set; }
        public Answer[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Question
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answer
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }

}
