// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string json = File.ReadAllText("data.json");
Console.WriteLine(json);

var model = JsonConvert.DeserializeObject<Dto>(json);
Console.WriteLine(model);

foreach (var number in model.numberList)
{
    Console.WriteLine(number);
}


foreach(var questions in model.questions)
{
    Console.WriteLine(questions.questionName);
}

Console.ReadKey();

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



