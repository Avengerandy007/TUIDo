using System;

class Program{

	static List<Task> tasks = new List<Task>();

	static void Main(){
		Console.WriteLine("Welcome to the TUIDo To-Do application!");
		ReadFile();
		Options();
		return;
	}

	static void ReadFile(){
		try{
			var lines = File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tasks.txt"));
			foreach(string line in lines){
				string[] propriety = line.Split('-');
				tasks.Add(new Task(propriety[0], propriety[1]));
			}
		}catch(IOException e){
			Console.WriteLine($"Could not open the file: {e.Message}");
		}
	}

	static void Options(){
		Console.WriteLine("----------------------------------------------------------\nChoose one of the following actions.\n");
		Console.WriteLine("'add' - create a new task\n'view' - view your current tasks along side their notes\n'com' - mark a task as completed\n'quit' - exit the program");

		string? option = Console.ReadLine();

		switch (option){
			case "add":
				AddNew();
			break;

			case "view":
				View();
			break;

			case "com":
				MarkComplete();
			break;

			case "quit":
				using (StreamWriter outputFile = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tasks.txt")))
        			{
            				foreach (Task task in tasks)
                			outputFile.WriteLine(task.Name + '-' + task.Description);
        			}
				return;
			break;

			default:
				Console.WriteLine("Sorry, your option is not valid, please input a valid option.");
				Options();
			break;
		}
	}

	static void AddNew(){
		Console.Write("Please name you task: ");
		string? name = Console.ReadLine();
		Console.Write("Please add a description to your task: ");
		string? description = Console.ReadLine();
		tasks.Add(new Task(name, description));
		Options();
	}

	static void View(){
		foreach(var task in tasks){
			Console.WriteLine($"\n{task.Name}, description: {task.Description}\n");
		}
		Options();
	}

	static void MarkComplete(){
		Console.WriteLine("Type the name of the task you want to mark as complete: ");
		string name = Console.ReadLine();
		foreach(Task task in tasks){
			if (task.Name == name){
				tasks.Remove(task);
				break;
			}
		}
		Options();
	}

}


class Task{

	public string? Name{get; set;}

	public string? Description{get; set;}

	public Task(string inName, string inDescription){
		Name = inName;
		Description = inDescription;
	}
}
